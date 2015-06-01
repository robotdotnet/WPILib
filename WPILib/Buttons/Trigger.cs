using WPILib.Commands;
using NetworkTablesDotNet.Tables;

namespace WPILib.Buttons
{
    /// <summary>
    /// This class provides an easy way to link commands to inputs.
    /// <para> </para>
    /// It is very easy to link a button to a command.  For instance, you could
    /// link the trigger button of a joystick to a "score" command.
    /// <para> </para>
    /// It is encouraged that teams write a subclass of Trigger if they want to have
    /// something unusual (for instance, if they want to react to the user holding
    /// a button while the robot is reading a certain sensor input).  For this, they
    /// only have to write the <see cref="Trigger.Get()"/> method to get the full functionality
    /// of the Trigger class.
    /// 
    /// </summary>
    public abstract class Trigger : Sendable
    {
        /// <summary>
        /// Returns whether or not the trigger is active
        /// 
        /// This method will be called repeatedly a command is linked to the Trigger.
        /// </summary>
        /// <returns>Whether or not the trigger condition is active.</returns>
        public abstract bool Get();

        /// <summary>
        /// Returns whether Get() return true or the internal table for SmartDashboard use is pressed.
        /// </summary>
        /// <returns>whether Get() return true or the internal table for SmartDashboard use is pressed</returns>
        public bool Grab()
        {
            return Get() || (table != null && table.GetBoolean("pressed", false));
        }

        /// <summary>
        /// Starts the given command whenever the trigger just becomes active.
        /// </summary>
        /// <param name="command">The command to start</param>
        public void WhenActive(Command command)
        {
            PressedButtonScheduler pbs = new PressedButtonScheduler(Grab(), this, command);
            pbs.Start();
        }

        /// <summary>
        /// Constantly starts the given command while the button is held.
        /// 
        /// <see cref="Command.Start()"/> will be called repeatedly while the button is held,
        /// and will be canceled when the button is released.
        /// </summary>
        /// <param name="command">The command to start</param>
        public void WhileActive(Command command)
        {
            HeldButtonScheduler hbs = new HeldButtonScheduler(Grab(), this, command);
            hbs.Start();
        }

        /// <summary>
        /// Starts the command when the button is released
        /// </summary>
        /// <param name="command">The command to start</param>
        public void WhenInactive(Command command)
        {
            ReleasedButtonScheduler rbs = new ReleasedButtonScheduler(Grab(), this, command);
            rbs.Start();
        }

        /// <summary>
        /// Toggles the command whenever the button is pressed (on then off then on)
        /// </summary>
        /// <param name="command">The command to start</param>
        public void ToggleWhenActive(Command command)
        {
            ToggleButtonScheduler tbs = new ToggleButtonScheduler(Grab(), this, command);
        }

        /// <summary>
        /// Cancel the command when the button is pressed.
        /// </summary>
        /// <param name="command">The command to start</param>
        public void CancelWhenActive(Command command)
        {
            CancelButtonScheduler cbs = new CancelButtonScheduler(Grab(), this, command);
            cbs.Start();
        }


        public void InitTable(ITable subtable)
        {
            this.table = subtable;
            if (table != null)
            {
                table.PutBoolean("pressed", Get());
            }
        }

        public ITable GetTable()
        {
            return table;
        }

        /// <summary>
        /// These methods continue to return the "Button" SmartDashboard type until we decided
        /// to create a Trigger widget type for the dashboard.
        /// </summary>
        /// <returns></returns>
        public string GetSmartDashboardType()
        {
            return "Button";
        }

        private ITable table;

    }
}
