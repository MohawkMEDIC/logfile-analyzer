/*
 * Copyright 2015-2019 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: khannan
 * Date: 2019-3-10
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace LogFileAnalyzer
{
	/// <summary>
	/// Represents a log file analyzer.
	/// </summary>
	public class LogFileAnalyzer : ILogFileAnalyzer
	{
		private readonly IEnumerable<string> extensions;

		/// <summary>
		/// Initializes a new instance of the <see cref="LogFileAnalyzer" /> class.
		/// </summary>
		/// <param name="baseDirectory">The base directory.</param>
		/// <exception cref="ArgumentNullException">baseDirectory - Value cannot be null</exception>
		/// <exception cref="ArgumentException">baseDirectory</exception>
		public LogFileAnalyzer(string baseDirectory) : this(baseDirectory, new List<string> { ".log" })
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LogFileAnalyzer" /> class.
		/// </summary>
		/// <param name="baseDirectory">The base directory.</param>
		/// <param name="extensions">The extensions.</param>
		/// <exception cref="System.ArgumentNullException">baseDirectory - Value cannot be null</exception>
		/// <exception cref="System.ArgumentException">baseDirectory</exception>
		public LogFileAnalyzer(string baseDirectory, IEnumerable<string> extensions)
		{
			if (string.IsNullOrEmpty(baseDirectory))
			{
				throw new ArgumentNullException(nameof(baseDirectory), "Value cannot be null");
			}

			if (!Directory.Exists(baseDirectory))
			{
				throw new ArgumentException($"The base directory {baseDirectory} does not exist", nameof(baseDirectory));
			}

			if (extensions == null || !extensions?.Any() == true)
			{
				throw new ArgumentException("At least one extension must be provided", nameof(extensions));
			}

			this.BaseDirectory = baseDirectory;
			this.extensions = extensions;

			this.RawFiles = this.GetFiles().ToList();
		}

		/// <summary>
		/// Occurs when an error is detected.
		/// </summary>
		public event EventHandler<LogEntryEventArgs> ErrorDetected;

		/// <summary>
		/// Gets the base directory.
		/// </summary>
		/// <value>The base directory.</value>
		public string BaseDirectory { get; }

		/// <summary>
		/// Gets or sets the log file entries.
		/// </summary>
		/// <value>The log file entries.</value>
		public IEnumerable<LogFileEntry> LogFileEntries { get; private set; }

		/// <summary>
		/// Gets the files.
		/// </summary>
		/// <value>The files.</value>
		public IEnumerable<string> RawFiles { get; }

		/// <summary>
		/// Analyzes the files with the specified extensions.
		/// </summary>
		public void Analyze()
		{
			var entries = new List<LogFileEntry>();

			foreach (var line in this.RawFiles.Where(c => DateTime.Now.AddDays(-7) < new FileInfo(c).CreationTime).SelectMany(c => File.ReadAllLines(c).Where(f => f.Split(' ')?[5].ToLowerInvariant() == "error")))
			{
				var squareBracketIndex = line.IndexOf("[");

				var entry = new LogFileEntry(DateTime.Parse(line.Substring(0, squareBracketIndex)));

				if (int.TryParse(line.Substring(squareBracketIndex + 1, line.LastIndexOf("]") - squareBracketIndex - 1).Replace("@", ""), out var threadId))
				{
					entry.ThreadId = threadId;
				}

				entry.Category = line.Split(' ')[6];
				entry.Content = string.Join(" ", line.Split(' ').Skip(7));

				entries.Add(entry);

				this.ErrorDetected?.Invoke(this, new LogEntryEventArgs(entry));
			}

			this.LogFileEntries = entries.AsEnumerable();
		}

		/// <summary>
		/// Gets the files based on the extensions provided.
		/// </summary>
		/// <returns>Returns a list of files.</returns>
		private IEnumerable<string> GetFiles()
		{
			var parameterExpression = Expression.Parameter(typeof(string), "f");

			var expressionStack = new Stack<Expression>();

			foreach (var extension in this.extensions)
			{
				var left = Expression.Call(parameterExpression, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), Expression.Constant(extension.ToLowerInvariant()));
				var right = Expression.Constant(true);

				var equalExpression = Expression.Equal(left, right);

				expressionStack.Push(equalExpression);
			}

			var overallExpression = expressionStack.Pop();

			for (var i = 0; i < expressionStack.Count; i++)
			{
				var right = expressionStack.Pop();

				if (expressionStack.Count == 0)
				{
					overallExpression = Expression.OrElse(overallExpression, right);
					break;
				}

				var left = expressionStack.Pop();

				overallExpression = Expression.OrElse(Expression.OrElse(overallExpression, left), right);
			}

			var expression = Expression.Lambda<Func<string, bool>>(overallExpression, parameterExpression).Compile();

			return Directory.GetFiles(this.BaseDirectory, "*.*", SearchOption.AllDirectories).Where(expression).ToList();
		}
	}
}
