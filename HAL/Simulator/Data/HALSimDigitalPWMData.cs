using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using static HAL.Base.HAL;

namespace HAL.Simulator.Data
{
    public class HALSimDigitalPWMData
    {
        public static void Ping() { }

        static HALSimDigitalPWMData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimDigitalPWMData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimDigitalPWMData(int index)
        {
            Index = index;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetDigitalPWMDataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetDigitalPWMDataDelegate HALSIM_ResetDigitalPWMData;
        public void ResetData()
        {
            m_initializedCallbacks.Clear();
            m_dutyCycleCallbacks.Clear();
            m_pinCallbacks.Clear();
            HALSIM_ResetDigitalPWMData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterDigitalPWMInitializedCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterDigitalPWMInitializedCallbackDelegate HALSIM_RegisterDigitalPWMInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelDigitalPWMInitializedCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelDigitalPWMInitializedCallbackDelegate HALSIM_CancelDigitalPWMInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetDigitalPWMInitializedDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetDigitalPWMInitializedDelegate HALSIM_GetDigitalPWMInitialized;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetDigitalPWMInitializedDelegate(int index, bool initialized);
        [NativeDelegate]
        internal static HALSIM_SetDigitalPWMInitializedDelegate HALSIM_SetDigitalPWMInitialized;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_initializedCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterInitializedCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterDigitalPWMInitializedCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_initializedCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelDigitalPWMInitializedCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelInitializedCallback(int uid)
        {
            HALSIM_CancelDigitalPWMInitializedCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_initializedCallbacks.TryRemove(uid, out cb);
        }
        public bool GetInitialized() => HALSIM_GetDigitalPWMInitialized(Index);
        public void SetInitialized(bool initialized) => HALSIM_SetDigitalPWMInitialized(Index, initialized);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterDigitalPWMDutyCycleCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterDigitalPWMDutyCycleCallbackDelegate HALSIM_RegisterDigitalPWMDutyCycleCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelDigitalPWMDutyCycleCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelDigitalPWMDutyCycleCallbackDelegate HALSIM_CancelDigitalPWMDutyCycleCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetDigitalPWMDutyCycleDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetDigitalPWMDutyCycleDelegate HALSIM_GetDigitalPWMDutyCycle;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetDigitalPWMDutyCycleDelegate(int index, double dutyCycle);
        [NativeDelegate]
        internal static HALSIM_SetDigitalPWMDutyCycleDelegate HALSIM_SetDigitalPWMDutyCycle;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_dutyCycleCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterDutyCycleCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterDigitalPWMDutyCycleCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_dutyCycleCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelDigitalPWMDutyCycleCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelDutyCycleCallback(int uid)
        {
            HALSIM_CancelDigitalPWMDutyCycleCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_dutyCycleCallbacks.TryRemove(uid, out cb);
        }
        public double GetDutyCycle() => HALSIM_GetDigitalPWMDutyCycle(Index);
        public void SetDutyCycle(double dutyCycle) => HALSIM_SetDigitalPWMDutyCycle(Index, dutyCycle);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterDigitalPWMPinCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterDigitalPWMPinCallbackDelegate HALSIM_RegisterDigitalPWMPinCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelDigitalPWMPinCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelDigitalPWMPinCallbackDelegate HALSIM_CancelDigitalPWMPinCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetDigitalPWMPinDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetDigitalPWMPinDelegate HALSIM_GetDigitalPWMPin;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetDigitalPWMPinDelegate(int index, int pin);
        [NativeDelegate]
        internal static HALSIM_SetDigitalPWMPinDelegate HALSIM_SetDigitalPWMPin;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_pinCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterPinCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterDigitalPWMPinCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_pinCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelDigitalPWMPinCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelPinCallback(int uid)
        {
            HALSIM_CancelDigitalPWMPinCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_pinCallbacks.TryRemove(uid, out cb);
        }
        public int GetPin() => HALSIM_GetDigitalPWMPin(Index);
        public void SetPin(int pin) => HALSIM_SetDigitalPWMPin(Index, pin);
    }
}
