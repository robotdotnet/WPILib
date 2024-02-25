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

    void SetSafeState(Action func);

    void AddBooleanProperty(string key, Func<bool> getter, Action<bool> setter);

    void PublishConstBoolean(string key, bool value);

    void AddIntegerProperty(string key, Func<long> getter, Action<long> setter);

    void PublishConstInteger(string key, long value);

    void AddFloatProperty(string key, Func<float> getter, Action<float> setter);

    void PublishConstFloat(string key, float value);

    void AddDoubleProperty(string key, Func<double> getter, Action<double> setter);

    void PublishConstDouble(string key, double value);

    void AddStringProperty(string key, Func<string>? getter, Action<string>? setter);

    void PublishConstString(string key, string value);

    void PublishConstString(string key, ReadOnlySpan<byte> value);

    void AddBooleanArrayProperty(string key, Func<bool[]> getter, Action<bool[]> setter);

    void PublishConstBooleanArray(string key, ReadOnlySpan<bool> value);

    void AddIntegerArrayProperty(string key, Func<long[]> getter, Action<long[]> setter);

    void PublishConstIntegerArray(string key, ReadOnlySpan<long> value);

    void AddFloatArrayProperty(string key, Func<float[]> getter, Action<float[]> setter);

    void PublishConstFloatArray(string key, ReadOnlySpan<float> value);

    void AddDoubleArrayProperty(string key, Func<double[]> getter, Action<double[]> setter);

    void PublishConstDoubleArray(string key, ReadOnlySpan<double> value);

    void AddStringArrayProperty(string key, Func<string[]> getter, Action<string[]> setter);

    void PublishConstStringArray(string key, ReadOnlySpan<string> value);

    void AddRawProperty(string key, string typeString, Func<byte[]> getter, Action<byte[]> setter);

    void PublishConstRaw(string key, string typeString, ReadOnlySpan<byte> value);

    void Update();

    void ClearProperties();

    void AddDisposable(IDisposable disposable);
}
