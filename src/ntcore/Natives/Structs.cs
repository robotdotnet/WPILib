using System;
using System.Runtime.InteropServices;

namespace NetworkTables.Natives
{
#pragma warning disable CA1815 // Override equals and operator equals on value types
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// NT Bool for interop
    /// </summary>
    public readonly struct NtBool
    {
        private readonly int m_value;

        /// <summary>
        /// Creates an NT Bool from an int
        /// </summary>
        /// <param name="value">value</param>
        public NtBool(int value)
        {
            this.m_value = value;
        }

        /// <summary>
        /// Creates an NT Bool from a bool
        /// </summary>
        /// <param name="value"></param>
        public NtBool(bool value)
        {
            this.m_value = value ? 1 : 0;
        }

        /// <summary>
        /// Gets the value
        /// </summary>
        /// <returns>value</returns>
        public bool Get()
        {
            return m_value != 0;
        }

        /// <summary>
        /// Converts a bool to an NT Bool
        /// </summary>
        /// <param name="value">bool balue</param>
#pragma warning disable CA2225 // Operator overloads have named alternates
        public static implicit operator NtBool(bool value)
#pragma warning restore CA2225 // Operator overloads have named alternates
        {
            return new NtBool(value);
        }
    }

    /// <summary>
    /// NT String
    /// </summary>
    public unsafe struct NtString
    {
        /// <summary>
        /// String Pointer
        /// </summary>

        public byte* str;
        /// <summary>
        /// String Length
        /// </summary>
        public UIntPtr len;
    }

    /// <summary>
    /// NT Boolean Array
    /// </summary>
    public unsafe struct BoolArr
    {
        /// <summary>
        /// Array Pointer
        /// </summary>
        public NtBool* arr;
        /// <summary>
        /// Array Length
        /// </summary>
        public UIntPtr len;
    }


    /// <summary>
    /// NT Double Array
    /// </summary>
    public unsafe struct DoubleArr
    {
        /// <summary>
        /// Array Pointer
        /// </summary>
        public double* arr;
        /// <summary>
        /// Array Length
        /// </summary>
        public UIntPtr len;
    }

    /// <summary>
    /// NT String Array
    /// </summary>
    public unsafe struct StringArr
    {
        /// <summary>
        /// Array Pointer
        /// </summary>
        public NtString* arr;
        /// <summary>
        /// Array Length
        /// </summary>
        public UIntPtr len;
    }

    /// <summary>
    /// NT Entry Data Union
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct DataUnion
    {
        /// <summary>
        /// Boolean Value
        /// </summary>
        [FieldOffset(0)]
        public NtBool v_boolean;
        /// <summary>
        /// Double Value
        /// </summary>
        [FieldOffset(0)]
        public double v_double;
        /// <summary>
        /// String Value
        /// </summary>
        [FieldOffset(0)]
        public NtString v_string;
        /// <summary>
        /// Raw Value
        /// </summary>
        [FieldOffset(0)]
        public NtString v_raw;
        /// <summary>
        /// Boolean Array Value
        /// </summary>
        [FieldOffset(0)]
        public BoolArr arr_boolean;
        /// <summary>
        /// Double Array Value
        /// </summary>
        [FieldOffset(0)]
        public DoubleArr arr_double;
        /// <summary>
        /// String Array Value
        /// </summary>
        [FieldOffset(0)]
        public StringArr arr_string;
    }

    /// <summary>
    /// NT Value
    /// </summary>
    public struct NtValue
    {
        /// <summary>
        /// Value Type
        /// </summary>
        public NtType type;
        /// <summary>
        /// Value Last Change
        /// </summary>
        public ulong last_change;
        /// <summary>
        /// Value Data
        /// </summary>
        public DataUnion data;
    }

    /// <summary>
    /// NT Entry Info
    /// </summary>
    public struct NtEntryInfo
    {
        /// <summary>
        /// Entry Handle
        /// </summary>
        public NtEntry entry;
        /// <summary>
        /// Name
        /// </summary>
        public NtString name;
        /// <summary>
        /// Entry Type
        /// </summary>
        public NtType type;
        /// <summary>
        /// Entry Flags
        /// </summary>
        public uint flags;
        /// <summary>
        /// Last Entry Change
        /// </summary>
        public ulong last_change;
    }

    /// <summary>
    /// NT Connection Info
    /// </summary>
    public struct NtConnectionInfo
    {
        /// <summary>
        /// Remote ID
        /// </summary>
        public NtString remote_id;
        /// <summary>
        /// Remote IP
        /// </summary>
        public NtString remote_ip;
        /// <summary>
        /// Remote Port
        /// </summary>
        public uint remote_port;
        /// <summary>
        /// Last Update
        /// </summary>
        public ulong last_update;
        /// <summary>
        /// NT Protocol Version
        /// </summary>
        public uint protocol_version;
    }

    /// <summary>
    /// NT Rpc Parameter Definition
    /// </summary>
    public struct NtRpcParamDef
    {
        /// <summary>
        /// Parameter Name
        /// </summary>
        public NtString name;
        /// <summary>
        /// Parameter Type
        /// </summary>
        public NtType type;
    }

    /// <summary>
    /// NT Rpc Result Definition
    /// </summary>
    public struct NtRpcResultDef
    {
        /// <summary>
        /// Result Name
        /// </summary>
        public NtString name;
        /// <summary>
        /// Result Type
        /// </summary>
        public NtType type;
    }

    /// <summary>
    /// NT Rpc Definition
    /// </summary>
    public unsafe struct NtRpcDefinition
    {
        /// <summary>
        /// Definition Version
        /// </summary>
        public uint version;
        /// <summary>
        /// Rpc Name
        /// </summary>
        public NtString name;
        /// <summary>
        /// Number of Parameters
        /// </summary>
        public UIntPtr num_params;
        /// <summary>
        /// Array of Parameter Definitions
        /// </summary>
        public NtRpcParamDef* @params;
        /// <summary>
        /// Number of Results
        /// </summary>
        public UIntPtr num_results;
        /// <summary>
        /// Array of Result Definitions
        /// </summary>
        public NtRpcResultDef* results;
    }

    /// <summary>
    /// NT Rpc Answer Version 1
    /// </summary>
    public struct NtRpcAnswer
    {
        /// <summary>
        /// Rpc Entry Handle
        /// </summary>
        public NtEntry entry;
        /// <summary>
        /// Rpc Call Handle
        /// </summary>
        public NtRpcCall call;
        /// <summary>
        /// Rpc Name
        /// </summary>
        public NtString name;
        /// <summary>
        /// Rpc Parameters
        /// </summary>
        public NtString @params;
        /// <summary>
        /// Rpc Connection
        /// </summary>
        public NtConnectionInfo conn;
    }

    /// <summary>
    /// NT Entry Notification
    /// </summary>
    public struct NtEntryNotification
    {
        /// <summary>
        /// Entry Listener Handle
        /// </summary>
        public NtEntryListener listener;
        /// <summary>
        /// Entry Handle
        /// </summary>
        public NtEntry entry;
        /// <summary>
        /// Entry Name
        /// </summary>
        public NtString name;
        /// <summary>
        /// Entry Value
        /// </summary>
        public NtValue value;
        /// <summary>
        /// Notification Flags
        /// </summary>
        public uint flags;
    }

    /// <summary>
    /// NT Connection Notification
    /// </summary>
    public struct NtConnectionNotification
    {
        /// <summary>
        /// Listener Handle
        /// </summary>
        public NtConnectionListener listener;
        /// <summary>
        /// Connected or Disconnected
        /// </summary>
        public NtBool connected;
        /// <summary>
        /// Connection Info
        /// </summary>
        public NtConnectionInfo conn;
    }

    /// <summary>
    /// NT Log Message
    /// </summary>
    public unsafe struct NtLogMessage
    {
        /// <summary>
        /// Logger Handle
        /// </summary>
        public NtLogger logger;
        /// <summary>
        /// Log Level
        /// </summary>
        public uint level;
        /// <summary>
        /// Filename
        /// </summary>
        public byte* filename;
        /// <summary>
        ///  Line
        /// </summary>
        public uint line;
        /// <summary>
        /// Message
        /// </summary>
        public byte* message;
    }
}
