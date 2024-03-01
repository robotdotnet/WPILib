using System.Numerics;
using NetworkTables.Handles;
using NetworkTables.Natives;
using WPIUtil.Serialization.Struct;

namespace NetworkTables;

public sealed class StructArrayTopic<T> : Topic, IEquatable<StructArrayTopic<T>?>, IEqualityOperators<StructArrayTopic<T>?, StructArrayTopic<T>?, bool> where T : IStructSerializable<T>
{
    public StructArrayTopic(Topic topic) : base(topic.Instance, topic.Handle)
    {
    }

    public StructArrayTopic(NetworkTableInstance inst, NtTopic handle) : base(inst, handle)
    {
    }

    public IStructArraySubscriber<T> Subscribe(T[] defaultValue, PubSubOptions options)
    {
        return new StructArrayEntryImpl<T, NtSubscriber>(this, NtCore.Subscribe(Handle, NetworkTableType.Raw, T.Struct.TypeString, options), defaultValue, false);
    }

    public IStructArrayPublisher<T> Publish(PubSubOptions options)
    {
        return new StructArrayEntryImpl<T, NtPublisher>(this, NtCore.Publish(Handle, NetworkTableType.Raw, T.Struct.TypeString, options), default!, false);
    }

    public IStructArrayPublisher<T> PublishEx(string properties, PubSubOptions options)
    {
        return new StructArrayEntryImpl<T, NtPublisher>(this, NtCore.PublishEx(Handle, NetworkTableType.Raw, T.Struct.TypeString, properties, options), default!, false);
    }

    public IStructArrayEntry<T> GetEntry(T[] defaultValue, PubSubOptions options)
    {
        return new StructArrayEntryImpl<T, NtEntry>(this, NtCore.GetEntry(Handle, NetworkTableType.Raw, T.Struct.TypeString, options), defaultValue, false);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as StructArrayTopic<T>);
    }

    public bool Equals(StructArrayTopic<T>? other)
    {
        return other?.Equals((Topic)this) ?? false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
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
