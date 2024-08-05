using WPIUtil.Serialization.Struct;

namespace Epilogue.Logging;

public class NullLogger : IDataLogger
{
    public IDataLogger Lazy => this;
    public IDataLogger GetSubLogger(string path) => this;

    public void Log(string identifier, bool value)
    {

    }

    public void Log(string identifier, int value)
    {

    }

    public void Log(string identifier, long value)
    {

    }

    public void Log(string identifier, float value)
    {

    }

    public void Log(string identifier, double value)
    {

    }

    public void Log(string identifier, ReadOnlySpan<byte> value)
    {

    }

    public void Log(string identifier, ReadOnlySpan<bool> value)
    {

    }

    public void Log(string identifier, ReadOnlySpan<int> value)
    {

    }

    public void Log(string identifier, ReadOnlySpan<long> value)
    {

    }

    public void Log(string identifier, ReadOnlySpan<float> value)
    {

    }

    public void Log(string identifier, ReadOnlySpan<double> value)
    {

    }

    public void Log(string identifier, string value)
    {

    }

    public void Log(string identifier, ReadOnlySpan<char> value)
    {

    }

    public void Log(string identifier, ReadOnlySpan<string> value)
    {

    }

    public void Log<T>(string identifier, in T value) where T : IStructSerializable<T>
    {

    }

    public void Log<T>(string identifier, T[] value) where T : IStructSerializable<T>
    {

    }

    public void Log<T>(string identifier, Span<T> value) where T : IStructSerializable<T>
    {

    }

    public void Log<T>(string identifier, ReadOnlySpan<T> value) where T : IStructSerializable<T>
    {

    }

    public void Log<T>(string identifier, T value) where T : Enum
    {

    }
}
