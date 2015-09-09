using NetworkTables;

namespace WPILib.Buttons
{
    /// <summary>
    /// This class represents a button hosted by the NetworkTable.
    /// </summary>
    public class NetworkButton : Button
    {
        public NetworkTable SourceTable { get; }
        public string Field { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkButton"/> class
        /// </summary>
        /// <param name="table">The name of the table to locate the button on</param>
        /// <param name="field">The name of the button</param>
        public NetworkButton(string table, string field) : this(NetworkTable.GetTable(table), field)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkButton"/> class
        /// </summary>
        /// <param name="table">The <see cref="NetworkTable"/> to locate the button on</param>
        /// <param name="field">The name of the button</param>
        public NetworkButton(NetworkTable table, string field)
        {
            SourceTable = table;
            Field = field;
        }

        /// <summary>
        /// Returns whether or not the trigger is active
        /// 
        /// This method will be called repeatedly a command is linked to the Trigger.
        /// </summary>
        /// <returns>Whether or not the trigger condition is active.</returns>
        public override bool Get()
        {
            return SourceTable.IsConnected() && SourceTable.GetBoolean(Field, false);
        }
    }
}
