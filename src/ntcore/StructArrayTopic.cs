using System.Numerics;
using NetworkTables.Handles;
using NetworkTables.Natives;
using WPIUtil.Serialization.Struct;

namespace NetworkTables;

public sealed class StructArrayTopic<T> : Topic, IEquatable<StructArrayTopic<T>?>, IEqualityOperators<StructArrayTopic<T>?, StructArrayTopic<T>?, bool> where T : IStructSerializable<T>
{
    public IStruct<T> Proto { get; } = T.Struct;

    private StructArrayTopic(Topic topic) : base(topic.Instance, topic.Handle)
    {
    }

    internal StructArrayTopic(NetworkTableInstance inst, NtTopic handle) : base(inst, handle)
    {
    }

    public static StructArrayTopic<T> Wrap(Topic topic)
    {
        return new StructArrayTopic<T>(topic);
    }

    public static StructArrayTopic<T> Wrap(NetworkTableInstance instance, NtTopic handle)
    {
        return new StructArrayTopic<T>(instance, handle);
    }

    public IStructArraySubscriber<T> Subscribe(T[] defaultValue, PubSubOptions options)
    {
        return new StructArrayEntryImpl<T, NtSubscriber>(this, NtCore.Subscribe(Handle, NetworkTableType.Raw, Proto.TypeString, options), defaultValue, false);
    }

    public IStructArrayPublisher<T> Publish(PubSubOptions options)
    {
        return new StructArrayEntryImpl<T, NtPublisher>(this, NtCore.Publish(Handle, NetworkTableType.Raw, Proto.TypeString, options), default!, false);
    }

    public IStructArrayPublisher<T> PublishEx(string properties, PubSubOptions options)
    {
        return new StructArrayEntryImpl<T, NtPublisher>(this, NtCore.PublishEx(Handle, NetworkTableType.Raw, Proto.TypeString, properties, options), default!, false);
    }

    public IStructArrayEntry<T> GetEntry(T[] defaultValue, PubSubOptions options)
    {
        return new StructArrayEntryImpl<T, NtEntry>(this, NtCore.GetEntry(Handle, NetworkTableType.Raw, Proto.TypeString, options), defaultValue, false);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as StructArrayTopic<T>);
    }

    public bool Equals(StructArrayTopic<T>? other)
    {
        return other is not null &&
               EqualityComparer<IStruct<T>>.Default.Equals(Proto, other.Proto);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Proto);
    }

    public static bool operator ==(StructArrayTopic<T>? left, StructArrayTopic<T>? right)
    {
        return EqualityComparer<StructArrayTopic<T>>.Default.Equals(left, right);
    }

    public static bool operator !=(StructArrayTopic<T>? left, StructArrayTopic<T>? right)
    {
        return !(left == right);
    }
}
