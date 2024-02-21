using WPIUtil.Sendable;

namespace NetworkTables;

public interface INtSendableBuilder : ISendableBuilder
{
    Action UpdateTable { set; }

    Topic GetTopic(string key);

    NetworkTable Table { get; }

    BackingKind ISendableBuilder.BackendKind => BackingKind.NetworkTables;
}
