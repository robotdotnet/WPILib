using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using static HAL.Base.HAL;
namespace HAL.Simulator.Data
{
    public class HALSimAccelerometerData
    {
        static HALSimAccelerometerData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimAccelerometerData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimAccelerometerData(int index)
        {
            Index = index;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetAccelerometerDataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetAccelerometerDataDelegate HALSIM_ResetAccelerometerData;
        public void ResetData()
        {
            m_activeCallbacks.Clear();
            m_rangeCallbacks.Clear();
            m_xCallbacks.Clear();
            m_yCallbacks.Clear();
            m_zCallbacks.Clear();
            HALSIM_ResetAccelerometerData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAccelerometerActiveCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAccelerometerActiveCallbackDelegate HALSIM_RegisterAccelerometerActiveCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAccelerometerActiveCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAccelerometerActiveCallbackDelegate HALSIM_CancelAccelerometerActiveCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetAccelerometerActiveDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAccelerometerActiveDelegate HALSIM_GetAccelerometerActive;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_activeCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAccelerometerActiveCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterAccelerometerActiveCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_activeCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAccelerometerActiveCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAccelerometerActiveCallback(int uid)
        {
            HALSIM_CancelAccelerometerActiveCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_activeCallbacks.TryRemove(uid, out cb);
        }
        public bool GetActive() => HALSIM_GetAccelerometerActive(Index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAccelerometerRangeCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAccelerometerRangeCallbackDelegate HALSIM_RegisterAccelerometerRangeCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAccelerometerRangeCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAccelerometerRangeCallbackDelegate HALSIM_CancelAccelerometerRangeCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate HALAccelerometerRange HALSIM_GetAccelerometerRangeDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAccelerometerRangeDelegate HALSIM_GetAccelerometerRange;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_rangeCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAccelerometerRangeCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterAccelerometerRangeCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_rangeCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAccelerometerRangeCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAccelerometerRangeCallback(int uid)
        {
            HALSIM_CancelAccelerometerRangeCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_rangeCallbacks.TryRemove(uid, out cb);
        }
        public HALAccelerometerRange GetRange() => HALSIM_GetAccelerometerRange(Index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAccelerometerXCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAccelerometerXCallbackDelegate HALSIM_RegisterAccelerometerXCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAccelerometerXCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAccelerometerXCallbackDelegate HALSIM_CancelAccelerometerXCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetAccelerometerXDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAccelerometerXDelegate HALSIM_GetAccelerometerX;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAccelerometerXDelegate(int index, double x);
        [NativeDelegate]
        internal static HALSIM_SetAccelerometerXDelegate HALSIM_SetAccelerometerX;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_xCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAccelerometerXCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterAccelerometerXCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_xCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAccelerometerXCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAccelerometerXCallback(int uid)
        {
            HALSIM_CancelAccelerometerXCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_xCallbacks.TryRemove(uid, out cb);
        }
        public double GetX() => HALSIM_GetAccelerometerX(Index);
        public void SetX(double x) => HALSIM_SetAccelerometerX(Index, x);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAccelerometerYCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAccelerometerYCallbackDelegate HALSIM_RegisterAccelerometerYCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAccelerometerYCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAccelerometerYCallbackDelegate HALSIM_CancelAccelerometerYCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetAccelerometerYDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAccelerometerYDelegate HALSIM_GetAccelerometerY;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAccelerometerYDelegate(int index, double y);
        [NativeDelegate]
        internal static HALSIM_SetAccelerometerYDelegate HALSIM_SetAccelerometerY;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_yCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAccelerometerYCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterAccelerometerYCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_yCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAccelerometerYCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAccelerometerYCallback(int uid)
        {
            HALSIM_CancelAccelerometerYCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_yCallbacks.TryRemove(uid, out cb);
        }
        public double GetY() => HALSIM_GetAccelerometerY(Index);
        public void SetY(double y) => HALSIM_SetAccelerometerY(Index, y);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAccelerometerZCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAccelerometerZCallbackDelegate HALSIM_RegisterAccelerometerZCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAccelerometerZCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAccelerometerZCallbackDelegate HALSIM_CancelAccelerometerZCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetAccelerometerZDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAccelerometerZDelegate HALSIM_GetAccelerometerZ;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAccelerometerZDelegate(int index, double z);
        [NativeDelegate]
        internal static HALSIM_SetAccelerometerZDelegate HALSIM_SetAccelerometerZ;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_zCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAccelerometerZCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterAccelerometerZCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_zCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAccelerometerZCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAccelerometerZCallback(int uid)
        {
            HALSIM_CancelAccelerometerZCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_zCallbacks.TryRemove(uid, out cb);
        }
        public double GetZ() => HALSIM_GetAccelerometerZ(Index);
        public void SetZ(double z) => HALSIM_SetAccelerometerZ(Index, z);
    }
}
