using NetworkTables.Natives;
using System;
using System.Runtime.InteropServices;
using WPIUtil;

namespace NetworkTables
{
#pragma warning disable CA1066 // Type {0} should implement IEquatable<T> because it overrides Equals
#pragma warning disable CA2231 // Overload operator equals on overriding value type Equals
#pragma warning disable CA1815 // Override equals and operator equals on value types
    public readonly ref struct RefManagedValue
#pragma warning restore CA1815 // Override equals and operator equals on value types
#pragma warning restore CA2231 // Overload operator equals on overriding value type Equals
#pragma warning restore CA1066 // Type {0} should implement IEquatable<T> because it overrides Equals
    {
#pragma warning disable CA1051 // Do not declare visible instance fields
        public readonly NtType Type;
        public readonly ulong LastChange;
        public readonly RefEntryUnion Data;
#pragma warning restore CA1051 // Do not declare visible instance fields

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + Type.GetHashCode();
                switch (Type)
                {
                    case NtType.Boolean:
                        hash = (hash * 7) + Data.VBoolean.GetHashCode();
                        break;
                    case NtType.Double:
                        hash = (hash * 7) + Data.VDouble.GetHashCode();
                        break;
                    case NtType.String:
                        hash = (hash * 7) + Data.VString.GetHashCode();
                        break;
                    case NtType.Rpc:
                    case NtType.Raw:
                        hash = (hash * 7) + Data.VRaw.GetHashCode();
                        break;
                    case NtType.BooleanArray:
                        hash = (hash * 7) + Data.VBooleanArray.GetHashCode();
                        break;
                    case NtType.DoubleArray:
                        hash = (hash * 7) + Data.VDoubleArray.GetHashCode();
                        break;
                    case NtType.StringArray:
                        hash = (hash * 7) + Data.VStringArray.GetHashCode();
                        break;
                }
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(RefManagedValue other)
        {
            if (Type != other.Type) return false;
            switch (Type)
            {
                case NtType.Unassigned:
                    return true;
                case NtType.Boolean:
                    return Data.VBoolean == other.Data.VBoolean;
                case NtType.Double:
                    return Data.VDouble == other.Data.VDouble;
                case NtType.String:
                    return Data.VString.SequenceEqual(other.Data.VString);
                case NtType.Raw:
                case NtType.Rpc:
                    return Data.VRaw.SequenceEqual(other.Data.VRaw);
                case NtType.BooleanArray:
                    return Data.VBooleanArray.SequenceEqual(other.Data.VBooleanArray);
                case NtType.DoubleArray:
                    return Data.VDoubleArray.SequenceEqual(other.Data.VDoubleArray);
                case NtType.StringArray:
                    return Data.VStringArray.SequenceEqual(other.Data.VStringArray);
                default:
                    return false;
            }
        }

        public object? GetValue()
        {
            switch (Type)
            {
                case NtType.Boolean:
                    return Data.VBoolean;
                case NtType.Double:
                    return Data.VDouble;
                case NtType.String:
                    return Data.VString.ToString();
                case NtType.Rpc:
                case NtType.Raw:
                    return Data.VRaw.ToArray();
                case NtType.BooleanArray:
                    return Data.VBooleanArray.ToArray();
                case NtType.DoubleArray:
                    return Data.VDoubleArray.ToArray();
                case NtType.StringArray:
                    return Data.VStringArray.ToArray();
                default:
                    return null;
            }
        }

        internal unsafe RefManagedValue(in NtValue v)
        {
            LastChange = v.last_change;
            Type = v.type;
            Data = new RefEntryUnion(v);

        }

        internal unsafe RefManagedValue(bool v, ulong t)
        {
            LastChange = t;
            Type = NtType.Boolean;
            Data = new RefEntryUnion(v);
        }

        internal unsafe RefManagedValue(double v, ulong t)
        {
            LastChange = t;
            Type = NtType.Double;
            Data = new RefEntryUnion(v);
        }

        internal unsafe RefManagedValue(ReadOnlySpan<char> v, ulong t)
        {
            LastChange = t;
            Type = NtType.String;
            Data = new RefEntryUnion(v);
        }

        internal unsafe RefManagedValue(ReadOnlySpan<byte> v, ulong t)
        {
            LastChange = t;
            Type = NtType.Raw;
            Data = new RefEntryUnion(v);
        }

        internal unsafe RefManagedValue(ReadOnlySpan<byte> v, ulong t, bool r)
        {
            LastChange = t;
            Type = r ? NtType.Rpc : NtType.Raw;
            Data = new RefEntryUnion(v);
        }

        internal unsafe RefManagedValue(ReadOnlySpan<bool> v, ulong t)
        {
            LastChange = t;
            Type = NtType.BooleanArray;
            Data = new RefEntryUnion(v);
        }

        internal unsafe RefManagedValue(ReadOnlySpan<double> v, ulong t)
        {
            LastChange = t;
            Type = NtType.DoubleArray;
            Data = new RefEntryUnion(v);
        }

        internal unsafe RefManagedValue(ReadOnlySpan<string> v, ulong t)
        {
            LastChange = t;
            Type = NtType.StringArray;
            Data = new RefEntryUnion(v);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
#pragma warning disable CA1815 // Override equals and operator equals on value types
    public readonly ref struct RefEntryUnion
#pragma warning restore CA1815 // Override equals and operator equals on value types
    {
        [FieldOffset(0)]
#pragma warning disable CA1051 // Do not declare visible instance fields
        public readonly bool VBoolean;

        [FieldOffset(0)]
        public readonly double VDouble;
        [FieldOffset(8)]
        public readonly ReadOnlySpan<char> VString;
        [FieldOffset(8)]
        public readonly ReadOnlySpan<byte> VRaw;
        [FieldOffset(8)]
        public readonly ReadOnlySpan<bool> VBooleanArray;
        [FieldOffset(8)]
        public readonly ReadOnlySpan<double> VDoubleArray;
        [FieldOffset(8)]
        public readonly ReadOnlySpan<string> VStringArray;
#pragma warning restore CA1051 // Do not declare visible instance fields

        internal RefEntryUnion(bool v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            VBoolean = v;
        }

        internal RefEntryUnion(double v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            VDouble = v;
        }

        internal RefEntryUnion(ReadOnlySpan<char> v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            VString = v;
        }

        internal RefEntryUnion(ReadOnlySpan<byte> v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            VRaw = v;
        }

        internal RefEntryUnion(ReadOnlySpan<bool> v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            VBooleanArray = v;
        }

        internal RefEntryUnion(ReadOnlySpan<double> v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            VDoubleArray = v;
        }

        internal RefEntryUnion(ReadOnlySpan<string> v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            VStringArray = v;
        }

        internal unsafe RefEntryUnion(in NtValue v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            switch (v.type)
            {
                case NtType.Unassigned:
                    break;
                case NtType.Boolean:
                    VBoolean = v.data.v_boolean.Get();
                    break;
                case NtType.Double:
                    VDouble = v.data.v_double;
                    break;
                case NtType.String:
                    VString = UTF8String.ReadUTF8String(v.data.v_string.str, v.data.v_string.len).AsSpan();
                    break;
                case NtType.Rpc:
                case NtType.Raw:
                    VRaw = new ReadOnlySpan<byte>(v.data.v_raw.str, (int)v.data.v_raw.len);
                    break;
                case NtType.BooleanArray:
                    VBooleanArray = new ReadOnlySpan<bool>(v.data.arr_boolean.arr, (int)v.data.arr_boolean.len);
                    break;
                case NtType.DoubleArray:
                    VDoubleArray = new ReadOnlySpan<double>(v.data.arr_double.arr, (int)v.data.arr_double.len);
                    break;
                case NtType.StringArray:
                    var sarr = new string[(int)v.data.arr_string.len];
                    for (int i = 0; i < sarr.Length; i++)
                    {
                        sarr[i] = UTF8String.ReadUTF8String(v.data.arr_string.arr[i].str, v.data.arr_string.arr[i].len);
                    }
                    VStringArray = sarr;
                    break;
            }
        }
    }
}
