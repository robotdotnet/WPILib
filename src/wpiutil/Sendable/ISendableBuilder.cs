using WPIUtil.Function;

namespace WPIUtil.Sendable;

public interface ISendableBuilder : IDisposable
{
    enum BackingKind
    {
        Unknown,
        NetworkTables
    }

    BackingKind BackendKind { get; }

    bool IsPublished { get; }

    void SetSmartDashboardType(string type);

    void SetActuator(bool value);

    void SetSafeState(Runnable func);

    void AddBooleanProperty(string key, Supplier<bool> getter, Consumer<bool> setter);

    void PublishConstBoolean(string key, bool value);

    void AddIntegerProperty(string key, Supplier<long> getter, Consumer<long> setter);

    void PublishConstInteger(string key, long value);

    void AddFloatProperty(string key, Supplier<float> getter, Consumer<float> setter);

    void PublishConstFloat(string key, float value);

    void AddDoubleProperty(string key, Supplier<double> getter, Consumer<double> setter);

    void PublishConstDouble(string key, double value);

    void AddStringProperty(string key, Supplier<string> getter, Consumer<string> setter);

    void PublishConstString(string key, string value);

    void PublishConstString(string key, ReadOnlySpan<byte> value);

    void AddBooleanArrayProperty(string key, Supplier<bool[]> getter, Consumer<bool[]> setter);

    void PublishConstBooleanArray(string key, ReadOnlySpan<bool> value);

    void AddIntegerArrayProperty(string key, Supplier<long[]> getter, Consumer<long[]> setter);

    void PublishConstIntegerArray(string key, ReadOnlySpan<long> value);

    void AddFloatArrayProperty(string key, Supplier<float[]> getter, Consumer<float[]> setter);

    void PublishConstFloatArray(string key, ReadOnlySpan<float> value);

    void AddDoubleArrayProperty(string key, Supplier<double[]> getter, Consumer<double[]> setter);

    void PublishConstDoubleArray(string key, ReadOnlySpan<double> value);

    void AddStringArrayProperty(string key, Supplier<string[]> getter, Consumer<string[]> setter);

    void PublishConstStringArray(string key, ReadOnlySpan<string> value);

    void AddRawProperty(string key, string typeString, Supplier<byte[]> getter, Consumer<byte[]> setter);

    void PublishConstRaw(string key, string typeString, ReadOnlySpan<byte> value);

    void Update();

    void ClearProperties();

    void AddDisposable(IDisposable disposable);
}
