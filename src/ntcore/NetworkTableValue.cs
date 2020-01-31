using NetworkTables.Natives;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NetworkTables
{
    public readonly struct NetworkTableValue : IEquatable<NetworkTableValue>
    {
        public NtType Type => Value.Type;
        public readonly ManagedValue Value;
        public bool IsValid => Type != NtType.Unassigned;

        internal NetworkTableValue(in ManagedValue value)
        {
            this.Value = value;
        }

        internal NetworkTableValue(in RefManagedValue value)
        {
            Value = new ManagedValue(value);
        }

        /// <summary>
        /// Gets if the type is boolean
        /// </summary>
        /// <returns>True if the type is boolean</returns>
        public bool IsBoolean() => Type == NtType.Boolean;
        /// <summary>
        /// Gets if the type is double
        /// </summary>
        /// <returns>True if the type is double</returns>
        public bool IsDouble() => Type == NtType.Double;
        /// <summary>
        /// Gets if the type is string
        /// </summary>
        /// <returns>True if the type is string</returns>
        public bool IsString() => Type == NtType.String;

        /// <summary>
        /// Gets if the type is raw
        /// </summary>
        /// <returns>True if the type is raw</returns>
        public bool IsRaw() => Type == NtType.Raw;

        /// <summary>
        /// Gets if the type is Rpc
        /// </summary>
        /// <returns>True if the type is Rpc</returns>
        public bool IsRpc() => Type == NtType.Rpc;

        /// <summary>
        /// Gets if the type is boolean array
        /// </summary>
        /// <returns>True if the type is a boolean array</returns>
        public bool IsBooleanArray() => Type == NtType.BooleanArray;

        /// <summary>
        /// Gets if the type is double array
        /// </summary>
        /// <returns>True if the type is a double array</returns>
        public bool IsDoubleArray() => Type == NtType.DoubleArray;

        /// <summary>
        /// Gets if the type is string array
        /// </summary>
        /// <returns>True if the type is a string array</returns>
        public bool IsStringArray() => Type == NtType.StringArray;

        /// <summary>
        /// Gets the boolean value from the type
        /// </summary>
        /// <returns>boolean contained in type</returns>
        /// <exception cref="InvalidCastException">Thrown if
        /// type is not boolean.</exception>
        public bool GetBoolean()
        {
            if (Type != NtType.Boolean)
            {
                throw new InvalidCastException($"cannot convert {Type} to boolean");
            }
            return Value.Data.VBoolean;
        }
        /// <summary>
        /// Gets the double value from the type
        /// </summary>
        /// <returns>double contained in type</returns>
        /// <exception cref="InvalidCastException">Thrown if
        /// type is not double.</exception>
        public double GetDouble()
        {
            if (Type != NtType.Double)
            {
                throw new InvalidCastException($"cannot convert {Type} to double");
            }
            return Value.Data.VDouble;
        }

        /// <summary>
        /// Gets the string value from the type
        /// </summary>
        /// <returns>string contained in type</returns>
        /// <exception cref="InvalidCastException">Thrown if
        /// type is not string.</exception>
        public string GetString()
        {
            if (Type != NtType.String)
            {
                throw new InvalidCastException($"cannot convert {Type} to string");
            }
            return Value.Data.VString!;
        }

        //For reference types (other then strings) return copies;

        /// <summary>
        /// Gets the raw value from the type
        /// </summary>
        /// <returns>raw byte array contained in type</returns>
        /// <exception cref="InvalidCastException">Thrown if
        /// type is not raw.</exception>
        public byte[] GetRaw()
        {
            if (Type != NtType.Raw)
            {
                throw new InvalidCastException($"cannot convert {Type} to raw");
            }
            return Value.Data.VRaw!;
        }

        /// <summary>
        /// Gets the rpc value from the type
        /// </summary>
        /// <returns>rpc byte array contained in type</returns>
        /// <exception cref="InvalidCastException">Thrown if
        /// type is not rpc.</exception>
        public byte[] GetRpc()
        {
            if (Type != NtType.Rpc)
            {
                throw new InvalidCastException($"cannot convert {Type} to Rpc");
            }
            return Value.Data.VRaw!;
        }

        /// <summary>
        /// Gets the boolean array value from the type
        /// </summary>
        /// <returns>boolean array contained in type</returns>
        /// <exception cref="InvalidCastException">Thrown if
        /// type is not boolean array.</exception>
        public bool[] GetBooleanArray()
        {
            if (Type != NtType.BooleanArray)
            {
                throw new InvalidCastException($"cannot convert {Type} to boolean array");
            }
            return Value.Data.VBooleanArray!;
        }

        /// <summary>
        /// Gets the double array value from the type
        /// </summary>
        /// <returns>double array contained in type</returns>
        /// <exception cref="InvalidCastException">Thrown if
        /// type is not double array.</exception>
        public double[] GetDoubleArray()
        {
            if (Type != NtType.DoubleArray)
            {
                throw new InvalidCastException($"cannot convert {Type} to double array");
            }
            return Value.Data.VDoubleArray!;
        }

        /// <summary>
        /// Gets the string array value from the type
        /// </summary>
        /// <returns>string array contained in type</returns>
        /// <exception cref="InvalidCastException">Thrown if
        /// type is not string arrya.</exception>
        public string[] GetStringArray()
        {
            if (Type != NtType.StringArray)
            {
                throw new InvalidCastException($"cannot convert {Type} to string array");
            }
            return Value.Data.VStringArray!;
        }

        public static NetworkTableValue MakeBoolean(bool value)
        {
            return new NetworkTableValue(new ManagedValue(value, NtCore.Now()));
        }

        public static NetworkTableValue MakeDouble(double value)
        {
            return new NetworkTableValue(new ManagedValue(value, NtCore.Now()));
        }

        public static NetworkTableValue MakeString(string value)
        {
            return new NetworkTableValue(new ManagedValue(value.AsSpan(), NtCore.Now()));
        }

        public static NetworkTableValue MakeString(ReadOnlySpan<char> value)
        {
            return new NetworkTableValue(new ManagedValue(value, NtCore.Now()));
        }

        public static NetworkTableValue MakeRaw(ReadOnlySpan<byte> value)
        {
            return new NetworkTableValue(new ManagedValue(value, NtCore.Now()));
        }

        public static NetworkTableValue MakeRpc(ReadOnlySpan<byte> value)
        {
            return new NetworkTableValue(new ManagedValue(value, NtCore.Now(), true));
        }

        public static NetworkTableValue MakeBooleanArray(ReadOnlySpan<bool> value)
        {
            return new NetworkTableValue(new ManagedValue(value, NtCore.Now()));
        }

        public static NetworkTableValue MakeDoubleArray(ReadOnlySpan<double> value)
        {
            return new NetworkTableValue(new ManagedValue(value, NtCore.Now()));
        }

        public static NetworkTableValue MakeStringArray(ReadOnlySpan<string> value)
        {
            return new NetworkTableValue(new ManagedValue(value, NtCore.Now()));
        }

        public override bool Equals(object obj)
        {
            if (obj is NetworkTableValue v)
            {
                return Equals(v);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(NetworkTableValue other)
        {
            return Value.Equals(other.Value);
        }

        public static bool operator ==(NetworkTableValue lhs, NetworkTableValue rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(NetworkTableValue lhs, NetworkTableValue rhs)
        {
            return !lhs.Equals(rhs);
        }


    }
}
