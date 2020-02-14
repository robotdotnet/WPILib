using System;
using System.Collections.Generic;

namespace NetworkTables
{
    public readonly struct ServerPortPair : IEquatable<ServerPortPair>
    {
        public string Server { get; }
        public int Port { get; }
        public ServerPortPair(string server, int port)
        {
            Server = server;
            Port = port;
        }

        public override bool Equals(object? obj)
        {
            return obj is ServerPortPair pair && Equals(pair);
        }

        public bool Equals(ServerPortPair other)
        {
            return Server == other.Server &&
                   Port == other.Port;
        }

        public override int GetHashCode()
        {
            var hashCode = -1903882360;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Server);
            hashCode = hashCode * -1521134295 + Port.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(ServerPortPair left, ServerPortPair right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ServerPortPair left, ServerPortPair right)
        {
            return !(left == right);
        }
    }
}
