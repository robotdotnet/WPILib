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
        private static NetworkTable table = NetworkTable.GetTable("SmartDashboard");
        private static Dictionary<ITable, Sendable> tablesToData = new Dictionary<ITable, Sendable>();

        static SmartDashboard()
        {
            HLUsageReporting.ReportSmartDashboard();
        }

        public static void PutData(string key, Sendable data)
        {
            ITable dataTable = table.GetSubTable(key);
            dataTable.PutString("~TYPE~", data.GetSmartDashboardType());
            data.InitTable(dataTable);
            tablesToData.Add(dataTable, data);
        }

        public static void PutData(NamedSendable value)
        {
            PutData(value.GetName(), value);
        }

        public static Sendable GetData(string key)
        {
            ITable subtable = table.GetSubTable(key);
            try
            {
                return tablesToData[subtable];
            }
            catch (KeyNotFoundException e)
            {
                throw new ArgumentException("SmartDashboard data does not exist: " + key);
            }
        }
    }
}
