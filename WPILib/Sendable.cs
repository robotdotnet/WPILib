using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkTablesDotNet.Tables;

namespace WPILib
{
    public interface Sendable
    {
        void InitTable(ITable subtable);
        ITable GetTable();
        string GetSmartDashboardType();
    }
}
