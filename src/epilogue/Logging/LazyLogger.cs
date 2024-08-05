namespace Epilogue.Logging;

using System;
using System.Runtime.InteropServices;
using WPIUtil.Serialization.Struct;
using static WPIUtil.WpiGuard;

public class LazyLogger : IDataLogger
{
    private readonly IDataLogger m_logger;

    private readonly Dictionary<string, object> m_previousValues = [];
    private readonly Dictionary<string, SubLogger> m_subLoggers = [];

    public LazyLogger(IDataLogger logger)
    {
        m_logger = RequireNotNull(logger);
    }

    public IDataLogger Lazy => this;
    public IDataLogger GetSubLogger(string path) => IDataLogger.GetDefaultSubLogger(this, path, m_subLoggers);

    public void Log(string identifier, bool value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is bool oldValue && oldValue == value)
        {
            return;
        }
        preivous = value;
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, int value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is int oldValue && oldValue == value)
        {
            return;
        }
        preivous = value;
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, long value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is long oldValue && oldValue == value)
        {
            return;
        }
        preivous = value;
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, float value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is float oldValue && oldValue == value)
        {
            return;
        }
        preivous = value;
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, double value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is double oldValue && oldValue == value)
        {
            return;
        }
        preivous = value;
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<byte> value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is byte[] oldValue && oldValue.AsSpan().SequenceEqual(value))
        {
            return;
        }
        preivous = value.ToArray();
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<bool> value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is bool[] oldValue && oldValue.AsSpan().SequenceEqual(value))
        {
            return;
        }
        preivous = value.ToArray();
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<int> value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is int[] oldValue && oldValue.AsSpan().SequenceEqual(value))
        {
            return;
        }
        preivous = value.ToArray();
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<long> value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is long[] oldValue && oldValue.AsSpan().SequenceEqual(value))
        {
            return;
        }
        preivous = value.ToArray();
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<float> value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is float[] oldValue && oldValue.AsSpan().SequenceEqual(value))
        {
            return;
        }
        preivous = value.ToArray();
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<double> value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is double[] oldValue && oldValue.AsSpan().SequenceEqual(value))
        {
            return;
        }
        preivous = value.ToArray();
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, string value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is string oldValue && oldValue == value)
        {
            return;
        }
        preivous = value;
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<char> value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is char[] oldValue && oldValue.AsSpan().SequenceEqual(value))
        {
            return;
        }
        preivous = value.ToArray();
        m_logger.Log(identifier, value);
    }

    public void Log(string identifier, ReadOnlySpan<string> value)
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is string[] oldValue && oldValue.AsSpan().SequenceEqual(value))
        {
            return;
        }
        preivous = value.ToArray();
        m_logger.Log(identifier, value);
    }

    public void Log<T>(string identifier, in T value) where T : IStructSerializable<T>
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is T oldValue && EqualityComparer<T>.Default.Equals(oldValue, value))
        {
            return;
        }
        preivous = value;
        m_logger.Log(identifier, value);
    }

    public void Log<T>(string identifier, T[] value) where T : IStructSerializable<T>
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is T[] oldValue && oldValue.AsSpan().SequenceEqual(value))
        {
            return;
        }
        preivous = value;
        m_logger.Log(identifier, value);
    }

    public void Log<T>(string identifier, Span<T> value) where T : IStructSerializable<T>
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is T[] oldValue && oldValue.AsSpan().SequenceEqual(value))
        {
            return;
        }
        preivous = value.ToArray();
        m_logger.Log(identifier, value);
    }

    public void Log<T>(string identifier, ReadOnlySpan<T> value) where T : IStructSerializable<T>
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is T[] oldValue && oldValue.AsSpan().SequenceEqual(value))
        {
            return;
        }
        preivous = value.ToArray();
        m_logger.Log(identifier, value);
    }

    public void Log<T>(string identifier, T value) where T : Enum
    {
        ref var preivous = ref CollectionsMarshal.GetValueRefOrAddDefault(m_previousValues, identifier, out var _);
        if (preivous is T oldValue && EqualityComparer<T>.Default.Equals(oldValue, value))
        {
            return;
        }
        preivous = value;
        m_logger.Log(identifier, value);
    }
}
