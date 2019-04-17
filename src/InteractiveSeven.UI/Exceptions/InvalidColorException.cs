using System;

namespace InteractiveSeven.UI.Exceptions
{
    public class InvalidColorException : ApplicationException
    {
        public InvalidColorException(string color) : base($"Invalid color \"{color}\" requested.")
        {
        }
    }
}