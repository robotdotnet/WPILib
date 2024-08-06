using WPIUtil.Sendable;
using static WPIUtil.WpiGuard;

namespace WPILib.Logging.Loggers;

public class LogBackedSenabledBuilder : ISendableBuilder
{
    private readonly IDataLogger m_logger;
    private readonly List<Action> m_updates = [];

    public LogBackedSenabledBuilder(IDataLogger logger)
    {
        m_logger = RequireNotNull(logger);
    }

    public ISendableBuilder.BackingKind BackendKind => throw new NotImplementedException();

    public bool IsPublished => throw new NotImplementedException();

    public void AddBooleanArrayProperty(string key, Func<bool[]> getter, Action<bool[]> setter)
    {
        m_updates.Add(() => m_logger.Log(key, getter()));
    }

    public void AddBooleanProperty(string key, Func<bool> getter, Action<bool> setter)
    {
        m_updates.Add(() => m_logger.Log(key, getter()));
    }

    public void AddDisposable(IDisposable disposable)
    {
        // ignore
    }

    public void AddDoubleArrayProperty(string key, Func<double[]> getter, Action<double[]> setter)
    {
        m_updates.Add(() => m_logger.Log(key, getter()));
    }

    public void AddDoubleProperty(string key, Func<double> getter, Action<double> setter)
    {
        m_updates.Add(() => m_logger.Log(key, getter()));
    }

    public void AddFloatArrayProperty(string key, Func<float[]> getter, Action<float[]> setter)
    {
        m_updates.Add(() => m_logger.Log(key, getter()));
    }

    public void AddFloatProperty(string key, Func<float> getter, Action<float> setter)
    {
        m_updates.Add(() => m_logger.Log(key, getter()));
    }

    public void AddIntegerArrayProperty(string key, Func<long[]> getter, Action<long[]> setter)
    {
        m_updates.Add(() => m_logger.Log(key, getter()));
    }

    public void AddIntegerProperty(string key, Func<long> getter, Action<long> setter)
    {
        m_updates.Add(() => m_logger.Log(key, getter()));
    }

    public void AddRawProperty(string key, string typeString, Func<byte[]> getter, Action<byte[]> setter)
    {
        m_updates.Add(() => m_logger.Log(key, getter()));
    }

    public void AddStringArrayProperty(string key, Func<string[]> getter, Action<string[]> setter)
    {
        m_updates.Add(() => m_logger.Log(key, getter()));
    }

    public void AddStringProperty(string key, Func<string> getter, Action<string>? setter)
    {
        m_updates.Add(() => m_logger.Log(key, getter()));
    }

    public void ClearProperties()
    {
        m_updates.Clear();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        ClearProperties();
    }

    public void PublishConstBoolean(string key, bool value)
    {
        m_logger.Log(key, value);
    }

    public void PublishConstBooleanArray(string key, ReadOnlySpan<bool> value)
    {
        m_logger.Log(key, value);
    }

    public void PublishConstDouble(string key, double value)
    {
        m_logger.Log(key, value);
    }

    public void PublishConstDoubleArray(string key, ReadOnlySpan<double> value)
    {
        m_logger.Log(key, value);
    }

    public void PublishConstFloat(string key, float value)
    {
        m_logger.Log(key, value);
    }

    public void PublishConstFloatArray(string key, ReadOnlySpan<float> value)
    {
        m_logger.Log(key, value);
    }

    public void PublishConstInteger(string key, long value)
    {
        m_logger.Log(key, value);
    }

    public void PublishConstIntegerArray(string key, ReadOnlySpan<long> value)
    {
        m_logger.Log(key, value);
    }

    public void PublishConstRaw(string key, string typeString, ReadOnlySpan<byte> value)
    {
        m_logger.Log(key, value);
    }

    public void PublishConstString(string key, string value)
    {
        m_logger.Log(key, value);
    }

    public void PublishConstString(string key, ReadOnlySpan<byte> value)
    {
        m_logger.Log(key, value);
    }

    public void PublishConstStringArray(string key, ReadOnlySpan<string> value)
    {
        m_logger.Log(key, value);
    }

    public void SetActuator(bool value)
    {
        // ignore
    }

    public void SetSafeState(Action func)
    {
        // ignore
    }

    public void SetSmartDashboardType(string type)
    {
        m_logger.Log(".type", type);
    }

    public void Update()
    {
        foreach (var update in m_updates)
        {
            update();
        }
    }
}
