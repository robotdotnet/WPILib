using System.Runtime.InteropServices;
using NetworkTables;
using WPIUtil.Serialization.Struct;
using static WPIUtil.WpiGuard;

namespace Epilogue.Logging;

public class NTDataLogger : IDataLogger
{
    private readonly NetworkTableInstance m_nt;

    private readonly Dictionary<string, IPublisher> m_publishers = [];
    private readonly Dictionary<string, SubLogger> m_subLoggers = [];

    public NTDataLogger(NetworkTableInstance nt)
    {
        m_nt = RequireNotNull(nt);
    }

    public IDataLogger Lazy => new LazyLogger(this);
    public IDataLogger GetSubLogger(string path) => IDataLogger.GetDefaultSubLogger(this, path, m_subLoggers);

    private E GetEntry<E>(string identifier, Func<NetworkTableInstance, string, E> ctor) where E : IPublisher
    {
        ref var entry = ref CollectionsMarshal.GetValueRefOrAddDefault(m_publishers, identifier, out var _);
        entry ??= ctor(m_nt, identifier);
        return (E)entry;
    }

    public void Log(string identifier, bool value)
    {
        GetEntry(identifier, (a, b) => a.GetBooleanTopic(b).Publish()).Set(value);
    }

    public void Log(string identifier, int value)
    {
        GetEntry(identifier, (a, b) => a.GetIntegerTopic(b).Publish()).Set(value);
    }

    public void Log(string identifier, long value)
    {
        GetEntry(identifier, (a, b) => a.GetIntegerTopic(b).Publish()).Set(value);
    }

    public void Log(string identifier, float value)
    {
        GetEntry(identifier, (a, b) => a.GetFloatTopic(b).Publish()).Set(value);
    }

    public void Log(string identifier, double value)
    {
        GetEntry(identifier, (a, b) => a.GetDoubleTopic(b).Publish()).Set(value);
    }

    public void Log(string identifier, ReadOnlySpan<byte> value)
    {
        GetEntry(identifier, (a, b) => a.GetRawTopic(b).Publish("raw")).Set(value);
    }

    public void Log(string identifier, ReadOnlySpan<bool> value)
    {
        GetEntry(identifier, (a, b) => a.GetBooleanArrayTopic(b).Publish()).Set(value);
    }

    public void Log(string identifier, ReadOnlySpan<int> value)
    {
        long[] widended = new long[value.Length];
        for (int i = 0; i < value.Length; i++)
        {
            widended[i] = value[i];
        }
        GetEntry(identifier, (a, b) => a.GetIntegerArrayTopic(b).Publish()).Set(widended);
    }

    public void Log(string identifier, ReadOnlySpan<long> value)
    {
        GetEntry(identifier, (a, b) => a.GetIntegerArrayTopic(b).Publish()).Set(value);
    }

    public void Log(string identifier, ReadOnlySpan<float> value)
    {
        GetEntry(identifier, (a, b) => a.GetFloatArrayTopic(b).Publish()).Set(value);
    }

    public void Log(string identifier, ReadOnlySpan<double> value)
    {
        GetEntry(identifier, (a, b) => a.GetDoubleArrayTopic(b).Publish()).Set(value);
    }

    public void Log(string identifier, string value)
    {
        GetEntry(identifier, (a, b) => a.GetStringTopic(b).Publish()).Set(value);
    }

    public void Log(string identifier, ReadOnlySpan<char> value)
    {
        GetEntry(identifier, (a, b) => a.GetStringTopic(b).Publish()).Set(value);
    }

    public void Log(string identifier, ReadOnlySpan<string> value)
    {
        GetEntry(identifier, (a, b) => a.GetStringArrayTopic(b).Publish()).Set(value);
    }

    public void Log<T>(string identifier, in T value) where T : IStructSerializable<T>
    {
        GetEntry(identifier, (a, b) => a.GetStructTopic<T>(b).Publish()).Set(value);
    }

    public void Log<T>(string identifier, T[] value) where T : IStructSerializable<T>
    {
        GetEntry(identifier, (a, b) => a.GetStructArrayTopic<T>(b).Publish()).Set(value);
    }

    public void Log<T>(string identifier, Span<T> value) where T : IStructSerializable<T>
    {
        GetEntry(identifier, (a, b) => a.GetStructArrayTopic<T>(b).Publish()).Set(value);
    }

    public void Log<T>(string identifier, ReadOnlySpan<T> value) where T : IStructSerializable<T>
    {
        GetEntry(identifier, (a, b) => a.GetStructArrayTopic<T>(b).Publish()).Set(value);
    }

    public void Log<T>(string identifier, T value) where T : Enum
    {
        GetEntry(identifier, (a, b) => a.GetStringTopic(b).Publish()).Set(value.ToString());
    }
}
