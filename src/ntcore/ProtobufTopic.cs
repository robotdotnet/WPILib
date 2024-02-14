using System;
using System.Collections.Generic;
using System.Numerics;
using NetworkTables.Handles;
using NetworkTables.Natives;
using WPIUtil.Serialization.Protobuf;

namespace NetworkTables;

public sealed class ProtobufTopic<T> : Topic, IEquatable<ProtobufTopic<T>?>, IEqualityOperators<ProtobufTopic<T>?, ProtobufTopic<T>?, bool> where T : IProtobufSerializable<T>
{
    public IGenericProtobuf<T> Proto { get; } = T.ProtoGeneric;

    private ProtobufTopic(Topic topic) : base(topic.Instance, topic.Handle)
    {
    }

    internal ProtobufTopic(NetworkTableInstance inst, NtTopic handle) : base(inst, handle)
    {
    }

    public static ProtobufTopic<T> Wrap(Topic topic)
    {
        return new ProtobufTopic<T>(topic);
    }

    public static ProtobufTopic<T> Wrap(NetworkTableInstance instance, NtTopic handle)
    {
        return new ProtobufTopic<T>(instance, handle);
    }

    public IProtobufSubscriber<T> Subscribe(T defaultValue, PubSubOptions options)
    {
        return new ProtobufEntryImpl<T, NtSubscriber>(this, NtCore.Subscribe(Handle, NetworkTableType.Raw, Proto.TypeString, options), defaultValue, false);
    }

    public IProtobufPublisher<T> Publish(PubSubOptions options)
    {
        return new ProtobufEntryImpl<T, NtPublisher>(this, NtCore.Publish(Handle, NetworkTableType.Raw, Proto.TypeString, options), default!, false);
    }

    public IProtobufPublisher<T> PublishEx(string properties, PubSubOptions options)
    {
        return new ProtobufEntryImpl<T, NtPublisher>(this, NtCore.PublishEx(Handle, NetworkTableType.Raw, Proto.TypeString, properties, options), default!, false);
    }

    public IProtobufEntry<T> GetEntry(T defaultValue, PubSubOptions options)
    {
        return new ProtobufEntryImpl<T, NtEntry>(this, NtCore.GetEntry(Handle, NetworkTableType.Raw, Proto.TypeString, options), defaultValue, false);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ProtobufTopic<T>);
    }

    public bool Equals(ProtobufTopic<T>? other)
    {
        return other is not null &&
               EqualityComparer<IGenericProtobuf<T>>.Default.Equals(Proto, other.Proto);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Proto);
    }

    public static bool operator ==(ProtobufTopic<T>? left, ProtobufTopic<T>? right)
    {
        return EqualityComparer<ProtobufTopic<T>>.Default.Equals(left, right);
    }

    public static bool operator !=(ProtobufTopic<T>? left, ProtobufTopic<T>? right)
    {
        return !(left == right);
    }
}
