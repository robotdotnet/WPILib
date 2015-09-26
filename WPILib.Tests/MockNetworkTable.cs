using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public object GetValue(string key)
        {
            throw new NotImplementedException();
        }

        public void PutValue(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void RetrieveValue(string key, object externalValue)
        {
            throw new NotImplementedException();
        }

        public void PutNumber(string key, double value)
        {
            throw new NotImplementedException();
        }

        public double GetNumber(string key)
        {
            throw new NotImplementedException();
        }

        public double GetNumber(string key, double defaultValue)
        {
            throw new NotImplementedException();
        }

        public void PutString(string key, string value)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key, string defaultValue)
        {
            throw new NotImplementedException();
        }

        public void PutBoolean(string key, bool value)
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(string key)
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(string key, bool defaultValue)
        {
            throw new NotImplementedException();
        }

        public void AddTableListener(ITableListener listener)
        {
            throw new NotImplementedException();
        }

        public void AddTableListener(ITableListener listener, bool immediateNotify)
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
