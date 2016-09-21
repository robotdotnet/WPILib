using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using static HAL.Base.HAL;

namespace HAL.Simulator.Data
{
    public class HALSimDIOData
    {
        public static void Ping() { }

        static HALSimDIOData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimDIOData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimDIOData(int index)
        {
            Index = index;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetDIODataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetDIODataDelegate HALSIM_ResetDIOData;
        public void ResetData()
        {
            m_initializedCallbacks.Clear();
            m_valueCallbacks.Clear();
            m_pulseLengthCallbacks.Clear();
            m_isInputCallbacks.Clear();
            m_filterIndexCallbacks.Clear();
            HALSIM_ResetDIOData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterDIOInitializedCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterDIOInitializedCallbackDelegate HALSIM_RegisterDIOInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelDIOInitializedCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelDIOInitializedCallbackDelegate HALSIM_CancelDIOInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetDIOInitializedDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetDIOInitializedDelegate HALSIM_GetDIOInitialized;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_initializedCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterDIOInitializedCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterDIOInitializedCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_initializedCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelDIOInitializedCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelDIOInitializedCallback(int uid)
        {
            HALSIM_CancelDIOInitializedCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_initializedCallbacks.TryRemove(uid, out cb);
        }
        public bool GetInitialized() => HALSIM_GetDIOInitialized(Index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterDIOValueCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterDIOValueCallbackDelegate HALSIM_RegisterDIOValueCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelDIOValueCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelDIOValueCallbackDelegate HALSIM_CancelDIOValueCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetDIOValueDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetDIOValueDelegate HALSIM_GetDIOValue;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetDIOValueDelegate(int index, bool value);
        [NativeDelegate]
        internal static HALSIM_SetDIOValueDelegate HALSIM_SetDIOValue;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_valueCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterDIOValueCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterDIOValueCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_valueCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelDIOValueCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelDIOValueCallback(int uid)
        {
            HALSIM_CancelDIOValueCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_valueCallbacks.TryRemove(uid, out cb);
        }
        public bool GetValue() => HALSIM_GetDIOValue(Index);
        public void SetValue(bool value) => HALSIM_SetDIOValue(Index, value);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterDIOPulseLengthCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterDIOPulseLengthCallbackDelegate HALSIM_RegisterDIOPulseLengthCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelDIOPulseLengthCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelDIOPulseLengthCallbackDelegate HALSIM_CancelDIOPulseLengthCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetDIOPulseLengthDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetDIOPulseLengthDelegate HALSIM_GetDIOPulseLength;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_pulseLengthCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterDIOPulseLengthCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterDIOPulseLengthCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_pulseLengthCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelDIOPulseLengthCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelDIOPulseLengthCallback(int uid)
        {
            HALSIM_CancelDIOPulseLengthCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_pulseLengthCallbacks.TryRemove(uid, out cb);
        }
        public double GetPulseLength() => HALSIM_GetDIOPulseLength(Index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterDIOIsInputCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterDIOIsInputCallbackDelegate HALSIM_RegisterDIOIsInputCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelDIOIsInputCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelDIOIsInputCallbackDelegate HALSIM_CancelDIOIsInputCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetDIOIsInputDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetDIOIsInputDelegate HALSIM_GetDIOIsInput;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_isInputCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterDIOIsInputCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterDIOIsInputCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_isInputCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelDIOIsInputCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelDIOIsInputCallback(int uid)
        {
            HALSIM_CancelDIOIsInputCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_isInputCallbacks.TryRemove(uid, out cb);
        }
        public bool GetIsInput() => HALSIM_GetDIOIsInput(Index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterDIOFilterIndexCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterDIOFilterIndexCallbackDelegate HALSIM_RegisterDIOFilterIndexCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelDIOFilterIndexCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelDIOFilterIndexCallbackDelegate HALSIM_CancelDIOFilterIndexCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetDIOFilterIndexDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetDIOFilterIndexDelegate HALSIM_GetDIOFilterIndex;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_filterIndexCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterDIOFilterIndexCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterDIOFilterIndexCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_filterIndexCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelDIOFilterIndexCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelDIOFilterIndexCallback(int uid)
        {
            HALSIM_CancelDIOFilterIndexCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_filterIndexCallbacks.TryRemove(uid, out cb);
        }
        public int GetFilterIndex() => HALSIM_GetDIOFilterIndex(Index);
    }
}
