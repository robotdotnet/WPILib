using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Commands;

namespace WPILib.Buttons
{
    public abstract class Trigger : Sendable
    {
        public abstract bool Get();

        private bool Grab()
        {
            return Get(); // Add table when we have it.
        }

        public void WhenActive(Command command)
        {
            
        }

        public void InitTable(NetworkTablesDotNet.Tables.ITable subtable)
        {
            throw new NotImplementedException();
        }

        public NetworkTablesDotNet.Tables.ITable GetTable()
        {
            throw new NotImplementedException();
        }

        public string GetSmartDashboardType()
        {
            throw new NotImplementedException();
        }
    }
}
