using System;

namespace InteractiveSeven.Core.Exceptions
{
    public class InvalidColorException : ApplicationException
    {
        public InvalidColorException(string color) : base($"Invalid color \"{color}\" requested.")
        {
        }
    }
}