using System;
using NetworkTables.Natives;

namespace NetworkTables;

public class Topic
{
    internal Topic(NetworkTableInstance inst, int handle)
    {
        Handle = handle;
        Instance = inst;
    }

    public bool IsValid => Handle != 0;

    public int Handle { get; }

    public NetworkTableInstance Instance { get; }

    public string Name => NtCore.GetTopicName(Handle);

    public NetworkTableType Type => NtCore.GetTopicType(Handle);

    public string TypeString => NtCore.GetTopicTypeString(Handle);

    public TopicInfo Info => NtCore.GetTopicInfo(Handle);

    public bool Persistent
    {
        get => NtCore.GetTopicPersistent(Handle);
        set => NtCore.SetTopicPersistent(Handle, value);
    }

    public bool Retained
    {
        get => NtCore.GetTopicRetained(Handle);
        set => NtCore.SetTopicRetained(Handle, value);
    }

    public bool Cached
    {
        get => NtCore.GetTopicCached(Handle);
        set => NtCore.SetTopicCached(Handle, value);
    }

    public bool Exists => NtCore.GetTopicExists(Handle);

    public string GetProperty(string name) => NtCore.GetTopicProperty(Handle, name);

    public void SetProperty(string name, string value) => NtCore.SetTopicProperty(Handle, name, value);

    public void DeleteProperty(string name) => NtCore.DeleteTopicProperty(Handle, name);

    public string Properties
    {
        get => NtCore.GetTopicProperties(Handle);
        set => NtCore.SetTopicProperties(Handle, value);
    }

    public GenericSubscriber GenericSubscribe(in PubSubOptions options) => GenericSubscribe("", options);

    // TODO move this
    private NetworkTableType GetFromString(string typeString) => NetworkTableType.Raw;

    public GenericSubscriber GenericSubscribe(string typeString, in PubSubOptions options)
    {
        return new GenericEntryImpl(this, NtCore.Subscribe(Handle, GetFromString(typeString), typeString, options));
    }

    public GenericPublisher GenericPublish(string typeString, in PubSubOptions options)
    {
        return new GenericEntryImpl(this, NtCore.Publish(Handle, GetFromString(typeString), typeString, options));
    }

    public GenericPublisher GenericPublishEx(string typeString, string properties, in PubSubOptions options)
    {
        return new GenericEntryImpl(this, NtCore.PublishEx(Handle, GetFromString(typeString), typeString, properties, options));
    }

    public GenericEntry GetGenericEntry(in PubSubOptions options)
    {
        return GetGenericEntry("", options);
    }

    public GenericEntry GetGenericEntry(string typeString, in PubSubOptions options)
    {
        return new GenericEntryImpl(this, NtCore.GetEntry(Handle, GetFromString(typeString), typeString, options));
    }

    // TODO implement equals and hash code
}
