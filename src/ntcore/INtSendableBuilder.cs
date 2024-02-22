using WPIUtil.Function;
using WPIUtil.Sendable;

namespace NetworkTables;

public interface INtSendableBuilder : ISendableBuilder
{
    Runnable UpdateTable { set; }

    Topic GetTopic(string key);

    NetworkTable Table { get; }

    BackingKind ISendableBuilder.BackendKind => BackingKind.NetworkTables;
}
