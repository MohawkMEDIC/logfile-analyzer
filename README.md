# logfile-analyzer
Represents a .NET Standard 2.0 log file analyzer

# Installation
```cmd
Install-Package LogFileAnalyzer -Version 1.0.0
```

# Configuration

## Add this to your Program.cs file
```C#

public static void Main(string[] args)
{
	// setup the log file analyzer
	var analyzer = new LogFileAnalyzer(Path.Combine(AppContext.BaseDirectory, "YourLogDirectory"), new List<string> { ".log" });
	
	// wire up the error event
	this.analyzer.ErrorDetected += (o, e) =>
	{
		Console.WriteLine($"Found error : {e.LogFileEntry}");
	};
	
	// start analyzing files
	this.analyzer.Analyze();
}

```
