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

namespace LogFileAnalyzer
{
	/// <summary>
	/// Represents a log file analyzer.
	/// </summary>
	public interface ILogFileAnalyzer
	{
		/// <summary>
		/// Occurs when an error is detected.
		/// </summary>
		event EventHandler<LogEntryEventArgs> ErrorDetected;

		/// <summary>
		/// Gets the base directory.
		/// </summary>
		/// <value>The base directory.</value>
		string BaseDirectory { get; }

		/// <summary>
		/// Gets or sets the log file entries.
		/// </summary>
		/// <value>The log file entries.</value>
		IEnumerable<LogFileEntry> LogFileEntries { get; }

		/// <summary>
		/// Gets the raw files.
		/// </summary>
		/// <value>The raw files.</value>
		IEnumerable<string> RawFiles { get; }

		/// <summary>
		/// Analyzes the files.
		/// </summary>
		void Analyze();
	}
}
