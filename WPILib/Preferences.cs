using System;
using System.Collections.Generic;
using System.Linq;
using HAL.Base;
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

        private readonly NetworkTable m_table;

        /// <inheritdoc/>
        public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
        {
            m_table.SetPersistent(key);
        }

        private Preferences()
        {
            m_table = NetworkTable.GetTable(TableName);
            m_table.AddTableListenerEx(this, NotifyFlags.NotifyNew | NotifyFlags.NotifyImmediate);
            HAL.Base.HAL.Report(ResourceType.kResourceType_Preferences, 0);
        }

        /// <summary>
        /// Gets a  list of the keys.
        /// </summary>
        public List<string> GetKeys => m_table.GetKeys().ToList();

        /// <summary>
        /// Puts the given string into the preferences table.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <exception cref="ArgumentNullException">Thrown if value is null.</exception>
        public void PutString(string key, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            m_table.PutString(key, value);
            m_table.SetPersistent(key);
        }

        /// <summary>
        /// Puts the given double into the preferences table.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void PutDouble(string key, double value)
        {
            m_table.PutNumber(key, value);
            m_table.SetPersistent(key);
        }

        /// <summary>
        /// Puts the given int into the preferences table.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void PutInt(string key, int value)
        {
            m_table.PutNumber(key, value);
            m_table.SetPersistent(key);
        }

        /// <summary>
        /// Puts the given long into the preferences table.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void PutLong(string key, long value)
        {
            m_table.PutNumber(key, value);
            m_table.SetPersistent(key);
        }

        /// <summary>
        /// Puts the given float into the preferences table.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void PutFloat(string key, float value)
        {
            m_table.PutNumber(key, value);
            m_table.SetPersistent(key);
        }

        /// <summary>
        /// Puts the given boolean into the preferences table.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void PutBoolean(string key, bool value)
        {
            m_table.PutBoolean(key, value);
            m_table.SetPersistent(key);
        }

        /// <summary>
        /// Gets whether or not there is a key with the given name.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>True if there is a value at the given key.</returns>
        public bool ContainsKey(string key)
        {
            return m_table.ContainsKey(key);
        }

        /// <summary>
        /// Removes a preference.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            m_table.Delete(key);
        }

        /// <summary>
        /// Returns the string at the given key. 
        /// </summary>
        /// <remarks>
        /// If this table does not have a value for that position, then the given backup value will be returned.
        /// </remarks>
        /// <param name="key">The key.</param>
        /// <param name="backup">The value to return if non exists in the table.</param>
        /// <returns>The value in the table, or the backup if value does not exist in table.</returns>
        public string GetString(string key, string backup)
        {
            return m_table.GetString(key, backup);
        }

        /// <summary>
        /// Returns the double at the given key. 
        /// </summary>
        /// <remarks>
        /// If this table does not have a value for that position, then the given backup value will be returned.
        /// </remarks>
        /// <param name="key">The key.</param>
        /// <param name="backup">The value to return if non exists in the table.</param>
        /// <returns>The value in the table, or the backup if value does not exist in table.</returns>
        public double GetDouble(string key, double backup)
        {
            return m_table.GetNumber(key, backup);
        }

        /// <summary>
        /// Returns the boolean at the given key. 
        /// </summary>
        /// <remarks>
        /// If this table does not have a value for that position, then the given backup value will be returned.
        /// </remarks>
        /// <param name="key">The key.</param>
        /// <param name="backup">The value to return if non exists in the table.</param>
        /// <returns>The value in the table, or the backup if value does not exist in table.</returns>
        public bool GetBoolean(string key, bool backup)
        {
            return m_table.GetBoolean(key, backup);
        }

        /// <summary>
        /// Returns the int at the given key. 
        /// </summary>
        /// <remarks>
        /// If this table does not have a value for that position, then the given backup value will be returned.
        /// </remarks>
        /// <param name="key">The key.</param>
        /// <param name="backup">The value to return if non exists in the table.</param>
        /// <returns>The value in the table, or the backup if value does not exist in table.</returns>
        public int GetInt(string key, int backup)
        {
            return (int)m_table.GetNumber(key, backup);
        }

        /// <summary>
        /// Returns the long at the given key. 
        /// </summary>
        /// <remarks>
        /// If this table does not have a value for that position, then the given backup value will be returned.
        /// </remarks>
        /// <param name="key">The key.</param>
        /// <param name="backup">The value to return if non exists in the table.</param>
        /// <returns>The value in the table, or the backup if value does not exist in table.</returns>
        public long GetLong(string key, long backup)
        {
            return (long)m_table.GetNumber(key, backup);
        }

        /// <summary>
        /// Returns the float at the given key. 
        /// </summary>
        /// <remarks>
        /// If this table does not have a value for that position, then the given backup value will be returned.
        /// </remarks>
        /// <param name="key">The key.</param>
        /// <param name="backup">The value to return if non exists in the table.</param>
        /// <returns>The value in the table, or the backup if value does not exist in table.</returns>
        public float GetFloat(string key, float backup)
        {
            return (float)m_table.GetNumber(key, backup);
        }
    }
}
