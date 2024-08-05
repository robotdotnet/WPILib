using WPIUtil.Sendable;

namespace Epilogue.Logging;

public abstract class ClassSpecificLogger {
    private readonly Dictionary<ISendable, ISendableBuilder> m_sendables = [];

    protected ClassSpecificLogger(Type loggedType) {
        LoggedType = loggedType;
    }

    public bool Disabled { get; private set; }

    public void Disable() {
        Disabled = true;
    }

    public void Reenable() {
        Disabled = false;
    }

    public Type LoggedType { get; }

    protected virtual void LogSendable(DataLogger dataLogger, ISendable? sendable) {
        if (sendable == null) {
            return;
        }

        var builder = m_sendables.
    } 
}

public abstract class ClassSpecificLogger<T> : ClassSpecificLogger {
    protected ClassSpecificLogger() : base(typeof(T))
    {
    }

    protected abstract void Update(DataLogger dataLogger, T obj);

    public void TryUpdate(DataLogger dataLogger, T obj, ErrorHandler errorHandler) {
        if (Disabled) {
            return;
        }

        try {
            Update(dataLogger, obj);
        } catch (Exception e) {
            errorHandler(e, this);
        }
    }
}
