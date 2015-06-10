using NetworkTablesDotNet.Tables;

namespace WPILib
{
    /// <summary>
    /// The base interface for objects that can be sent over the network
    /// through network tables.
    /// </summary>
    public interface ISendable
    {
        /// <summary>
        /// Initialize a table for this sendable object.
        /// </summary>
        /// <param name="subtable">The table to put the values in.</param>
        void InitTable(ITable subtable);

        /// <summary>
        /// Returns the table that is currently associated with the sendable
        /// </summary>
 
        ITable Table { get; }

        /// <summary>
        /// Returns the string representation of the named data type that will be used by the smart dashboard for this sendable
        /// </summary>
        string SmartDashboardType { get; }
    }
}
