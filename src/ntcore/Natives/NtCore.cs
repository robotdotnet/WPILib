using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using WPIUtil;
using WPIUtil.NativeUtilities;

[assembly: InternalsVisibleTo("NetworkTable.Test")]

namespace NetworkTables.Natives
{
#pragma warning disable CA1062 // Validate arguments of public methods

    /// <summary>
    /// Raw NT Core access function
    /// </summary>
    [NativeInterface(typeof(INtCore))]
    public static class NtCore
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static INtCore m_ntcore;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        private unsafe readonly static char* NullTerminator;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CA1810 // Initialize reference type static fields inline
        static NtCore()
#pragma warning restore CA1810 // Initialize reference type static fields inline
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            unsafe
            {
                NullTerminator = (char*)Marshal.AllocHGlobal(sizeof(char));
                *NullTerminator = '\0';
            }
        }

        public static void Initialize()
        {
            if (m_ntcore != null) return;
            NativeInterfaceInitializer.LoadAndInitializeNativeTypes(typeof(NtCore).Assembly,
                "ntcore",
                out var generator);
        }


        private static Span<T> GetSpanOrBuffer<T>(Span<T> store, int length)
        {
            return store.Length >= length ? store.Slice(0, length) : new T[length];
        }

        public static ulong Now()
        {
            return m_ntcore.NT_Now();
        }

        public static NtInst GetDefaultInstance()
        {
            return m_ntcore.NT_GetDefaultInstance();
        }

        public static NtInst CreateInstance()
        {
            return m_ntcore.NT_CreateInstance();
        }

        public static void DestroyInstance(NtInst inst)
        {
            m_ntcore.NT_DestroyInstance(inst);
        }

        public static NtInst GetInstanceFromHandle(NtHandle handle)
        {
            return m_ntcore.NT_GetInstanceFromHandle(handle);
        }

        public static unsafe NtEntry GetEntry(NtInst inst, ReadOnlySpan<char> name)
        {
            fixed (char* p = name.IsEmpty ? new ReadOnlySpan<char>(NullTerminator, 1) : name)
            {
                var dLen = Encoding.UTF8.GetByteCount(p, name.Length);
                Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                fixed (byte* d = dSpan)
                {
                    Encoding.UTF8.GetBytes(p, name.Length, d, dLen);
                    return m_ntcore.NT_GetEntry(inst, d, (UIntPtr)dLen);
                }
            }
        }

        public static unsafe NtEntry GetEntry(NtInst inst, string name)
        {
            fixed (char* p = name)
            {
                var dLen = Encoding.UTF8.GetByteCount(p, name.Length);
                Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                fixed (byte* d = dSpan)
                {
                    Encoding.UTF8.GetBytes(p, name.Length, d, dLen);
                    return m_ntcore.NT_GetEntry(inst, d, (UIntPtr)dLen);
                }
            }
        }

        public static unsafe int GetEntryCount(NtInst inst, ReadOnlySpan<char> prefix, NtType types)
        {
            fixed (char* p = prefix.IsEmpty ? new ReadOnlySpan<char>(NullTerminator, 1) : prefix)
            {
                var dLen = Encoding.UTF8.GetByteCount(p, prefix.Length);
                Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                fixed (byte* d = dSpan)
                {
                    Encoding.UTF8.GetBytes(p, prefix.Length, d, dLen);
                    UIntPtr count = UIntPtr.Zero;
                    var data = m_ntcore.NT_GetEntries(inst, d, (UIntPtr)dLen, (uint)types, &count);
                    int len = (int)count;
                    m_ntcore.NT_DisposeEntryArray(data, count);
                    return len;
                }
            }
        }

        public static unsafe int GetEntryCount(NtInst inst, string prefix, NtType types)
        {
            fixed (char* p = prefix)
            {

                var dLen = Encoding.UTF8.GetByteCount(p, prefix.Length);
                Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                fixed (byte* d = dSpan)
                {
                    Encoding.UTF8.GetBytes(p, prefix.Length, d, dLen);
                    UIntPtr count = UIntPtr.Zero;
                    var data = m_ntcore.NT_GetEntries(inst, d, (UIntPtr)dLen, (uint)types, &count);
                    int len = (int)count;
                    m_ntcore.NT_DisposeEntryArray(data, count);
                    return len;
                }
            }
        }

        public static unsafe ReadOnlySpan<NtEntry> GetEntries(NtInst inst, ReadOnlySpan<char> prefix, NtType types, Span<NtEntry> store)
        {
            fixed (char* p = prefix.IsEmpty ? new ReadOnlySpan<char>(NullTerminator, 1) : prefix)
            {
                var dLen = Encoding.UTF8.GetByteCount(p, prefix.Length);
                Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                fixed (byte* d = dSpan)
                {
                    Encoding.UTF8.GetBytes(p, prefix.Length, d, dLen);
                    UIntPtr count = UIntPtr.Zero;
                    var data = m_ntcore.NT_GetEntries(inst, d, (UIntPtr)dLen, (uint)types, &count);
                    int len = (int)count;
                    Span<NtEntry> entries = GetSpanOrBuffer(store, len);
                    new Span<NtEntry>(data, len).CopyTo(entries);
                    m_ntcore.NT_DisposeEntryArray(data, count);
                    return entries;
                }
            }
        }

        public static unsafe ReadOnlySpan<NtEntry> GetEntries(NtInst inst, string prefix, NtType types, Span<NtEntry> store)
        {
            fixed (char* p = prefix)
            {
                var dLen = Encoding.UTF8.GetByteCount(p, prefix.Length);
                Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                fixed (byte* d = dSpan)
                {
                    Encoding.UTF8.GetBytes(p, prefix.Length, d, dLen);
                    UIntPtr count = UIntPtr.Zero;
                    var data = m_ntcore.NT_GetEntries(inst, d, (UIntPtr)dLen, (uint)types, &count);
                    int len = (int)count;
                    Span<NtEntry> entries = GetSpanOrBuffer(store, len);
                    new Span<NtEntry>(data, len).CopyTo(entries);
                    m_ntcore.NT_DisposeEntryArray(data, count);
                    return entries;
                }
            }
        }

        public static unsafe ReadOnlySpan<NetworkTableEntry> GetEntriesManaged(NetworkTableInstance inst, ReadOnlySpan<char> prefix, NtType types, Span<NetworkTableEntry> store)
        {
            fixed (char* p = prefix.IsEmpty ? new ReadOnlySpan<char>(NullTerminator, 1) : prefix)
            {
                var dLen = Encoding.UTF8.GetByteCount(p, prefix.Length);
                Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                fixed (byte* d = dSpan)
                {
                    Encoding.UTF8.GetBytes(p, prefix.Length, d, dLen);
                    UIntPtr count = UIntPtr.Zero;
                    var data = m_ntcore.NT_GetEntries(inst.Handle, d, (UIntPtr)dLen, (uint)types, &count);
                    int len = (int)count;
                    Span<NetworkTableEntry> entries = GetSpanOrBuffer(store, len);
                    for (int i = 0; i < entries.Length; i++)
                    {
                        entries[i] = new NetworkTableEntry(inst, data[i]);
                    }
                    m_ntcore.NT_DisposeEntryArray(data, count);
                    return entries;
                }
            }
        }

        public static unsafe ReadOnlySpan<NetworkTableEntry> GetEntriesManaged(NetworkTableInstance inst, string prefix, NtType types, Span<NetworkTableEntry> store)
        {
            fixed (char* p = prefix)
            {
                var dLen = Encoding.UTF8.GetByteCount(p, prefix.Length);
                Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                fixed (byte* d = dSpan)
                {
                    Encoding.UTF8.GetBytes(p, prefix.Length, d, dLen);
                    UIntPtr count = UIntPtr.Zero;
                    var data = m_ntcore.NT_GetEntries(inst.Handle, d, (UIntPtr)dLen, (uint)types, &count);
                    int len = (int)count;
                    Span<NetworkTableEntry> entries = GetSpanOrBuffer(store, len);
                    for (int i = 0; i < entries.Length; i++)
                    {
                        entries[i] = new NetworkTableEntry(inst, data[i]);
                    }
                    m_ntcore.NT_DisposeEntryArray(data, count);
                    return entries;
                }
            }
        }

        public static unsafe string GetEntryName(NtEntry entry)
        {
            UIntPtr len = UIntPtr.Zero;
            var data = m_ntcore.NT_GetEntryName(entry, &len);
            string ret = UTF8String.ReadUTF8String(data, (int)len);
            m_ntcore.NT_FreeCharArray(data);
            return ret;
        }

        public static NtType GetEntryType(NtEntry entry)
        {
            return m_ntcore.NT_GetEntryType(entry);
        }

        public static ulong GetEntryLastChange(NtEntry entry)
        {
            return m_ntcore.NT_GetEntryLastChange(entry);
        }

        public static unsafe ManagedValue GetEntryValue(NtEntry entry)
        {
            NtValue value = new NtValue();
            m_ntcore.NT_GetEntryValue(entry, &value);
            var ret = new ManagedValue(&value);
            m_ntcore.NT_DisposeValue(&value);
            return ret;
        }

        internal static unsafe bool SetDefaultEntryValue(NtEntry entry, in ManagedValue value)
        {
            NtValue v = new NtValue
            {
                type = value.Type
            };

            switch (value.Type)
            {
                case NtType.Boolean:
                    v.data.v_boolean = value.Data.VBoolean;
                    return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                case NtType.Double:
                    v.data.v_double = value.Data.VDouble;
                    return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                case NtType.String:
                    fixed (char* p = value.Data.VString.AsSpan())
                    {
                        var dLen = Encoding.UTF8.GetByteCount(p, value.Data.VString!.Length);
                        Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                        fixed (byte* d = dSpan)
                        {
                            Encoding.UTF8.GetBytes(p, value.Data.VString.Length, d, dLen);
                            v.data.v_string.str = d;
                            v.data.v_string.len = (UIntPtr)dLen;
                            return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                        }
                    }
                case NtType.Raw:
                case NtType.Rpc:
                    fixed (byte* p = value.Data.VRaw.AsSpan())
                    {
                        v.data.v_raw.len = (UIntPtr)value.Data.VRaw!.Length;
                        v.data.v_raw.str = p;
                        return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                    }
                case NtType.BooleanArray:
                    fixed (bool* p = value.Data.VBooleanArray.AsSpan())
                    {
                        var len = value.Data.VBooleanArray!.Length;
                        Span<NtBool> dSpan = len <= 256 ? stackalloc NtBool[len == 0 ? 1 : 0] : new NtBool[len];
                        fixed (NtBool* d = dSpan)
                        {
                            v.data.arr_boolean.arr = d;
                            v.data.arr_boolean.len = (UIntPtr)len;
                            return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                        }
                    }
                case NtType.DoubleArray:
                    fixed (byte* p = value.Data.VRaw.AsSpan())
                    {
                        v.data.v_raw.len = (UIntPtr)value.Data.VRaw!.Length;
                        v.data.v_raw.str = p;
                        return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                    }
                case NtType.StringArray:
                    v.data.arr_string.arr = (NtString*)Marshal.AllocHGlobal(value.Data.VStringArray!.Length * sizeof(NtString));
                    v.data.arr_string.len = (UIntPtr)value.Data.VStringArray.Length;
                    var sSpan = value.Data.VStringArray.AsSpan();
                    for (int i = 0; i < sSpan.Length; i++)
                    {
                        Utilities.CreateNtString(sSpan[i], &v.data.arr_string.arr[i]);
                    }
                    var ret = m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                    int sLen = (int)v.data.arr_string.len;
                    for (int i = 0; i < sLen; i++)
                    {
                        Utilities.DisposeNtString(&v.data.arr_string.arr[i]);
                    }
                    Marshal.FreeHGlobal((IntPtr)v.data.arr_string.arr);
                    return ret;
                default:
                    return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
            }
        }

        public static unsafe bool SetEntryValue(NtEntry entry, in ManagedValue value)
        {
            NtValue v = new NtValue
            {
                type = value.Type
            };

            switch (value.Type)
            {
                case NtType.Boolean:
                    v.data.v_boolean = value.Data.VBoolean;
                    return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                case NtType.Double:
                    v.data.v_double = value.Data.VDouble;
                    return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                case NtType.String:
                    fixed (char* p = value.Data.VString.AsSpan())
                    {
                        var dLen = Encoding.UTF8.GetByteCount(p, value.Data.VString!.Length);
                        Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                        fixed (byte* d = dSpan)
                        {
                            Encoding.UTF8.GetBytes(p, value.Data.VString.Length, d, dLen);
                            v.data.v_string.str = d;
                            v.data.v_string.len = (UIntPtr)dLen;
                            return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                        }
                    }
                case NtType.Raw:
                case NtType.Rpc:
                    fixed (byte* p = value.Data.VRaw.AsSpan())
                    {
                        v.data.v_raw.len = (UIntPtr)value.Data.VRaw!.Length;
                        v.data.v_raw.str = p;
                        return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                    }
                case NtType.BooleanArray:
                    fixed (bool* p = value.Data.VBooleanArray.AsSpan())
                    {
                        var len = value.Data.VBooleanArray!.Length;
                        Span<NtBool> dSpan = len <= 256 ? stackalloc NtBool[len == 0 ? 1 : 0] : new NtBool[len];
                        fixed (NtBool* d = dSpan)
                        {
                            v.data.arr_boolean.arr = d;
                            v.data.arr_boolean.len = (UIntPtr)len;
                            return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                        }
                    }
                case NtType.DoubleArray:
                    fixed (byte* p = value.Data.VRaw.AsSpan())
                    {
                        v.data.v_raw.len = (UIntPtr)value.Data.VRaw!.Length;
                        v.data.v_raw.str = p;
                        return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                    }
                case NtType.StringArray:
                    v.data.arr_string.arr = (NtString*)Marshal.AllocHGlobal(value.Data.VStringArray!.Length * sizeof(NtString));
                    v.data.arr_string.len = (UIntPtr)value.Data.VStringArray.Length;
                    var sSpan = value.Data.VStringArray.AsSpan();
                    for (int i = 0; i < sSpan.Length; i++)
                    {
                        Utilities.CreateNtString(sSpan[i], &v.data.arr_string.arr[i]);
                    }
                    var ret = m_ntcore.NT_SetEntryValue(entry, &v).Get();
                    int sLen = (int)v.data.arr_string.len;
                    for (int i = 0; i < sLen; i++)
                    {
                        Utilities.DisposeNtString(&v.data.arr_string.arr[i]);
                    }
                    Marshal.FreeHGlobal((IntPtr)v.data.arr_string.arr);
                    return ret;
                default:
                    return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
            }
        }

        internal static unsafe void SetEntryTypeValue(NtEntry entry, in ManagedValue value)
        {
            NtValue v = new NtValue
            {
                type = value.Type
            };

            switch (value.Type)
            {
                case NtType.Boolean:
                    v.data.v_boolean = value.Data.VBoolean;
                    m_ntcore.NT_SetEntryTypeValue(entry, &v);
                    break;
                case NtType.Double:
                    v.data.v_double = value.Data.VDouble;
                    m_ntcore.NT_SetEntryTypeValue(entry, &v);
                    break;
                case NtType.String:
                    fixed (char* p = value.Data.VString.AsSpan())
                    {
                        var dLen = Encoding.UTF8.GetByteCount(p, value.Data.VString!.Length);
                        Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                        fixed (byte* d = dSpan)
                        {
                            Encoding.UTF8.GetBytes(p, value.Data.VString.Length, d, dLen);
                            v.data.v_string.str = d;
                            v.data.v_string.len = (UIntPtr)dLen;
                            m_ntcore.NT_SetEntryTypeValue(entry, &v);
                            break;
                        }
                    }
                case NtType.Raw:
                case NtType.Rpc:
                    fixed (byte* p = value.Data.VRaw.AsSpan())
                    {
                        v.data.v_raw.len = (UIntPtr)value.Data.VRaw!.Length;
                        v.data.v_raw.str = p;
                        m_ntcore.NT_SetEntryTypeValue(entry, &v);
                        break;
                    }
                case NtType.BooleanArray:
                    fixed (bool* p = value.Data.VBooleanArray.AsSpan())
                    {
                        var len = value.Data.VBooleanArray!.Length;
                        Span<NtBool> dSpan = len <= 256 ? stackalloc NtBool[len == 0 ? 1 : 0] : new NtBool[len];
                        fixed (NtBool* d = dSpan)
                        {
                            v.data.arr_boolean.arr = d;
                            v.data.arr_boolean.len = (UIntPtr)len;
                            m_ntcore.NT_SetEntryTypeValue(entry, &v);
                            break;
                        }
                    }
                case NtType.DoubleArray:
                    fixed (byte* p = value.Data.VRaw.AsSpan())
                    {
                        v.data.v_raw.len = (UIntPtr)value.Data.VRaw!.Length;
                        v.data.v_raw.str = p;
                        m_ntcore.NT_SetEntryTypeValue(entry, &v);
                        break;
                    }
                case NtType.StringArray:
                    v.data.arr_string.arr = (NtString*)Marshal.AllocHGlobal(value.Data.VStringArray!.Length * sizeof(NtString));
                    v.data.arr_string.len = (UIntPtr)value.Data.VStringArray.Length;
                    var sSpan = value.Data.VStringArray.AsSpan();
                    for (int i = 0; i < sSpan.Length; i++)
                    {
                        Utilities.CreateNtString(sSpan[i], &v.data.arr_string.arr[i]);
                    }
                    m_ntcore.NT_SetEntryTypeValue(entry, &v);
                    int sLen = (int)v.data.arr_string.len;
                    for (int i = 0; i < sLen; i++)
                    {
                        Utilities.DisposeNtString(&v.data.arr_string.arr[i]);
                    }
                    Marshal.FreeHGlobal((IntPtr)v.data.arr_string.arr);
                    break;
                default:
                    m_ntcore.NT_SetDefaultEntryValue(entry, &v);
                    break;
            }
        }


        internal static unsafe bool SetDefaultEntryValue(NtEntry entry, in RefManagedValue value)
        {
            NtValue v = new NtValue
            {
                type = value.Type
            };

            switch (value.Type)
            {
                case NtType.Boolean:
                    v.data.v_boolean = value.Data.VBoolean;
                    return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                case NtType.Double:
                    v.data.v_double = value.Data.VDouble;
                    return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                case NtType.String:
                    fixed (char* p = value.Data.VString)
                    {
                        var dLen = Encoding.UTF8.GetByteCount(p, value.Data.VString.Length);
                        Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                        fixed (byte* d = dSpan)
                        {
                            Encoding.UTF8.GetBytes(p, value.Data.VString.Length, d, dLen);
                            v.data.v_string.str = d;
                            v.data.v_string.len = (UIntPtr)dLen;
                            return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                        }
                    }
                case NtType.Raw:
                case NtType.Rpc:
                    fixed (byte* p = value.Data.VRaw)
                    {
                        v.data.v_raw.len = (UIntPtr)value.Data.VRaw.Length;
                        v.data.v_raw.str = p;
                        return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                    }
                case NtType.BooleanArray:
                    fixed (bool* p = value.Data.VBooleanArray)
                    {
                        var len = value.Data.VBooleanArray.Length;
                        Span<NtBool> dSpan = len <= 256 ? stackalloc NtBool[len == 0 ? 1 : 0] : new NtBool[len];
                        fixed (NtBool* d = dSpan)
                        {
                            v.data.arr_boolean.arr = d;
                            v.data.arr_boolean.len = (UIntPtr)len;
                            return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                        }
                    }
                case NtType.DoubleArray:
                    fixed (byte* p = value.Data.VRaw)
                    {
                        v.data.v_raw.len = (UIntPtr)value.Data.VRaw.Length;
                        v.data.v_raw.str = p;
                        return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                    }
                case NtType.StringArray:
                    v.data.arr_string.arr = (NtString*)Marshal.AllocHGlobal(value.Data.VStringArray.Length * sizeof(NtString));
                    v.data.arr_string.len = (UIntPtr)value.Data.VStringArray.Length;
                    var sSpan = value.Data.VStringArray;
                    for (int i = 0; i < sSpan.Length; i++)
                    {
                        Utilities.CreateNtString(sSpan[i], &v.data.arr_string.arr[i]);
                    }
                    var ret = m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
                    int sLen = (int)v.data.arr_string.len;
                    for (int i = 0; i < sLen; i++)
                    {
                        Utilities.DisposeNtString(&v.data.arr_string.arr[i]);
                    }
                    Marshal.FreeHGlobal((IntPtr)v.data.arr_string.arr);
                    return ret;
                default:
                    return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
            }
        }


        internal static unsafe bool SetEntryStringDirect(NtEntry entry, byte* str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            NtValue value = new NtValue
            {
                type = NtType.String,
                last_change = 0
            };

            value.data.v_string.str = str;
            value.data.v_string.len = (UIntPtr)len;


            return m_ntcore.NT_SetEntryValue(entry, &value).Get();
        }

        internal static unsafe bool SetEntryValue(NtEntry entry, in RefManagedValue value)
        {
            NtValue v = new NtValue
            {
                type = value.Type,
                last_change = value.LastChange
            };

            switch (value.Type)
            {
                case NtType.Boolean:
                    v.data.v_boolean = value.Data.VBoolean;
                    return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                case NtType.Double:
                    v.data.v_double = value.Data.VDouble;
                    return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                case NtType.String:
                    fixed (char* p = value.Data.VString)
                    {
                        if (p == null)
                        {
                            v.data.v_string.str = null;
                            v.data.v_string.len = (UIntPtr)0;
                            return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                        }
                        var dLen = Encoding.UTF8.GetByteCount(p, value.Data.VString.Length);
                        Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                        fixed (byte* d = dSpan)
                        {
                            Encoding.UTF8.GetBytes(p, value.Data.VString.Length, d, dLen);
                            v.data.v_string.str = d;
                            v.data.v_string.len = (UIntPtr)dLen;
                            return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                        }
                    }
                case NtType.Raw:
                case NtType.Rpc:
                    fixed (byte* p = value.Data.VRaw)
                    {
                        v.data.v_raw.len = (UIntPtr)value.Data.VRaw.Length;
                        v.data.v_raw.str = p;
                        return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                    }
                case NtType.BooleanArray:
                    fixed (bool* p = value.Data.VBooleanArray)
                    {
                        var len = value.Data.VBooleanArray.Length;
                        Span<NtBool> dSpan = len <= 256 ? stackalloc NtBool[len == 0 ? 1 : 0] : new NtBool[len];
                        fixed (NtBool* d = dSpan)
                        {
                            v.data.arr_boolean.arr = d;
                            v.data.arr_boolean.len = (UIntPtr)len;
                            return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                        }
                    }
                case NtType.DoubleArray:
                    fixed (byte* p = value.Data.VRaw)
                    {
                        v.data.v_raw.len = (UIntPtr)value.Data.VRaw.Length;
                        v.data.v_raw.str = p;
                        return m_ntcore.NT_SetEntryValue(entry, &v).Get();
                    }
                case NtType.StringArray:
                    v.data.arr_string.arr = (NtString*)Marshal.AllocHGlobal(value.Data.VStringArray.Length * sizeof(NtString));
                    v.data.arr_string.len = (UIntPtr)value.Data.VStringArray.Length;
                    var sSpan = value.Data.VStringArray;
                    for (int i = 0; i < sSpan.Length; i++)
                    {
                        Utilities.CreateNtString(sSpan[i], &v.data.arr_string.arr[i]);
                    }
                    var ret = m_ntcore.NT_SetEntryValue(entry, &v).Get();
                    int sLen = (int)v.data.arr_string.len;
                    for (int i = 0; i < sLen; i++)
                    {
                        Utilities.DisposeNtString(&v.data.arr_string.arr[i]);
                    }
                    Marshal.FreeHGlobal((IntPtr)v.data.arr_string.arr);
                    return ret;
                default:
                    return m_ntcore.NT_SetDefaultEntryValue(entry, &v).Get();
            }
        }

        internal static unsafe void SetEntryTypeValue(NtEntry entry, in RefManagedValue value)
        {
            NtValue v = new NtValue
            {
                type = value.Type
            };

            switch (value.Type)
            {
                case NtType.Boolean:
                    v.data.v_boolean = value.Data.VBoolean;
                    m_ntcore.NT_SetEntryTypeValue(entry, &v);
                    break;
                case NtType.Double:
                    v.data.v_double = value.Data.VDouble;
                    m_ntcore.NT_SetEntryTypeValue(entry, &v);
                    break;
                case NtType.String:
                    fixed (char* p = value.Data.VString)
                    {
                        if (p == null)
                        {
                            v.data.v_string.str = null;
                            v.data.v_string.len = (UIntPtr)0;
                            m_ntcore.NT_SetEntryValue(entry, &v).Get();
                            return;
                        }
                        var dLen = Encoding.UTF8.GetByteCount(p, value.Data.VString.Length);
                        Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                        fixed (byte* d = dSpan)
                        {
                            Encoding.UTF8.GetBytes(p, value.Data.VString.Length, d, dLen);
                            v.data.v_string.str = d;
                            v.data.v_string.len = (UIntPtr)dLen;
                            m_ntcore.NT_SetEntryTypeValue(entry, &v);
                            break;
                        }
                    }
                case NtType.Raw:
                case NtType.Rpc:
                    fixed (byte* p = value.Data.VRaw)
                    {
                        v.data.v_raw.len = (UIntPtr)value.Data.VRaw.Length;
                        v.data.v_raw.str = p;
                        m_ntcore.NT_SetEntryTypeValue(entry, &v);
                        break;
                    }
                case NtType.BooleanArray:
                    fixed (bool* p = value.Data.VBooleanArray)
                    {
                        var len = value.Data.VBooleanArray.Length;
                        Span<NtBool> dSpan = len <= 256 ? stackalloc NtBool[len == 0 ? 1 : 0] : new NtBool[len];
                        fixed (NtBool* d = dSpan)
                        {
                            v.data.arr_boolean.arr = d;
                            v.data.arr_boolean.len = (UIntPtr)len;
                            m_ntcore.NT_SetEntryTypeValue(entry, &v);
                            break;
                        }
                    }
                case NtType.DoubleArray:
                    fixed (byte* p = value.Data.VRaw)
                    {
                        v.data.v_raw.len = (UIntPtr)value.Data.VRaw.Length;
                        v.data.v_raw.str = p;
                        m_ntcore.NT_SetEntryTypeValue(entry, &v);
                        break;
                    }
                case NtType.StringArray:
                    v.data.arr_string.arr = (NtString*)Marshal.AllocHGlobal(value.Data.VStringArray.Length * sizeof(NtString));
                    v.data.arr_string.len = (UIntPtr)value.Data.VStringArray.Length;
                    var sSpan = value.Data.VStringArray;
                    for (int i = 0; i < sSpan.Length; i++)
                    {
                        Utilities.CreateNtString(sSpan[i], &v.data.arr_string.arr[i]);
                    }
                    m_ntcore.NT_SetEntryTypeValue(entry, &v);
                    int sLen = (int)v.data.arr_string.len;
                    for (int i = 0; i < sLen; i++)
                    {
                        Utilities.DisposeNtString(&v.data.arr_string.arr[i]);
                    }
                    Marshal.FreeHGlobal((IntPtr)v.data.arr_string.arr);
                    break;
                default:
                    m_ntcore.NT_SetDefaultEntryValue(entry, &v);
                    break;
            }
        }

        public static void SetEntryFlags(NtEntry entry, EntryFlags flags)
        {
            m_ntcore.NT_SetEntryFlags(entry, (uint)flags);
        }

        public static EntryFlags GetEntryFlags(NtEntry entry)
        {
            return (EntryFlags)m_ntcore.NT_GetEntryFlags(entry);
        }

        public static void DeleteEntry(NtEntry entry)
        {
            m_ntcore.NT_DeleteEntry(entry);
        }

        public static void DeleteAllEntries(NtInst inst)
        {
            m_ntcore.NT_DeleteAllEntries(inst);
        }

        public static unsafe ReadOnlySpan<EntryInfo> GetEntryInfo(NetworkTableInstance inst, ReadOnlySpan<char> prefix, NtType types, Span<EntryInfo> store)
        {
            fixed (char* p = prefix.IsEmpty ? new ReadOnlySpan<char>(NullTerminator, 1) : prefix)
            {
                var dLen = Encoding.UTF8.GetByteCount(p, prefix.Length);
                Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                fixed (byte* d = dSpan)
                {
                    Encoding.UTF8.GetBytes(p, prefix.Length, d, dLen);
                    UIntPtr count = UIntPtr.Zero;
                    var data = m_ntcore.NT_GetEntryInfo(inst.Handle, d, (UIntPtr)dLen, (uint)types, &count);
                    int len = (int)count;
                    Span<EntryInfo> entries = GetSpanOrBuffer(store, len);
                    for (int i = 0; i < entries.Length; i++)
                    {
                        entries[i] = new EntryInfo(inst, &data[i]);
                    }
                    m_ntcore.NT_DisposeEntryInfoArray(data, count);
                    return entries;
                }
            }
        }

        public static unsafe ReadOnlySpan<EntryInfo> GetEntryInfo(NetworkTableInstance inst, string prefix, NtType types, Span<EntryInfo> store)
        {
            fixed (char* p = prefix)
            {
                var dLen = Encoding.UTF8.GetByteCount(p, prefix.Length);
                Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                fixed (byte* d = dSpan)
                {
                    Encoding.UTF8.GetBytes(p, prefix.Length, d, dLen);
                    UIntPtr count = UIntPtr.Zero;
                    var data = m_ntcore.NT_GetEntryInfo(inst.Handle, d, (UIntPtr)dLen, (uint)types, &count);
                    int len = (int)count;
                    Span<EntryInfo> entries = GetSpanOrBuffer(store, len);
                    for (int i = 0; i < entries.Length; i++)
                    {
                        entries[i] = new EntryInfo(inst, &data[i]);
                    }
                    m_ntcore.NT_DisposeEntryInfoArray(data, count);
                    return entries;
                }
            }
        }

        public static unsafe EntryInfo? GetEntryInfoHandle(NetworkTableInstance inst, NtEntry entry)
        {
            NtEntryInfo info = new NtEntryInfo();
            var ret = m_ntcore.NT_GetEntryInfoHandle(entry, &info);
            if (!ret.Get())
            {
                m_ntcore.NT_DisposeEntryInfo(&info);
                return null;
            }
            EntryInfo infoM = new EntryInfo(inst, &info);
            m_ntcore.NT_DisposeEntryInfo(&info);
            return infoM;
        }

        public static NtEntryListenerPoller CreateEntryListenerPoller(NtInst inst)
        {
            return m_ntcore.NT_CreateEntryListenerPoller(inst);
        }

        internal static unsafe PointerSpan<NtEntryNotification> PollEntryListener(NtEntryListenerPoller poller)
        {
            UIntPtr length = UIntPtr.Zero;
            NtEntryNotification* notifications = m_ntcore.NT_PollEntryListener(poller, &length);
            return new PointerSpan<NtEntryNotification>(notifications, (int)length);
        }

        internal static unsafe PointerSpan<NtEntryNotification> PollEntryListenerTimeout(NtEntryListenerPoller poller, double timeout, out bool timedOut)
        {
            UIntPtr length = UIntPtr.Zero;
            NtBool timedOutNt = false;
            NtEntryNotification* notifications = m_ntcore.NT_PollEntryListenerTimeout(poller, &length, timeout, &timedOutNt);
            timedOut = timedOutNt.Get();
            return new PointerSpan<NtEntryNotification>(notifications, (int)length);
        }

        internal static unsafe void DisposeEntryListenerSpan(PointerSpan<NtEntryNotification> listeners)
        {
            m_ntcore.NT_DisposeEntryNotificationArray(listeners.Pointer, (UIntPtr)listeners.Length);
        }

        public static unsafe NtEntryListener AddPolledEntryListener(NtEntryListenerPoller poller, string prefix, NotifyFlags flags)
        {
            fixed (char* p = prefix)
            {
                var dLen = Encoding.UTF8.GetByteCount(p, prefix.Length);
                Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                fixed (byte* d = dSpan)
                {
                    Encoding.UTF8.GetBytes(p, prefix.Length, d, dLen);
                    return m_ntcore.NT_AddPolledEntryListener(poller, d, (UIntPtr)dLen, (uint)flags);
                }
            }

        }

        public static unsafe NtEntryListener AddPolledEntryListener(NtEntryListenerPoller poller, ReadOnlySpan<char> prefix, NotifyFlags flags)
        {
            fixed (char* p = prefix.IsEmpty ? new ReadOnlySpan<char>(NullTerminator, 1) : prefix)
            {
                var dLen = Encoding.UTF8.GetByteCount(p, prefix.Length);
                Span<byte> dSpan = dLen <= 256 ? stackalloc byte[dLen == 0 ? 1 : dLen] : new byte[dLen];
                fixed (byte* d = dSpan)
                {
                    Encoding.UTF8.GetBytes(p, prefix.Length, d, dLen);
                    return m_ntcore.NT_AddPolledEntryListener(poller, d, (UIntPtr)dLen, (uint)flags);
                }
            }

        }

        public static unsafe NtEntryListener AddPolledEntryListener(NtEntryListenerPoller poller, NetworkTableEntry entry, NotifyFlags flags)
        {
            return m_ntcore.NT_AddPolledEntryListenerSingle(poller, entry.Handle, (uint)flags);
        }

        public static unsafe void RemoveEntryListener(NtEntryListener listener)
        {
            m_ntcore.NT_RemoveEntryListener(listener);
        }

        public static unsafe bool WaitForEntryListenerQueue(NtInst inst, double timeout)
        {
            return m_ntcore.NT_WaitForEntryListenerQueue(inst, timeout).Get();
        }

        public static unsafe void CancelPollEntryListener(NtEntryListenerPoller poller)
        {
            m_ntcore.NT_CancelPollEntryListener(poller);
        }

        public static void DestroyEntryListenerPoller(NtEntryListenerPoller poller)
        {
            m_ntcore.NT_DestroyEntryListenerPoller(poller);
        }

        public static NtConnectionListenerPoller CreateConnectionListenerPoller(NtInst inst)
        {
            return m_ntcore.NT_CreateConnectionListenerPoller(inst);
        }

        internal static unsafe PointerSpan<NtConnectionNotification> PollConnectionListener(NtConnectionListenerPoller poller)
        {
            UIntPtr length = UIntPtr.Zero;
            NtConnectionNotification* notifications = m_ntcore.NT_PollConnectionListener(poller, &length);
            return new PointerSpan<NtConnectionNotification>(notifications, (int)length);
        }

        internal static unsafe PointerSpan<NtConnectionNotification> PollConnectionListenerTimeout(NtConnectionListenerPoller poller, double timeout, out bool timedOut)
        {
            UIntPtr length = UIntPtr.Zero;
            NtBool timedOutNt = false;
            NtConnectionNotification* notifications = m_ntcore.NT_PollConnectionListenerTimeout(poller, &length, timeout, &timedOutNt);
            timedOut = timedOutNt.Get();
            return new PointerSpan<NtConnectionNotification>(notifications, (int)length);
        }

        internal static unsafe void DisposeConnectionListenerSpan(PointerSpan<NtConnectionNotification> listeners)
        {
            m_ntcore.NT_DisposeConnectionNotificationArray(listeners.Pointer, (UIntPtr)listeners.Length);
        }

        public static unsafe NtConnectionListener AddPolledConnectionListener(NtConnectionListenerPoller poller, bool immediateNotify)
        {
            return m_ntcore.NT_AddPolledConnectionListener(poller, immediateNotify);
        }

        public static unsafe void RemoveConnectionListener(NtConnectionListener listener)
        {
            m_ntcore.NT_RemoveConnectionListener(listener);
        }

        public static unsafe bool WaitForConnectionListenerQueue(NtInst inst, double timeout)
        {
            return m_ntcore.NT_WaitForConnectionListenerQueue(inst, timeout).Get();
        }

        public static unsafe void CancelPollConnectionListener(NtConnectionListenerPoller poller)
        {
            m_ntcore.NT_CancelPollConnectionListener(poller);
        }

        public static void DestroyConnectionListenerPoller(NtConnectionListenerPoller poller)
        {
            m_ntcore.NT_DestroyConnectionListenerPoller(poller);
        }

        public static NtRpcCallPoller CreateRpcCallPoller(NtInst inst)
        {
            return m_ntcore.NT_CreateRpcCallPoller(inst);
        }

        internal static unsafe PointerSpan<NtRpcAnswer> PollRpc(NtRpcCallPoller poller)
        {
            UIntPtr length = UIntPtr.Zero;
            NtRpcAnswer* notifications = m_ntcore.NT_PollRpc(poller, &length);
            return new PointerSpan<NtRpcAnswer>(notifications, (int)length);
        }

        internal static unsafe void DisposeRpcAnswerSpan(PointerSpan<NtRpcAnswer> answers)
        {
            m_ntcore.NT_DisposeRpcAnswerArray(answers.Pointer, (UIntPtr)answers.Length);
        }

        internal static unsafe PointerSpan<NtRpcAnswer> PollRpcTimeout(NtRpcCallPoller poller, double timeout, out bool timedOut)
        {
            UIntPtr length = UIntPtr.Zero;
            NtBool timedOutNt = false;
            NtRpcAnswer* notifications = m_ntcore.NT_PollRpcTimeout(poller, &length, timeout, &timedOutNt);
            timedOut = timedOutNt.Get();
            return new PointerSpan<NtRpcAnswer>(notifications, (int)length);
        }

        public static unsafe void CreatePolledRpc(NtEntry entry, ReadOnlySpan<byte> def, NtRpcCallPoller poller)
        {
            fixed (byte* p = def)
            {
                m_ntcore.NT_CreatePolledRpc(entry, p, (UIntPtr)def.Length, poller);
            }
        }

        public static unsafe bool WaitForRpcCallQueue(NtInst inst, double timeout)
        {
            return m_ntcore.NT_WaitForRpcCallQueue(inst, timeout).Get();
        }

        public static unsafe void CancelPollRpc(NtRpcCallPoller poller)
        {
            m_ntcore.NT_CancelPollRpc(poller);
        }

        public static void DestroyRpcCallPoller(NtRpcCallPoller poller)
        {
            m_ntcore.NT_DestroyRpcCallPoller(poller);
        }


        public static NtLoggerPoller CreateLoggerPoller(NtInst inst)
        {
            return m_ntcore.NT_CreateLoggerPoller(inst);
        }

        internal static unsafe PointerSpan<NtLogMessage> PollLogger(NtLoggerPoller poller)
        {
            UIntPtr length = UIntPtr.Zero;
            NtLogMessage* notifications = m_ntcore.NT_PollLogger(poller, &length);
            return new PointerSpan<NtLogMessage>(notifications, (int)length);
        }

        internal static unsafe PointerSpan<NtLogMessage> PollLoggerTimeout(NtLoggerPoller poller, double timeout, out bool timedOut)
        {
            UIntPtr length = UIntPtr.Zero;
            NtBool timedOutNt = false;
            NtLogMessage* notifications = m_ntcore.NT_PollLoggerTimeout(poller, &length, timeout, &timedOutNt);
            timedOut = timedOutNt.Get();
            return new PointerSpan<NtLogMessage>(notifications, (int)length);
        }

        internal static unsafe void DisposeLoggerSpan(PointerSpan<NtLogMessage> listeners)
        {
            m_ntcore.NT_DisposeLogMessageArray(listeners.Pointer, (UIntPtr)listeners.Length);
        }

        public static unsafe NtLogger AddPolledLogger(NtLoggerPoller poller, int minLevel, int maxLevel)
        {
            return m_ntcore.NT_AddPolledLogger(poller, (uint)minLevel, (uint)maxLevel);
        }

        public static unsafe void RemoveLogger(NtLogger logger)
        {
            m_ntcore.NT_RemoveLogger(logger);
        }

        public static unsafe bool WaitForLoggerQueue(NtInst inst, double timeout)
        {
            return m_ntcore.NT_WaitForLoggerQueue(inst, timeout).Get();
        }

        public static unsafe void CancelPollLogger(NtLoggerPoller poller)
        {
            m_ntcore.NT_CancelPollLogger(poller);
        }

        public static void DestroyLoggerPoller(NtLoggerPoller poller)
        {
            m_ntcore.NT_DestroyLoggerPoller(poller);
        }


        public static unsafe void PostRpcResponse(NtEntry entry, NtRpcCall call, ReadOnlySpan<byte> result)
        {
            fixed (byte* p = result)
            {
                UIntPtr len = (UIntPtr)result.Length;
                m_ntcore.NT_PostRpcResponse(entry, call, p, len);
            }
        }

        public static unsafe ReadOnlySpan<byte> GetRpcResult(NtEntry entry, NtRpcCall call, Span<byte> store)
        {
            UIntPtr length = UIntPtr.Zero;
            byte* res = m_ntcore.NT_GetRpcResult(entry, call, &length);
            int len = (int)length;
            Span<byte> retVal = GetSpanOrBuffer(store, len);
            new Span<byte>(res, len).CopyTo(retVal);
            m_ntcore.NT_FreeCharArray(res);
            return retVal;
        }

        public static unsafe ReadOnlySpan<byte> GetRpcResult(NtEntry entry, NtRpcCall call, double timeout, Span<byte> store)
        {
            UIntPtr length = UIntPtr.Zero;
            NtBool timedOut = false;
            byte* res = m_ntcore.NT_GetRpcResultTimeout(entry, call, &length, timeout, &timedOut);
            if (timedOut.Get())
            {
                return null;
            }
            int len = (int)length;
            Span<byte> retVal = GetSpanOrBuffer(store, len);
            new Span<byte>(res, len).CopyTo(retVal);
            m_ntcore.NT_FreeCharArray(res);
            return retVal;
        }

        public static unsafe void CancelRpcResult(NtEntry entry, NtRpcCall call)
        {
            m_ntcore.NT_CancelRpcResult(entry, call);
        }

        public static unsafe NtRpcCall CallRpc(NtEntry entry, Span<byte> @params)
        {
            fixed (byte* b = @params)
            {
                return m_ntcore.NT_CallRpc(entry, b, (UIntPtr)@params.Length);
            }
        }

        public static unsafe void SetNetworkIdentity(NtInst inst, string name)
        {
            var str = new UTF8String(name);
            fixed (byte* buf = str.Buffer)
            {
                m_ntcore.NT_SetNetworkIdentity(inst, buf, str.Length);
            }
        }

        public static unsafe NetworkMode GetNetworkMode(NtInst inst)
        {
            return (NetworkMode)m_ntcore.NT_GetNetworkMode(inst);
        }

        public static unsafe void StartServer(NtInst inst, string persistFilename, string listenAddress, int port)
        {
            var pStr = new UTF8String(persistFilename);
            var lStr = new UTF8String(listenAddress);
            fixed (byte* pBuf = pStr.Buffer)
            fixed (byte* lBuf = lStr.Buffer)
            {
                m_ntcore.NT_StartServer(inst, pBuf, lBuf, (uint)port);
            }
        }

        public static unsafe void StopServer(NtInst inst)
        {
            m_ntcore.NT_StopServer(inst);
        }

        public static unsafe void StartClient(NtInst inst)
        {
            m_ntcore.NT_StartClientNone(inst);
        }

        public static unsafe void StartClient(NtInst inst, string serverName, int port)
        {
            var str = new UTF8String(serverName);
            fixed (byte* buf = str.Buffer)
            {
                m_ntcore.NT_StartClient(inst, buf, (uint)port);
            }
        }

        public static unsafe void StartClient(NtInst inst, ReadOnlySpan<ServerPortPair> servers)
        {
            var serverStrs = stackalloc byte*[servers.Length];
            var serverPorts = stackalloc uint[servers.Length];
            for (int i = 0; i < servers.Length; i++)
            {
                serverPorts[i] = (uint)servers[i].Port;
                string vStr = servers[i].Server;
                fixed (char* str = vStr)
                {
                    var encoding = Encoding.UTF8;
                    int bytes = encoding.GetByteCount(str, vStr.Length);
                    serverStrs[i] = (byte*)Marshal.AllocHGlobal((bytes + 1) * sizeof(byte));
                    encoding.GetBytes(str, vStr.Length, serverStrs[i], bytes);
                    serverStrs[i][bytes] = 0;
                }
            }
            m_ntcore.NT_StartClientMulti(inst, (UIntPtr)servers.Length, serverStrs, serverPorts);

            for (int i = 0; i < servers.Length; i++)
            {
                Marshal.FreeHGlobal((IntPtr)serverStrs[i]);
            }
        }

        public static unsafe void StartClientTeam(NtInst inst, int team, int port)
        {
            m_ntcore.NT_StartClientTeam(inst, (uint)team, (uint)port);
        }

        public static unsafe void StopClient(NtInst inst)
        {
            m_ntcore.NT_StopClient(inst);
        }

        public static unsafe void SetServer(NtInst inst, string serverName, int port)
        {
            var str = new UTF8String(serverName);
            fixed (byte* buf = str.Buffer)
            {
                m_ntcore.NT_SetServer(inst, buf, (uint)port);
            }
        }

        public static unsafe void SetServer(NtInst inst, ReadOnlySpan<ServerPortPair> servers)
        {
            var serverStrs = stackalloc byte*[servers.Length];
            var serverPorts = stackalloc uint[servers.Length];
            for (int i = 0; i < servers.Length; i++)
            {
                serverPorts[i] = (uint)servers[i].Port;
                string vStr = servers[i].Server;
                fixed (char* str = vStr)
                {
                    var encoding = Encoding.UTF8;
                    int bytes = encoding.GetByteCount(str, vStr.Length);
                    serverStrs[i] = (byte*)Marshal.AllocHGlobal((bytes + 1) * sizeof(byte));
                    encoding.GetBytes(str, vStr.Length, serverStrs[i], bytes);
                    serverStrs[i][bytes] = 0;
                }
            }
            m_ntcore.NT_SetServerMulti(inst, (UIntPtr)servers.Length, serverStrs, serverPorts);

            for (int i = 0; i < servers.Length; i++)
            {
                Marshal.FreeHGlobal((IntPtr)serverStrs[i]);
            }
        }

        public static unsafe void SetServerTeam(NtInst inst, int team, int port)
        {
            m_ntcore.NT_SetServerTeam(inst, (uint)team, (uint)port);
        }

        public static unsafe void StartDSClient(NtInst inst, int port)
        {
            m_ntcore.NT_StartDSClient(inst, (uint)port);
        }

        public static void StopDSClient(NtInst inst)
        {
            m_ntcore.NT_StopDSClient(inst);
        }

        public static unsafe void SetUpdateRate(NtInst inst, double interval)
        {
            m_ntcore.NT_SetUpdateRate(inst, interval);
        }

        public static unsafe void Flush(NtInst inst)
        {
            m_ntcore.NT_Flush(inst);
        }

        public static unsafe ReadOnlySpan<ConnectionInfo> GetConnections(NtInst inst, Span<ConnectionInfo> store)
        {
            UIntPtr count = UIntPtr.Zero;
            var conns = m_ntcore.NT_GetConnections(inst, &count);
            int len = (int)count;
            Span<ConnectionInfo> retConns = GetSpanOrBuffer(store, len);
            for (int i = 0; i < retConns.Length; i++)
            {
                retConns[i] = new ConnectionInfo(&conns[i]);
            }
            m_ntcore.NT_DisposeConnectionInfoArray(conns, count);
            return retConns;
        }

        public static unsafe bool IsConnected(NtInst inst)
        {
            return m_ntcore.NT_IsConnected(inst).Get();
        }

        public static unsafe void SavePersistent(NtInst inst, string filename)
        {
            var f = new UTF8String(filename);
            fixed (byte* buf = f.Buffer)
            {
                var error = m_ntcore.NT_SavePersistent(inst, buf);
                if (error != null)
                {
                    throw new PersistentException(UTF8String.ReadUTF8String(error));
                }

            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate void WarnFunc(UIntPtr line, byte* messsage);

        public static unsafe List<string> LoadPersistent(NtInst inst, string filename)
        {
            var f = new UTF8String(filename);
            fixed (byte* buf = f.Buffer)
            {
                List<string> warns = new List<string>(4);
                var fp = Marshal.GetFunctionPointerForDelegate<WarnFunc>((UIntPtr line, byte* message) =>
                {
                    warns.Add($"{(int)line} : {UTF8String.ReadUTF8String(message)}");
                });
                var callback = (delegate* unmanaged[Cdecl]<UIntPtr, byte*, void>)fp;
                var error = m_ntcore.NT_LoadPersistent(inst, buf, callback);
                GC.KeepAlive(fp);
                if (error != null)
                {
                    throw new PersistentException(UTF8String.ReadUTF8String(error));
                }
                return warns;

            }

        }

        public static unsafe void SaveEntries(NtInst inst, string filename, string prefix)
        {
            var fStr = new UTF8String(filename);
            var pStr = new UTF8String(prefix);
            fixed (byte* f = fStr.Buffer)
            fixed (byte* p = pStr.Buffer)
            {
                var error = m_ntcore.NT_SaveEntries(inst, f, p, pStr.Length);
                if (error != null)
                {
                    throw new PersistentException(UTF8String.ReadUTF8String(error));
                }
            }
        }

        public static unsafe List<string> LoadEntries(NtInst inst, string filename, string prefix)
        {
            var fStr = new UTF8String(filename);
            var pStr = new UTF8String(prefix);
            fixed (byte* f = fStr.Buffer)
            fixed (byte* p = pStr.Buffer)
            {
                List<string> warns = new List<string>(4);
                var fp = Marshal.GetFunctionPointerForDelegate<WarnFunc>((UIntPtr line, byte* message) =>
                {
                    warns.Add($"{(int)line} : {UTF8String.ReadUTF8String(message)}");
                });
                var callback = (delegate* unmanaged[Cdecl] < UIntPtr, byte *, void >)fp;
                var error = m_ntcore.NT_LoadEntries(inst, f, p, pStr.Length, callback);
                GC.KeepAlive(fp);
                if (error != null)
                {
                    throw new PersistentException(UTF8String.ReadUTF8String(error));
                }
                return warns;
            }
        }


    }
}
