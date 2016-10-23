using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using static HAL.Base.HAL;

namespace HAL.Simulator.Data
{
    public class HALSimAnalogGyroData
    {
        public static void Ping() { }

        static HALSimAnalogGyroData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimAnalogGyroData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimAnalogGyroData(int index)
        {
            Index = index;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetAnalogGyroDataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetAnalogGyroDataDelegate HALSIM_ResetAnalogGyroData;
        public void ResetData()
        {
            m_angleCallbacks.Clear();
            m_rateCallbacks.Clear();
            m_initializedCallbacks.Clear();
            HALSIM_ResetAnalogGyroData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogGyroAngleCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogGyroAngleCallbackDelegate HALSIM_RegisterAnalogGyroAngleCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogGyroAngleCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogGyroAngleCallbackDelegate HALSIM_CancelAnalogGyroAngleCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetAnalogGyroAngleDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogGyroAngleDelegate HALSIM_GetAnalogGyroAngle;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogGyroAngleDelegate(int index, double angle);
        [NativeDelegate]
        internal static HALSIM_SetAnalogGyroAngleDelegate HALSIM_SetAnalogGyroAngle;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_angleCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAngleCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogGyroAngleCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_angleCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogGyroAngleCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAngleCallback(int uid)
        {
            HALSIM_CancelAnalogGyroAngleCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_angleCallbacks.TryRemove(uid, out cb);
        }
        public double GetAngle() => HALSIM_GetAnalogGyroAngle(Index);
        public void SetAngle(double angle) => HALSIM_SetAnalogGyroAngle(Index, angle);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogGyroRateCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogGyroRateCallbackDelegate HALSIM_RegisterAnalogGyroRateCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogGyroRateCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogGyroRateCallbackDelegate HALSIM_CancelAnalogGyroRateCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetAnalogGyroRateDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogGyroRateDelegate HALSIM_GetAnalogGyroRate;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogGyroRateDelegate(int index, double rate);
        [NativeDelegate]
        internal static HALSIM_SetAnalogGyroRateDelegate HALSIM_SetAnalogGyroRate;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_rateCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterRateCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogGyroRateCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_rateCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogGyroRateCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelRateCallback(int uid)
        {
            HALSIM_CancelAnalogGyroRateCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_rateCallbacks.TryRemove(uid, out cb);
        }
        public double GetRate() => HALSIM_GetAnalogGyroRate(Index);
        public void SetRate(double rate) => HALSIM_SetAnalogGyroRate(Index, rate);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogGyroInitializedCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogGyroInitializedCallbackDelegate HALSIM_RegisterAnalogGyroInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogGyroInitializedCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogGyroInitializedCallbackDelegate HALSIM_CancelAnalogGyroInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetAnalogGyroInitializedDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogGyroInitializedDelegate HALSIM_GetAnalogGyroInitialized;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogGyroInitializedDelegate(int index, bool initialized);
        [NativeDelegate]
        internal static HALSIM_SetAnalogGyroInitializedDelegate HALSIM_SetAnalogGyroInitialized;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_initializedCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterInitializedCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogGyroInitializedCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_initializedCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogGyroInitializedCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelInitializedCallback(int uid)
        {
            HALSIM_CancelAnalogGyroInitializedCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_initializedCallbacks.TryRemove(uid, out cb);
        }
        public bool GetInitialized() => HALSIM_GetAnalogGyroInitialized(Index);
        public void SetInitialized(bool initialized) => HALSIM_SetAnalogGyroInitialized(Index, initialized);
    }
}
