using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NetworkTables.Handles;
using NetworkTables.Natives;

namespace NetworkTables;

public sealed class NetworkTable : IEquatable<NetworkTable?>
{
    public const char PATH_SEPARATOR = '/';
    private readonly string m_pathWithSep;

    public static ReadOnlySpan<char> BasenameKey(ReadOnlySpan<char> key)
    {
        int slash = key.LastIndexOf(PATH_SEPARATOR);
        if (slash == -1)
        {
            return key;
        }
        return key[(slash + 1)..];
    }

    public static string NormalizeKey(ReadOnlySpan<char> key, bool withLeadingSlash = true)
    {
        throw new NotImplementedException();
    }

    public static List<string> GetHierarchy(ReadOnlySpan<char> key)
    {
        string normal = NormalizeKey(key);
        List<string> hierarchy = [];
        if (normal.Length == 1)
        {
            hierarchy.Add(normal);
            return hierarchy;
        }
        for (int i = 0; ; i = normal.IndexOf(PATH_SEPARATOR, i + 1))
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
        m_pathWithSep = $"{path}{PATH_SEPARATOR}";
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
        Topic[] topics = Instance.GetTopics($"{m_pathWithSep}{key}{PATH_SEPARATOR}");
        return topics.Length != 0;
    }

    public List<TopicInfo> GetTopicInfo(NetworkTableType types = NetworkTableType.Unassigned)
    {
        List<TopicInfo> infos = [];
        int prefixLen = Path.Length + 1;
        foreach (TopicInfo info in Instance.GetTopicInfo(m_pathWithSep, types))
        {
            ReadOnlySpan<char> relativeKey = info.Name.AsSpan()[prefixLen..];
            if (relativeKey.IndexOf(PATH_SEPARATOR) != -1)
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
            if (relativeKey.IndexOf(PATH_SEPARATOR) != -1)
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
            if (relativeKey.IndexOf(PATH_SEPARATOR) != -1)
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
            int endSubTable = relativeKey.IndexOf(PATH_SEPARATOR);
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

    // public NtListener AddListener(EventFlags kinds, Action<NetworkTable, string, NetworkTableEvent> listener) {
    //     int prefixLex = Path.Length + 1;
    //     return Instance.Add
    // }

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
}
