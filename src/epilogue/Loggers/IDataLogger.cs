using System.Runtime.InteropServices;
using WPIUtil.Serialization.Struct;

namespace WPILib.Logging.Loggers;

public interface IDataLogger
{
    public static IDataLogger Multi(params IDataLogger[] loggers)
    {
        return new MultiLogger(loggers);
    }

    IDataLogger Lazy { get; }

    IDataLogger GetSubLogger(string path);

    internal static IDataLogger GetDefaultSubLogger(IDataLogger thisInstance, string path, Dictionary<string, SubLogger> subLoggers)
    {
        ref var subLogger = ref CollectionsMarshal.GetValueRefOrAddDefault(subLoggers, path, out var _);
        subLogger ??= new(path, thisInstance);
        return subLogger;
    }

    void Log(string identifier, bool value);

    void Log(string identifier, int value);

    void Log(string identifier, long value);

    void Log(string identifier, float value);

    void Log(string identifier, double value);

    void Log(string identifier, ReadOnlySpan<byte> value);

    void Log(string identifier, ReadOnlySpan<bool> value);

    void Log(string identifier, ReadOnlySpan<int> value);

    void Log(string identifier, ReadOnlySpan<long> value);

    void Log(string identifier, ReadOnlySpan<float> value);

    void Log(string identifier, ReadOnlySpan<double> value);

    void Log(string identifier, string value);

    void Log(string identifier, ReadOnlySpan<char> value);

    void Log(string identifier, ReadOnlySpan<string> value);

    void Log<T>(string identifier, in T value) where T : IStructSerializable<T>;

    void Log<T>(string identifier, T[] value) where T : IStructSerializable<T>;

    void Log<T>(string identifier, Span<T> value) where T : IStructSerializable<T>;

    void Log<T>(string identifier, ReadOnlySpan<T> value) where T : IStructSerializable<T>;

    void Log<T>(string identifier, T value) where T : Enum;
}
