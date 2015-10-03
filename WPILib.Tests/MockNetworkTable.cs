using System;
using System.Collections.Generic;
using NetworkTables;
using NetworkTables.Tables;

namespace WPILib.Tests
{
    class MockNetworkTable : ITable
    {
        public int ContainsKeyCount = 0;
        public bool ContainsKey(string key)
        {
            ContainsKeyCount++;
            return true;
        }
        public int ContainsSubTableCount = 0;
        public bool ContainsSubTable(string key)
        {
            ContainsSubTableCount++;
            return true;
        }
        public int GetSubTableCount = 0;
        public ITable GetSubTable(string key)
        {
            GetSubTableCount++;
            return this;
        }

        public HashSet<string> GetKeys(int types)
        {
            throw new NotImplementedException();
        }

        public HashSet<string> GetKeys()
        {
            throw new NotImplementedException();
        }

        public HashSet<string> GetSubTables()
        {
            throw new NotImplementedException();
        }

        public void SetPersistent(string key)
        {
            throw new NotImplementedException();
        }

        public void ClearPersistent(string key)
        {
            throw new NotImplementedException();
        }

        public bool IsPersistent(string key)
        {
            throw new NotImplementedException();
        }

        public void SetFlags(string key, EntryFlags flags)
        {
            throw new NotImplementedException();
        }

        public void ClearFlags(string key, EntryFlags flags)
        {
            throw new NotImplementedException();
        }

        public EntryFlags GetFlags(string key)
        {
            throw new NotImplementedException();
        }

        public void Delete(string key)
        {
            throw new NotImplementedException();
        }

        public object GetValue(string key)
        {
            throw new NotImplementedException();
        }

        public bool PutValue(string key, object value)
        {
            throw new NotImplementedException();
        }

        public bool PutNumber(string key, double value)
        {
            throw new NotImplementedException();
        }

        public double GetNumber(string key, double defaultValue)
        {
            throw new NotImplementedException();
        }

        public double GetNumber(string key)
        {
            throw new NotImplementedException();
        }

        public bool PutString(string key, string value)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key, string defaultValue)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key)
        {
            throw new NotImplementedException();
        }

        public bool PutBoolean(string key, bool value)
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(string key, bool defaultValue)
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(string key)
        {
            throw new NotImplementedException();
        }

        public bool PutBooleanArray(string key, bool[] value)
        {
            throw new NotImplementedException();
        }

        public bool[] GetBooleanArray(string key)
        {
            throw new NotImplementedException();
        }

        public bool[] GetBooleanArray(string key, bool[] defaultValue)
        {
            throw new NotImplementedException();
        }

        public bool PutNumberArray(string key, double[] value)
        {
            throw new NotImplementedException();
        }

        public double[] GetNumberArray(string key)
        {
            throw new NotImplementedException();
        }

        public double[] GetNumberArray(string key, double[] defaultValue)
        {
            throw new NotImplementedException();
        }

        public bool PutStringArray(string key, string[] value)
        {
            throw new NotImplementedException();
        }

        public string[] GetStringArray(string key)
        {
            throw new NotImplementedException();
        }

        public string[] GetStringArray(string key, string[] defaultValue)
        {
            throw new NotImplementedException();
        }

        public void AddTableListenerEx(ITableListener listener, NotifyFlags flags)
        {
            throw new NotImplementedException();
        }

        public void AddTableListenerEx(string key, ITableListener listener, NotifyFlags flags)
        {
            throw new NotImplementedException();
        }

        public void AddSubTableListener(ITableListener listener, bool localNotify)
        {
            throw new NotImplementedException();
        }

        public void AddTableListener(ITableListener listener, bool immediateNotify = false)
        {
            throw new NotImplementedException();
        }

        public void AddTableListener(string key, ITableListener listener, bool immediateNotify)
        {
            throw new NotImplementedException();
        }

        public void AddSubTableListener(ITableListener listener)
        {
            throw new NotImplementedException();
        }

        public void RemoveTableListener(ITableListener listener)
        {
            throw new NotImplementedException();
        }
    }
}
