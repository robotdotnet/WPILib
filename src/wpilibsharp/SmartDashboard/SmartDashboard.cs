using System;
using System.Collections.Generic;
using System.Text;
using Hal;
using NetworkTables;

namespace WPILib.SmartDashboard
{
    public static class SmartDashboard
    {
        private static readonly NetworkTable table = NetworkTableInstance.Default.GetTable("SmartDashboard");

        static SmartDashboard()
        {
            UsageReporting.Report(ResourceType.SmartDashboard, 0);
        }

        private static readonly ListenerExecutor listenerExecutor = new ListenerExecutor();

        private static readonly Dictionary<string, ISendable> m_tablesToData = new Dictionary<string, ISendable>();

        public static NetworkTableEntry GetEntry(string key) => table.GetEntry(key);

        public static bool ContainsKey(string key) => table.ContainsKey(key);

        public static HashSet<string> GetKeys(NtType types) => table.GetKeys(types);

        public static HashSet<string> GetKeys() => table.GetKeys();

        public static void SetPersistent(string key) => GetEntry(key).SetPersistent();

        public static bool IsPersistent(string key) => GetEntry(key).IsPersistent();

        public static bool PutNumber(string key, double value) => GetEntry(key).SetDouble(value);

        public static void PostListenerTask(Action task)
        {
            listenerExecutor.Execute(task);
        }

        public static void UpdateValues()
        {
            var inst = SendableRegistry.Instance;
            foreach (var data in m_tablesToData.Values)
            {
                inst.Update(data);
            }
            listenerExecutor.RunListenerTasks();
        }
    }
}
