using System.Runtime.InteropServices;
using WPILib.Logging.Loggers;
using WPIUtil.Serialization.Struct;

namespace WPILib.Logging;

public class TestLogger : IDataLogger
{
    public record LogEntry(string identifier, object value);

    private readonly Dictionary<string, SubLogger> m_subLoggers = [];

    private readonly List<LogEntry> m_entries = [];

    public List<LogEntry> Entries => m_entries;

    public IDataLogger Lazy => new LazyLogger(this);

    public IDataLogger GetSubLogger(string path)
    {
        ref var subLogger = ref CollectionsMarshal.GetValueRefOrAddDefault(m_subLoggers, path, out var _);
        subLogger ??= new(path, this);
        return subLogger;
    }

    public void Log(string identifier, bool value)
    {
        m_entries.Add(new LogEntry(identifier, value));
    }

    public void Log(string identifier, int value)
    {
        m_entries.Add(new LogEntry(identifier, value));
    }

    public void Log(string identifier, long value)
    {
        m_entries.Add(new LogEntry(identifier, value));
    }

    public void Log(string identifier, float value)
    {
        m_entries.Add(new LogEntry(identifier, value));
    }

    public void Log(string identifier, double value)
    {
        m_entries.Add(new LogEntry(identifier, value));
    }

    public void Log(string identifier, ReadOnlySpan<byte> value)
    {
        m_entries.Add(new LogEntry(identifier, value.ToArray()));
    }

    public void Log(string identifier, ReadOnlySpan<bool> value)
    {
        m_entries.Add(new LogEntry(identifier, value.ToArray()));
    }

    public void Log(string identifier, ReadOnlySpan<int> value)
    {
        m_entries.Add(new LogEntry(identifier, value.ToArray()));
    }

    public void Log(string identifier, ReadOnlySpan<long> value)
    {
        m_entries.Add(new LogEntry(identifier, value.ToArray()));
    }

    public void Log(string identifier, ReadOnlySpan<float> value)
    {
        m_entries.Add(new LogEntry(identifier, value.ToArray()));
    }

    public void Log(string identifier, ReadOnlySpan<double> value)
    {
        m_entries.Add(new LogEntry(identifier, value.ToArray()));
    }

    public void Log(string identifier, string value)
    {
        m_entries.Add(new LogEntry(identifier, value));
    }

    public void Log(string identifier, ReadOnlySpan<char> value)
    {
        m_entries.Add(new LogEntry(identifier, value.ToArray()));
    }

    public void Log(string identifier, ReadOnlySpan<string> value)
    {
        m_entries.Add(new LogEntry(identifier, value.ToArray()));
    }

    public void Log<T>(string identifier, in T value) where T : IStructSerializable<T>
    {
        m_entries.Add(new LogEntry(identifier, value));
    }

    public void Log<T>(string identifier, T[] value) where T : IStructSerializable<T>
    {
        m_entries.Add(new LogEntry(identifier, value));
    }

    public void Log<T>(string identifier, Span<T> value) where T : IStructSerializable<T>
    {
        m_entries.Add(new LogEntry(identifier, value.ToArray()));
    }

    public void Log<T>(string identifier, ReadOnlySpan<T> value) where T : IStructSerializable<T>
    {
        m_entries.Add(new LogEntry(identifier, value.ToArray()));
    }

    public void Log<T>(string identifier, T value) where T : Enum
    {
        m_entries.Add(new LogEntry(identifier, value));
    }
}
