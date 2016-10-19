using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using static HAL.Base.HAL;

namespace HAL.Simulator.Data
{
    public class HALSimRoboRioData
    {
        public static void Ping() { }

        static HALSimRoboRioData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimRoboRioData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimRoboRioData(int index)
        {
            Index = index;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetRoboRioDataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetRoboRioDataDelegate HALSIM_ResetRoboRioData;
        public void ResetData()
        {
            m_fPGAButtonCallbacks.Clear();
            m_vInVoltageCallbacks.Clear();
            m_vInCurrentCallbacks.Clear();
            m_userVoltage6VCallbacks.Clear();
            m_userCurrent6VCallbacks.Clear();
            m_userActive6VCallbacks.Clear();
            m_userVoltage5VCallbacks.Clear();
            m_userCurrent5VCallbacks.Clear();
            m_userActive5VCallbacks.Clear();
            m_userVoltage3V3Callbacks.Clear();
            m_userCurrent3V3Callbacks.Clear();
            m_userActive3V3Callbacks.Clear();
            m_userFaults6VCallbacks.Clear();
            m_userFaults5VCallbacks.Clear();
            m_userFaults3V3Callbacks.Clear();
            HALSIM_ResetRoboRioData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioFPGAButtonCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioFPGAButtonCallbackDelegate HALSIM_RegisterRoboRioFPGAButtonCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioFPGAButtonCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioFPGAButtonCallbackDelegate HALSIM_CancelRoboRioFPGAButtonCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetRoboRioFPGAButtonDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioFPGAButtonDelegate HALSIM_GetRoboRioFPGAButton;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioFPGAButtonDelegate(int index, bool fPGAButton);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioFPGAButtonDelegate HALSIM_SetRoboRioFPGAButton;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_fPGAButtonCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterFPGAButtonCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioFPGAButtonCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_fPGAButtonCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioFPGAButtonCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelFPGAButtonCallback(int uid)
        {
            HALSIM_CancelRoboRioFPGAButtonCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_fPGAButtonCallbacks.TryRemove(uid, out cb);
        }
        public bool GetFPGAButton() => HALSIM_GetRoboRioFPGAButton(Index);
        public void SetFPGAButton(bool fPGAButton) => HALSIM_SetRoboRioFPGAButton(Index, fPGAButton);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioVInVoltageCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioVInVoltageCallbackDelegate HALSIM_RegisterRoboRioVInVoltageCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioVInVoltageCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioVInVoltageCallbackDelegate HALSIM_CancelRoboRioVInVoltageCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetRoboRioVInVoltageDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioVInVoltageDelegate HALSIM_GetRoboRioVInVoltage;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioVInVoltageDelegate(int index, double vInVoltage);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioVInVoltageDelegate HALSIM_SetRoboRioVInVoltage;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_vInVoltageCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterVInVoltageCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioVInVoltageCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_vInVoltageCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioVInVoltageCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelVInVoltageCallback(int uid)
        {
            HALSIM_CancelRoboRioVInVoltageCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_vInVoltageCallbacks.TryRemove(uid, out cb);
        }
        public double GetVInVoltage() => HALSIM_GetRoboRioVInVoltage(Index);
        public void SetVInVoltage(double vInVoltage) => HALSIM_SetRoboRioVInVoltage(Index, vInVoltage);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioVInCurrentCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioVInCurrentCallbackDelegate HALSIM_RegisterRoboRioVInCurrentCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioVInCurrentCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioVInCurrentCallbackDelegate HALSIM_CancelRoboRioVInCurrentCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetRoboRioVInCurrentDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioVInCurrentDelegate HALSIM_GetRoboRioVInCurrent;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioVInCurrentDelegate(int index, double vInCurrent);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioVInCurrentDelegate HALSIM_SetRoboRioVInCurrent;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_vInCurrentCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterVInCurrentCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioVInCurrentCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_vInCurrentCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioVInCurrentCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelVInCurrentCallback(int uid)
        {
            HALSIM_CancelRoboRioVInCurrentCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_vInCurrentCallbacks.TryRemove(uid, out cb);
        }
        public double GetVInCurrent() => HALSIM_GetRoboRioVInCurrent(Index);
        public void SetVInCurrent(double vInCurrent) => HALSIM_SetRoboRioVInCurrent(Index, vInCurrent);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioUserVoltage6VCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioUserVoltage6VCallbackDelegate HALSIM_RegisterRoboRioUserVoltage6VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioUserVoltage6VCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioUserVoltage6VCallbackDelegate HALSIM_CancelRoboRioUserVoltage6VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetRoboRioUserVoltage6VDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioUserVoltage6VDelegate HALSIM_GetRoboRioUserVoltage6V;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioUserVoltage6VDelegate(int index, double userVoltage6V);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioUserVoltage6VDelegate HALSIM_SetRoboRioUserVoltage6V;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_userVoltage6VCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterUserVoltage6VCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioUserVoltage6VCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_userVoltage6VCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioUserVoltage6VCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelUserVoltage6VCallback(int uid)
        {
            HALSIM_CancelRoboRioUserVoltage6VCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_userVoltage6VCallbacks.TryRemove(uid, out cb);
        }
        public double GetUserVoltage6V() => HALSIM_GetRoboRioUserVoltage6V(Index);
        public void SetUserVoltage6V(double userVoltage6V) => HALSIM_SetRoboRioUserVoltage6V(Index, userVoltage6V);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioUserCurrent6VCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioUserCurrent6VCallbackDelegate HALSIM_RegisterRoboRioUserCurrent6VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioUserCurrent6VCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioUserCurrent6VCallbackDelegate HALSIM_CancelRoboRioUserCurrent6VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetRoboRioUserCurrent6VDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioUserCurrent6VDelegate HALSIM_GetRoboRioUserCurrent6V;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioUserCurrent6VDelegate(int index, double userCurrent6V);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioUserCurrent6VDelegate HALSIM_SetRoboRioUserCurrent6V;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_userCurrent6VCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterUserCurrent6VCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioUserCurrent6VCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_userCurrent6VCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioUserCurrent6VCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelUserCurrent6VCallback(int uid)
        {
            HALSIM_CancelRoboRioUserCurrent6VCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_userCurrent6VCallbacks.TryRemove(uid, out cb);
        }
        public double GetUserCurrent6V() => HALSIM_GetRoboRioUserCurrent6V(Index);
        public void SetUserCurrent6V(double userCurrent6V) => HALSIM_SetRoboRioUserCurrent6V(Index, userCurrent6V);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioUserActive6VCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioUserActive6VCallbackDelegate HALSIM_RegisterRoboRioUserActive6VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioUserActive6VCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioUserActive6VCallbackDelegate HALSIM_CancelRoboRioUserActive6VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetRoboRioUserActive6VDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioUserActive6VDelegate HALSIM_GetRoboRioUserActive6V;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioUserActive6VDelegate(int index, bool userActive6V);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioUserActive6VDelegate HALSIM_SetRoboRioUserActive6V;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_userActive6VCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterUserActive6VCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioUserActive6VCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_userActive6VCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioUserActive6VCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelUserActive6VCallback(int uid)
        {
            HALSIM_CancelRoboRioUserActive6VCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_userActive6VCallbacks.TryRemove(uid, out cb);
        }
        public bool GetUserActive6V() => HALSIM_GetRoboRioUserActive6V(Index);
        public void SetUserActive6V(bool userActive6V) => HALSIM_SetRoboRioUserActive6V(Index, userActive6V);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioUserVoltage5VCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioUserVoltage5VCallbackDelegate HALSIM_RegisterRoboRioUserVoltage5VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioUserVoltage5VCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioUserVoltage5VCallbackDelegate HALSIM_CancelRoboRioUserVoltage5VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetRoboRioUserVoltage5VDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioUserVoltage5VDelegate HALSIM_GetRoboRioUserVoltage5V;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioUserVoltage5VDelegate(int index, double userVoltage5V);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioUserVoltage5VDelegate HALSIM_SetRoboRioUserVoltage5V;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_userVoltage5VCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterUserVoltage5VCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioUserVoltage5VCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_userVoltage5VCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioUserVoltage5VCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelUserVoltage5VCallback(int uid)
        {
            HALSIM_CancelRoboRioUserVoltage5VCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_userVoltage5VCallbacks.TryRemove(uid, out cb);
        }
        public double GetUserVoltage5V() => HALSIM_GetRoboRioUserVoltage5V(Index);
        public void SetUserVoltage5V(double userVoltage5V) => HALSIM_SetRoboRioUserVoltage5V(Index, userVoltage5V);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioUserCurrent5VCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioUserCurrent5VCallbackDelegate HALSIM_RegisterRoboRioUserCurrent5VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioUserCurrent5VCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioUserCurrent5VCallbackDelegate HALSIM_CancelRoboRioUserCurrent5VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetRoboRioUserCurrent5VDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioUserCurrent5VDelegate HALSIM_GetRoboRioUserCurrent5V;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioUserCurrent5VDelegate(int index, double userCurrent5V);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioUserCurrent5VDelegate HALSIM_SetRoboRioUserCurrent5V;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_userCurrent5VCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterUserCurrent5VCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioUserCurrent5VCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_userCurrent5VCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioUserCurrent5VCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelUserCurrent5VCallback(int uid)
        {
            HALSIM_CancelRoboRioUserCurrent5VCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_userCurrent5VCallbacks.TryRemove(uid, out cb);
        }
        public double GetUserCurrent5V() => HALSIM_GetRoboRioUserCurrent5V(Index);
        public void SetUserCurrent5V(double userCurrent5V) => HALSIM_SetRoboRioUserCurrent5V(Index, userCurrent5V);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioUserActive5VCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioUserActive5VCallbackDelegate HALSIM_RegisterRoboRioUserActive5VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioUserActive5VCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioUserActive5VCallbackDelegate HALSIM_CancelRoboRioUserActive5VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetRoboRioUserActive5VDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioUserActive5VDelegate HALSIM_GetRoboRioUserActive5V;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioUserActive5VDelegate(int index, bool userActive5V);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioUserActive5VDelegate HALSIM_SetRoboRioUserActive5V;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_userActive5VCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterUserActive5VCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioUserActive5VCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_userActive5VCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioUserActive5VCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelUserActive5VCallback(int uid)
        {
            HALSIM_CancelRoboRioUserActive5VCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_userActive5VCallbacks.TryRemove(uid, out cb);
        }
        public bool GetUserActive5V() => HALSIM_GetRoboRioUserActive5V(Index);
        public void SetUserActive5V(bool userActive5V) => HALSIM_SetRoboRioUserActive5V(Index, userActive5V);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioUserVoltage3V3CallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioUserVoltage3V3CallbackDelegate HALSIM_RegisterRoboRioUserVoltage3V3Callback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioUserVoltage3V3CallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioUserVoltage3V3CallbackDelegate HALSIM_CancelRoboRioUserVoltage3V3Callback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetRoboRioUserVoltage3V3Delegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioUserVoltage3V3Delegate HALSIM_GetRoboRioUserVoltage3V3;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioUserVoltage3V3Delegate(int index, double userVoltage3V3);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioUserVoltage3V3Delegate HALSIM_SetRoboRioUserVoltage3V3;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_userVoltage3V3Callbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterUserVoltage3V3Callback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioUserVoltage3V3Callback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_userVoltage3V3Callbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioUserVoltage3V3Callback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelUserVoltage3V3Callback(int uid)
        {
            HALSIM_CancelRoboRioUserVoltage3V3Callback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_userVoltage3V3Callbacks.TryRemove(uid, out cb);
        }
        public double GetUserVoltage3V3() => HALSIM_GetRoboRioUserVoltage3V3(Index);
        public void SetUserVoltage3V3(double userVoltage3V3) => HALSIM_SetRoboRioUserVoltage3V3(Index, userVoltage3V3);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioUserCurrent3V3CallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioUserCurrent3V3CallbackDelegate HALSIM_RegisterRoboRioUserCurrent3V3Callback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioUserCurrent3V3CallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioUserCurrent3V3CallbackDelegate HALSIM_CancelRoboRioUserCurrent3V3Callback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetRoboRioUserCurrent3V3Delegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioUserCurrent3V3Delegate HALSIM_GetRoboRioUserCurrent3V3;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioUserCurrent3V3Delegate(int index, double userCurrent3V3);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioUserCurrent3V3Delegate HALSIM_SetRoboRioUserCurrent3V3;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_userCurrent3V3Callbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterUserCurrent3V3Callback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioUserCurrent3V3Callback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_userCurrent3V3Callbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioUserCurrent3V3Callback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelUserCurrent3V3Callback(int uid)
        {
            HALSIM_CancelRoboRioUserCurrent3V3Callback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_userCurrent3V3Callbacks.TryRemove(uid, out cb);
        }
        public double GetUserCurrent3V3() => HALSIM_GetRoboRioUserCurrent3V3(Index);
        public void SetUserCurrent3V3(double userCurrent3V3) => HALSIM_SetRoboRioUserCurrent3V3(Index, userCurrent3V3);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioUserActive3V3CallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioUserActive3V3CallbackDelegate HALSIM_RegisterRoboRioUserActive3V3Callback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioUserActive3V3CallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioUserActive3V3CallbackDelegate HALSIM_CancelRoboRioUserActive3V3Callback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetRoboRioUserActive3V3Delegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioUserActive3V3Delegate HALSIM_GetRoboRioUserActive3V3;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioUserActive3V3Delegate(int index, bool userActive3V3);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioUserActive3V3Delegate HALSIM_SetRoboRioUserActive3V3;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_userActive3V3Callbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterUserActive3V3Callback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioUserActive3V3Callback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_userActive3V3Callbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioUserActive3V3Callback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelUserActive3V3Callback(int uid)
        {
            HALSIM_CancelRoboRioUserActive3V3Callback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_userActive3V3Callbacks.TryRemove(uid, out cb);
        }
        public bool GetUserActive3V3() => HALSIM_GetRoboRioUserActive3V3(Index);
        public void SetUserActive3V3(bool userActive3V3) => HALSIM_SetRoboRioUserActive3V3(Index, userActive3V3);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioUserFaults6VCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioUserFaults6VCallbackDelegate HALSIM_RegisterRoboRioUserFaults6VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioUserFaults6VCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioUserFaults6VCallbackDelegate HALSIM_CancelRoboRioUserFaults6VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetRoboRioUserFaults6VDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioUserFaults6VDelegate HALSIM_GetRoboRioUserFaults6V;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioUserFaults6VDelegate(int index, int userFaults6V);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioUserFaults6VDelegate HALSIM_SetRoboRioUserFaults6V;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_userFaults6VCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterUserFaults6VCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioUserFaults6VCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_userFaults6VCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioUserFaults6VCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelUserFaults6VCallback(int uid)
        {
            HALSIM_CancelRoboRioUserFaults6VCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_userFaults6VCallbacks.TryRemove(uid, out cb);
        }
        public int GetUserFaults6V() => HALSIM_GetRoboRioUserFaults6V(Index);
        public void SetUserFaults6V(int userFaults6V) => HALSIM_SetRoboRioUserFaults6V(Index, userFaults6V);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioUserFaults5VCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioUserFaults5VCallbackDelegate HALSIM_RegisterRoboRioUserFaults5VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioUserFaults5VCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioUserFaults5VCallbackDelegate HALSIM_CancelRoboRioUserFaults5VCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetRoboRioUserFaults5VDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioUserFaults5VDelegate HALSIM_GetRoboRioUserFaults5V;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioUserFaults5VDelegate(int index, int userFaults5V);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioUserFaults5VDelegate HALSIM_SetRoboRioUserFaults5V;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_userFaults5VCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterUserFaults5VCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioUserFaults5VCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_userFaults5VCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioUserFaults5VCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelUserFaults5VCallback(int uid)
        {
            HALSIM_CancelRoboRioUserFaults5VCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_userFaults5VCallbacks.TryRemove(uid, out cb);
        }
        public int GetUserFaults5V() => HALSIM_GetRoboRioUserFaults5V(Index);
        public void SetUserFaults5V(int userFaults5V) => HALSIM_SetRoboRioUserFaults5V(Index, userFaults5V);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterRoboRioUserFaults3V3CallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterRoboRioUserFaults3V3CallbackDelegate HALSIM_RegisterRoboRioUserFaults3V3Callback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelRoboRioUserFaults3V3CallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelRoboRioUserFaults3V3CallbackDelegate HALSIM_CancelRoboRioUserFaults3V3Callback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetRoboRioUserFaults3V3Delegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetRoboRioUserFaults3V3Delegate HALSIM_GetRoboRioUserFaults3V3;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetRoboRioUserFaults3V3Delegate(int index, int userFaults3V3);
        [NativeDelegate]
        internal static HALSIM_SetRoboRioUserFaults3V3Delegate HALSIM_SetRoboRioUserFaults3V3;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_userFaults3V3Callbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterUserFaults3V3Callback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterRoboRioUserFaults3V3Callback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_userFaults3V3Callbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelRoboRioUserFaults3V3Callback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelUserFaults3V3Callback(int uid)
        {
            HALSIM_CancelRoboRioUserFaults3V3Callback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_userFaults3V3Callbacks.TryRemove(uid, out cb);
        }
        public int GetUserFaults3V3() => HALSIM_GetRoboRioUserFaults3V3(Index);
        public void SetUserFaults3V3(int userFaults3V3) => HALSIM_SetRoboRioUserFaults3V3(Index, userFaults3V3);
    }
}
