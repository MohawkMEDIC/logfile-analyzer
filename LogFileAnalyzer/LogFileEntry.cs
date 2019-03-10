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

namespace LogFileAnalyzer
{
	/// <summary>
	/// Represents a log file entry.
	/// </summary>
	public class LogFileEntry
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LogFileEntry"/> class.
		/// </summary>
		public LogFileEntry()
		{
			this.LogLevel = "Error";
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LogFileEntry" /> class.
		/// </summary>
		/// <param name="logEntryDateTime">The log entry date time.</param>
		public LogFileEntry(DateTime logEntryDateTime) : this()
		{
			this.LogEntryDateTime = logEntryDateTime;
		}

		/// <summary>
		/// Gets or sets the category.
		/// </summary>
		/// <value>The category.</value>
		public string Category { get; set; }
		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>The content.</value>
		public string Content { get; set; }

		/// <summary>
		/// Gets or sets the log entry date time.
		/// </summary>
		/// <value>The log entry date time.</value>
		public DateTime LogEntryDateTime { get; set; }

		/// <summary>
		/// Gets or sets the log level.
		/// </summary>
		/// <value>The log level.</value>
		public string LogLevel { get; set; }

		/// <summary>
		/// Gets or sets the thread identifier.
		/// </summary>
		/// <value>The thread identifier.</value>
		public int? ThreadId { get; set; }

		/// <summary>
		/// Returns a string that represents the log file entry.
		/// </summary>
		/// <returns>A string that represents the log file entry.</returns>
		public override string ToString()
		{
			return $"{this.LogEntryDateTime:yyyy-MM-dd hh:mm:ss aa} [@{this.ThreadId}] : {this.LogLevel} {this.Category} {this.Content}";
		}
	}
}