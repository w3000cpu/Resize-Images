using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FileLogger : ILogger
{

    private readonly string _dateFormat = "dd/MM/yyyy HH:mm:ss";

    public FileLogger(string path) => Path = path;

    private string Path { get; }

    public void LogError(string message) => Log(message, "ERROR");

    public void LogFatal(string message) => Log(message, "FATAL");

    public void LogInfo(string message) => Log(message, "INFO");

    public void LogWarning(string message) => Log(message, "WARNING");

    private void Log(string message, string messageType)
    {
        using (var streamWriter = new StreamWriter(Path, true))
        {
            streamWriter.WriteLine($"{ DateTime.Now.ToString(_dateFormat)} - {messageType} : {message}");
        }
    }
}