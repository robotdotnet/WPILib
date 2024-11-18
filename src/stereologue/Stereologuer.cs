using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetworkTables;
using WPIUtil.Logging;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace Stereologue;

public sealed class Stereologuer
{
    private string rootPath = "NOTSET";

    private readonly NetworkTableInstance ntInstance = NetworkTableInstance.Default;

    private Stereologuer() { }

    public static Stereologuer Logger { get; } = new();

#pragma warning disable IDE0060 // Remove unused parameter
    public void Setup(string name, bool fileOnly, bool lazyLogging)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        rootPath = name;
        FileOnly = fileOnly;
    }

    public bool FileOnly { get; set; }

    public static void UpdateAll(ILogged logged)
    {
        var logger = Logger;
        logged.UpdateStereologue(logger.rootPath, Logger);
    }

    private readonly DataLog log = null!;

    private record struct TypedLogs<TNT, TDataLog>(TNT? topic, TDataLog? logEntry) where TNT : class, IPublisher where TDataLog : DataLogEntry;

    private readonly Dictionary<string, TypedLogs<IBooleanPublisher, BooleanLogEntry>> booleanLogs = [];
    private readonly Dictionary<string, TypedLogs<IIntegerPublisher, IntegerLogEntry>> integerLogs = [];
    private readonly Dictionary<string, TypedLogs<IFloatPublisher, FloatLogEntry>> floatLogs = [];
    private readonly Dictionary<string, TypedLogs<IDoublePublisher, DoubleLogEntry>> doubleLogs = [];
    private readonly Dictionary<string, TypedLogs<IStringPublisher, StringLogEntry>> stringLogs = [];
    private readonly Dictionary<string, TypedLogs<IPublisher, DataLogEntry>> structLogs = [];
    private readonly Dictionary<string, TypedLogs<IPublisher, DataLogEntry>> protobufLogs = [];

    private readonly Dictionary<string, TypedLogs<IBooleanArrayPublisher, BooleanArrayLogEntry>> booleanArrayLogs = [];
    private readonly Dictionary<string, TypedLogs<IIntegerArrayPublisher, IntegerArrayLogEntry>> integerArrayLogs = [];
    private readonly Dictionary<string, TypedLogs<IFloatArrayPublisher, FloatArrayLogEntry>> floatArrayLogs = [];
    private readonly Dictionary<string, TypedLogs<IDoubleArrayPublisher, DoubleArrayLogEntry>> doubleArrayLogs = [];
    private readonly Dictionary<string, TypedLogs<IStringArrayPublisher, StringArrayLogEntry>> stringArrayLogs = [];
    private readonly Dictionary<string, TypedLogs<IPublisher, DataLogEntry>> structArrayLogs = [];
    private readonly Dictionary<string, TypedLogs<IRawPublisher, RawLogEntry>> rawLogs = [];

    public void LogBoolean(string path, LogType logType, bool value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, booleanLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetBooleanTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new BooleanLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogInteger(string path, LogType logType, long value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, integerLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetIntegerTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new IntegerLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogStruct<T>(string path, LogType logType, in T value, LogLevel logLevel = LogLevel.Default) where T : IStructSerializable<T>
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, structLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetStructTopic<T>(path).Publish(PubSubOptions.None);
            IStructPublisher<T> tl = (IStructPublisher<T>)logs.topic;
            tl.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new StructLogEntry<T>(log, path);
            StructLogEntry<T> tl = (StructLogEntry<T>)logs.logEntry;
            tl.Append(value);
        }
    }

    public void LogStructArray<T>(string path, LogType logType, T[]? value, LogLevel logLevel = LogLevel.Default) where T : IStructSerializable<T>
    {
        ReadOnlySpan<T> ros = value.AsSpan();
        LogStructArray(path, logType, ros, logLevel);
    }

    public void LogStructArray<T>(string path, LogType logType, Span<T> value, LogLevel logLevel = LogLevel.Default) where T : IStructSerializable<T>
    {
        ReadOnlySpan<T> ros = value;
        LogStructArray(path, logType, ros, logLevel);
    }

    public void LogStructArray<T>(string path, LogType logType, ReadOnlySpan<T> value, LogLevel logLevel = LogLevel.Default) where T : IStructSerializable<T>
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, structArrayLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetStructArrayTopic<T>(path).Publish(PubSubOptions.None);
            IStructArrayPublisher<T> tl = (IStructArrayPublisher<T>)logs.topic;
            tl.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new StructArrayLogEntry<T>(log, path);
            StructArrayLogEntry<T> tl = (StructArrayLogEntry<T>)logs.logEntry;
            tl.Append(value);
        }
    }

    public void LogProto<T>(string path, LogType logType, in T value, LogLevel logLevel = LogLevel.Default) where T : IProtobufSerializable<T>
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, protobufLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetProtobufTopic<T>(path).Publish(PubSubOptions.None);
            IProtobufPublisher<T> tl = (IProtobufPublisher<T>)logs.topic;
            tl.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new ProtobufLogEntry<T>(log, path);
            ProtobufLogEntry<T> tl = (ProtobufLogEntry<T>)logs.logEntry;
            tl.Append(value);
        }
    }

    public void LogChar(string path, LogType logType, char value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, stringLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        var stringValue = $"{value}";
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetStringTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(stringValue);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new StringLogEntry(log, path);
            logs.logEntry.Append(stringValue);
        }
    }

    public void LogFloat(string path, LogType logType, float value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, floatLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetFloatTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new FloatLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogDouble(string path, LogType logType, double value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, doubleLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetDoubleTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new DoubleLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogString(string path, LogType logType, string value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, stringLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetStringTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new StringLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogString(string path, LogType logType, ReadOnlySpan<char> value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, stringLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetStringTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new StringLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogString(string path, LogType logType, ReadOnlySpan<byte> value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, stringLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetStringTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new StringLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogBooleanArray(string path, LogType logType, ReadOnlySpan<bool> value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, booleanArrayLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetBooleanArrayTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new BooleanArrayLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogStringArray(string path, LogType logType, ReadOnlySpan<string> value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, stringArrayLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetStringArrayTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new StringArrayLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogRaw(string path, LogType logType, ReadOnlySpan<byte> value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, rawLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetRawTopic(path).Publish("raw", PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new RawLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogIntegerArray(string path, LogType logType, ReadOnlySpan<long> value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, integerArrayLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetIntegerArrayTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new IntegerArrayLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogFloatArray(string path, LogType logType, ReadOnlySpan<float> value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, floatArrayLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetFloatArrayTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new FloatArrayLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogDoubleArray(string path, LogType logType, ReadOnlySpan<double> value, LogLevel logLevel = LogLevel.Default)
    {
        ref var logs = ref CheckDoLog(path, ref logType, logLevel, doubleArrayLogs);
        if (Unsafe.IsNullRef(ref logs))
        {
            return;
        }
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= ntInstance.GetDoubleArrayTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new DoubleArrayLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    private ref TypedLogs<TNT, TDataLog> CheckDoLog<TNT, TDataLog>(string path,
        ref LogType logType, LogLevel logLevel, Dictionary<string, TypedLogs<TNT, TDataLog>> dict)
            where TNT : class, IPublisher
            where TDataLog : DataLogEntry
    {
        // If we have no log type, do nothing
        if (logType == 0)
        {
            return ref Unsafe.NullRef<TypedLogs<TNT, TDataLog>>();
        }

        // We have the once flag, we need to check if we've already sent the path
        if ((logType & LogType.Once) != 0)
        {
            ref var onceRef = ref CollectionsMarshal.GetValueRefOrAddDefault(dict, path, out bool exists);
            // If we've already done a write, do nothing
            if (exists)
            {
                return ref Unsafe.NullRef<TypedLogs<TNT, TDataLog>>();
            }
            // Otherwise do a single write
            return ref onceRef;
        }

        // If we're override file only, we're unconditionally logging
        if (logLevel == LogLevel.OverrideFileOnly)
        {
            return ref CollectionsMarshal.GetValueRefOrAddDefault(dict, path, out _);
        }

        ref var element = ref CollectionsMarshal.GetValueRefOrAddDefault(dict, path, out _);

        if (FileOnly)
        {
            // We need to prune the NT Entry
            element.topic?.Dispose();
            element.topic = null;

            // If not file only is set, skip
            if (logLevel == LogLevel.NotFileOnly)
            {
                return ref Unsafe.NullRef<TypedLogs<TNT, TDataLog>>();
            }

            // We're default in file only mode, force file only
            logType = LogType.File;
        }

        // We've either forced us into file only, or we just want to handle what we are already doing
        return ref element;
    }

}
