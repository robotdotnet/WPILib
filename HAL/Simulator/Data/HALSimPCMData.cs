using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using static HAL.Base.HAL;
using static HAL.Base.HALPorts;

namespace HAL.Simulator.Data
{
    public class HALSimPCMData
    {
        public static void Ping() { }

        static HALSimPCMData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimPCMData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimPCMData(int index)
        {
            Index = index;
            for (int i = 0; i < HAL_GetNumSolenoidChannels(); i++)
            {
                m_solenoidInitializedCallbacks.Add(new ConcurrentDictionary<int, HAL_NotifyCallback>());
                m_solenoidOutputCallbacks.Add(new ConcurrentDictionary<int, HAL_NotifyCallback>());
            } 
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetPCMDataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetPCMDataDelegate HALSIM_ResetPCMData;
        public void ResetData()
        {
            foreach (var solenoidInitializedCallback in m_solenoidInitializedCallbacks)
            {
                solenoidInitializedCallback.Clear();

            }
            foreach (var solenoidOutputCallback in m_solenoidOutputCallbacks)
            {
                solenoidOutputCallback.Clear();
            }
            m_compressorInitializedCallbacks.Clear();
            m_compressorOnCallbacks.Clear();
            m_closedLoopEnabledCallbacks.Clear();
            m_pressureSwitchCallbacks.Clear();
            m_compressorCurrentCallbacks.Clear();
            HALSIM_ResetPCMData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPCMSolenoidInitializedCallbackDelegate(int index, int channel, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPCMSolenoidInitializedCallbackDelegate HALSIM_RegisterPCMSolenoidInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPCMSolenoidInitializedCallbackDelegate(int index, int channel, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPCMSolenoidInitializedCallbackDelegate HALSIM_CancelPCMSolenoidInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetPCMSolenoidInitializedDelegate(int index, int channel);
        [NativeDelegate]
        internal static HALSIM_GetPCMSolenoidInitializedDelegate HALSIM_GetPCMSolenoidInitialized;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPCMSolenoidInitializedDelegate(int index, int channel, bool solenoidInitialized);
        [NativeDelegate]
        internal static HALSIM_SetPCMSolenoidInitializedDelegate HALSIM_SetPCMSolenoidInitialized;
        private readonly List<ConcurrentDictionary<int, HAL_NotifyCallback>> m_solenoidInitializedCallbacks = new List<ConcurrentDictionary<int, HAL_NotifyCallback>>();
        public int RegisterSolenoidInitializedCallback(int channel, NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterPCMSolenoidInitializedCallback(Index, channel, modCallback, IntPtr.Zero, initialNotify);
            if (!m_solenoidInitializedCallbacks[channel].TryAdd(uid, modCallback))
            {
                HALSIM_CancelPCMSolenoidInitializedCallback(Index, channel, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelSolenoidInitializedCallback(int channel, int uid)
        {
            HALSIM_CancelPCMSolenoidInitializedCallback(Index, channel, uid);
            HAL_NotifyCallback cb = null;
            m_solenoidInitializedCallbacks[channel].TryRemove(uid, out cb);
        }
        public bool GetSolenoidInitialized(int channel) => HALSIM_GetPCMSolenoidInitialized(Index, channel);
        public void SetSolenoidInitialized(int channel, bool solenoidInitialized) => HALSIM_SetPCMSolenoidInitialized(Index, channel, solenoidInitialized);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPCMSolenoidOutputCallbackDelegate(int index, int channel, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPCMSolenoidOutputCallbackDelegate HALSIM_RegisterPCMSolenoidOutputCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPCMSolenoidOutputCallbackDelegate(int index, int channel, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPCMSolenoidOutputCallbackDelegate HALSIM_CancelPCMSolenoidOutputCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetPCMSolenoidOutputDelegate(int index, int channel);
        [NativeDelegate]
        internal static HALSIM_GetPCMSolenoidOutputDelegate HALSIM_GetPCMSolenoidOutput;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPCMSolenoidOutputDelegate(int index, int channel, bool solenoidOutput);
        [NativeDelegate]
        internal static HALSIM_SetPCMSolenoidOutputDelegate HALSIM_SetPCMSolenoidOutput;
        private readonly List<ConcurrentDictionary<int, HAL_NotifyCallback>> m_solenoidOutputCallbacks = new List<ConcurrentDictionary<int, HAL_NotifyCallback>>();
        public int RegisterSolenoidOutputCallback(int channel, NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterPCMSolenoidOutputCallback(Index, channel, modCallback, IntPtr.Zero, initialNotify);
            if (!m_solenoidOutputCallbacks[channel].TryAdd(uid, modCallback))
            {
                HALSIM_CancelPCMSolenoidOutputCallback(Index, channel, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelSolenoidOutputCallback(int channel, int uid)
        {
            HALSIM_CancelPCMSolenoidOutputCallback(Index, channel, uid);
            HAL_NotifyCallback cb = null;
            m_solenoidOutputCallbacks[channel].TryRemove(uid, out cb);
        }
        public bool GetSolenoidOutput(int channel) => HALSIM_GetPCMSolenoidOutput(Index, channel);
        public void SetSolenoidOutput(int channel, bool solenoidOutput) => HALSIM_SetPCMSolenoidOutput(Index, channel, solenoidOutput);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPCMCompressorInitializedCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPCMCompressorInitializedCallbackDelegate HALSIM_RegisterPCMCompressorInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPCMCompressorInitializedCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPCMCompressorInitializedCallbackDelegate HALSIM_CancelPCMCompressorInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetPCMCompressorInitializedDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetPCMCompressorInitializedDelegate HALSIM_GetPCMCompressorInitialized;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPCMCompressorInitializedDelegate(int index, bool compressorInitialized);
        [NativeDelegate]
        internal static HALSIM_SetPCMCompressorInitializedDelegate HALSIM_SetPCMCompressorInitialized;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_compressorInitializedCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterCompressorInitializedCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterPCMCompressorInitializedCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_compressorInitializedCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelPCMCompressorInitializedCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelCompressorInitializedCallback(int uid)
        {
            HALSIM_CancelPCMCompressorInitializedCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_compressorInitializedCallbacks.TryRemove(uid, out cb);
        }
        public bool GetCompressorInitialized() => HALSIM_GetPCMCompressorInitialized(Index);
        public void SetCompressorInitialized(bool compressorInitialized) => HALSIM_SetPCMCompressorInitialized(Index, compressorInitialized);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPCMCompressorOnCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPCMCompressorOnCallbackDelegate HALSIM_RegisterPCMCompressorOnCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPCMCompressorOnCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPCMCompressorOnCallbackDelegate HALSIM_CancelPCMCompressorOnCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetPCMCompressorOnDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetPCMCompressorOnDelegate HALSIM_GetPCMCompressorOn;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPCMCompressorOnDelegate(int index, bool compressorOn);
        [NativeDelegate]
        internal static HALSIM_SetPCMCompressorOnDelegate HALSIM_SetPCMCompressorOn;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_compressorOnCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterCompressorOnCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterPCMCompressorOnCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_compressorOnCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelPCMCompressorOnCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelCompressorOnCallback(int uid)
        {
            HALSIM_CancelPCMCompressorOnCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_compressorOnCallbacks.TryRemove(uid, out cb);
        }
        public bool GetCompressorOn() => HALSIM_GetPCMCompressorOn(Index);
        public void SetCompressorOn(bool compressorOn) => HALSIM_SetPCMCompressorOn(Index, compressorOn);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPCMClosedLoopEnabledCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPCMClosedLoopEnabledCallbackDelegate HALSIM_RegisterPCMClosedLoopEnabledCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPCMClosedLoopEnabledCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPCMClosedLoopEnabledCallbackDelegate HALSIM_CancelPCMClosedLoopEnabledCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetPCMClosedLoopEnabledDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetPCMClosedLoopEnabledDelegate HALSIM_GetPCMClosedLoopEnabled;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPCMClosedLoopEnabledDelegate(int index, bool closedLoopEnabled);
        [NativeDelegate]
        internal static HALSIM_SetPCMClosedLoopEnabledDelegate HALSIM_SetPCMClosedLoopEnabled;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_closedLoopEnabledCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterClosedLoopEnabledCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterPCMClosedLoopEnabledCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_closedLoopEnabledCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelPCMClosedLoopEnabledCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelClosedLoopEnabledCallback(int uid)
        {
            HALSIM_CancelPCMClosedLoopEnabledCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_closedLoopEnabledCallbacks.TryRemove(uid, out cb);
        }
        public bool GetClosedLoopEnabled() => HALSIM_GetPCMClosedLoopEnabled(Index);
        public void SetClosedLoopEnabled(bool closedLoopEnabled) => HALSIM_SetPCMClosedLoopEnabled(Index, closedLoopEnabled);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPCMPressureSwitchCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPCMPressureSwitchCallbackDelegate HALSIM_RegisterPCMPressureSwitchCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPCMPressureSwitchCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPCMPressureSwitchCallbackDelegate HALSIM_CancelPCMPressureSwitchCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetPCMPressureSwitchDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetPCMPressureSwitchDelegate HALSIM_GetPCMPressureSwitch;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPCMPressureSwitchDelegate(int index, bool pressureSwitch);
        [NativeDelegate]
        internal static HALSIM_SetPCMPressureSwitchDelegate HALSIM_SetPCMPressureSwitch;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_pressureSwitchCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterPressureSwitchCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterPCMPressureSwitchCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_pressureSwitchCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelPCMPressureSwitchCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelPressureSwitchCallback(int uid)
        {
            HALSIM_CancelPCMPressureSwitchCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_pressureSwitchCallbacks.TryRemove(uid, out cb);
        }
        public bool GetPressureSwitch() => HALSIM_GetPCMPressureSwitch(Index);
        public void SetPressureSwitch(bool pressureSwitch) => HALSIM_SetPCMPressureSwitch(Index, pressureSwitch);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterPCMCompressorCurrentCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterPCMCompressorCurrentCallbackDelegate HALSIM_RegisterPCMCompressorCurrentCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelPCMCompressorCurrentCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelPCMCompressorCurrentCallbackDelegate HALSIM_CancelPCMCompressorCurrentCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetPCMCompressorCurrentDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetPCMCompressorCurrentDelegate HALSIM_GetPCMCompressorCurrent;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetPCMCompressorCurrentDelegate(int index, double compressorCurrent);
        [NativeDelegate]
        internal static HALSIM_SetPCMCompressorCurrentDelegate HALSIM_SetPCMCompressorCurrent;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_compressorCurrentCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterCompressorCurrentCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterPCMCompressorCurrentCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_compressorCurrentCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelPCMCompressorCurrentCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelCompressorCurrentCallback(int uid)
        {
            HALSIM_CancelPCMCompressorCurrentCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_compressorCurrentCallbacks.TryRemove(uid, out cb);
        }
        public double GetCompressorCurrent() => HALSIM_GetPCMCompressorCurrent(Index);
        public void SetCompressorCurrent(double compressorCurrent) => HALSIM_SetPCMCompressorCurrent(Index, compressorCurrent);
    }
}
