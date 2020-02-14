using NetworkTables.Natives;
using System;
using System.Collections.Generic;
using WPIUtil;

namespace NetworkTables
{
    public readonly struct LogMessage : IEquatable<LogMessage>
    {
        public NtLogger Logger { get; }
        public LogLevel Level { get; }
        public string Filename { get; }
        public int Line { get; }
        public string Message { get; }
        internal NetworkTableInstance Instance { get; }

        internal unsafe LogMessage(NetworkTableInstance inst, in NtLogMessage log)
        {
            Instance = inst;
            Logger = log.logger;
            Level = (LogLevel)log.level;
            Filename = UTF8String.ReadUTF8String(log.filename);
            Line = (int)log.line;
            Message = UTF8String.ReadUTF8String(log.message);
        }

        public override bool Equals(object? obj)
        {
            return obj is LogMessage message && Equals(message);
        }

        public bool Equals(LogMessage other)
        {
            return Logger.Equals(other.Logger) &&
                   Level == other.Level &&
                   Filename == other.Filename &&
                   Line == other.Line &&
                   Message == other.Message;
        }

        public override int GetHashCode()
        {
            var hashCode = -829874283;
            hashCode = hashCode * -1521134295 + Logger.GetHashCode();
            hashCode = hashCode * -1521134295 + Level.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Filename);
            hashCode = hashCode * -1521134295 + Line.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Message);
            return hashCode;
        }

        public static bool operator ==(LogMessage left, LogMessage right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LogMessage left, LogMessage right)
        {
            return !(left == right);
        }
    }
}
