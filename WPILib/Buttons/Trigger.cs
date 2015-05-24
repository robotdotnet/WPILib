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

        private bool Grab()
        {
            return Get() || (table != null && table.GetBoolean("pressed", false));
            //return Get(); // Add table when we have it.
        }

        public void WhenActive(Command command)
        {
            
        }

        public void WhileActive(Command command)
        {
            //FigureThisOut
        }

        public void WhenInactive(Command command)
        {

        }

        public void ToggleWhenActive(Command command)
        {

        }

        public void CancelWhenActive(Command command)
        {

        }

        public abstract class ButtonScheduler
        {
            public abstract void Execute();

            protected void Start()
            {
                Scheduler.GetInstance().AddButton(this);
            }
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
