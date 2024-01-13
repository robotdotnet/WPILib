using System;
using System.Collections.Generic;
using NetworkTables.Natives;

namespace NetworkTables;

public sealed class NetworkTable
{
    public const char PATH_SEPARATOR = '/';

    private readonly string m_path;
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
        m_path = path;
        m_pathWithSep = $"{path}{PATH_SEPARATOR}";
        Instance = inst;
    }

    public NetworkTableInstance Instance { get; }

    public override string ToString()
    {
        return $"NetworkTable: {m_path}";
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
        int prefixLen = m_path.Length + 1;
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
        int prefixLen = m_path.Length + 1;
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
        int prefixLen = m_path.Length + 1;
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
}
