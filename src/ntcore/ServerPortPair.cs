using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTables
{
    public readonly struct ServerPortPair
    {
        public readonly string Server;
        public readonly int Port;
        public ServerPortPair(string server, int port)
        {
            Server = server;
            Port = port;
        }
    }
}
