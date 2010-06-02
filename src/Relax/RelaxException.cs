using System;

namespace Relax
{
    public class RelaxException : Exception
    {
        public RelaxException(string message) : base(message)
        {
        }

        public RelaxException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}