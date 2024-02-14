using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NetworkTables;
using WPIUtil.Logging;
using static WPIUtil.Sendable.ISendableBuilder;

namespace Monologue;

internal class DataLogSendableBuilder : INtSendableBuilder
{
    private static readonly DataLog log = null!;
    private static NetworkTable rootTable = NetworkTableInstance.Default.GetTable("DataLogSendable");

    private static NetworkTable? networkTable = null;

    private readonly Dictionary<DataLogEntry, Func<object>> dataLogMap = [];
    private readonly List<Action> updateTables = [];
    private readonly List<IDisposable> disposables = [];
    private string prefix;

    /// <summary>
    ///
    /// </summary>
    /// <param name="prefix"></param>
    public DataLogSendableBuilder(string prefix)
    {
        if (!prefix.EndsWith('/'))
        {
            this.prefix = $"{prefix}/";
        }
        else
        {
            this.prefix = prefix;
        }
    }

    public BackingKind BackendKind => BackingKind.Unknown;

    public Action UpdateTable
    {
        set
        {
            updateTables.Add(value);
        }
    }

    public NetworkTable Table
    {
        get
        {
            if (networkTable == null)
            {
                networkTable = rootTable.GetSubTable(prefix);
            }
            return networkTable;
        }
    }

    public bool IsPublished => true;

    public void AddBooleanArrayProperty(string key, Func<bool[]> getter, Action<bool[]> setter)
    {
        if (getter != null)
        {
            CollectionsMarshal.GetValueRefOrAddDefault(dataLogMap, new BooleanArrayLogEntry(log, $"{prefix}{key}"), out _) = () => getter();
        }
    }

    public void AddBooleanProperty(string key, Func<bool> getter, Action<bool> setter)
    {
        if (getter != null)
        {
            CollectionsMarshal.GetValueRefOrAddDefault(dataLogMap, new BooleanLogEntry(log, $"{prefix}{key}"), out _) = () => getter();
        }
    }

    public void AddDisposable(IDisposable disposable)
    {
        disposables.Add(disposable);
    }

    public void AddDoubleArrayProperty(string key, Func<double[]> getter, Action<double[]> setter)
    {
        if (getter != null)
        {
            CollectionsMarshal.GetValueRefOrAddDefault(dataLogMap, new DoubleArrayLogEntry(log, $"{prefix}{key}"), out _) = () => getter();
        }
    }

    public void AddDoubleProperty(string key, Func<double> getter, Action<double> setter)
    {
        if (getter != null)
        {
            CollectionsMarshal.GetValueRefOrAddDefault(dataLogMap, new DoubleLogEntry(log, $"{prefix}{key}"), out _) = () => getter();
        }
    }

    public void AddFloatArrayProperty(string key, Func<float[]> getter, Action<float[]> setter)
    {
        if (getter != null)
        {
            CollectionsMarshal.GetValueRefOrAddDefault(dataLogMap, new FloatArrayLogEntry(log, $"{prefix}{key}"), out _) = () => getter();
        }
    }

    public void AddFloatProperty(string key, Func<float> getter, Action<float> setter)
    {
        if (getter != null)
        {
            CollectionsMarshal.GetValueRefOrAddDefault(dataLogMap, new FloatLogEntry(log, $"{prefix}{key}"), out _) = () => getter();
        }
    }

    public void AddIntegerArrayProperty(string key, Func<long[]> getter, Action<long[]> setter)
    {
        if (getter != null)
        {
            CollectionsMarshal.GetValueRefOrAddDefault(dataLogMap, new IntegerArrayLogEntry(log, $"{prefix}{key}"), out _) = () => getter();
        }
    }

    public void AddIntegerProperty(string key, Func<long> getter, Action<long> setter)
    {
        if (getter != null)
        {
            CollectionsMarshal.GetValueRefOrAddDefault(dataLogMap, new IntegerLogEntry(log, $"{prefix}{key}"), out _) = () => getter();
        }
    }

    public void AddRawProperty(string key, string typeString, Func<byte[]> getter, Action<byte[]> setter)
    {
        if (getter != null)
        {
            CollectionsMarshal.GetValueRefOrAddDefault(dataLogMap, new RawLogEntry(log, $"{prefix}{key}({typeString})"), out _) = () => getter();
        }
    }

    public void AddStringArrayProperty(string key, Func<string[]> getter, Action<string[]> setter)
    {
        if (getter != null)
        {
            CollectionsMarshal.GetValueRefOrAddDefault(dataLogMap, new StringArrayLogEntry(log, $"{prefix}{key}"), out _) = () => getter();
        }
    }

    public void AddStringProperty(string key, Func<string> getter, Action<string> setter)
    {
        if (getter != null)
        {
            CollectionsMarshal.GetValueRefOrAddDefault(dataLogMap, new StringLogEntry(log, $"{prefix}{key}"), out _) = () => getter();
        }
    }

    public void ClearProperties()
    {
        dataLogMap.Clear();
    }

    public void Dispose()
    {
        ClearProperties();

        foreach (var d in disposables)
        {
            try
            {
                d?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ignoring Dispose Exception {ex}");
                // Ignore
            }
        }
    }

    public Topic GetTopic(string key)
    {
        return Table.GetTopic(key);
    }

    public void PublishConstBoolean(string key, bool value)
    {
        var handle = log.Start($"{prefix}{key}", BooleanLogEntry.DataTypeUft8);
        log.AppendBoolean(handle, value);
    }

    public void PublishConstBooleanArray(string key, ReadOnlySpan<bool> value)
    {
        var handle = log.Start($"{prefix}{key}", BooleanArrayLogEntry.DataTypeUft8);
        log.AppendBooleanArray(handle, value);
    }

    public void PublishConstDouble(string key, double value)
    {
        var handle = log.Start($"{prefix}{key}", DoubleLogEntry.DataTypeUft8);
        log.AppendDouble(handle, value);
    }

    public void PublishConstDoubleArray(string key, ReadOnlySpan<double> value)
    {
        var handle = log.Start($"{prefix}{key}", DoubleArrayLogEntry.DataTypeUft8);
        log.AppendDoubleArray(handle, value);
    }

    public void PublishConstFloat(string key, float value)
    {
        var handle = log.Start($"{prefix}{key}", FloatLogEntry.DataTypeUft8);
        log.AppendFloat(handle, value);
    }

    public void PublishConstFloatArray(string key, ReadOnlySpan<float> value)
    {
        var handle = log.Start($"{prefix}{key}", FloatArrayLogEntry.DataTypeUft8);
        log.AppendFloatArray(handle, value);
    }

    public void PublishConstInteger(string key, long value)
    {
        var handle = log.Start($"{prefix}{key}", IntegerLogEntry.DataTypeUft8);
        log.AppendInteger(handle, value);
    }

    public void PublishConstIntegerArray(string key, ReadOnlySpan<long> value)
    {
        var handle = log.Start($"{prefix}{key}", IntegerArrayLogEntry.DataTypeUft8);
        log.AppendIntegerArray(handle, value);
    }

    public void PublishConstRaw(string key, string typeString, ReadOnlySpan<byte> value)
    {
        var handle = log.Start($"{prefix}{key}({typeString})", RawLogEntry.DataTypeUft8);
        log.AppendRaw(handle, value);
    }

    public void PublishConstString(string key, string value)
    {
        var handle = log.Start($"{prefix}{key}", StringLogEntry.DataTypeUft8);
        log.AppendString(handle, value);
    }

    public void PublishConstString(string key, ReadOnlySpan<byte> value)
    {
        var handle = log.Start($"{prefix}{key}", StringLogEntry.DataTypeUft8);
        log.AppendString(handle, value);
    }

    public void PublishConstStringArray(string key, ReadOnlySpan<string> value)
    {
        var handle = log.Start($"{prefix}{key}", StringArrayLogEntry.DataTypeUft8);
        log.AppendStringArray(handle, value);
    }

    public void SetActuator(bool value)
    {
    }

    public void SetSafeState(Action func)
    {
    }

    public void SetSmartDashboardType(string type)
    {
    }

    public void Update()
    {
        long timestamp = 0; // Get FPGA timestamp
        foreach (var entry in dataLogMap)
        {
            var key = entry.Key;
            var val = entry.Value();
            switch (key)
            {
                case BooleanArrayLogEntry logEntry:
                    logEntry.Append((bool[])val, timestamp);
                    break;
                case BooleanLogEntry logEntry:
                    logEntry.Append((bool)val, timestamp);
                    break;
                case DoubleArrayLogEntry logEntry:
                    logEntry.Append((double[])val, timestamp);
                    break;
                case DoubleLogEntry logEntry:
                    logEntry.Append((double)val, timestamp);
                    break;
                case FloatArrayLogEntry logEntry:
                    logEntry.Append((float[])val, timestamp);
                    break;
                case FloatLogEntry logEntry:
                    logEntry.Append((float)val, timestamp);
                    break;
                case IntegerArrayLogEntry logEntry:
                    logEntry.Append((long[])val, timestamp);
                    break;
                case IntegerLogEntry logEntry:
                    logEntry.Append((long)val, timestamp);
                    break;
                case RawLogEntry logEntry:
                    logEntry.Append((byte[])val, timestamp);
                    break;
                case StringArrayLogEntry logEntry:
                    logEntry.Append((string[])val, timestamp);
                    break;
                case StringLogEntry logEntry:
                    logEntry.Append((string)val, timestamp);
                    break;
            }
        }
        foreach(var updateTable in updateTables) {
            updateTable();
        }
    }
}
