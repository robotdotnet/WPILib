using CommunityToolkit.Diagnostics;
using NetworkTables;

namespace WPILib.Event;

public class NetworkBooleanEvent : BooleanEvent
{
    public NetworkBooleanEvent(EventLoop loop, BooleanTopic topic) : this(loop, topic.Subscribe(false))
    {

    }

    public NetworkBooleanEvent(EventLoop loop, IBooleanSubscriber sub) : base(loop, () => sub.Topic.Instance.Connected && sub.Get())
    {
        Guard.IsNotNull(sub);
    }

    public NetworkBooleanEvent(EventLoop loop, NetworkTable table, string topicName) : this(loop, table.GetBooleanTopic(topicName))
    {

    }

    public NetworkBooleanEvent(EventLoop loop, string tableName, string topicName) : this(loop, NetworkTableInstance.Default, tableName, topicName)
    {

    }

    public NetworkBooleanEvent(EventLoop loop, NetworkTableInstance inst, string tableName, string topicName) : this(loop, inst.GetTable(tableName), topicName)
    {

    }
}
