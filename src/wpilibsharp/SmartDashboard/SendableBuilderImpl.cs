using NetworkTables;
using NetworkTables.Natives;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WPILib.SmartDashboard
{
    public class SendableBuilderImpl : ISendableBuilder
    {
        private class Property : IDisposable
        {
            public NetworkTableEntry Entry { get; }
            public NtEntryListener Listener { get; set; }
            public Action<NetworkTableEntry>? Update { get; set; }
            public Func<NetworkTableEntry, NtEntryListener>? CreateListener { get; set; }

            public Property(NetworkTable table, string key)
            {
                Entry = table.GetEntry(key);
                Listener = new NtEntryListener(0);
                Update = null;
                CreateListener = null;
            }

            public void StartListener()
            {
                if (Entry.IsValid && Listener.Get() == 0 && CreateListener != null)
                {
                    Listener = CreateListener(Entry);
                }
            }

            public void StopListener()
            {
                if (Entry.IsValid && Listener.Get() != 0)
                {
                    Entry.RemoveListener(Listener);
                    Listener = new NtEntryListener(0);
                }
            }

            public void Dispose()
            {
                StopListener();
            }
        }

        private readonly List<Property> m_properties = new List<Property>();
        private readonly List<Action> m_updateTables = new List<Action>();
        private bool m_isActuator;

        private NetworkTableEntry m_controllableEntry;

        private NetworkTable? m_table;

        public string SmartDashboardType
        {
            set => m_table!.GetEntry(".type").SetString(value);
        }
        public bool IsActuator
        {
            get => m_isActuator;
            set
            {
                m_table!.GetEntry(".actuator").SetBoolean(value);
                m_isActuator = value;
            }
        }
        [DisallowNull]
        public Action? SafeState { private get; set; }
        [DisallowNull]
        public Action UpdateTable
        {
            set
            {
                m_updateTables.Add(value);
            }
        }


        [DisallowNull]
        internal NetworkTable? Table
        {
            get => m_table;
            set
            {
                m_table = value;
                m_controllableEntry = value!.GetEntry(".controllable");
            }
        }

        public bool HasTable => m_table != null;

        public void TriggerUpdateTable()
        {
            foreach (var property in m_properties)
            {
                property.Update?.Invoke(property.Entry);
            }
            foreach (var updateTable in m_updateTables)
            {
                updateTable();
            }
        }

        public void StartListeners()
        {
            foreach (var property in m_properties)
            {
                property.StartListener();
            }
            if (m_controllableEntry.IsValid)
            {
                m_controllableEntry.SetBoolean(true);
            }
        }

        public void StopListeners()
        {
            foreach (var property in m_properties)
            {
                property.StopListener();
            }
            if (m_controllableEntry.IsValid)
            {
                m_controllableEntry.SetBoolean(false);
            }
        }

        public void StartLiveWindowMode()
        {
            SafeState?.Invoke();
            StartListeners();
        }

        public void StopLiveWindowMode()
        {
            StopListeners();
            SafeState?.Invoke();
        }

        public void ClearProperties()
        {
            StopListeners();
            foreach (var property in m_properties)
            {
                property.Dispose();
            }
            m_properties.Clear();
        }

        public void AddBooleanArrayProperty(string key, Func<bool[]>? getter, Action<bool[]>? setter)
        {
            var property = new Property(m_table!, key);
            if (getter != null)
            {
                property.Update = entry => entry.SetBooleanArray(getter());
            }
            if (setter != null)
            {
                property.CreateListener = entry => entry.AddListener((in RefEntryNotification evnt) =>
                {
                    if (evnt.Value.IsStringArray())
                    {
                        bool[] v = evnt.Value.GetBooleanArray().ToArray();
                        SmartDashboard.PostListenerTask(() => setter(v));
                    }
                }, NotifyFlags.Immediate | NotifyFlags.New | NotifyFlags.Update);
            }
            m_properties.Add(property);
        }

        public void AddBooleanProperty(string key, Func<bool>? getter, Action<bool>? setter)
        {
            var property = new Property(m_table!, key);
            if (getter != null)
            {
                property.Update = entry => entry.SetBoolean(getter());
            }
            if (setter != null)
            {
                property.CreateListener = entry => entry.AddListener((in RefEntryNotification evnt) =>
                {
                    if (evnt.Value.IsBoolean())
                    {
                        bool v = evnt.Value.GetBoolean();
                        SmartDashboard.PostListenerTask(() => setter(v));
                    }
                }, NotifyFlags.Immediate | NotifyFlags.New | NotifyFlags.Update);
            }
            m_properties.Add(property);
        }

        public void AddDoubleArrayProperty(string key, Func<double[]>? getter, Action<double[]>? setter)
        {
            var property = new Property(m_table!, key);
            if (getter != null)
            {
                property.Update = entry => entry.SetDoubleArray(getter());
            }
            if (setter != null)
            {
                property.CreateListener = entry => entry.AddListener((in RefEntryNotification evnt) =>
                {
                    if (evnt.Value.IsStringArray())
                    {
                        double[] v = evnt.Value.GetDoubleArray().ToArray();
                        SmartDashboard.PostListenerTask(() => setter(v));
                    }
                }, NotifyFlags.Immediate | NotifyFlags.New | NotifyFlags.Update);
            }
            m_properties.Add(property);
        }

        public void AddDoubleProperty(string key, Func<double>? getter, Action<double>? setter)
        {
            var property = new Property(m_table!, key);
            if (getter != null)
            {
                property.Update = entry => entry.SetDouble(getter());
            }
            if (setter != null)
            {
                property.CreateListener = entry => entry.AddListener((in RefEntryNotification evnt) =>
                {
                    if (evnt.Value.IsDouble())
                    {
                        double v = evnt.Value.GetDouble();
                        SmartDashboard.PostListenerTask(() => setter(v));
                    }
                }, NotifyFlags.Immediate | NotifyFlags.New | NotifyFlags.Update);
            }
            m_properties.Add(property);
        }

        public void AddRawProperty(string key, Func<byte[]>? getter, Action<byte[]>? setter)
        {
            var property = new Property(m_table!, key);
            if (getter != null)
            {
                property.Update = entry => entry.SetRaw(getter());
            }
            if (setter != null)
            {
                property.CreateListener = entry => entry.AddListener((in RefEntryNotification evnt) =>
                {
                    if (evnt.Value.IsStringArray())
                    {
                        byte[] v = evnt.Value.GetRaw().ToArray();
                        SmartDashboard.PostListenerTask(() => setter(v));
                    }
                }, NotifyFlags.Immediate | NotifyFlags.New | NotifyFlags.Update);
            }
            m_properties.Add(property);
        }

        public void AddStringArrayProperty(string key, Func<string[]>? getter, Action<string[]>? setter)
        {
            var property = new Property(m_table!, key);
            if (getter != null)
            {
                property.Update = entry => entry.SetStringArray(getter());
            }
            if (setter != null)
            {
                property.CreateListener = entry => entry.AddListener((in RefEntryNotification evnt) =>
                {
                    if (evnt.Value.IsStringArray())
                    {
                        string[] v = evnt.Value.GetStringArray().ToArray();
                        SmartDashboard.PostListenerTask(() => setter(v));
                    }
                }, NotifyFlags.Immediate | NotifyFlags.New | NotifyFlags.Update);
            }
            m_properties.Add(property);
        }

        public void AddStringProperty(string key, Func<string>? getter, Action<string>? setter)
        {
            var property = new Property(m_table!, key);
            if (getter != null)
            {
                property.Update = entry => entry.SetString(getter());
            }
            if (setter != null)
            {
                property.CreateListener = entry => entry.AddListener((in RefEntryNotification evnt) =>
                {
                    if (evnt.Value.IsString())
                    {
                        string v = evnt.Value.GetString().ToString();
                        SmartDashboard.PostListenerTask(() => setter(v));
                    }
                }, NotifyFlags.Immediate | NotifyFlags.New | NotifyFlags.Update);
            }
            m_properties.Add(property);
        }

        public void AddValueProperty(string key, Func<NetworkTableValue>? getter, Action<NetworkTableValue>? setter)
        {
            var property = new Property(m_table!, key);
            if (getter != null)
            {
                property.Update = entry => entry.SetValue(getter());
            }
            if (setter != null)
            {
                property.CreateListener = entry => entry.AddListener((in RefEntryNotification evnt) =>
                {
                    var v = evnt.Value.ToValue();
                    SmartDashboard.PostListenerTask(() => setter(v));
                }, NotifyFlags.Immediate | NotifyFlags.New | NotifyFlags.Update);
            }
            m_properties.Add(property);
        }

        public NetworkTableEntry GetEntry(string key)
        {
            return m_table!.GetEntry(key);
        }
    }
}
