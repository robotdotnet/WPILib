using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using NativeLibraryUtilities;
using static HAL.Base.HAL;

namespace HAL.Simulator.Data
{
    public class HALSimRelayData
    {
        public static void Ping() { }

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
            m_initializedForwardCallbacks.Clear();
            m_initializedReverseCallbacks.Clear();
            m_forwardCallbacks.Clear();
            m_reverseCallbacks.Clear();
            HALSIM_ResetRelayData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRelayInitializedForwardCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRelayInitializedForwardCallbackDelegate HALSIM_RegisterRelayInitializedForwardCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRelayInitializedForwardCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRelayInitializedForwardCallbackDelegate HALSIM_CancelRelayInitializedForwardCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetRelayInitializedForwardDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRelayInitializedForwardDelegate HALSIM_GetRelayInitializedForward;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRelayInitializedForwardDelegate(int index, bool initializedForward);
        [NativeDelegate]
        internal static HALSIM_SetRelayInitializedForwardDelegate HALSIM_SetRelayInitializedForward;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_initializedForwardCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterInitializedForwardCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRelayInitializedForwardCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_initializedForwardCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRelayInitializedForwardCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelInitializedForwardCallback(int uid)
        {
            HALSIM_CancelRelayInitializedForwardCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_initializedForwardCallbacks.TryRemove(uid, out cb);
        }
        public bool GetInitializedForward() => HALSIM_GetRelayInitializedForward(Index);
        public void SetInitializedForward(bool initializedForward) => HALSIM_SetRelayInitializedForward(Index, initializedForward);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRelayInitializedReverseCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRelayInitializedReverseCallbackDelegate HALSIM_RegisterRelayInitializedReverseCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRelayInitializedReverseCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRelayInitializedReverseCallbackDelegate HALSIM_CancelRelayInitializedReverseCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetRelayInitializedReverseDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRelayInitializedReverseDelegate HALSIM_GetRelayInitializedReverse;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRelayInitializedReverseDelegate(int index, bool initializedReverse);
        [NativeDelegate]
        internal static HALSIM_SetRelayInitializedReverseDelegate HALSIM_SetRelayInitializedReverse;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_initializedReverseCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterInitializedReverseCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRelayInitializedReverseCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_initializedReverseCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRelayInitializedReverseCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelInitializedReverseCallback(int uid)
        {
            HALSIM_CancelRelayInitializedReverseCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_initializedReverseCallbacks.TryRemove(uid, out cb);
        }
        public bool GetInitializedReverse() => HALSIM_GetRelayInitializedReverse(Index);
        public void SetInitializedReverse(bool initializedReverse) => HALSIM_SetRelayInitializedReverse(Index, initializedReverse);
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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRelayForwardDelegate(int index, bool forward);
        [NativeDelegate]
        internal static HALSIM_SetRelayForwardDelegate HALSIM_SetRelayForward;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_forwardCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterForwardCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRelayForwardCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_forwardCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRelayForwardCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelForwardCallback(int uid)
        {
            HALSIM_CancelRelayForwardCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_forwardCallbacks.TryRemove(uid, out cb);
        }
        public bool GetForward() => HALSIM_GetRelayForward(Index);
        public void SetForward(bool forward) => HALSIM_SetRelayForward(Index, forward);
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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRelayReverseDelegate(int index, bool reverse);
        [NativeDelegate]
        internal static HALSIM_SetRelayReverseDelegate HALSIM_SetRelayReverse;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_reverseCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterReverseCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRelayReverseCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_reverseCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRelayReverseCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelReverseCallback(int uid)
        {
            HALSIM_CancelRelayReverseCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_reverseCallbacks.TryRemove(uid, out cb);
        }
        public bool GetReverse() => HALSIM_GetRelayReverse(Index);
        public void SetReverse(bool reverse) => HALSIM_SetRelayReverse(Index, reverse);
    }
}
