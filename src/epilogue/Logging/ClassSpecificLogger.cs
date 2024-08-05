using System.Runtime.InteropServices;
using Epilogue.Logging.Errors;
using WPIUtil.Sendable;

namespace Epilogue.Logging;

public abstract class ClassSpecificLogger
{
    private readonly Dictionary<ISendable, ISendableBuilder> m_sendables = [];

    protected ClassSpecificLogger(Type loggedType)
    {
        LoggedType = loggedType;
    }

    public bool Disabled { get; private set; }

    public void Disable()
    {
        Disabled = true;
    }

    public void Reenable()
    {
        Disabled = false;
    }

    public Type LoggedType { get; }

    protected virtual void LogSendable(IDataLogger dataLogger, ISendable? sendable)
    {
        if (sendable is null)
        {
            return;
        }

        ref ISendableBuilder? builder = ref CollectionsMarshal.GetValueRefOrAddDefault(m_sendables, sendable, out var _);
        if (builder is null)
        {
            builder = new LogBackedSenabledBuilder(dataLogger);
            sendable.InitSendable(builder);
        }
        builder.Update();
    }
}

public abstract class ClassSpecificLogger<T> : ClassSpecificLogger
{
    protected ClassSpecificLogger() : base(typeof(T))
    {
    }

    protected abstract void Update(IDataLogger dataLogger, T obj);

    public void TryUpdate(IDataLogger dataLogger, T obj, IErrorHandler errorHandler)
    {
        if (Disabled)
        {
            return;
        }

        try
        {
            Update(dataLogger, obj);
        }
        catch (Exception e)
        {
            errorHandler.Handle(e, this);
        }
    }
}
