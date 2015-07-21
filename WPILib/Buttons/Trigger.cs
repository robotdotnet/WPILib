using NetworkTables.Tables;
using WPILib.Commands;
using WPILib.Interfaces;

namespace WPILib.Buttons
{
    /// <summary>
    /// This class provides an easy way to link commands to inputs.
    /// </summary>
    /// <remarks>
    /// <para> </para>
    /// It is very easy to link a button to a command.  For instance, you could
    /// link the trigger button of a joystick to a "score" command.
    /// <para> </para>
    /// It is encouraged that teams write a subclass of <see cref="Trigger"/> if they want to have
    /// something unusual (for instance, if they want to react to the user holding
    /// a button while the robot is reading a certain sensor input).  For this, they
    /// only have to write the <see cref="Trigger.Get()"/> method to get the full functionality
    /// of the <see cref="Trigger"/> class.
    /// 
    /// </remarks>
    public abstract class Trigger : ISendable
    {
        /// <summary>
        /// Returns whether or not the trigger is active
        /// </summary><remarks>
        /// This method will be called repeatedly a command is linked to the Trigger.
        /// </remarks>
        /// <returns>Whether or not the trigger condition is active.</returns>
        public abstract bool Get();

        /// <summary>
        /// Returns whether Get() return true or the internal table for SmartDashboard use is pressed.
        /// </summary>
        /// <returns>Whether <see cref="Get()"/> return true or the internal table for SmartDashboard use is pressed</returns>
        public bool Grab() => Get() || (Table?.GetBoolean("pressed", false) ?? false);

        /// <summary>
        /// Starts the given command whenever the trigger just becomes active.
        /// </summary>
        /// <param name="command">The command to start</param>
        public void WhenActive(Command command)
        {
            new PressedButtonScheduler(Grab(), this, command).Start();
        }

        /// <summary>
        /// Constantly starts the given command while the button is held.
        /// </summary><remarks>
        /// <see cref="Command.Start()"/> will be called repeatedly while the button is held,
        /// and will be canceled when the button is released.
        /// </remarks>
        /// <param name="command">The command to start</param>
        public void WhileActive(Command command)
        {
            new HeldButtonScheduler(Grab(), this, command).Start();
        }

        /// <summary>
        /// Starts the command when the button is released
        /// </summary>
        /// <param name="command">The command to start</param>
        public void WhenInactive(Command command)
        {
            new ReleasedButtonScheduler(Grab(), this, command).Start();
        }

        /// <summary>
        /// Toggles the command whenever the button is pressed (on then off then on)
        /// </summary>
        /// <param name="command">The command to start</param>
        public void ToggleWhenActive(Command command)
        {
            new ToggleButtonScheduler(Grab(), this, command).Start();
        }

        /// <summary>
        /// Cancel the command when the button is pressed.
        /// </summary>
        /// <param name="command">The command to start</param>
        public void CancelWhenActive(Command command)
        {
            new CancelButtonScheduler(Grab(), this, command).Start();
        }

        /// <summary>
        /// Initialize a table for this sendable object.
        /// </summary>
        /// <param name="subtable">The table to put the values in.</param>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            Table?.PutBoolean("pressed", Get());
        }

        /// <summary>
        /// Returns the table that is currently associated with the sendable
        /// </summary>
        public ITable Table { get; private set; }

        /// <summary>
        /// These methods continue to return the "Button" SmartDashboard type until we decided
        /// to create a Trigger widget type for the dashboard.
        /// </summary>
        /// <value></value>
        public string SmartDashboardType => "Button";
    }
}
