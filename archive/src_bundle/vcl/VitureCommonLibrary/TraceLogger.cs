#define TRACE
using System.Diagnostics;

namespace VitureCommonLibrary;

internal class TraceLogger : ILogger
{
	public void Debug(string message)
	{
		Trace.TraceInformation(message);
	}

	public void Error(string message)
	{
		Trace.TraceError(message);
	}

	public void Fatal(string message)
	{
		Trace.Fail(message);
	}

	public void Info(string message)
	{
		Trace.TraceInformation(message);
	}

	public void Warning(string message)
	{
		Trace.TraceWarning(message);
	}
}
