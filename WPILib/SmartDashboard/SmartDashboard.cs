using System;
using System.Collections.Generic;
using NetworkTables;
using NetworkTables.Tables;
using WPILib.Interfaces;

namespace WPILib.SmartDashboard
{
    /// <summary>
    /// The <see cref="SmartDashboard"/> class is the bridge between robot programs and the SmartDashboard on the laptop.
    /// </summary>
    /// <remarks>When a value is put into the SmartDashboard here, it pops up on the SmartDashboard on the laptop.
    /// Users can put values into and get values from the SmartDashboard</remarks>
    public class SmartDashboard
    {
        private static NetworkTable s_table = NetworkTable.GetTable("SmartDashboard");
        private static Dictionary<ITable, ISendable> s_tablesToData = new Dictionary<ITable, ISendable>();

        static SmartDashboard()
        {
            HLUsageReporting.ReportSmartDashboard();
        }

        /// <summary>
        /// Maps the specified key to the specified value in this table.
        /// </summary>
        /// <remarks>The key cannot be null.</remarks>
        /// <param name="key">The Key</param>
        /// <param name="data">The Value</param>
        /// <exception cref="ArgumentNullException">If key is null.</exception>
        public static void PutData(string key, ISendable data)
        {
            ITable dataTable = s_table.GetSubTable(key);
            dataTable.PutString("~TYPE~", data.SmartDashboardType);
            data.InitTable(dataTable);
            s_tablesToData.Add(dataTable, data);
        }

        public static void PutData(INamedSendable value)
        {
            PutData(value.Name, value);
        }

        public static ISendable GetData(string key)
        {
            ITable subtable = s_table.GetSubTable(key);

            ISendable value;
            var valid = s_tablesToData.TryGetValue(subtable, out value);
            if (valid) return value;
            throw new ArgumentOutOfRangeException(nameof(key), "SmartDashboard data does not exist: " + key);
        }

        public static void PutBoolean(string key, bool value)
        {
            s_table.PutBoolean(key, value);
        }

        [Obsolete("Please use the Default Value Get... Method instead.")]
        public static bool GetBoolean(string key)
        {
            return s_table.GetBoolean(key);
        }

        public static bool GetBoolean(string key, bool defaultValue)
        {
            return s_table.GetBoolean(key, defaultValue);
        }

        public static void PutString(string key, string value)
        {
            s_table.PutString(key, value);
        }

        [Obsolete("Please use the Default Value Get... Method instead.")]
        public static string GetString(string key)
        {
            return s_table.GetString(key);
        }

        public static string GetString(string key, string defaultValue)
        {
            return s_table.GetString(key, defaultValue);
        }

        public static void PutNumber(string key, double value)
        {
            s_table.PutNumber(key, value);
        }

        [Obsolete("Please use the Default Value Get... Method instead.")]
        public static double GetNumber(string key)
        {
            return s_table.GetNumber(key);
        }

        public static double GetNumber(string key, double defaultValue)
        {
            return s_table.GetNumber(key, defaultValue);
        }

        public static void PutBooleanArray(string key, bool[] value)
        {
            s_table.PutBooleanArray(key, value);
        }

        [Obsolete("Please use the Default Value Get... Method instead.")]
        public static bool[] GetBooleanArray(string key)
        {
            return s_table.GetBooleanArray(key);
        }

        public static bool[] GetBooleanArray(string key, bool[] defaultValue)
        {
            return s_table.GetBooleanArray(key, defaultValue);
        }

        public static void PutStringArray(string key, string[] value)
        {
            s_table.PutStringArray(key, value);
        }

        [Obsolete("Please use the Default Value Get... Method instead.")]
        public static string[] GetStringArray(string key)
        {
            return s_table.GetStringArray(key);
        }

        public static string[] GetStringArray(string key, string[] defaultValue)
        {
            return s_table.GetStringArray(key, defaultValue);
        }

        public static void PutNumberArray(string key, double[] value)
        {
            s_table.PutNumberArray(key, value);
        }

        [Obsolete("Please use the Default Value Get... Method instead.")]
        public static double[] GetNumberArray(string key)
        {
            return s_table.GetNumberArray(key);
        }

        public static double[] GetNumberArray(string key, double[] defaultValue)
        {
            return s_table.GetNumberArray(key, defaultValue);
        }
    }
}
