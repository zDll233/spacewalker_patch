using NLog;

namespace VitureCommonLibrary;

internal class NLogger : ILogger
{
	private ILogger logger;

	public NLogger(ILogger? logger = null)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)LogManager.GetCurrentClassLogger()));
	}

	public void Debug(string message)
	{
		logger.Debug(message);
	}

	public void Error(string message)
	{
		logger.Error(message);
	}

	public void Fatal(string message)
	{
		logger.Fatal(message);
	}

	public void Info(string message)
	{
		logger.Info(message);
	}

	public void Warning(string message)
	{
		logger.Warn(message);
	}
}
