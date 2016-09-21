using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using static HAL.Base.HAL;

namespace HAL.Simulator.Data
{
    public enum HALSIMAnalogTriggerMode : int
    {
        Unassigned,
        Filtered,
        Averaged,
    }

    public class HALSimAnalogTriggerData
    {
        public static void Ping() { }

        static HALSimAnalogTriggerData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimAnalogTriggerData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimAnalogTriggerData(int index)
        {
            Index = index;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetAnalogTriggerDataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetAnalogTriggerDataDelegate HALSIM_ResetAnalogTriggerData;
        public void ResetData()
        {
            m_initializedCallbacks.Clear();
            m_triggerLowerBoundCallbacks.Clear();
            m_triggerUpperBoundCallbacks.Clear();
            m_triggerModeCallbacks.Clear();
            HALSIM_ResetAnalogTriggerData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogTriggerInitializedCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogTriggerInitializedCallbackDelegate HALSIM_RegisterAnalogTriggerInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogTriggerInitializedCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogTriggerInitializedCallbackDelegate HALSIM_CancelAnalogTriggerInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetAnalogTriggerInitializedDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogTriggerInitializedDelegate HALSIM_GetAnalogTriggerInitialized;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_initializedCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAnalogTriggerInitializedCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterAnalogTriggerInitializedCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_initializedCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogTriggerInitializedCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAnalogTriggerInitializedCallback(int uid)
        {
            HALSIM_CancelAnalogTriggerInitializedCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_initializedCallbacks.TryRemove(uid, out cb);
        }
        public bool GetInitialized() => HALSIM_GetAnalogTriggerInitialized(Index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogTriggerTriggerLowerBoundCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogTriggerTriggerLowerBoundCallbackDelegate HALSIM_RegisterAnalogTriggerTriggerLowerBoundCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogTriggerTriggerLowerBoundCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogTriggerTriggerLowerBoundCallbackDelegate HALSIM_CancelAnalogTriggerTriggerLowerBoundCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetAnalogTriggerTriggerLowerBoundDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogTriggerTriggerLowerBoundDelegate HALSIM_GetAnalogTriggerTriggerLowerBound;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_triggerLowerBoundCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAnalogTriggerTriggerLowerBoundCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterAnalogTriggerTriggerLowerBoundCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_triggerLowerBoundCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogTriggerTriggerLowerBoundCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAnalogTriggerTriggerLowerBoundCallback(int uid)
        {
            HALSIM_CancelAnalogTriggerTriggerLowerBoundCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_triggerLowerBoundCallbacks.TryRemove(uid, out cb);
        }
        public double GetTriggerLowerBound() => HALSIM_GetAnalogTriggerTriggerLowerBound(Index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogTriggerTriggerUpperBoundCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogTriggerTriggerUpperBoundCallbackDelegate HALSIM_RegisterAnalogTriggerTriggerUpperBoundCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogTriggerTriggerUpperBoundCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogTriggerTriggerUpperBoundCallbackDelegate HALSIM_CancelAnalogTriggerTriggerUpperBoundCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetAnalogTriggerTriggerUpperBoundDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogTriggerTriggerUpperBoundDelegate HALSIM_GetAnalogTriggerTriggerUpperBound;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_triggerUpperBoundCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAnalogTriggerTriggerUpperBoundCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterAnalogTriggerTriggerUpperBoundCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_triggerUpperBoundCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogTriggerTriggerUpperBoundCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAnalogTriggerTriggerUpperBoundCallback(int uid)
        {
            HALSIM_CancelAnalogTriggerTriggerUpperBoundCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_triggerUpperBoundCallbacks.TryRemove(uid, out cb);
        }
        public double GetTriggerUpperBound() => HALSIM_GetAnalogTriggerTriggerUpperBound(Index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogTriggerTriggerModeCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogTriggerTriggerModeCallbackDelegate HALSIM_RegisterAnalogTriggerTriggerModeCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogTriggerTriggerModeCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogTriggerTriggerModeCallbackDelegate HALSIM_CancelAnalogTriggerTriggerModeCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate HALSIMAnalogTriggerMode HALSIM_GetAnalogTriggerTriggerModeDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogTriggerTriggerModeDelegate HALSIM_GetAnalogTriggerTriggerMode;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_triggerModeCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAnalogTriggerTriggerModeCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterAnalogTriggerTriggerModeCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_triggerModeCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogTriggerTriggerModeCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAnalogTriggerTriggerModeCallback(int uid)
        {
            HALSIM_CancelAnalogTriggerTriggerModeCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_triggerModeCallbacks.TryRemove(uid, out cb);
        }
        public HALSIMAnalogTriggerMode GetTriggerMode() => HALSIM_GetAnalogTriggerTriggerMode(Index);
    }
}
