using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NetworkTables;

namespace WPILib.SmartDashboardNS
{
    public sealed class SendableChooser<T> : ISendable, IDisposable
    {
        private const string DEFAULT = "default";
        private const string SELECTED = "selected";
        private const string ACTIVE = "active";
        private const string OPTIONS = "options";
        private const string INSTANCE = "instance";

        private readonly Dictionary<string, T> m_map = new Dictionary<string, T>();
        private string m_defaultChoice = "";
        private readonly int m_instance;
        private static int s_instances = 0;

        private string? m_selected;
        private readonly List<NetworkTableEntry> m_activeEntries = new List<NetworkTableEntry>();
        private readonly object m_mutex = new object();

        public SendableChooser()
        {
            m_instance = Interlocked.Increment(ref s_instances);
            SendableRegistry.Instance.Add(this, "SendableChooser", m_instance);
        }

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
        }

        public void AddOption(string name, T option)
        {
            m_map.Add(name, option);
        }

        public void SetDefaultOption(string name, T option)
        {
            m_defaultChoice = name;
            AddOption(name, option);
        }

        public T Selected
        {
            get
            {
                lock (m_mutex)
                {
                    if (m_selected != null)
                    {
                        return m_map[m_selected];
                    }
                    else
                    {
                        return m_map[m_defaultChoice];
                    }
                }
            }
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "String Chooser";
            builder.GetEntry(INSTANCE).SetDouble(m_instance);
            builder.AddStringProperty(DEFAULT, () => m_defaultChoice, null);
            builder.AddStringArrayProperty(OPTIONS, () => m_map.Keys.ToArray(), null);
            builder.AddStringProperty(ACTIVE, () =>
            {
                lock (m_mutex)
                {
                    if (m_selected != null)
                    {
                        return m_selected;
                    }
                    else
                    {
                        return m_defaultChoice;
                    }
                }
            }, null);
            lock (m_mutex)
            {
                m_activeEntries.Add(builder.GetEntry(ACTIVE));
            }
            builder.AddStringProperty(SELECTED, null, val =>
            {
                lock (m_mutex)
                {
                    m_selected = val;
                    foreach (var entry in m_activeEntries)
                    {
                        entry.SetString(val);
                    }
                }
            });
        }
    }
}
