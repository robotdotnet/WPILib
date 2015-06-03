using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkTablesDotNet.NetworkTables;
using NetworkTablesDotNet.Tables;

namespace WPILib.smartdashboard
{
    public class SmartDashboard
    {
        private static NetworkTable s_table = NetworkTable.GetTable("SmartDashboard");
        private static Dictionary<ITable, Sendable> s_tablesToData = new Dictionary<ITable, Sendable>();

        static SmartDashboard()
        {
            HLUsageReporting.ReportSmartDashboard();
        }

        public static void PutData(string key, Sendable data)
        {
            ITable dataTable = s_table.GetSubTable(key);
            dataTable.PutString("~TYPE~", data.GetSmartDashboardType());
            data.InitTable(dataTable);
            s_tablesToData.Add(dataTable, data);
        }

        public static void PutData(NamedSendable value)
        {
            PutData(value.GetName(), value);
        }

        public static Sendable GetData(string key)
        {
            ITable subtable = s_table.GetSubTable(key);
            try
            {
                return s_tablesToData[subtable];
            }
            catch (KeyNotFoundException e)
            {
                throw new ArgumentException("SmartDashboard data does not exist: " + key);
            }
        }
    }
}
