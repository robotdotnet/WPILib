using NetworkTables.Natives;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTables
{
    public readonly struct NetworkTableEntry
    {
        public const int kPersistent = 0x01;

        public NetworkTableEntry(NetworkTableInstance inst, NtEntry handle)
        {
            Instance = inst;
            Handle = handle;
        }

        public bool IsValid => Handle.Get() != 0;

        public readonly NtEntry Handle;

        public readonly NetworkTableInstance Instance;

        public bool Exists() => GetEntryType() != NtType.Unassigned;

        public string GetEntryName() => NtCore.GetEntryName(Handle);

        public NtType GetEntryType() => NtCore.GetEntryType(Handle);

        public EntryFlags GetEntryFlags() => NtCore.GetEntryFlags(Handle);

        public ulong GetLastChange() => NtCore.GetEntryLastChange(Handle);

        public EntryInfo? GetEntryInfo()
        {
            return NtCore.GetEntryInfoHandle(Instance, Handle);
        }

        public NetworkTableValue GetValue()
        {
            return new NetworkTableValue(NtCore.GetEntryValue(Handle));
        }

        public object? GetObjectValue()
        {
            var v = NtCore.GetEntryValue(Handle);
            return v.GetValue();
        }

        public bool GetBoolean(bool defaultValue)
        {
            var entry = NtCore.GetEntryValue(Handle);
            if (entry.Type == NtType.Boolean)
            {
                return entry.Data.VBoolean;
            }
            return defaultValue;
        }

        public double GetDouble(double defaultValue)
        {
            var entry = NtCore.GetEntryValue(Handle);
            if (entry.Type == NtType.Double)
            {
                return entry.Data.VDouble;
            }
            return defaultValue;
        }

        public string GetString(string defaultValue)
        {
            var entry = NtCore.GetEntryValue(Handle);
            if (entry.Type == NtType.String)
            {
                return entry.Data.VString!;
            }
            return defaultValue;
        }

        public ReadOnlySpan<char> GetString(ReadOnlySpan<char> defaultValue)
        {
            var entry = NtCore.GetEntryValue(Handle);
            if (entry.Type == NtType.String)
            {
                return entry.Data.VString.AsSpan();
            }
            return defaultValue;
        }

        public ReadOnlySpan<byte> GetRaw(ReadOnlySpan<byte> defaultValue)
        {
            var entry = NtCore.GetEntryValue(Handle);
            if (entry.Type == NtType.Raw)
            {
                return entry.Data.VRaw.AsSpan();
            }
            return defaultValue;
        }

        public ReadOnlySpan<bool> GetBooleanArray(ReadOnlySpan<bool> defaultValue)
        {
            var entry = NtCore.GetEntryValue(Handle);
            if (entry.Type == NtType.BooleanArray)
            {
                return entry.Data.VBooleanArray.AsSpan();
            }
            return defaultValue;
        }

        public ReadOnlySpan<double> GetDoubleArray(ReadOnlySpan<double> defaultValue)
        {
            var entry = NtCore.GetEntryValue(Handle);
            if (entry.Type == NtType.DoubleArray)
            {
                return entry.Data.VDoubleArray.AsSpan();
            }
            return defaultValue;
        }

        public ReadOnlySpan<string> GetStringArray(ReadOnlySpan<string> defaultValue)
        {
            var entry = NtCore.GetEntryValue(Handle);
            if (entry.Type == NtType.StringArray)
            {
                return entry.Data.VStringArray.AsSpan();
            }
            return defaultValue;
        }

        private double? GetAsDouble<T>(T value)
        {
            return value switch
            {
                double v => v,
                int v => v,
                short v => v,
                byte v => v,
                long v => v,
                float v => v,
                ushort v => v,
                uint v => v,
                ulong v => v,
                sbyte v => v,
                decimal v => (double)v,
                _ => default(double?),
            };
        }

        public bool SetDefaultValue(in NetworkTableValue value)
        {
            return NtCore.SetDefaultEntryValue(Handle, value.Value);
        }

        public bool SetDefaultValue<T>(T defaultValue)
        {
            switch(defaultValue)
            {
                case bool v:
                    return SetDefaultBoolean(v);
                case string v:
                    return SetDefaultString(v.AsSpan());
                case byte[] v:
                    return SetDefaultRaw(v);
                case bool[] v:
                    return SetDefaultBooleanArray(v);
                case double[] v:
                    return SetDefaultDoubleArray(v);
                case string[] v:
                    return SetDefaultStringArray(v);
                default:
                    var d = GetAsDouble(defaultValue);
                    if (d != null)
                    {
                        return SetDefaultDouble(d.Value);
                    }
                    throw new InvalidOperationException($"Value of type {typeof(T).Name} cannot be put into a table");

            }
        }

        public bool SetDefaultBoolean(bool defaultValue)
        {
            return NtCore.SetDefaultEntryValue(Handle, new RefManagedValue(defaultValue, 0));
        }

        public bool SetDefaultDouble(double defaultValue)
        {
            return NtCore.SetDefaultEntryValue(Handle, new RefManagedValue(defaultValue, 0));
        }

        public bool SetDefaultString(string defaultValue)
        {
            return NtCore.SetDefaultEntryValue(Handle, new RefManagedValue(defaultValue.AsSpan(), 0));
        }

        public bool SetDefaultString(ReadOnlySpan<char> defaultValue)
        {
            return NtCore.SetDefaultEntryValue(Handle, new RefManagedValue(defaultValue, 0));
        }

        public bool SetDefaultRaw(ReadOnlySpan<byte> defaultValue)
        {
            return NtCore.SetDefaultEntryValue(Handle, new RefManagedValue(defaultValue, 0));
        }

        public bool SetDefaultBooleanArray(ReadOnlySpan<bool> defaultValue)
        {
            return NtCore.SetDefaultEntryValue(Handle, new RefManagedValue(defaultValue, 0));
        }

        public bool SetDefaultDoubleArray(ReadOnlySpan<double> defaultValue)
        {
            return NtCore.SetDefaultEntryValue(Handle, new RefManagedValue(defaultValue, 0));
        }

        public bool SetDefaultStringArray(ReadOnlySpan<string> defaultValue)
        {
            return NtCore.SetDefaultEntryValue(Handle, new RefManagedValue(defaultValue, 0));
        }

        public bool SetValue(in NetworkTableValue value)
        {
            return NtCore.SetEntryValue(Handle, value.Value);
        }

        public bool SetValue<T>(T value)
        {
            switch (value)
            {
                case bool v:
                    return SetBoolean(v);
                case string v:
                    return SetString(v.AsSpan());
                case byte[] v:
                    return SetRaw(v);
                case bool[] v:
                    return SetBooleanArray(v);
                case double[] v:
                    return SetDoubleArray(v);
                case string[] v:
                    return SetStringArray(v);
                default:
                    var d = GetAsDouble(value);
                    if (d != null)
                    {
                        return SetDouble(d.Value);
                    }
                    throw new InvalidOperationException($"Value of type {typeof(T).Name} cannot be put into a table");

            }
        }

        public bool SetBoolean(bool value)
        {
            return NtCore.SetEntryValue(Handle, new RefManagedValue(value, 0));
        }

        public bool SetDouble(double value)
        {
            return NtCore.SetEntryValue(Handle, new RefManagedValue(value, 0));
        }

        public bool SetString(string value)
        {
            return NtCore.SetEntryValue(Handle, new RefManagedValue(value.AsSpan(), 0));
        }

        public bool SetString(ReadOnlySpan<char> value)
        {
            return NtCore.SetEntryValue(Handle, new RefManagedValue(value, 0));
        }

        public unsafe bool SetStringDirect(byte* str, int len)
        {
            return NtCore.SetEntryStringDirect(Handle, str, len);
        }

        public bool SetRaw(ReadOnlySpan<byte> value)
        {
            return NtCore.SetEntryValue(Handle, new RefManagedValue(value, 0));
        }

        public bool SetBooleanArray(ReadOnlySpan<bool> value)
        {
            return NtCore.SetEntryValue(Handle, new RefManagedValue(value, 0));
        }

        public bool SetDoubleArray(ReadOnlySpan<double> value)
        {
            return NtCore.SetEntryValue(Handle, new RefManagedValue(value, 0));
        }

        public bool SetStringArray(ReadOnlySpan<string> value)
        {
            return NtCore.SetEntryValue(Handle, new RefManagedValue(value, 0));
        }

        public void ForceSetValue(in NetworkTableValue value)
        {
            NtCore.SetDefaultEntryValue(Handle, value.Value);
        }

        public void ForceSetValue<T>(T value)
        {
            switch (value)
            {
                case bool v:
                    ForceSetBoolean(v);
                    break;
                case string v:
                    ForceSetString(v.AsSpan());
                    break;
                case byte[] v:
                    ForceSetRaw(v);
                    break;
                case bool[] v:
                    ForceSetBooleanArray(v);
                    break;
                case double[] v:
                    ForceSetDoubleArray(v);
                    break;
                case string[] v:
                    ForceSetStringArray(v);
                    break;
                default:
                    var d = GetAsDouble(value);
                    if (d != null)
                    {
                        ForceSetDouble(d.Value);
                        break;
                    }
                    throw new InvalidOperationException($"Value of type {typeof(T).Name} cannot be put into a table");

            }
        }

        public void ForceSetBoolean(bool value)
        {
            NtCore.SetEntryTypeValue(Handle, new RefManagedValue(value, 0));
        }

        public void ForceSetDouble(double value)
        {
            NtCore.SetEntryTypeValue(Handle, new RefManagedValue(value, 0));
        }

        public void ForceSetString(string value)
        {
            NtCore.SetEntryTypeValue(Handle, new RefManagedValue(value.AsSpan(), 0));
        }

        public void ForceSetString(ReadOnlySpan<char> value)
        {
            NtCore.SetEntryTypeValue(Handle, new RefManagedValue(value, 0));
        }

        public void ForceSetRaw(ReadOnlySpan<byte> value)
        {
            NtCore.SetEntryTypeValue(Handle, new RefManagedValue(value, 0));
        }

        public void ForceSetBooleanArray(ReadOnlySpan<bool> value)
        {
            NtCore.SetEntryTypeValue(Handle, new RefManagedValue(value, 0));
        }

        public void ForceSetDoubleArray(ReadOnlySpan<double> value)
        {
            NtCore.SetEntryTypeValue(Handle, new RefManagedValue(value, 0));
        }

        public void ForceSetStringArray(ReadOnlySpan<string> value)
        {
            NtCore.SetEntryTypeValue(Handle, new RefManagedValue(value, 0));
        }

        public void SetFlags(EntryFlags flags)
        {
            NtCore.SetEntryFlags(Handle, GetEntryFlags() | flags);
        }

        public void ClearFlags(EntryFlags flags)
        {
            NtCore.SetEntryFlags(Handle, GetEntryFlags() & ~flags);
        }

        public void SetPersistent()
        {
            SetFlags(EntryFlags.Persistent);
        }

        public void ClearPersistent()
        {
            ClearFlags(EntryFlags.Persistent);
        }

        public bool IsPersistent()
        {
            return (GetEntryFlags() & EntryFlags.Persistent) != 0;
        }

        public void Delete()
        {
            NtCore.DeleteEntry(Handle);
        }

        public void CreateRpc(RpcAnswerDelegate callback)
        {
            Instance.CreateRpc(this, callback);
        }

        public RpcCall CallRpc(Span<byte> @params)
        {
            return new RpcCall(this, NtCore.CallRpc(Handle, @params));
        }

        public NtEntryListener AddListener(EntryNotificationDelegate listener, NotifyFlags flags)
        {
            return Instance.AddEntryListener(this, listener, flags);
        }

        public void RemoveListener(NtEntryListener listener)
        {
            Instance.RemoveEntryListener(listener);
        }

        //TODO : Equals
    }
}
