using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using static HAL.Base.HAL;

namespace HAL.Simulator.Data
{
    public class HALSimPWMData
    {
        public static void Ping() { }

        static HALSimPWMData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimPWMData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimPWMData(int index)
        {
            Index = index;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetPWMDataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetPWMDataDelegate HALSIM_ResetPWMData;
        public void ResetData()
        {
            m_initializedCallbacks.Clear();
            m_rawValueCallbacks.Clear();
            m_speedCallbacks.Clear();
            m_positionCallbacks.Clear();
            m_periodScaleCallbacks.Clear();
            m_zeroLatchCallbacks.Clear();
            HALSIM_ResetPWMData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPWMInitializedCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPWMInitializedCallbackDelegate HALSIM_RegisterPWMInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPWMInitializedCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPWMInitializedCallbackDelegate HALSIM_CancelPWMInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetPWMInitializedDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetPWMInitializedDelegate HALSIM_GetPWMInitialized;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPWMInitializedDelegate(int index, bool initialized);
        [NativeDelegate]
        internal static HALSIM_SetPWMInitializedDelegate HALSIM_SetPWMInitialized;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_initializedCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterInitializedCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterPWMInitializedCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_initializedCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelPWMInitializedCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelInitializedCallback(int uid)
        {
            HALSIM_CancelPWMInitializedCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_initializedCallbacks.TryRemove(uid, out cb);
        }
        public bool GetInitialized() => HALSIM_GetPWMInitialized(Index);
        public void SetInitialized(bool initialized) => HALSIM_SetPWMInitialized(Index, initialized);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPWMRawValueCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPWMRawValueCallbackDelegate HALSIM_RegisterPWMRawValueCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPWMRawValueCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPWMRawValueCallbackDelegate HALSIM_CancelPWMRawValueCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetPWMRawValueDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetPWMRawValueDelegate HALSIM_GetPWMRawValue;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPWMRawValueDelegate(int index, int rawValue);
        [NativeDelegate]
        internal static HALSIM_SetPWMRawValueDelegate HALSIM_SetPWMRawValue;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_rawValueCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterRawValueCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterPWMRawValueCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_rawValueCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelPWMRawValueCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelRawValueCallback(int uid)
        {
            HALSIM_CancelPWMRawValueCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_rawValueCallbacks.TryRemove(uid, out cb);
        }
        public int GetRawValue() => HALSIM_GetPWMRawValue(Index);
        public void SetRawValue(int rawValue) => HALSIM_SetPWMRawValue(Index, rawValue);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPWMSpeedCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPWMSpeedCallbackDelegate HALSIM_RegisterPWMSpeedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPWMSpeedCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPWMSpeedCallbackDelegate HALSIM_CancelPWMSpeedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetPWMSpeedDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetPWMSpeedDelegate HALSIM_GetPWMSpeed;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPWMSpeedDelegate(int index, double speed);
        [NativeDelegate]
        internal static HALSIM_SetPWMSpeedDelegate HALSIM_SetPWMSpeed;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_speedCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterSpeedCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterPWMSpeedCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_speedCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelPWMSpeedCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelSpeedCallback(int uid)
        {
            HALSIM_CancelPWMSpeedCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_speedCallbacks.TryRemove(uid, out cb);
        }
        public double GetSpeed() => HALSIM_GetPWMSpeed(Index);
        public void SetSpeed(double speed) => HALSIM_SetPWMSpeed(Index, speed);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPWMPositionCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPWMPositionCallbackDelegate HALSIM_RegisterPWMPositionCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPWMPositionCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPWMPositionCallbackDelegate HALSIM_CancelPWMPositionCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetPWMPositionDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetPWMPositionDelegate HALSIM_GetPWMPosition;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPWMPositionDelegate(int index, double position);
        [NativeDelegate]
        internal static HALSIM_SetPWMPositionDelegate HALSIM_SetPWMPosition;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_positionCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterPositionCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterPWMPositionCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_positionCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelPWMPositionCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelPositionCallback(int uid)
        {
            HALSIM_CancelPWMPositionCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_positionCallbacks.TryRemove(uid, out cb);
        }
        public double GetPosition() => HALSIM_GetPWMPosition(Index);
        public void SetPosition(double position) => HALSIM_SetPWMPosition(Index, position);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPWMPeriodScaleCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPWMPeriodScaleCallbackDelegate HALSIM_RegisterPWMPeriodScaleCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPWMPeriodScaleCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPWMPeriodScaleCallbackDelegate HALSIM_CancelPWMPeriodScaleCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetPWMPeriodScaleDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetPWMPeriodScaleDelegate HALSIM_GetPWMPeriodScale;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPWMPeriodScaleDelegate(int index, int periodScale);
        [NativeDelegate]
        internal static HALSIM_SetPWMPeriodScaleDelegate HALSIM_SetPWMPeriodScale;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_periodScaleCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterPeriodScaleCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterPWMPeriodScaleCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_periodScaleCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelPWMPeriodScaleCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelPeriodScaleCallback(int uid)
        {
            HALSIM_CancelPWMPeriodScaleCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_periodScaleCallbacks.TryRemove(uid, out cb);
        }
        public int GetPeriodScale() => HALSIM_GetPWMPeriodScale(Index);
        public void SetPeriodScale(int periodScale) => HALSIM_SetPWMPeriodScale(Index, periodScale);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPWMZeroLatchCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPWMZeroLatchCallbackDelegate HALSIM_RegisterPWMZeroLatchCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPWMZeroLatchCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPWMZeroLatchCallbackDelegate HALSIM_CancelPWMZeroLatchCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetPWMZeroLatchDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetPWMZeroLatchDelegate HALSIM_GetPWMZeroLatch;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPWMZeroLatchDelegate(int index, bool zeroLatch);
        [NativeDelegate]
        internal static HALSIM_SetPWMZeroLatchDelegate HALSIM_SetPWMZeroLatch;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_zeroLatchCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterZeroLatchCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterPWMZeroLatchCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_zeroLatchCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelPWMZeroLatchCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelZeroLatchCallback(int uid)
        {
            HALSIM_CancelPWMZeroLatchCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_zeroLatchCallbacks.TryRemove(uid, out cb);
        }
        public bool GetZeroLatch() => HALSIM_GetPWMZeroLatch(Index);
        public void SetZeroLatch(bool zeroLatch) => HALSIM_SetPWMZeroLatch(Index, zeroLatch);
    }
}
