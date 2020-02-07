using System;
using System.Collections.Generic;
using System.Text;
using NetworkTables;

namespace WPILib.SmartDashboard
{
    public static class SmartDashboard
    {
        private static readonly NetworkTable table = NetworkTableInstance.Default.GetTable("SmartDashboard");

        public static NetworkTableEntry GetEntry(string key) => table.GetEntry(key);

        public static bool ContainsKey(string key) => table.ContainsKey(key);

        public static HashSet<string> GetKeys(NtType types) => table.GetKeys(types);

        public static HashSet<string> GetKeys() => table.GetKeys();

        public static void SetPersistent(string key) => GetEntry(key).SetPersistent();

        public static bool IsPersistent(string key) => GetEntry(key).IsPersistent();

        public static bool PutNumber(string key, double value) => GetEntry(key).SetDouble(value);
    }
}
