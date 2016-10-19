using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using static HAL.Base.HAL;

namespace HAL.Simulator.Data
{
    public class HALSimSPIAccelerometerData
    {
        public static void Ping() { }

        static HALSimSPIAccelerometerData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimSPIAccelerometerData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimSPIAccelerometerData(int index)
        {
            Index = index;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetSPIAccelerometerDataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetSPIAccelerometerDataDelegate HALSIM_ResetSPIAccelerometerData;
        public void ResetData()
        {
            m_activeCallbacks.Clear();
            m_rangeCallbacks.Clear();
            m_xCallbacks.Clear();
            m_yCallbacks.Clear();
            m_zCallbacks.Clear();
            HALSIM_ResetSPIAccelerometerData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterSPIAccelerometerActiveCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterSPIAccelerometerActiveCallbackDelegate HALSIM_RegisterSPIAccelerometerActiveCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelSPIAccelerometerActiveCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelSPIAccelerometerActiveCallbackDelegate HALSIM_CancelSPIAccelerometerActiveCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetSPIAccelerometerActiveDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetSPIAccelerometerActiveDelegate HALSIM_GetSPIAccelerometerActive;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetSPIAccelerometerActiveDelegate(int index, bool active);
        [NativeDelegate]
        internal static HALSIM_SetSPIAccelerometerActiveDelegate HALSIM_SetSPIAccelerometerActive;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_activeCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterActiveCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterSPIAccelerometerActiveCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_activeCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelSPIAccelerometerActiveCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelActiveCallback(int uid)
        {
            HALSIM_CancelSPIAccelerometerActiveCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_activeCallbacks.TryRemove(uid, out cb);
        }
        public bool GetActive() => HALSIM_GetSPIAccelerometerActive(Index);
        public void SetActive(bool active) => HALSIM_SetSPIAccelerometerActive(Index, active);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterSPIAccelerometerRangeCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterSPIAccelerometerRangeCallbackDelegate HALSIM_RegisterSPIAccelerometerRangeCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelSPIAccelerometerRangeCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelSPIAccelerometerRangeCallbackDelegate HALSIM_CancelSPIAccelerometerRangeCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetSPIAccelerometerRangeDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetSPIAccelerometerRangeDelegate HALSIM_GetSPIAccelerometerRange;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetSPIAccelerometerRangeDelegate(int index, int range);
        [NativeDelegate]
        internal static HALSIM_SetSPIAccelerometerRangeDelegate HALSIM_SetSPIAccelerometerRange;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_rangeCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterRangeCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterSPIAccelerometerRangeCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_rangeCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelSPIAccelerometerRangeCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelRangeCallback(int uid)
        {
            HALSIM_CancelSPIAccelerometerRangeCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_rangeCallbacks.TryRemove(uid, out cb);
        }
        public int GetRange() => HALSIM_GetSPIAccelerometerRange(Index);
        public void SetRange(int range) => HALSIM_SetSPIAccelerometerRange(Index, range);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterSPIAccelerometerXCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterSPIAccelerometerXCallbackDelegate HALSIM_RegisterSPIAccelerometerXCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelSPIAccelerometerXCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelSPIAccelerometerXCallbackDelegate HALSIM_CancelSPIAccelerometerXCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetSPIAccelerometerXDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetSPIAccelerometerXDelegate HALSIM_GetSPIAccelerometerX;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetSPIAccelerometerXDelegate(int index, double x);
        [NativeDelegate]
        internal static HALSIM_SetSPIAccelerometerXDelegate HALSIM_SetSPIAccelerometerX;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_xCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterXCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterSPIAccelerometerXCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_xCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelSPIAccelerometerXCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelXCallback(int uid)
        {
            HALSIM_CancelSPIAccelerometerXCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_xCallbacks.TryRemove(uid, out cb);
        }
        public double GetX() => HALSIM_GetSPIAccelerometerX(Index);
        public void SetX(double x) => HALSIM_SetSPIAccelerometerX(Index, x);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterSPIAccelerometerYCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterSPIAccelerometerYCallbackDelegate HALSIM_RegisterSPIAccelerometerYCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelSPIAccelerometerYCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelSPIAccelerometerYCallbackDelegate HALSIM_CancelSPIAccelerometerYCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetSPIAccelerometerYDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetSPIAccelerometerYDelegate HALSIM_GetSPIAccelerometerY;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetSPIAccelerometerYDelegate(int index, double y);
        [NativeDelegate]
        internal static HALSIM_SetSPIAccelerometerYDelegate HALSIM_SetSPIAccelerometerY;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_yCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterYCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterSPIAccelerometerYCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_yCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelSPIAccelerometerYCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelYCallback(int uid)
        {
            HALSIM_CancelSPIAccelerometerYCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_yCallbacks.TryRemove(uid, out cb);
        }
        public double GetY() => HALSIM_GetSPIAccelerometerY(Index);
        public void SetY(double y) => HALSIM_SetSPIAccelerometerY(Index, y);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterSPIAccelerometerZCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterSPIAccelerometerZCallbackDelegate HALSIM_RegisterSPIAccelerometerZCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelSPIAccelerometerZCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelSPIAccelerometerZCallbackDelegate HALSIM_CancelSPIAccelerometerZCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetSPIAccelerometerZDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetSPIAccelerometerZDelegate HALSIM_GetSPIAccelerometerZ;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetSPIAccelerometerZDelegate(int index, double z);
        [NativeDelegate]
        internal static HALSIM_SetSPIAccelerometerZDelegate HALSIM_SetSPIAccelerometerZ;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_zCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterZCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterSPIAccelerometerZCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_zCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelSPIAccelerometerZCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelZCallback(int uid)
        {
            HALSIM_CancelSPIAccelerometerZCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_zCallbacks.TryRemove(uid, out cb);
        }
        public double GetZ() => HALSIM_GetSPIAccelerometerZ(Index);
        public void SetZ(double z) => HALSIM_SetSPIAccelerometerZ(Index, z);
    }
}
