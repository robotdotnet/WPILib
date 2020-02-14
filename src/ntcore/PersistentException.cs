using System;
using System.IO;

namespace NetworkTables
{
    public sealed class PersistentException : IOException
    {
        public PersistentException(string message) : base(message)
        {

        }

        public PersistentException()
        {
        }

        public PersistentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
