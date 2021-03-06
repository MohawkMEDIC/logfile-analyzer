﻿/*
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
using System.Text;

namespace LogFileAnalyzer
{
	/// <summary>
	/// Represents an event for a log entry.
	/// </summary>
	public class LogEntryEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LogEntryEventArgs" /> class.
		/// </summary>
		public LogEntryEventArgs()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LogEntryEventArgs"/> class.
		/// </summary>
		/// <param name="logFileEntry">The log file entry.</param>
		public LogEntryEventArgs(LogFileEntry logFileEntry)
		{
			this.LogFileEntry = logFileEntry;
		}

		/// <summary>
		/// Gets or sets the log file entry.
		/// </summary>
		/// <value>The log file entry.</value>
		public LogFileEntry LogFileEntry { get; set; }
	}
}
