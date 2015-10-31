using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using HAL_Base;
using NetworkTables;
using NetworkTables.Tables;

namespace WPILib
{
    /// <summary>
    /// The preferences class provides a relatively simple way to save important
    /// values to the RoboRIO to access the next time the RoboRIO is booted.
    /// </summary>
    /// <remarks>This class loads and saves from a file inside the RoboRIO. The user can not
    /// access the file directly, but may modify values at specific fields which will
    /// then be automatically saved to the file by the NetworkTables server.
    /// <para/>This class is thread safe.
    /// <para/>This will also interact with <see cref="NetworkTable"/> by creating a table called
    /// "Preferences" with all the key-value pairs.</remarks>
    public class Preferences : ITableListener
    {
        private const string TableName = "Preferences";

        private static Preferences s_instance;

        private static readonly object s_lockObject = new object();

        /// <summary>
        /// Returns the preferences instance
        /// </summary>
        public static Preferences Instance
        {
            get
            {
                lock (s_lockObject)
                {
                    return s_instance ?? (s_instance = new Preferences());
                }
            }
        }

        private NetworkTable m_table;

        /// <inheritdoc/>
        public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
        {
            m_table.SetPersistent(key);
        }

        private Preferences()
        {
            m_table = NetworkTable.GetTable(TableName);
            m_table.AddTableListenerEx(this, NotifyFlags.NotifyNew | NotifyFlags.NotifyImmediate);
            HAL.Report(ResourceType.kResourceType_Preferences, (byte)0);
        }

        /// <summary>
        /// Gets a  list of the keys.
        /// </summary>
        public List<string> GetKeys => m_table.GetKeys().ToList();

        public void PutString(string key, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            m_table.PutString(key, value);
            m_table.SetPersistent(key);
        }

        public void PutDouble(string key, double value)
        {
            m_table.PutNumber(key, value);
            m_table.SetPersistent(key);
        }

        public void PutBoolean(string key, bool value)
        {
            m_table.PutBoolean(key, value);
            m_table.SetPersistent(key);
        }

        public bool ContainsKey(string key)
        {
            return m_table.ContainsKey(key);
        }

        public void Remove(string key)
        {
            m_table.Delete(key);
        }

        public string GetString(string key, string backup)
        {
            return m_table.GetString(key, backup);
        }

        public double GetDouble(string key, double backup)
        {
            return m_table.GetNumber(key, backup);
        }

        public bool GetBoolean(string key, bool backup)
        {
            return m_table.GetBoolean(key, backup);
        }
    }
}
