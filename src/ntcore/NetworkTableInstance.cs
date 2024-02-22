// Copyright (c) FIRST and other WPILib contributors.
// Open Source Software; you can modify and/or share it under the terms of
// the WPILib BSD license file in the root directory of this project.

// THIS FILE WAS AUTO-GENERATED BY ./ntcore/generate_topics.py. DO NOT MODIFY

using System.Collections.Concurrent;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using CommunityToolkit.Diagnostics;
using NetworkTables.Handles;
using NetworkTables.Natives;
using WPIUtil.Concurrent;
using WPIUtil.Handles;
using WPIUtil.Logging;
using WPIUtil.Natives;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace NetworkTables;

/**
 * NetworkTables Instance.
 *
 * <p>Instances are completely independent from each other. Table operations on one instance will
 * not be visible to other instances unless the instances are connected via the network. The main
 * limitation on instances is that you cannot have two servers on the same network port. The main
 * utility of instances is for unit testing, but they can also enable one program to connect to two
 * different NetworkTables networks.
 *
 * <p>The global "default" instance (as returned by {@link #GetDefault()}) is always available, and
 * is intended for the common case when there is only a single NetworkTables instance being used in
 * the program.
 *
 * <p>Additional instances can be created with the {@link #create()} function. A reference must be
 * kept to the NetworkTableInstance returned by this function to keep it from being garbage
 * collected.
 */
public sealed partial class NetworkTableInstance : IDisposable, IEquatable<NetworkTableInstance?>, IEqualityOperators<NetworkTableInstance?, NetworkTableInstance?, bool>
{
    private static readonly ConcurrentDictionary<NtInst, NetworkTableInstance> s_instances = new();

    private static NetworkTableInstance InstanceCreator(NtInst inst)
    {
        if (inst == NtCore.GetDefaultInstance())
        {
            return new NetworkTableInstance(inst, false);
        }
        else
        {
            return new NetworkTableInstance(inst, true);
        }
    }

    public static NetworkTableInstance GetInstanceForHandle(NtInst inst)
    {
        return s_instances.GetOrAdd(inst, InstanceCreator);
    }

    private readonly ConcurrentDictionary<string, Topic> m_topics = new();
    private readonly ConcurrentDictionary<NtTopic, Topic> m_topicsByHandle = new();
    private readonly ListenerStorage m_listeners;
    private readonly bool m_owned;

    public const int KDefaultPort3 = 1735;
    public const int KDefaultPort4 = 5810;

    private NetworkTableInstance(NtInst inst, bool owned)
    {
        Handle = inst;
        m_owned = owned;
        m_listeners = new(Handle);
    }

    public NtInst Handle { get; private set; }

    public bool IsValid => Handle.Handle != 0;

    public static NetworkTableInstance Default => GetInstanceForHandle(NtCore.GetDefaultInstance());

    public static NetworkTableInstance Create() => GetInstanceForHandle(NtCore.CreateInstance());

    public static NetworkTableInstance FromHandle<T>(T handle) where T : IWPIIntHandle => GetInstanceForHandle(NtCore.GetInstanceFromHandle(handle));

    public void Dispose()
    {
        if (m_owned & Handle.Handle != 0)
        {
            m_listeners.Dispose();
            NtCore.DestroyInstance(Handle);
            s_instances.TryRemove(Handle, out var _);
            Handle = default;
        }
    }

    private static Topic TopicCreator(string name, NetworkTableInstance instance)
    {
        Topic topic = new Topic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }

    private static Topic TopicByHandleAdder(NtTopic handle, Topic newTopic)
    {
        return newTopic;
    }
    private static Topic TopicByHandleUpdater(NtTopic handle, Topic oldTopic, Topic newTopic)
    {
        return newTopic;
    }

    public Topic GetTopic(string name)
    {
        return m_topics.GetOrAdd(name, TopicCreator, this);
    }

    private static Topic CachedTopicAdder(NtTopic handle, NetworkTableInstance instance)
    {
        return new Topic(instance, handle);
    }

    private Topic GetCachedTopic(NtTopic handle)
    {
        Topic topic = m_topicsByHandle.GetOrAdd(handle, CachedTopicAdder, this);
        return topic;
    }

    private Topic[] TopicHandlesToTopics(NtTopic[] handles)
    {
        Topic[] topics = new Topic[handles.Length];
        for (int i = 0; i < handles.Length; i++)
        {
            topics[i] = GetCachedTopic(handles[i]);
        }
        return topics;
    }

    public Topic[] GetTopics(string prefix = "", NetworkTableType types = NetworkTableType.Unassigned)
    {
        return TopicHandlesToTopics(NtCore.GetTopics(Handle, prefix, types));
    }

    public Topic[] GetTopics(ReadOnlySpan<byte> prefix = default, NetworkTableType types = NetworkTableType.Unassigned)
    {
        return TopicHandlesToTopics(NtCore.GetTopics(Handle, prefix, types));
    }

    public Topic[] GetTopics(string prefix, ReadOnlySpan<string> types)
    {
        return TopicHandlesToTopics(NtCore.GetTopics(Handle, prefix, types));
    }

    public Topic[] GetTopics(ReadOnlySpan<byte> prefix, ReadOnlySpan<string> types)
    {
        return TopicHandlesToTopics(NtCore.GetTopics(Handle, prefix, types));
    }

    public TopicInfo[] GetTopicInfo(string prefix = "", NetworkTableType types = NetworkTableType.Unassigned)
    {
        return NtCore.GetTopicInfos(Handle, prefix, types);
    }

    public TopicInfo[] GetTopicInfo(ReadOnlySpan<byte> prefix = default, NetworkTableType types = NetworkTableType.Unassigned)
    {
        return NtCore.GetTopicInfos(Handle, prefix, types);
    }

    public TopicInfo[] GetTopicInfo(string prefix, ReadOnlySpan<string> types)
    {
        return NtCore.GetTopicInfos(Handle, prefix, types);
    }

    public TopicInfo[] GetTopicInfo(ReadOnlySpan<byte> prefix, ReadOnlySpan<string> types)
    {
        return NtCore.GetTopicInfos(Handle, prefix, types);
    }

    public NetworkTable GetTable(string key)
    {
        if (string.IsNullOrWhiteSpace(key) || key == "/")
        {
            return new NetworkTable(this, "");
        }
        else if (key[0] == NetworkTable.PATH_SEPARATOR)
        {
            return new NetworkTable(this, key);
        }
        else
        {
            return new NetworkTable(this, $"/{key}");
        }
    }

    public void RemoveListener(NtListener listener)
    {
        m_listeners.Remove(listener);
    }

    public bool WaitForListenerQueue(TimeSpan? timeout)
    {
        return m_listeners.WaitForQueue(timeout);
    }

    private class ListenerStorage(NtInst inst) : IDisposable
    {
        private readonly object m_lock = new();
        private readonly Dictionary<NtListener, Action<NetworkTableEvent>> m_listeners = [];
        private Thread? m_thread;
        private NtListenerPoller m_poller;
        private bool m_waitQueue;
        private readonly Event m_waitQueueEvent = new();
        private readonly NtInst m_inst = inst;

        public NtListener Add(ReadOnlySpan<string> prefixes, EventFlags eventKinds, Action<NetworkTableEvent> listener)
        {
            lock (m_lock)
            {
                if (m_poller.Handle == 0)
                {
                    m_poller = NtCore.CreateListenerPoller(m_inst);
                    StartThread();
                }
                NtListener h = NtCore.AddListener(m_poller, prefixes, eventKinds);
                m_listeners.Add(h, listener);
                return h;
            }
        }

        public NtListener Add(NtInst handle, EventFlags eventKinds, Action<NetworkTableEvent> listener)
        {
            lock (m_lock)
            {
                if (m_poller.Handle == 0)
                {
                    m_poller = NtCore.CreateListenerPoller(m_inst);
                    StartThread();
                }
                NtListener h = NtCore.AddListener(m_poller, handle, eventKinds);
                m_listeners.Add(h, listener);
                return h;
            }
        }

        public NtListener Add(NtTopic handle, EventFlags eventKinds, Action<NetworkTableEvent> listener)
        {
            lock (m_lock)
            {
                if (m_poller.Handle == 0)
                {
                    m_poller = NtCore.CreateListenerPoller(m_inst);
                    StartThread();
                }
                NtListener h = NtCore.AddListener(m_poller, handle, eventKinds);
                m_listeners.Add(h, listener);
                return h;
            }
        }

        public NtListener Add(NtMultiSubscriber handle, EventFlags eventKinds, Action<NetworkTableEvent> listener)
        {
            lock (m_lock)
            {
                if (m_poller.Handle == 0)
                {
                    m_poller = NtCore.CreateListenerPoller(m_inst);
                    StartThread();
                }
                NtListener h = NtCore.AddListener(m_poller, handle, eventKinds);
                m_listeners.Add(h, listener);
                return h;
            }
        }

        public NtListener Add<T>(T handle, EventFlags eventKinds, Action<NetworkTableEvent> listener) where T : struct, INtEntryHandle
        {
            lock (m_lock)
            {
                if (m_poller.Handle == 0)
                {
                    m_poller = NtCore.CreateListenerPoller(m_inst);
                    StartThread();
                }
                NtListener h = NtCore.AddListener(m_poller, handle, eventKinds);
                m_listeners.Add(h, listener);
                return h;
            }
        }

        public NtListener AddLogger(int minLevel, int maxLevel, Action<NetworkTableEvent> listener)
        {
            lock (m_lock)
            {
                if (m_poller.Handle == 0)
                {
                    m_poller = NtCore.CreateListenerPoller(m_inst);
                    StartThread();
                }
                NtListener h = NtCore.AddLogger(m_poller, (uint)minLevel, (uint)maxLevel);
                m_listeners.Add(h, listener);
                return h;
            }
        }

        public void Remove(NtListener listener)
        {
            lock (m_lock)
            {
                m_listeners.Remove(listener);
            }
            NtCore.RemoveListener(listener);
        }

        public void Dispose()
        {
            if (m_poller.Handle != 0)
            {
                NtCore.DestroyListenerPoller(m_poller);
                m_poller = default;
            }
        }

        private void StartThread()
        {
            m_thread = new Thread(() =>
            {
                bool wasInterrupted = false;
                ReadOnlySpan<int> handles = [m_poller.Handle, m_waitQueueEvent.Handle.Handle];
                Span<int> signaledStorage = [0, 0];
                while (true)
                {
                    var signaled = SynchronizationNative.WaitForObjects(handles, signaledStorage);
                    if (signaled.Length == 0)
                    {
                        lock (m_lock)
                        {
                            if (m_waitQueue)
                            {
                                m_waitQueue = false;
                                Monitor.PulseAll(m_lock);
                            }
                        }
                        wasInterrupted = true;
                        break;
                    }
                    foreach (NetworkTableEvent evnt in NtCore.ReadListenerQueue(m_poller))
                    {
                        Action<NetworkTableEvent>? listener;
                        lock (m_lock)
                        {
                            if (!m_listeners.TryGetValue(evnt.ListenerHandle, out listener))
                            {
                                listener = null;
                            }
                        }
                        try
                        {
                            listener?.Invoke(evnt);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Unhandled exception during listener callback: {ex}");
                        }
                    }
                    lock (m_lock)
                    {
                        if (m_waitQueue)
                        {
                            m_waitQueue = false;
                            Monitor.PulseAll(m_lock);
                        }
                    }
                }
                lock (m_lock)
                {
                    if (!wasInterrupted)
                    {
                        NtCore.DestroyListenerPoller(m_poller);
                    }
                    m_poller = default;
                }

            })
            {
                Name = "NTListener",
                IsBackground = true
            };
            m_thread.Start();
        }

        public bool WaitForQueue(TimeSpan? timeout)
        {
            lock (m_lock)
            {
                if (m_poller.Handle != 0)
                {
                    m_waitQueue = true;
                    m_waitQueueEvent.Set();
                }
                while (m_waitQueue)
                {
                    if (!timeout.HasValue)
                    {
                        Monitor.Wait(m_lock);
                    }
                    else
                    {
                        return Monitor.Wait(m_lock, timeout.Value);
                    }
                }
            }
            return true;
        }
    }

    public NtListener AddConnectionListener(bool immediateNotify, Action<NetworkTableEvent> listener)
    {
        EventFlags flags = EventFlags.Connection;
        if (immediateNotify)
        {
            flags |= EventFlags.Immediate;
        }
        return m_listeners.Add(Handle, flags, listener);
    }

    public NtListener AddTimeSyncListener(bool immediateNotify, Action<NetworkTableEvent> listener)
    {
        EventFlags flags = EventFlags.TimeSync;
        if (immediateNotify)
        {
            flags |= EventFlags.Immediate;
        }
        return m_listeners.Add(Handle, flags, listener);
    }

    public NtListener AddListener(Topic topic, EventFlags eventKinds, Action<NetworkTableEvent> listener)
    {
        if (topic.Instance.Handle != Handle)
        {
            ThrowHelper.ThrowArgumentException("Topic is not from this instance");
        }
        return m_listeners.Add(topic.Handle, eventKinds, listener);
    }

    public NtListener AddListener(ISubscriber subscriber, EventFlags eventKinds, Action<NetworkTableEvent> listener)
    {
        if (subscriber.Topic.Instance.Handle != Handle)
        {
            ThrowHelper.ThrowArgumentException("Topic is not from this instance");
        }
        return m_listeners.Add(subscriber.Handle, eventKinds, listener);
    }

    public NtListener AddListener(MultiSubscriber subscriber, EventFlags eventKinds, Action<NetworkTableEvent> listener)
    {
        if (subscriber.Instance.Handle != Handle)
        {
            ThrowHelper.ThrowArgumentException("Topic is not from this instance");
        }

        return m_listeners.Add(subscriber.Handle, eventKinds, listener);
    }

    public NtListener AddListener(ReadOnlySpan<string> prefixes, EventFlags eventKinds, Action<NetworkTableEvent> listener)
    {
        return m_listeners.Add(prefixes, eventKinds, listener);
    }

    public NetworkMode GetNetworkMode()
    {
        return NtCore.GetNetworkMode(Handle);
    }

    public void StartLocal()
    {
        NtCore.StartLocal(Handle);
    }

    public void StopLocal()
    {
        NtCore.StopLocal(Handle);
    }

    public void StartServer(string persistFilename = "networktables.json", string listenAddress = "", int port3 = KDefaultPort3, int port4 = KDefaultPort4)
    {
        NtCore.StartServer(Handle, persistFilename, listenAddress, (uint)port3, (uint)port4);
    }

    public void StopServer()
    {
        NtCore.StopServer(Handle);
    }

    public void StartClient3(string identity)
    {
        NtCore.StartClient3(Handle, identity);
    }

    public void StartClient4(string identity)
    {
        NtCore.StartClient4(Handle, identity);
    }

    public void SetServer(string serverName, int port = 0)
    {
        NtCore.SetServer(Handle, serverName, (uint)port);
    }

    public void SetServer(params string[] serverNames)
    {
        SetServer(serverNames, 0);
    }

    public void SetServer(ReadOnlySpan<string> serverNames, int port)
    {
        Span<int> ports = stackalloc int[16];
        if (serverNames.Length > 16)
        {
            ports = new int[serverNames.Length];
        }
        else
        {
            ports = ports[..serverNames.Length];
        }

        ports.Fill(port);
        SetServer(serverNames, ports);
    }

    public void SetServer(ReadOnlySpan<string> serverNames, ReadOnlySpan<int> ports)
    {
        NtCore.SetServerMulti(Handle, serverNames, MemoryMarshal.Cast<int, uint>(ports));
    }

    public void SetServerTeam(int team, int port = 0)
    {
        NtCore.SetServerTeam(Handle, (uint)team, (uint)port);
    }

    public void Disconnect()
    {
        NtCore.Disconnect(Handle);
    }

    public void StartDsClient(int port = 0)
    {
        NtCore.StartDSClient(Handle, (uint)port);
    }

    public void StopDsClient()
    {
        NtCore.StopDSClient(Handle);
    }

    public void FlushLocal()
    {
        NtCore.FlushLocal(Handle);
    }

    public void Flush()
    {
        NtCore.Flush(Handle);
    }

    public ConnectionInfo[] GetConnections()
    {
        return NtCore.GetConnections(Handle);
    }

    public bool Connected => NtCore.IsConnected(Handle);

    public long? ServerTimeOffset => NtCore.GetServerTimeOffset(Handle);

    public unsafe NtDataLogger StartEntryDataLog(DataLog log, string prefix, string logPrefix)
    {
        return NtCore.StartEntryDataLog(Handle, log.NativeHandle, prefix, logPrefix);
    }

    public static void StopEntryDataLog(NtDataLogger logger)
    {
        NtCore.StopEntryDataLog(logger);
    }

    public unsafe NtConnectionDataLogger StartConnectionDataLog(DataLog log, string name)
    {
        return NtCore.StartConnectionDataLog(Handle, log.NativeHandle, name);
    }

    public static void StopConnectionDataLog(NtConnectionDataLogger logger)
    {
        NtCore.StopConnectionDataLog(logger);
    }

    public NtListener AddLogger(int minLevel, int maxLevel, Action<NetworkTableEvent> callback)
    {
        return m_listeners.AddLogger(minLevel, maxLevel, callback);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as NetworkTableInstance);
    }

    public bool Equals(NetworkTableInstance? other)
    {
        return other is not null &&
               Handle == other.Handle;
    }

    public override int GetHashCode()
    {
        return Handle.GetHashCode();
    }

    public static bool operator ==(NetworkTableInstance? left, NetworkTableInstance? right)
    {
        return EqualityComparer<NetworkTableInstance>.Default.Equals(left, right);
    }

    public static bool operator !=(NetworkTableInstance? left, NetworkTableInstance? right)
    {
        return !(left == right);
    }

    public bool HasSchema(string name)
    {
        return NtCore.HasSchema(Handle, name);
    }

    public void AddSchema(string name, string type, ReadOnlySpan<byte> schema)
    {
        NtCore.AddSchema(Handle, name, type, schema);
    }

    public void AddSchema(string name, string type, string schema)
    {
        NtCore.AddSchema(Handle, name, type, Encoding.UTF8.GetBytes(schema));
    }

    public void AddSchema(IStructBase proto)
    {
        AddSchemaImpl(proto, []);
    }

    public void AddSchema(IProtobuf proto)
    {
        proto.ForEachDescriptor(HasSchema, (typeString, schema) => AddSchema(typeString, "proto:FileDescriptorProto", schema));
    }

    private static ProtobufTopic<T> ProtobufTopicCreator<T>(string name, NetworkTableInstance instance) where T : IProtobufSerializable<T>
    {
        ProtobufTopic<T> topic = new ProtobufTopic<T>(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static ProtobufTopic<T> ProtobufTopicUpdator<T>(string name, Topic existingTopic, NetworkTableInstance instance) where T : IProtobufSerializable<T>
    {
        // Exists, but might be wrong type
        if (existingTopic is ProtobufTopic<T> protobufTopic)
        {
            return protobufTopic;
        }
        ProtobufTopic<T> topic = new ProtobufTopic<T>(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }

    public ProtobufTopic<T> GetProtobufTopic<T>(string name) where T : IProtobufSerializable<T>
    {
        return (ProtobufTopic<T>)m_topics.AddOrUpdate(name, ProtobufTopicCreator<T>, ProtobufTopicUpdator<T>, this);
    }

    private static StructTopic<T> StructTopicCreator<T>(string name, NetworkTableInstance instance) where T : IStructSerializable<T>
    {
        StructTopic<T> topic = new StructTopic<T>(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static StructTopic<T> StructTopicUpdator<T>(string name, Topic existingTopic, NetworkTableInstance instance) where T : IStructSerializable<T>
    {
        // Exists, but might be wrong type
        if (existingTopic is StructTopic<T> structTopic)
        {
            return structTopic;
        }
        StructTopic<T> topic = new StructTopic<T>(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }

    public StructTopic<T> GetStructTopic<T>(string name) where T : IStructSerializable<T>
    {
        return (StructTopic<T>)m_topics.AddOrUpdate(name, StructTopicCreator<T>, StructTopicUpdator<T>, this);
    }

    private static StructArrayTopic<T> StructArrayTopicCreator<T>(string name, NetworkTableInstance instance) where T : IStructSerializable<T>
    {
        StructArrayTopic<T> topic = new StructArrayTopic<T>(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static StructArrayTopic<T> StructArrayTopicUpdator<T>(string name, Topic existingTopic, NetworkTableInstance instance) where T : IStructSerializable<T>
    {
        // Exists, but might be wrong type
        if (existingTopic is StructArrayTopic<T> structTopic)
        {
            return structTopic;
        }
        StructArrayTopic<T> topic = new StructArrayTopic<T>(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }

    public StructArrayTopic<T> GetStructArrayTopic<T>(string name) where T : IStructSerializable<T>
    {
        return (StructArrayTopic<T>)m_topics.AddOrUpdate(name, StructArrayTopicCreator<T>, StructArrayTopicUpdator<T>, this);
    }

    private void AddSchemaImpl(IStructBase strct, HashSet<string> seen)
    {
        string typeString = strct.TypeString;
        if (HasSchema(typeString))
        {
            return;
        }
        if (!seen.Add(typeString))
        {
            throw new InvalidOperationException($"{typeString}: circular reference");
        }
        AddSchema(typeString, "structschema", strct.Schema);
        foreach (var inner in strct.Nested)
        {
            AddSchemaImpl(inner, seen);
        }
        seen.Remove(typeString);
    }

    private static StringTopic StringTopicCreator(string name, NetworkTableInstance instance)
    {
        StringTopic topic = new StringTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static StringTopic StringTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is StringTopic StringTopic)
        {
            return StringTopic;
        }
        StringTopic topic = new StringTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    /// <summary>
    /// Gets a string topic.
    /// </summary>
    /// <param name="name">topic name</param>
    /// <returns>StringTopic</returns>
    public StringTopic GetStringTopic(string name)
    {
        return (StringTopic)m_topics.AddOrUpdate(name, StringTopicCreator, StringTopicUpdator, this);
    }

    private static StringArrayTopic StringArrayTopicCreator(string name, NetworkTableInstance instance)
    {
        StringArrayTopic topic = new StringArrayTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static StringArrayTopic StringArrayTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is StringArrayTopic StringArrayTopic)
        {
            return StringArrayTopic;
        }
        StringArrayTopic topic = new StringArrayTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    /// <summary>
    /// Gets a string[] topic.
    /// </summary>
    /// <param name="name">topic name</param>
    /// <returns>StringArrayTopic</returns>
    public StringArrayTopic GetStringArrayTopic(string name)
    {
        return (StringArrayTopic)m_topics.AddOrUpdate(name, StringArrayTopicCreator, StringArrayTopicUpdator, this);
    }
}
