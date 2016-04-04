using System;
using System.Collections.Generic;
using HAL.Base;
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
        private static readonly NetworkTable s_table = NetworkTable.GetTable("SmartDashboard");
        private static readonly Dictionary<ITable, ISendable> s_tablesToData = new Dictionary<ITable, ISendable>();

        static SmartDashboard()
        {
            HAL.Base.HAL.Report(ResourceType.kResourceType_SmartDashboard, (byte)0);
        }

        /// <summary>
        /// Maps the specified key to the specified value in this table.
        /// </summary>
        /// <remarks>The key cannot be null.</remarks>
        /// <param name="key">The key to map the <see cref="ISendable"/> to.</param>
        /// <param name="data">The <see cref="ISendable"/> to map.</param>
        public static void PutData(string key, ISendable data)
        {
            ITable dataTable = s_table.GetSubTable(key);
            dataTable.PutString("~TYPE~", data.SmartDashboardType);
            data.InitTable(dataTable);
            s_tablesToData.Add(dataTable, data);
        }

        /// <summary>
        /// Maps the specified key to the specified value in this table.
        /// </summary>
        /// <remarks>The key cannot be null.</remarks>
        /// <param name="value">The <see cref="INamedSendable"/> to map.</param>
        public static void PutData(INamedSendable value)
        {
            PutData(value.Name, value);
        }

        /// <summary>
        /// Returns the <see cref="ISendable"/> at the specified key.
        /// </summary>
        /// <param name="key">The key to return.</param>
        /// <returns>The <see cref="ISendable"/> mapped to the key.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if there is no value mapped to the key.</exception>
        public static ISendable GetData(string key)
        {
            ITable subtable = s_table.GetSubTable(key);

            ISendable value;
            var valid = s_tablesToData.TryGetValue(subtable, out value);
            if (valid) return value;
            throw new ArgumentOutOfRangeException(nameof(key), "SmartDashboard data does not exist: " + key);
        }

        /// <inheritdoc cref="ITable.PutBoolean"/>
        public static void PutBoolean(string key, bool value)
        {
            s_table.PutBoolean(key, value);
        }

        /// <inheritdoc cref="ITable.GetBoolean(string)"/>
        [Obsolete("Please use the Default Value Get... Method instead.")]
        public static bool GetBoolean(string key)
        {
            return s_table.GetBoolean(key);
        }

        /// <inheritdoc cref="ITable.GetBoolean(string, bool)"/>
        public static bool GetBoolean(string key, bool defaultValue)
        {
            return s_table.GetBoolean(key, defaultValue);
        }

        /// <inheritdoc cref="ITable.PutString(string, string)"/>
        public static void PutString(string key, string value)
        {
            s_table.PutString(key, value);
        }

        /// <inheritdoc cref="ITable.GetString(string)"/>
        [Obsolete("Please use the Default Value Get... Method instead.")]
        public static string GetString(string key)
        {
            return s_table.GetString(key);
        }

        /// <inheritdoc cref="ITable.GetString(string, string)"/>
        public static string GetString(string key, string defaultValue)
        {
            return s_table.GetString(key, defaultValue);
        }

        /// <inheritdoc cref="ITable.PutNumber(string, double)"/>
        public static void PutNumber(string key, double value)
        {
            s_table.PutNumber(key, value);
        }

        /// <inheritdoc cref="ITable.GetNumber(string)"/>
        [Obsolete("Please use the Default Value Get... Method instead.")]
        public static double GetNumber(string key)
        {
            return s_table.GetNumber(key);
        }

        /// <inheritdoc cref="ITable.GetNumber(string, double)"/>
        public static double GetNumber(string key, double defaultValue)
        {
            return s_table.GetNumber(key, defaultValue);
        }

        /// <inheritdoc cref="ITable.PutBooleanArray(string, bool[])"/>
        public static void PutBooleanArray(string key, bool[] value)
        {
            s_table.PutBooleanArray(key, value);
        }

        /// <inheritdoc cref="ITable.GetBooleanArray(string)"/>
        [Obsolete("Please use the Default Value Get... Method instead.")]
        public static bool[] GetBooleanArray(string key)
        {
            return s_table.GetBooleanArray(key);
        }

        /// <inheritdoc cref="ITable.GetBooleanArray(string, bool[])"/>
        public static bool[] GetBooleanArray(string key, bool[] defaultValue)
        {
            return s_table.GetBooleanArray(key, defaultValue);
        }

        /// <inheritdoc cref="ITable.PutStringArray"/>
        public static void PutStringArray(string key, string[] value)
        {
            s_table.PutStringArray(key, value);
        }

        /// <inheritdoc cref="ITable.GetStringArray(string)"/>
        [Obsolete("Please use the Default Value Get... Method instead.")]
        public static string[] GetStringArray(string key)
        {
            return s_table.GetStringArray(key);
        }

        /// <inheritdoc cref="ITable.GetStringArray(string, string[])"/>
        public static string[] GetStringArray(string key, string[] defaultValue)
        {
            return s_table.GetStringArray(key, defaultValue);
        }

        /// <inheritdoc cref="ITable.PutNumberArray"/>
        public static void PutNumberArray(string key, double[] value)
        {
            s_table.PutNumberArray(key, value);
        }

        /// <inheritdoc cref="ITable.GetNumberArray(string)"/>
        [Obsolete("Please use the Default Value Get... Method instead.")]
        public static double[] GetNumberArray(string key)
        {
            return s_table.GetNumberArray(key);
        }

        /// <inheritdoc cref="ITable.GetNumberArray(string, double[])"/>
        public static double[] GetNumberArray(string key, double[] defaultValue)
        {
            return s_table.GetNumberArray(key, defaultValue);
        }
    }
}
