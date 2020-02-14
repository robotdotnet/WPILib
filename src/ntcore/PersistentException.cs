using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
