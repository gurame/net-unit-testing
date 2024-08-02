namespace BaseApi.Logging;

public class LoggerAdapter<TType> : ILoggerAdapter<TType>
{
	private ILogger<TType> _logger;
	public LoggerAdapter(ILogger<TType> logger)=> _logger = logger;
    public void LogInformation(string? message, params object?[] args)
    {
       _logger.LogInformation(message, args);
    }
}
