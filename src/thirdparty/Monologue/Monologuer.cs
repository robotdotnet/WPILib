using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NetworkTables;
using WPIUtil.Logging;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace Monologue;

public sealed class Monologuer
{
    private string rootPath = "NOTSET";

    public static Monologuer Logger { get; } = new();

    public void Setup(string name, bool fileOnly, bool lazyLogging)
    {
        rootPath = name;
        FileOnly = fileOnly;
    }

    public bool FileOnly { get; set; }

    public static void UpdateAll(ILogged logged)
    {
        var logger = Logger;
        logged.UpdateMonologue(logger.rootPath, Logger);
    }

    private readonly DataLog log = null!;

    private readonly Dictionary<string, (IIntegerPublisher? topic, IntegerLogEntry? logEntry)> integerLogs = [];
    private readonly Dictionary<string, (IBooleanPublisher? topic, BooleanLogEntry? logEntry)> booleanLogs = [];
    private readonly Dictionary<string, (object? topic, object? logEntry)> structLogs = [];

    public void LogBoolean(string path, LogType logType, bool value)
    {
        if (logType == LogType.None)
        {
            return;
        }
        ref var logs = ref CollectionsMarshal.GetValueRefOrAddDefault(booleanLogs, path, out _);
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= NetworkTableInstance.Default.GetBooleanTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new BooleanLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogInteger(string path, LogType logType, long value)
    {
        if (logType == LogType.None)
        {
            return;
        }
        ref var logs = ref CollectionsMarshal.GetValueRefOrAddDefault(integerLogs, path, out _);
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= NetworkTableInstance.Default.GetIntegerTopic(path).Publish(PubSubOptions.None);
            logs.topic.Set(value);
        }
        if (logType.HasFlag(LogType.File))
        {
            logs.logEntry ??= new IntegerLogEntry(log, path);
            logs.logEntry.Append(value);
        }
    }

    public void LogStruct<T>(string path, LogType logType, in T value) where T : IStructSerializable<T>
    {
        if (logType == LogType.None)
        {
            return;
        }
        ref var logs = ref CollectionsMarshal.GetValueRefOrAddDefault(structLogs, path, out _);
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= NetworkTableInstance.Default.GetStructTopic<T>(path).Publish(PubSubOptions.None);
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

    public void LogProto<T>(string path, LogType logType, in T value) where T : IProtobufSerializable<T>
    {
        if (logType == LogType.None)
        {
            return;
        }
        ref var logs = ref CollectionsMarshal.GetValueRefOrAddDefault(structLogs, path, out _);
        if (logType.HasFlag(LogType.Nt))
        {
            logs.topic ??= NetworkTableInstance.Default.GetProtobufTopic<T>(path).Publish(PubSubOptions.None);
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

    public void LogChar(string path, LogType logType, char value)
    {

    }

    public void LogFloat(string path, LogType logType, float value)
    {

    }

    public void LogDouble(string path, LogType logType, double value)
    {

    }

    public void LogString(string path, LogType logType, string? value)
    {
        LogString(path, logType, value.AsSpan());
    }

    public void LogString(string path, LogType logType, ReadOnlySpan<char> value)
    {

    }

    public void LogBooleanArray(string path, LogType logType, ReadOnlySpan<bool> value)
    {

    }

    public void LogStringArray(string path, LogType logType, ReadOnlySpan<string?> value)
    {

    }

    public void LogRaw(string path, LogType logType, ReadOnlySpan<byte> value)
    {

    }

    public void LogIntegerArray(string path, LogType logType, ReadOnlySpan<long> value)
    {

    }

    public void LogFloatArray(string path, LogType logType, ReadOnlySpan<float> value)
    {

    }

    public void LogDoubleArray(string path, LogType logType, ReadOnlySpan<double> value)
    {

    }

}
