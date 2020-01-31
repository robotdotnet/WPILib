using System;

namespace NetworkTables
{
    /// <summary>
    /// An enumeration of all types allowed in the NetworkTables.
    /// </summary>
    [Flags]
    public enum NtType : uint
    {
        /// <summary>
        /// No type assigned
        /// </summary>
        Unassigned = 0,
        /// <summary>
        /// Boolean type
        /// </summary>
        Boolean = 0x01,
        /// <summary>
        /// Double type
        /// </summary>
        Double = 0x02,
        /// <summary>
        /// String type
        /// </summary>
        String = 0x04,
        /// <summary>
        /// Raw type
        /// </summary>
        Raw = 0x08,
        /// <summary>
        /// Boolean Array type
        /// </summary>
        BooleanArray = 0x10,
        /// <summary>
        /// Double Array type
        /// </summary>
        DoubleArray = 0x20,
        /// <summary>
        /// String Array type
        /// </summary>
        StringArray = 0x40,
        /// <summary>
        /// Rpc type
        /// </summary>
        Rpc = 0x80
    }

    /// <summary>
    /// The flags avalible for TableListeners
    /// </summary>
    [Flags]
    public enum NotifyFlags : uint
    {
        /// <summary>
        ///  Notify nobody
        /// </summary>
        None = 0x00,
        /// <summary>
        /// Initial listener addition
        /// </summary>
        Immediate = 0x01,
        /// <summary>
        /// Changed locally
        /// </summary>
        Local = 0x02,
        /// <summary>
        /// Newly created entry
        /// </summary>
        New = 0x04,
        /// <summary>
        /// Deleted entry
        /// </summary>
        Delete = 0x08,
        /// <summary>
        /// Value changed for entry
        /// </summary>
        Update = 0x10,
        /// <summary>
        /// Flags changed for entry
        /// </summary>
        FlagsChanged = 0x20
    };

    /// <summary>
    /// The flags avalible for Entries
    /// </summary>
    [Flags]
    public enum EntryFlags : uint
    {
        /// <summary>
        /// No flags
        /// </summary>
        None = 0x00,
        /// <summary>
        /// Set entry to be persistent
        /// </summary>
        Persistent = 0x01
    }

    /// <summary>
    /// The log level to use for the NT logger
    /// </summary>
    public enum LogLevel : uint
    {
        ///
        Critical = 50,
        ///
        Error = 40,
        ///
        Warning = 30,
        ///
        Info = 20,
        ///
        Debug = 10,
        ///
        Debug1 = 9,
        ///
        Debug2 = 8,
        ///
        Debug3 = 7,
        ///
        Debug4 = 6
    }

    public enum NetworkMode : uint
    {
        None = 0x00,        /* not running */
        Server = 0x01,      /* running in server mode */
        Client = 0x02,      /* running in client mode */
        Starting = 0x04,    /* flag for starting (either client or server) */
        Failure = 0x08,     /* flag for failure (either client or server) */
    };

}
