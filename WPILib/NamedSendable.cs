
namespace WPILib
{
    /// <summary>
    /// The interface for sendable objects that gives the sendable a default name in the Smart Dashboard
    /// </summary>
    public interface NamedSendable : Sendable
    {
        /// <summary>
        /// Returns the name of the subtable of SmartDashboard that the Sendable object will use.
        /// </summary>
        /// <returns>The name of the subtable of SmartDashboard that the Sendable object will use</returns>
        string GetName();
    }
}
