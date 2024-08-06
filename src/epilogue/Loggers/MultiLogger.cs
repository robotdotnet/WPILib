using WPIUtil.Serialization.Struct;

namespace WPILib.Logging.Loggers;

public class MultiLogger : IDataLogger
{
    private readonly List<IDataLogger> m_loggers;
    private readonly Dictionary<string, SubLogger> m_subLoggers = [];

    internal MultiLogger(params IDataLogger[] loggers)
    {
        m_loggers = new List<IDataLogger>(loggers);
    }

    public IDataLogger Lazy => new LazyLogger(this);
    public IDataLogger GetSubLogger(string path) => IDataLogger.GetDefaultSubLogger(this, path, m_subLoggers);

    public void Log(string identifier, bool value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, int value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, long value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, float value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, double value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, ReadOnlySpan<byte> value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, ReadOnlySpan<bool> value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, ReadOnlySpan<int> value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, ReadOnlySpan<long> value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, ReadOnlySpan<float> value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, ReadOnlySpan<double> value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, string value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, ReadOnlySpan<char> value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log(string identifier, ReadOnlySpan<string> value)
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log<T>(string identifier, in T value) where T : IStructSerializable<T>
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log<T>(string identifier, T[] value) where T : IStructSerializable<T>
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log<T>(string identifier, Span<T> value) where T : IStructSerializable<T>
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log<T>(string identifier, ReadOnlySpan<T> value) where T : IStructSerializable<T>
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }

    public void Log<T>(string identifier, T value) where T : Enum
    {
        foreach (var logger in m_loggers)
        {
            logger.Log(identifier, value);
        }
    }
}
