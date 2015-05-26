using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Commands;
using NetworkTablesDotNet.Tables;

namespace WPILib.Buttons
{
    public abstract class Trigger : Sendable
    {
        public abstract bool Get();

        public bool Grab()
        {
            return Get() || (table != null && table.GetBoolean("pressed", false));
            //return Get(); // Add table when we have it.
        }

        public void WhenActive(Command command)
        {
            PressedButtonScheduler pbs = new PressedButtonScheduler(Grab(), this, command);
            pbs.Start();
        }

        public void WhileActive(Command command)
        {
            HeldButtonScheduler hbs = new HeldButtonScheduler(Grab(), this, command);
            hbs.Start();
        }

        public void WhenInactive(Command command)
        {
            ReleasedButtonScheduler rbs = new ReleasedButtonScheduler(Grab(), this, command);
            rbs.Start();
        }

        public void ToggleWhenActive(Command command)
        {
            ToggleButtonScheduler tbs = new ToggleButtonScheduler(Grab(), this, command);
        }

        public void CancelWhenActive(Command command)
        {
            CancelButtonScheduler cbs = new CancelButtonScheduler(Grab(), this, command);
            cbs.Start();
        }

        public void InitTable(NetworkTablesDotNet.Tables.ITable subtable)
        {
            this.table = subtable;
            if (table != null)
            {
                table.PutBoolean("pressed", Get());
            }
        }

        public NetworkTablesDotNet.Tables.ITable GetTable()
        {
            return table;
        }

        public string GetSmartDashboardType()
        {
            return "Button";
        }

        private ITable table;

    }
}
