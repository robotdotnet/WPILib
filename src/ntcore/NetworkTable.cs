using NetworkTables.Natives;
using System;
using System.Collections.Generic;

namespace NetworkTables
{
    public class NetworkTable : IEquatable<NetworkTable>
    {
        public static readonly char PathSeparator = '/';
        private readonly string m_pathWithSep;

        public static string BasenameKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            int slash = key.LastIndexOf(PathSeparator);
            if (slash == -1)
            {
                return key;
            }
            return key.Substring(slash + 1);
        }

        internal NetworkTable(NetworkTableInstance inst, ReadOnlySpan<char> path)
        {
            var str = path.ToString();
            Path = str;
            m_pathWithSep = str + PathSeparator;
            Instance = inst;
        }

        public NetworkTableInstance Instance { get; }

        public override string ToString()
        {
            return $"NetworkTable: {Path}";
        }

        public NetworkTableEntry GetEntry(string key)
        {
            return Instance.GetEntry(m_pathWithSep + key);
        }

        public NetworkTableEntry GetEntry(ReadOnlySpan<char> key)
        {
            return Instance.GetEntry(m_pathWithSep + key.ToString());
        }

        public NtEntryListener AddEntryListener(TableEntryListener listener, NotifyFlags flags)
        {
            int prefixLen = Path.Length + 1;
            return Instance.AddEntryListener(m_pathWithSep, (in RefEntryNotification evnt) =>
            {
                ReadOnlySpan<char> relativeKey = evnt.Name.AsSpan().Slice(prefixLen);
                if (relativeKey.IndexOf(PathSeparator) != -1)
                {
                    return;
                }
                listener(this, relativeKey, evnt.Entry, evnt.Value, evnt.Flags);
            }, flags);
        }

        public NtEntryListener AddEntryListener(ReadOnlySpan<char> key, TableEntryListener listener, NotifyFlags flags)
        {
            var entry = GetEntry(key);
            int prefixLen = Path.Length + 1;
            return Instance.AddEntryListener(entry, (in RefEntryNotification evnt) =>
            {
                ReadOnlySpan<char> relativeKey = evnt.Name.AsSpan().Slice(prefixLen);
                listener(this, relativeKey, evnt.Entry, evnt.Value, evnt.Flags);
            }, flags);
        }

        public NtEntryListener AddEntryListener(string key, TableEntryListener listener, NotifyFlags flags)
        {
            var entry = GetEntry(key);
            return Instance.AddEntryListener(entry, (in RefEntryNotification evnt) =>
            {
                listener(this, key.AsSpan(), evnt.Entry, evnt.Value, evnt.Flags);
            }, flags);
        }

        public void RemoveEntryListener(NtEntryListener listener)
        {
            Instance.RemoveEntryListener(listener);
        }

        public NtEntryListener AddSubTableListener(TableListener listener, bool localNotify)
        {
            NotifyFlags flags = NotifyFlags.New | NotifyFlags.Immediate;
            if (localNotify)
            {
                flags |= NotifyFlags.Local;
            }

            int prefixLen = Path.Length + 1;
            HashSet<string> notifiedTable = new HashSet<string>();

            return Instance.AddEntryListener(m_pathWithSep, (in RefEntryNotification evnt) =>
            {
                ReadOnlySpan<char> relativeKey = evnt.Name.AsSpan().Slice(prefixLen);
                int endSubTable = relativeKey.IndexOf(PathSeparator);
                if (endSubTable == -1)
                {
                    return;
                }
                relativeKey.Slice(0, endSubTable).ToString();
                ReadOnlySpan<char> subTableKeySpan = relativeKey.Slice(0, endSubTable);
                string subTableKeyStr = subTableKeySpan.ToString();
                if (notifiedTable.Contains(subTableKeyStr))
                {
                    return;
                }
                notifiedTable.Add(subTableKeyStr);
                listener(this, subTableKeySpan, evnt.Flags);
            }, flags);
        }

        public void RemoveTableListener(NtEntryListener listener)
        {
            Instance.RemoveEntryListener(listener);
        }

        public NetworkTable GetSubTable(string key)
        {
            return new NetworkTable(Instance, (m_pathWithSep + key).AsSpan());
        }

        public NetworkTable GetSubTable(ReadOnlySpan<char> key)
        {
            return new NetworkTable(Instance, (m_pathWithSep + key.ToString()).AsSpan());
        }

        public bool ContainsKey(string key)
        {
            return !(string.IsNullOrWhiteSpace(key)) && GetEntry(key).Exists();
        }

        public bool ContainsKey(ReadOnlySpan<char> key)
        {
            return !key.IsEmpty && GetEntry(key).Exists();
        }

        public bool ContainsSubTable(string key)
        {
            return NtCore.GetEntryCount(Instance.Handle, m_pathWithSep + key + PathSeparator, 0) != 0;
        }

        public bool ContainsSubTable(ReadOnlySpan<char> key)
        {
            return NtCore.GetEntryCount(Instance.Handle, m_pathWithSep + key.ToString() + PathSeparator, 0) != 0;
        }


        public HashSet<string> GetKeys(NtType types)
        {
            HashSet<string> keys = new HashSet<string>();
            int prefixLen = -Path.Length + 1;
            foreach (var info in Instance.GetEntryInfo(m_pathWithSep, types))
            {
                var relativeKey = info.Name.AsSpan().Slice(prefixLen);
                if (relativeKey.IndexOf(PathSeparator) != -1)
                {
                    continue;
                }
                string rKey = relativeKey.ToString();
                keys.Add(rKey);
            }
            return keys;
        }

        public HashSet<string> GetKeys()
        {
            return GetKeys(0);
        }

        public HashSet<string> GetSubTables()
        {
            HashSet<string> keys = new HashSet<string>();
            int prefixLen = Path.Length + 1;
            foreach (EntryInfo info in Instance.GetEntryInfo(m_pathWithSep, 0))
            {
                var relativeKey = info.Name.AsSpan().Slice(prefixLen);
                int endSubTable = relativeKey.IndexOf(PathSeparator);
                if (endSubTable == -1)
                {
                    continue;
                }
                keys.Add(relativeKey.Slice(0, endSubTable).ToString());
            }
            return keys;
        }

        public void Delete(string key)
        {
            GetEntry(key).Delete();
        }

        public void Delete(ReadOnlySpan<char> key)
        {
            GetEntry(key).Delete();
        }

        internal NetworkTableValue GetValue(string key)
        {
            return GetEntry(key).GetValue();
        }

        public string Path { get; }

        public void SaveEntries(string filename)
        {
            Instance.SaveEntries(filename, m_pathWithSep);
        }

        public List<string> LoadEntries(string filename)
        {
            return Instance.LoadEntries(filename, m_pathWithSep);
        }

        public bool Equals(NetworkTable? other)
        {
            if (ReferenceEquals(other, null)) return false;
            return Path.Equals(other.Path, StringComparison.CurrentCulture) && Instance.Equals(other.Instance);
        }

        public override bool Equals(object? obj)
        {
            if (obj is NetworkTable v)
            {
                return Equals(v);
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + string.GetHashCode(Path, StringComparison.InvariantCultureIgnoreCase);
                hash = (hash * 7) + Instance.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(NetworkTable? lhs, NetworkTable? rhs)
        {
            if (lhs == null || rhs == null) return false;
            return lhs.Equals(rhs);
        }

        public static bool operator !=(NetworkTable? lhs, NetworkTable? rhs)
        {
            if (lhs == null || rhs == null) return true;
            return !lhs.Equals(rhs);
        }

    }
}
