using NetworkTables.Handles;
using WPIUtil.Serialization;

namespace NetworkTables;

public sealed class StructArrayTopic<T> : Topic
{
    public IStruct<T> Struct { get; }

    private StructArrayTopic(Topic topic, IStruct<T> value) : base(topic.Instance, topic.Handle)
    {
        Struct = value;
    }

    private StructArrayTopic(NetworkTableInstance inst, NtTopic handle, IStruct<T> value) : base(inst, handle)
    {
        Struct = value;
    }

    public static StructArrayTopic<T> Wrap(Topic topic, IStruct<T> value)
    {
        return new StructArrayTopic<T>(topic, value);
    }

    public static StructArrayTopic<T> Wrap(NetworkTableInstance inst, NtTopic handle, IStruct<T> value)
    {
        return new StructArrayTopic<T>(inst, handle, value);
    }
}
