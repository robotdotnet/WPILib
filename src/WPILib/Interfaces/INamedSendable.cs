
namespace WPILib.Interfaces
{
    /// <summary>
    /// The interface for sendable objects that gives the sendable a default name in the Smart Dashboard
    /// </summary>
    public interface INamedSendable : ISendable
    {
        /// <summary>
        /// Returns the name of the subtable of SmartDashboard that the Sendable object will use.
        /// </summary>
        /// <value>The name of the subtable of SmartDashboard that the Sendable object will use</value>
        string Name { get; }
    }
}
