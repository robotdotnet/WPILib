using System.Runtime.InteropServices;
using WPIUtil.Logging;
using WPIUtil.Serialization.Struct;
using static WPIUtil.WpiGuard;

namespace Epilogue.Logging;

public class FileLogger : IDataLogger
{
    private readonly DataLog m_dataLog;
    private readonly Dictionary<string, DataLogEntry> m_entries = [];
    private readonly Dictionary<string, SubLogger> m_subLoggers = [];

    public FileLogger(DataLog dataLog)
    {
        m_dataLog = RequireNotNull(dataLog);
    }

    public IDataLogger Lazy => new LazyLogger(this);
    public IDataLogger GetSubLogger(string path) => IDataLogger.GetDefaultSubLogger(this, path, m_subLoggers);

    private E GetEntry<E>(string identifier, Func<DataLog, string, E> ctor) where E : DataLogEntry
    {
        ref var entry = ref CollectionsMarshal.GetValueRefOrAddDefault(m_entries, identifier, out var _);
        entry ??= ctor(m_dataLog, identifier);
        return (E)entry;
    }

    public void Log(string identifier, bool value)
    {
        GetEntry(identifier, static (a, b) => new BooleanLogEntry(a, b)).Append(value);
    }

    public void Log(string identifier, int value)
    {
        GetEntry(identifier, static (a, b) => new IntegerLogEntry(a, b)).Append(value);
    }

    public void Log(string identifier, long value)
    {
        GetEntry(identifier, static (a, b) => new IntegerLogEntry(a, b)).Append(value);
    }

    public void Log(string identifier, float value)
    {
        GetEntry(identifier, static (a, b) => new FloatLogEntry(a, b)).Append(value);
    }

    public void Log(string identifier, double value)
    {
        GetEntry(identifier, static (a, b) => new DoubleLogEntry(a, b)).Append(value);
    }

    public void Log(string identifier, ReadOnlySpan<byte> value)
    {
        GetEntry(identifier, static (a, b) => new RawLogEntry(a, b)).Append(value);
    }

    public void Log(string identifier, ReadOnlySpan<bool> value)
    {
        GetEntry(identifier, static (a, b) => new BooleanArrayLogEntry(a, b)).Append(value);
    }

    public void Log(string identifier, ReadOnlySpan<int> value)
    {
        long[] widended = new long[value.Length];
        for (int i = 0; i < value.Length; i++)
        {
            widended[i] = value[i];
        }
        GetEntry(identifier, static (a, b) => new IntegerArrayLogEntry(a, b)).Append(widended);
    }

    public void Log(string identifier, ReadOnlySpan<long> value)
    {
        GetEntry(identifier, static (a, b) => new IntegerArrayLogEntry(a, b)).Append(value);
    }

    public void Log(string identifier, ReadOnlySpan<float> value)
    {
        GetEntry(identifier, static (a, b) => new FloatArrayLogEntry(a, b)).Append(value);
    }

    public void Log(string identifier, ReadOnlySpan<double> value)
    {
        GetEntry(identifier, static (a, b) => new DoubleArrayLogEntry(a, b)).Append(value);
    }

    public void Log(string identifier, string value)
    {
        GetEntry(identifier, static (a, b) => new StringLogEntry(a, b)).Append(value);
    }

    public void Log(string identifier, ReadOnlySpan<char> value)
    {
        GetEntry(identifier, static (a, b) => new StringLogEntry(a, b)).Append(value);
    }

    public void Log(string identifier, ReadOnlySpan<string> value)
    {
        GetEntry(identifier, static (a, b) => new StringArrayLogEntry(a, b)).Append(value);
    }

    public void Log<T>(string identifier, in T value) where T : IStructSerializable<T>
    {
        GetEntry(identifier, static (a, b) => new StructLogEntry<T>(a, b)).Append(value);
    }

    public void Log<T>(string identifier, T[] value) where T : IStructSerializable<T>
    {
        GetEntry(identifier, static (a, b) => new StructArrayLogEntry<T>(a, b)).Append(value);
    }

    public void Log<T>(string identifier, Span<T> value) where T : IStructSerializable<T>
    {
        GetEntry(identifier, static (a, b) => new StructArrayLogEntry<T>(a, b)).Append(value);
    }

    public void Log<T>(string identifier, ReadOnlySpan<T> value) where T : IStructSerializable<T>
    {
        GetEntry(identifier, static (a, b) => new StructArrayLogEntry<T>(a, b)).Append(value);
    }

    public void Log<T>(string identifier, T value) where T : Enum
    {
        GetEntry(identifier, static (a, b) => new StringLogEntry(a, b)).Append(value.ToString());
    }
}
