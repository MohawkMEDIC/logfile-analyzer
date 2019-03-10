using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogFileAnalyzer.Tests
{
	/// <summary>
	/// Represents tests for the <see cref="LogFileAnalyzer"/> class.
	/// </summary>
	[TestClass]
	public class AnalyzerTests
	{
		/// <summary>
		/// The analyzer.
		/// </summary>
		private LogFileAnalyzer analyzer;

		/// <summary>
		/// Performs cleanup for the tests.
		/// </summary>
		[TestCleanup]
		public void Cleanup()
		{
			this.analyzer = null;
		}

		/// <summary>
		/// Performs initialization for the tests.
		/// </summary>
		[TestInitialize]
		public void Initialize()
		{
			this.analyzer = new LogFileAnalyzer(Path.Combine(AppContext.BaseDirectory, "SampleLogs"));
		}

		/// <summary>
		/// Tests the analyze process.
		/// </summary>
		[TestMethod]
		public void TestAnalyze()
		{
			this.analyzer = new LogFileAnalyzer(Path.Combine(AppContext.BaseDirectory, "SampleLogs"), new List<string> { ".txt" });

			this.analyzer.ErrorDetected += (o, e) =>
			{
				Console.WriteLine($"Found error : {e.LogFileEntry}");
			};

			this.analyzer.Analyze();

			Assert.IsTrue(this.analyzer.LogFileEntries.Count() == 1);
		}

		/// <summary>
		/// Tests the analyzer with multiple file extensions.
		/// </summary>
		[TestMethod]
		public void TestAnalyzerMultipleFileExtensions()
		{
			this.analyzer = new LogFileAnalyzer(Path.Combine(AppContext.BaseDirectory, "SampleLogs"), new List<string>{ ".txt", ".log", ".dll" });
			Assert.IsTrue(this.analyzer.RawFiles.Any());
		}

		/// <summary>
		/// Tests the analyzer with a single file extension.
		/// </summary>
		[TestMethod]
		public void TestAnalyzerSingleFileExtension()
		{
			this.analyzer = new LogFileAnalyzer(Path.Combine(AppContext.BaseDirectory, "SampleLogs"), new List<string> { ".txt" });

			Assert.IsTrue(this.analyzer.RawFiles.Any());
		}

		/// <summary>
		/// Tests the program throws an <see cref="ArgumentException"/> if the base directory doesn't exist.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestBaseDirectoryNotExists()
		{
			this.analyzer = new LogFileAnalyzer(@"C:\doesntexist");
		}

		/// <summary>
		/// Tests the program throws an <see cref="ArgumentNullException"/> if the base directory is null.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestNullBaseDirectory()
		{
			this.analyzer = new LogFileAnalyzer(null);
		}

		/// <summary>
		/// Tests the program throws an <see cref="ArgumentException"/> if the extensions value is null.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestNullExtensions()
		{
			this.analyzer = new LogFileAnalyzer(Path.Combine(AppContext.BaseDirectory, "SampleLogs"), extensions: null);
		}
	}
}