using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsoleLogger : ILogger
{
    private readonly string _dateFormat = "dd/MM/yyyy HH:mm:ss";
    public void LogInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{message} - {DateTime.Now.ToString(_dateFormat)}");
    }


    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{message} - {DateTime.Now.ToString(_dateFormat)}");
    }

    public void LogWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{message} - {DateTime.Now.ToString(_dateFormat)}");
    }

    public void LogFatal(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"{message} - {DateTime.Now.ToString(_dateFormat)}");
    }

}
