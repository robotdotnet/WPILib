using System;
using System.Collections.Generic;
using NetworkTables;
using NetworkTables.Native;
using NetworkTables.Tables;

namespace WPILib.Tests
{
    class MockNetworkTable : ITable
    {
        public int ContainsKeyCount;
        public bool ContainsKey(string key)
        {
            ContainsKeyCount++;
            return true;
        }
        public int ContainsSubTableCount;
        public bool ContainsSubTable(string key)
        {
            ContainsSubTableCount++;
            return true;
        }
        public int GetSubTableCount;
        public ITable GetSubTable(string key)
        {
            GetSubTableCount++;
            return this;
        }

        public HashSet<string> GetKeys(NtType types)
        {
            return null;
        }

        public HashSet<string> GetKeys(EntryFlags types)
        {
            return null;
        }

        public HashSet<string> GetKeys(int types)
        {
            return null;
        }

        public HashSet<string> GetKeys()
        {
            return null;
        }

        public HashSet<string> GetSubTables()
        {
            return null;
        }

        public void SetPersistent(string key)
        {
            
        }

        public void ClearPersistent(string key)
        {
            
        }

        public bool IsPersistent(string key)
        {
            return false;
        }

        public void SetFlags(string key, EntryFlags flags)
        {
            
        }

        public void ClearFlags(string key, EntryFlags flags)
        {
            
        }

        public EntryFlags GetFlags(string key)
        {
            return EntryFlags.None;
        }

        public void Delete(string key)
        {
            
        }

        public object GetValue(string key)
        {
            return null;
        }

        public bool PutValue(string key, object value)
        {
            return false;
        }

        public bool PutNumber(string key, double value)
        {
            return false;
        }

        public double GetNumber(string key, double defaultValue)
        {
            return 0.0;
        }

        public double GetNumber(string key)
        {
            return 0.0;
        }

        public bool PutString(string key, string value)
        {
            return false;
        }

        public string GetString(string key, string defaultValue)
        {
            return null;
        }

        public string GetString(string key)
        {
            return null;
        }

        public bool PutBoolean(string key, bool value)
        {
            return false;
        }

        public bool GetBoolean(string key, bool defaultValue)
        {
            return false;
        }

        public bool GetBoolean(string key)
        {
            return false;
        }

        public bool PutBooleanArray(string key, bool[] value)
        {
            return false;
        }

        public bool[] GetBooleanArray(string key)
        {
            return null;
        }

        public bool[] GetBooleanArray(string key, bool[] defaultValue)
        {
            return null;
        }

        public bool PutNumberArray(string key, double[] value)
        {
            return false;
        }

        public double[] GetNumberArray(string key)
        {
            return null;
        }

        public double[] GetNumberArray(string key, double[] defaultValue)
        {
            return null;
        }

        public bool PutStringArray(string key, string[] value)
        {
            return false;
        }

        public string[] GetStringArray(string key)
        {
            return null;
        }

        public string[] GetStringArray(string key, string[] defaultValue)
        {
            return null;
        }

        public bool PutRaw(string key, byte[] value)
        {
            return false;
        }

        public byte[] GetRaw(string key)
        {
            return null;
        }

        public byte[] GetRaw(string key, byte[] defaultValue)
        {
            return null;
        }

        public void AddTableListenerEx(ITableListener listener, NotifyFlags flags)
        {
            
        }

        public void AddTableListenerEx(string key, ITableListener listener, NotifyFlags flags)
        {
            
        }

        public void AddSubTableListener(ITableListener listener, bool localNotify)
        {
            
        }

        public void AddTableListener(ITableListener listener, bool immediateNotify = false)
        {
            
        }

        public void AddTableListener(string key, ITableListener listener, bool immediateNotify)
        {
            
        }

        public void AddSubTableListener(ITableListener listener)
        {
            
        }

        public void RemoveTableListener(ITableListener listener)
        {
            
        }

        public object GetValue(string key, object defaultValue)
        {
            return null;
        }

        public void AddTableListenerEx(Action<ITable, string, object, NotifyFlags> listenerDelegate, NotifyFlags flags)
        {
            
        }

        public void AddTableListenerEx(string key, Action<ITable, string, object, NotifyFlags> listenerDelegate, NotifyFlags flags)
        {
            
        }

        public void AddSubTableListener(Action<ITable, string, object, NotifyFlags> listenerDelegate, bool localNotify)
        {
            
        }

        public void AddTableListener(Action<ITable, string, object, NotifyFlags> listenerDelegate, bool immediateNotify = false)
        {
            
        }

        public void AddTableListener(string key, Action<ITable, string, object, NotifyFlags> listenerDelegate, bool immediateNotify)
        {
            
        }

        public void AddSubTableListener(Action<ITable, string, object, NotifyFlags> listenerDelegate)
        {
            
        }

        public void RemoveTableListener(Action<ITable, string, object, NotifyFlags> listenerDelegate)
        {
            
        }
    }
}
