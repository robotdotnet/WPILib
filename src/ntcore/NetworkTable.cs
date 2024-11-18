using System.Collections.Concurrent;
using NetworkTables.Handles;

namespace NetworkTables;

public sealed partial class NetworkTable : IEquatable<NetworkTable?>
{
    public const char PathSeparator = '/';
    private readonly string m_pathWithSep;

    public static ReadOnlySpan<char> BasenameKey(ReadOnlySpan<char> key)
    {
        int slash = key.LastIndexOf(PathSeparator);
        if (slash == -1)
        {
            return key;
        }
        return key[(slash + 1)..];
    }

    public static string NormalizeKey(string key, bool withLeadingSlash = true)
    {
        string normalized;
        if (withLeadingSlash)
        {
            normalized = $"{PathSeparator}{key}";
        }
        else
        {
            normalized = key;
        }
        normalized = normalized.Replace($"{PathSeparator}{PathSeparator}", $"{PathSeparator}");
        if (!withLeadingSlash && normalized[0] == PathSeparator)
        {
            normalized = normalized[1..];
        }
        return normalized;
    }

    public static List<string> GetHierarchy(string key)
    {
        string normal = NormalizeKey(key);
        List<string> hierarchy = [];
        if (normal.Length == 1)
        {
            hierarchy.Add(normal);
            return hierarchy;
        }
        for (int i = 0; ; i = normal.IndexOf(PathSeparator, i + 1))
        {
            if (i == -1)
            {
                // Add the full key
                hierarchy.Add(normal);
                break;
            }
            else
            {
                hierarchy.Add(normal[..i]);
            }
        }
        return hierarchy;
    }

    internal NetworkTable(NetworkTableInstance inst, string path)
    {
        Path = path;
        m_pathWithSep = $"{path}{PathSeparator}";
        Instance = inst;
    }

    public NetworkTableInstance Instance { get; }

    public override string ToString()
    {
        return $"NetworkTable: {Path}";
    }

    public Topic GetTopic(string name)
    {
        return Instance.GetTopic($"{m_pathWithSep}{name}");
    }

    public NetworkTable GetSubTable(string key)
    {
        return new NetworkTable(Instance, $"{m_pathWithSep}{key}");
    }

    public bool ContainsKey(string key)
    {
        return !(string.IsNullOrWhiteSpace(key) && GetTopic(key).Exists);
    }

    public bool ContainsSubTable(string key)
    {
        Topic[] topics = Instance.GetTopics($"{m_pathWithSep}{key}{PathSeparator}");
        return topics.Length != 0;
    }

    public List<TopicInfo> GetTopicInfo(NetworkTableType types = NetworkTableType.Unassigned)
    {
        List<TopicInfo> infos = [];
        int prefixLen = Path.Length + 1;
        foreach (TopicInfo info in Instance.GetTopicInfo(m_pathWithSep, types))
        {
            ReadOnlySpan<char> relativeKey = info.Name.AsSpan()[prefixLen..];
            if (relativeKey.IndexOf(PathSeparator) != -1)
            {
                continue;
            }
            infos.Add(info);
        }
        return infos;
    }

    public List<Topic> GetTopics(NetworkTableType types = NetworkTableType.Unassigned)
    {
        List<Topic> topics = [];
        int prefixLen = Path.Length + 1;
        foreach (TopicInfo info in Instance.GetTopicInfo(m_pathWithSep, types))
        {
            ReadOnlySpan<char> relativeKey = info.Name.AsSpan()[prefixLen..];
            if (relativeKey.IndexOf(PathSeparator) != -1)
            {
                continue;
            }
            topics.Add(info.GetTopic(Instance));
        }
        return topics;
    }

    public HashSet<string> GetKeys(NetworkTableType types = NetworkTableType.Unassigned)
    {
        HashSet<string> keys = [];
        int prefixLen = Path.Length + 1;
        foreach (TopicInfo info in Instance.GetTopicInfo(m_pathWithSep, types))
        {
            ReadOnlySpan<char> relativeKey = info.Name.AsSpan()[prefixLen..];
            if (relativeKey.IndexOf(PathSeparator) != -1)
            {
                continue;
            }
            keys.Add(relativeKey.ToString());
        }
        return keys;
    }

    public HashSet<string> GetSubTables(NetworkTableType types = NetworkTableType.Unassigned)
    {
        HashSet<string> keys = [];
        int prefixLen = Path.Length + 1;
        foreach (TopicInfo info in Instance.GetTopicInfo(m_pathWithSep, types))
        {
            ReadOnlySpan<char> relativeKey = info.Name.AsSpan()[prefixLen..];
            int endSubTable = relativeKey.IndexOf(PathSeparator);
            if (endSubTable == -1)
            {
                continue;
            }
            keys.Add(relativeKey[..endSubTable].ToString());
        }
        return keys;
    }

    private readonly ConcurrentDictionary<string, IGenericEntry> m_entries = [];

    public IGenericEntry GetEntry(string key)
    {
        IGenericEntry entry = m_entries.GetOrAdd(key, n =>
                {
                    Topic topic = GetTopic(n);
                    return topic.GetGenericEntry(default);
                });
        return entry;
    }

    public bool PutValue(string key, in RefNetworkTableValue value)
    {
        return GetEntry(key).Set(value);
    }

    public bool SetDefaultValue(string key, in RefNetworkTableValue defaultValue)
    {
        return GetEntry(key).SetDefault(defaultValue);
    }

    public NetworkTableValue GetValue(string key)
    {
        return GetEntry(key).Get();
    }

    public string Path { get; }

    public delegate void TableEventListener(NetworkTable table, string key, NetworkTableEvent tableEvent);

    public NtListener AddListener(EventFlags eventKinds, TableEventListener listener)
    {
        int prefixLex = Path.Length + 1;
        return Instance.AddListener([m_pathWithSep], eventKinds, ntEvent =>
        {
            string? topicName = null;
            if (ntEvent.TopicInfo is not null)
            {
                topicName = ntEvent.TopicInfo.Value.Name;
            }
            else if (ntEvent.ValueData is not null)
            {
                // Don't fully construct the lazy object
                topicName = ntEvent.ValueData.Value.GetTopicName();
            }
            if (topicName is null)
            {
                return;
            }
            string relativeKey = topicName[prefixLex..];
            if (relativeKey.Contains(PathSeparator))
            {
                // part of a subtable
                return;
            }
            listener(this, relativeKey, ntEvent);
        });
    }

    public NtListener AddListener(string key, EventFlags eventKinds, TableEventListener listener)
    {
        var entry = GetEntry(key);
        return Instance.AddListener(entry, eventKinds, ntEvent => listener(this, key, ntEvent));
    }

    public delegate void SubTableListener(NetworkTable parent, string name, NetworkTable table);

    private sealed class SubTableListenerHolder(int prefixLen, NetworkTable parent, SubTableListener listener)
    {
        private readonly int m_prefixLen = prefixLen;
        private readonly NetworkTable m_parent = parent;
        private readonly SubTableListener m_listener = listener;
        private readonly HashSet<string> m_notifiedTables = [];

        public void OnEvent(NetworkTableEvent ntEvent)
        {
            if (ntEvent.TopicInfo is null)
            {
                return;
            }
            var relativeKey = ntEvent.TopicInfo.Value.Name.AsSpan()[m_prefixLen..];
            int endSubTable = relativeKey.IndexOf(PathSeparator);
            if (endSubTable == -1)
            {
                return;
            }
            string subTableKey = relativeKey[..endSubTable].ToString();
            if (m_notifiedTables.Contains(subTableKey))
            {
                return;
            }
            m_notifiedTables.Add(subTableKey);
            m_listener(m_parent, subTableKey, m_parent.GetSubTable(subTableKey));
        }
    }

    public NtListener AddSubTableListener(SubTableListener listener)
    {
        int prefixLen = Path.Length + 1;
        SubTableListenerHolder holder = new(prefixLen, this, listener);
        return Instance.AddListener([m_pathWithSep], EventFlags.Publish | EventFlags.Immediate, holder.OnEvent);
    }

    public void RemoveListener(NtListener listener)
    {
        Instance.RemoveListener(listener);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as NetworkTable);
    }

    public bool Equals(NetworkTable? other)
    {
        return other is not null &&
               Instance == other.Instance && Path == other.Path;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Instance, Path);
    }

    public static bool operator ==(NetworkTable? left, NetworkTable? right)
    {
        return EqualityComparer<NetworkTable>.Default.Equals(left, right);
    }

    public static bool operator !=(NetworkTable? left, NetworkTable? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Gets a string topic.
    /// </summary>
    /// <param name="name">topic name</param>
    /// <returns>StringTopic</returns>
    public StringTopic GetStringTopic(string name)
    {
        return Instance.GetStringTopic($"{m_pathWithSep}{name}");
    }

    /// <summary>
    /// Gets a string[] topic.
    /// </summary>
    /// <param name="name">topic name</param>
    /// <returns>StringArrayTopic</returns>
    public StringArrayTopic GetStringArrayTopic(string name)
    {
        return Instance.GetStringArrayTopic($"{m_pathWithSep}{name}");
    }
}
