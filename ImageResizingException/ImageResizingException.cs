using System;

    public class ImageResizingException : Exception
    {
        public ImageResizingException(string message)
           : base(message)
        {
        }
        public ImageResizingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

