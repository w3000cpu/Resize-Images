using System;


public class ExtensionsException : Exception
{
    public ExtensionsException(string message)
       : base(message)
    {
    }
    public ExtensionsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
