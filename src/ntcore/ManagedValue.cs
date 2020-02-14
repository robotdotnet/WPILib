using NetworkTables.Natives;
using System;
using System.Runtime.InteropServices;
using WPIUtil;

namespace NetworkTables
{
    public readonly struct ManagedValue : IEquatable<ManagedValue>
    {
        public NtType Type { get; }
        public ulong LastChange { get; }
        public EntryUnion Data { get; }

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
                        hash = (hash * 7) + Data.VString!.GetHashCode();
                        break;
                    case NtType.Rpc:
                    case NtType.Raw:
                        hash = (hash * 7) + Data.VRaw!.GetHashCode();
                        break;
                    case NtType.BooleanArray:
                        hash = (hash * 7) + Data.VBooleanArray!.GetHashCode();
                        break;
                    case NtType.DoubleArray:
                        hash = (hash * 7) + Data.VDoubleArray!.GetHashCode();
                        break;
                    case NtType.StringArray:
                        hash = (hash * 7) + Data.VStringArray!.GetHashCode();
                        break;
                }
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ManagedValue v)
            {
                return Equals(v);
            }
            return false;
        }

        public bool Equals(ManagedValue other)
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
                    return Data.VString.AsSpan().SequenceEqual(other.Data.VString.AsSpan());
                case NtType.Raw:
                case NtType.Rpc:
                    return Data.VRaw.AsSpan().SequenceEqual(other.Data.VRaw.AsSpan());
                case NtType.BooleanArray:
                    return Data.VBooleanArray.AsSpan().SequenceEqual(other.Data.VBooleanArray.AsSpan());
                case NtType.DoubleArray:
                    return Data.VDoubleArray.AsSpan().SequenceEqual(other.Data.VDoubleArray.AsSpan());
                case NtType.StringArray:
                    return Data.VStringArray.AsSpan().SequenceEqual(other.Data.VStringArray.AsSpan());
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
                    return Data.VString!;
                case NtType.Rpc:
                case NtType.Raw:
                    return Data.VRaw.AsSpan().ToArray();
                case NtType.BooleanArray:
                    return Data.VBooleanArray.AsSpan().ToArray();
                case NtType.DoubleArray:
                    return Data.VDoubleArray.AsSpan().ToArray();
                case NtType.StringArray:
                    return Data.VStringArray.AsSpan().ToArray();
                default:
                    return null;
            }
        }

        internal unsafe ManagedValue(NtValue* v)
        {
            LastChange = v->last_change;
            Type = v->type;
            Data = new EntryUnion(v);
        }

        internal unsafe ManagedValue(in RefManagedValue v)
        {
            LastChange = v.LastChange;
            Type = v.Type;
            Data = new EntryUnion(v);
        }

        internal unsafe ManagedValue(bool v, ulong t)
        {
            LastChange = t;
            Type = NtType.Boolean;
            Data = new EntryUnion(v);
        }

        internal unsafe ManagedValue(double v, ulong t)
        {
            LastChange = t;
            Type = NtType.Double;
            Data = new EntryUnion(v);
        }

        internal unsafe ManagedValue(ReadOnlySpan<char> v, ulong t)
        {
            LastChange = t;
            Type = NtType.String;
            Data = new EntryUnion(v);
        }

        internal unsafe ManagedValue(ReadOnlySpan<byte> v, ulong t)
        {
            LastChange = t;
            Type = NtType.Raw;
            Data = new EntryUnion(v);
        }

        internal unsafe ManagedValue(ReadOnlySpan<byte> v, ulong t, bool r)
        {
            LastChange = t;
            Type = r ? NtType.Rpc : NtType.Raw;
            Data = new EntryUnion(v);
        }

        internal unsafe ManagedValue(ReadOnlySpan<bool> v, ulong t)
        {
            LastChange = t;
            Type = NtType.BooleanArray;
            Data = new EntryUnion(v);
        }

        internal unsafe ManagedValue(ReadOnlySpan<double> v, ulong t)
        {
            LastChange = t;
            Type = NtType.DoubleArray;
            Data = new EntryUnion(v);
        }

        internal unsafe ManagedValue(ReadOnlySpan<string> v, ulong t)
        {
            LastChange = t;
            Type = NtType.StringArray;
            Data = new EntryUnion(v);
        }

        public static bool operator ==(ManagedValue left, ManagedValue right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ManagedValue left, ManagedValue right)
        {
            return !(left == right);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
#pragma warning disable CA1815 // Override equals and operator equals on value types
    public readonly struct EntryUnion
#pragma warning restore CA1815 // Override equals and operator equals on value types
    {
#pragma warning disable CA1051 // Do not declare visible instance fields
        [FieldOffset(0)]
        public readonly bool VBoolean;
        [FieldOffset(0)]
        public readonly double VDouble;
        [FieldOffset(8)]
        public readonly string? VString;
        [FieldOffset(8)]
        public readonly byte[]? VRaw;
        [FieldOffset(8)]
        public readonly bool[]? VBooleanArray;
        [FieldOffset(8)]
        public readonly double[]? VDoubleArray;
        [FieldOffset(8)]
        public readonly string[]? VStringArray;
#pragma warning restore CA1051 // Do not declare visible instance fields

        internal EntryUnion(bool v)
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

        internal EntryUnion(double v)
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

        internal EntryUnion(ReadOnlySpan<char> v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            VString = v.ToString();
        }

        internal EntryUnion(string v)
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

        internal EntryUnion(ReadOnlySpan<byte> v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            VRaw = v.ToArray();
        }

        internal EntryUnion(byte[] v)
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

        internal EntryUnion(ReadOnlySpan<bool> v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            VBooleanArray = v.ToArray();
        }

        internal EntryUnion(bool[] v)
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

        internal EntryUnion(ReadOnlySpan<double> v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            VDoubleArray = v.ToArray();
        }

        internal EntryUnion(double[] v)
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

        internal EntryUnion(ReadOnlySpan<string> v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            VStringArray = v.ToArray();
        }

        internal EntryUnion(string[] v)
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

        internal unsafe EntryUnion(NtValue* v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            switch (v->type)
            {
                case NtType.Unassigned:
                    break;
                case NtType.Boolean:
                    VBoolean = v->data.v_boolean.Get();
                    break;
                case NtType.Double:
                    VDouble = v->data.v_double;
                    break;
                case NtType.String:
                    VString = UTF8String.ReadUTF8String(v->data.v_string.str, v->data.v_string.len);
                    break;
                case NtType.Rpc:
                case NtType.Raw:
                    var raw = new byte[(int)v->data.v_raw.len];
                    for (int i = 0; i < raw.Length; i++)
                    {
                        raw[i] = v->data.v_raw.str[i];
                    }
                    VRaw = raw;
                    break;
                case NtType.BooleanArray:
                    var barr = new bool[(int)v->data.arr_boolean.len];
                    for (int i = 0; i < barr.Length; i++)
                    {
                        barr[i] = v->data.arr_boolean.arr[i].Get();
                    }
                    VBooleanArray = barr;
                    break;
                case NtType.DoubleArray:
                    var darr = new double[(int)v->data.arr_double.len];
                    for (int i = 0; i < darr.Length; i++)
                    {
                        darr[i] = v->data.arr_double.arr[i];
                    }
                    VDoubleArray = darr;
                    break;
                case NtType.StringArray:
                    var sarr = new string[(int)v->data.arr_string.len];
                    for (int i = 0; i < sarr.Length; i++)
                    {
                        sarr[i] = UTF8String.ReadUTF8String(v->data.arr_string.arr[i].str, v->data.arr_string.arr[i].len);
                    }
                    VStringArray = sarr;
                    break;
            }
        }

        internal EntryUnion(in RefManagedValue v)
        {
            VBoolean = false;
            VDouble = 0;
            VString = null;
            VRaw = null;
            VBooleanArray = null;
            VDoubleArray = null;
            VStringArray = null;

            switch (v.Type)
            {
                case NtType.Unassigned:
                    break;
                case NtType.Boolean:
                    VBoolean = v.Data.VBoolean;
                    break;
                case NtType.Double:
                    VDouble = v.Data.VDouble;
                    break;
                case NtType.String:
                    VString = v.Data.VString.ToString();
                    break;
                case NtType.Rpc:
                case NtType.Raw:
                    VRaw = v.Data.VRaw.ToArray();
                    break;
                case NtType.BooleanArray:
                    VBooleanArray = v.Data.VBooleanArray.ToArray();
                    break;
                case NtType.DoubleArray:
                    VDoubleArray = v.Data.VDoubleArray.ToArray();
                    break;
                case NtType.StringArray:
                    VStringArray = v.Data.VStringArray.ToArray();
                    break;
            }
        }
    }
}
