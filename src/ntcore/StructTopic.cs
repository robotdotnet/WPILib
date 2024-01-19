using System;
using System.Collections.Generic;
using System.Numerics;
using NetworkTables.Handles;
using NetworkTables.Natives;
using WPIUtil.Serialization.Struct;

namespace NetworkTables;

public sealed class StructTopic<T> : Topic, IEquatable<StructTopic<T>?>, IEqualityOperators<StructTopic<T>?, StructTopic<T>?, bool> where T : IStructSerializable<T>
{
    public IStruct<T> Proto { get; } = T.Struct;

    private StructTopic(Topic topic) : base(topic.Instance, topic.Handle)
    {
    }

    private StructTopic(NetworkTableInstance inst, NtTopic handle) : base(inst, handle)
    {
    }

    public static StructTopic<T> Wrap(Topic topic)
    {
        return new StructTopic<T>(topic);
    }

    public static StructTopic<T> Wrap(NetworkTableInstance instance, NtTopic handle)
    {
        return new StructTopic<T>(instance, handle);
    }

    public IStructSubscriber<T> Subscribe(T defaultValue, PubSubOptions options)
    {
        return new StructEntryImpl<T, NtSubscriber>(this, NtCore.Subscribe(Handle, NetworkTableType.Raw, Proto.TypeString, options), defaultValue, false);
    }

    public IStructPublisher<T> Publish(PubSubOptions options)
    {
        return new StructEntryImpl<T, NtPublisher>(this, NtCore.Publish(Handle, NetworkTableType.Raw, Proto.TypeString, options), default!, false);
    }

    public IStructPublisher<T> PublishEx(string properties, PubSubOptions options)
    {
        return new StructEntryImpl<T, NtPublisher>(this, NtCore.PublishEx(Handle, NetworkTableType.Raw, Proto.TypeString, properties, options), default!, false);
    }

    public IStructEntry<T> GetEntry(T defaultValue, PubSubOptions options)
    {
        return new StructEntryImpl<T, NtEntry>(this, NtCore.GetEntry(Handle, NetworkTableType.Raw, Proto.TypeString, options), defaultValue, false);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as StructTopic<T>);
    }

    public bool Equals(StructTopic<T>? other)
    {
        return other is not null &&
               EqualityComparer<IStruct<T>>.Default.Equals(Proto, other.Proto);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Proto);
    }

    public static bool operator ==(StructTopic<T>? left, StructTopic<T>? right)
    {
        return EqualityComparer<StructTopic<T>>.Default.Equals(left, right);
    }

    public static bool operator !=(StructTopic<T>? left, StructTopic<T>? right)
    {
        return !(left == right);
    }
}
