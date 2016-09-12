using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using static HAL.Base.HAL;

namespace HAL.Simulator.Data
{
    public class HALSimRelayData
    {
        static HALSimRelayData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimRelayData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimRelayData(int index)
        {
            Index = index;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetRelayDataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetRelayDataDelegate HALSIM_ResetRelayData;
        public void ResetData()
        {
            m_initializedCallbacks.Clear();
            m_forwardCallbacks.Clear();
            m_reverseCallbacks.Clear();
            HALSIM_ResetRelayData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRelayInitializedCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRelayInitializedCallbackDelegate HALSIM_RegisterRelayInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRelayInitializedCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRelayInitializedCallbackDelegate HALSIM_CancelRelayInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetRelayInitializedDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRelayInitializedDelegate HALSIM_GetRelayInitialized;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_initializedCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterRelayInitializedCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterRelayInitializedCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_initializedCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRelayInitializedCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelRelayInitializedCallback(int uid)
        {
            HALSIM_CancelRelayInitializedCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_initializedCallbacks.TryRemove(uid, out cb);
        }
        public bool GetInitialized() => HALSIM_GetRelayInitialized(Index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRelayForwardCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRelayForwardCallbackDelegate HALSIM_RegisterRelayForwardCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRelayForwardCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRelayForwardCallbackDelegate HALSIM_CancelRelayForwardCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetRelayForwardDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRelayForwardDelegate HALSIM_GetRelayForward;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_forwardCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterRelayForwardCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterRelayForwardCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_forwardCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRelayForwardCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelRelayForwardCallback(int uid)
        {
            HALSIM_CancelRelayForwardCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_forwardCallbacks.TryRemove(uid, out cb);
        }
        public bool GetForward() => HALSIM_GetRelayForward(Index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRelayReverseCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRelayReverseCallbackDelegate HALSIM_RegisterRelayReverseCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRelayReverseCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRelayReverseCallbackDelegate HALSIM_CancelRelayReverseCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetRelayReverseDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRelayReverseDelegate HALSIM_GetRelayReverse;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_reverseCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterRelayReverseCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterRelayReverseCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_reverseCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRelayReverseCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelRelayReverseCallback(int uid)
        {
            HALSIM_CancelRelayReverseCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_reverseCallbacks.TryRemove(uid, out cb);
        }
        public bool GetReverse() => HALSIM_GetRelayReverse(Index);
    }
}
