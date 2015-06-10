using System;
using System.Collections.Generic;
using NetworkTablesDotNet.NetworkTables;
using NetworkTablesDotNet.Tables;

namespace WPILib.SmartDashboards
{
    public class SmartDashboard
    {
        private static NetworkTable s_table = NetworkTable.GetTable("SmartDashboard");
        private static Dictionary<ITable, ISendable> s_tablesToData = new Dictionary<ITable, ISendable>();

        static SmartDashboard()
        {
            HLUsageReporting.ReportSmartDashboard();
        }

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
            try
            {
                return s_tablesToData[subtable];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("SmartDashboard data does not exist: " + key);
            }
        }
    }
}
