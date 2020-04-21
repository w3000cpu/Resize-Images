
public interface ILogger
{
    void LogInfo(string message);
    void LogError(string message);
    void LogWarning(string message);
    void LogFatal(string message);
}
