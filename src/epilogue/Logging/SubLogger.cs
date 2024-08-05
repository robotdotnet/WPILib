using System.Runtime.InteropServices;
using CommunityToolkit.Diagnostics;
using WPIUtil.Serialization.Struct;
using static WPIUtil.WpiGuard;

namespace Epilogue.Logging;

public class SubLogger : IDataLogger
{
    private readonly string m_prefix;
    private readonly IDataLogger m_impl;
    private readonly Dictionary<string, SubLogger> m_subLoggers = [];

    public SubLogger(string prefix, IDataLogger impl)
    {
        Guard.IsNotNull(prefix);
        if (prefix.EndsWith("/", StringComparison.InvariantCultureIgnoreCase))
        {
            m_prefix = prefix;
        }
        else
        {
            m_prefix = prefix + "/";
        }
        m_impl = RequireNotNull(impl);
    }

    public IDataLogger Lazy => new LazyLogger(this);
    public IDataLogger GetSubLogger(string path) => IDataLogger.GetDefaultSubLogger(this, path, m_subLoggers);

    public void Log(string identifier, bool value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, int value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, long value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, float value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, double value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<byte> value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<bool> value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<int> value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<long> value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<float> value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<double> value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, string value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<char> value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<string> value)
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log<T>(string identifier, in T value) where T : IStructSerializable<T>
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log<T>(string identifier, T[] value) where T : IStructSerializable<T>
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log<T>(string identifier, Span<T> value) where T : IStructSerializable<T>
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log<T>(string identifier, ReadOnlySpan<T> value) where T : IStructSerializable<T>
    {
        m_impl.Log(m_prefix + identifier, value);
    }

    public void Log<T>(string identifier, T value) where T : Enum
    {
        m_impl.Log(m_prefix + identifier, value);
    }
}
