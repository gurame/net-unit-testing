namespace BaseApi.Logging;

public interface ILoggerAdapter<TType>
{
	void LogInformation(string? message, params object?[] args);
}
