using NetworkTables;
using System;
using WPILib.SmartDashboard;

namespace WPILib.LiveWindow
{
    public static class LiveWindow
    {
        private class Component
        {
            public bool FirstTime { get; set; }
            public bool TelemetryEnabled { get; set; }
        }

        private static readonly SendableRegistry registry = SendableRegistry.Instance;
        private static readonly int dataHandle = registry.DataHandle;

        private static readonly NetworkTable liveWindowTable = NetworkTableInstance.Default.GetTable("LiveWindow");
        private static readonly NetworkTable statusTable = liveWindowTable.GetSubTable(".status");
        private static readonly NetworkTableEntry enabledEntry = statusTable.GetEntry("LW Enabled");
        private static readonly object m_lockObject = new object();

        private static bool startLiveWindow;
        private static bool liveWindowEnabled;
        //private static bool telemetryEnabled;

        private static Action? enabledListener;
        private static Action? disabledListener;

        private static Component GetOrAdd(ISendable sendable)
        {
            Component? data = (Component?)registry.GetData(sendable, dataHandle);
            if (data == null)
            {
                data = new Component();
                registry.SetData(sendable, dataHandle, data);
            }
            return data;
        }

        public static Action? EnabledListener
        {
            get
            {
                lock (m_lockObject)
                {
                    return enabledListener;
                }
            }

            set
            {
                lock (m_lockObject)
                {
                    enabledListener = value;
                }
            }
        }

        public static Action? DisabledListener
        {
            get
            {
                lock (m_lockObject)
                {
                    return disabledListener;
                }
            }

            set
            {
                lock (m_lockObject)
                {
                    disabledListener = value;
                }
            }
        }

        public static bool Enabled
        {
            get
            {
                lock (m_lockObject)
                {
                    return liveWindowEnabled;
                }
            }
            set
            {
                lock (m_lockObject)
                {
                    if (liveWindowEnabled != value)
                    {
                        startLiveWindow = value;
                        liveWindowEnabled = value;
                        UpdateValues();
                        if (value)
                        {
                            Console.WriteLine("Starting live window mode.");
                            enabledListener?.Invoke();
                        }
                        else
                        {
                            Console.WriteLine("Stopping live window mode.");
                        }
                    }
                }
            }
        }

        private static void UpdateValues()
        {

        }
    }
}
