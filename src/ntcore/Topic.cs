using NetworkTables.Handles;
using NetworkTables.Natives;

namespace NetworkTables;

public class Topic
{
    internal Topic(NetworkTableInstance inst, NtTopic handle)
    {
        Handle = handle;
        Instance = inst;
    }

    public bool IsValid => Handle.Handle != 0;

    public NtTopic Handle { get; }

    public NetworkTableInstance Instance { get; }

    public string Name => NtCore.GetTopicName(Handle);

    public NetworkTableType Type => NtCore.GetTopicType(Handle);

    public string TypeString => NtCore.GetTopicTypeString(Handle);

    public TopicInfo? Info => NtCore.GetTopicInfo(Handle);

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

    public IGenericSubscriber GenericSubscribe(in PubSubOptions options) => GenericSubscribe("", options);

    public IGenericSubscriber GenericSubscribe(string typeString, in PubSubOptions options)
    {
        return new GenericEntryImpl<NtSubscriber>(this, NtCore.Subscribe(Handle, NetworkTableTypeHelpers.GetFromString(typeString), typeString, options));
    }

    public IGenericPublisher GenericPublish(string typeString, in PubSubOptions options)
    {
        return new GenericEntryImpl<NtPublisher>(this, NtCore.Publish(Handle, NetworkTableTypeHelpers.GetFromString(typeString), typeString, options));
    }

    public IGenericPublisher GenericPublishEx(string typeString, string properties, in PubSubOptions options)
    {
        return new GenericEntryImpl<NtPublisher>(this, NtCore.PublishEx(Handle, NetworkTableTypeHelpers.GetFromString(typeString), typeString, properties, options));
    }

    public IGenericEntry GetGenericEntry(in PubSubOptions options)
    {
        return GetGenericEntry("", options);
    }

    public IGenericEntry GetGenericEntry(string typeString, in PubSubOptions options)
    {
        return new GenericEntryImpl<NtEntry>(this, NtCore.GetEntry(Handle, NetworkTableTypeHelpers.GetFromString(typeString), typeString, options));
    }

    // TODO implement equals and hash code
}
