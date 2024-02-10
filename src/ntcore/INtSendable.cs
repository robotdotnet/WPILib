using WPIUtil.Sendable;

namespace NetworkTables;

public interface INtSendable : ISendable
{
    void InitSendable(INtSendableBuilder builder);

    void ISendable.InitSendable(ISendableBuilder builder)
    {
        if (builder.BackendKind == ISendableBuilder.BackingKind.NetworkTables)
        {
            InitSendable((INtSendableBuilder)builder);
        }
    }
}
