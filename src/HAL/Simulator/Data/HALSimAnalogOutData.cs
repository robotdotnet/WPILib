using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using FRC.NativeLibraryUtilities;
using static HAL.Base.HAL;

namespace HAL.Simulator.Data
{
    public class HALSimAnalogOutData
    {
        public static void Ping() { }

        static HALSimAnalogOutData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimAnalogOutData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimAnalogOutData(int index)
        {
            Index = index;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetAnalogOutDataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetAnalogOutDataDelegate HALSIM_ResetAnalogOutData;
        public void ResetData()
        {
            m_voltageCallbacks.Clear();
            m_initializedCallbacks.Clear();
            HALSIM_ResetAnalogOutData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogOutVoltageCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogOutVoltageCallbackDelegate HALSIM_RegisterAnalogOutVoltageCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogOutVoltageCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogOutVoltageCallbackDelegate HALSIM_CancelAnalogOutVoltageCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetAnalogOutVoltageDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogOutVoltageDelegate HALSIM_GetAnalogOutVoltage;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogOutVoltageDelegate(int index, double voltage);
        [NativeDelegate]
        internal static HALSIM_SetAnalogOutVoltageDelegate HALSIM_SetAnalogOutVoltage;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_voltageCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterVoltageCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogOutVoltageCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_voltageCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogOutVoltageCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelVoltageCallback(int uid)
        {
            HALSIM_CancelAnalogOutVoltageCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_voltageCallbacks.TryRemove(uid, out cb);
        }
        public double GetVoltage() => HALSIM_GetAnalogOutVoltage(Index);
        public void SetVoltage(double voltage) => HALSIM_SetAnalogOutVoltage(Index, voltage);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogOutInitializedCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogOutInitializedCallbackDelegate HALSIM_RegisterAnalogOutInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogOutInitializedCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogOutInitializedCallbackDelegate HALSIM_CancelAnalogOutInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetAnalogOutInitializedDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogOutInitializedDelegate HALSIM_GetAnalogOutInitialized;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogOutInitializedDelegate(int index, bool initialized);
        [NativeDelegate]
        internal static HALSIM_SetAnalogOutInitializedDelegate HALSIM_SetAnalogOutInitialized;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_initializedCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterInitializedCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogOutInitializedCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_initializedCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogOutInitializedCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelInitializedCallback(int uid)
        {
            HALSIM_CancelAnalogOutInitializedCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_initializedCallbacks.TryRemove(uid, out cb);
        }
        public bool GetInitialized() => HALSIM_GetAnalogOutInitialized(Index);
        public void SetInitialized(bool initialized) => HALSIM_SetAnalogOutInitialized(Index, initialized);
    }
}
