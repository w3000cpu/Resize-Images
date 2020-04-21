using System.Diagnostics;

public class EventLogger : ILogger
{
    private string Source { get; }

    public EventLogger(string source)
    {
        Source = source;
    }

    public void LogError(string message) => Logg<string>(message, EventLogEntryType.Error);

    public void LogFatal(string message) => Logg<string>(message, EventLogEntryType.Error);

    public void LogInfo(string message) => Logg<string>(message, EventLogEntryType.Information);

    public void LogWarning(string message) => Logg<string>(message, EventLogEntryType.Warning);


    public void Logg<T>(string message, EventLogEntryType type)
    {
        using (var eventLog = new EventLog("Application"))
        {
            eventLog.Source = Source;
            eventLog.WriteEntry(message, type);
        }

    }


}
