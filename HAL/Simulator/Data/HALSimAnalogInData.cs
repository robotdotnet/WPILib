using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using static HAL.Base.HAL;

namespace HAL.Simulator.Data
{
    public class HALSimAnalogInData
    {
        public static void Ping() { }

        static HALSimAnalogInData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimAnalogInData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimAnalogInData(int index)
        {
            Index = index;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetAnalogInDataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetAnalogInDataDelegate HALSIM_ResetAnalogInData;
        public void ResetData()
        {
            m_initializedCallbacks.Clear();
            m_averageBitsCallbacks.Clear();
            m_oversampleBitsCallbacks.Clear();
            m_voltageCallbacks.Clear();
            m_accumulatorInitializedCallbacks.Clear();
            m_accumulatorValueCallbacks.Clear();
            m_accumulatorCountCallbacks.Clear();
            m_accumulatorCenterCallbacks.Clear();
            m_accumulatorDeadbandCallbacks.Clear();
            HALSIM_ResetAnalogInData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogInInitializedCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogInInitializedCallbackDelegate HALSIM_RegisterAnalogInInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogInInitializedCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogInInitializedCallbackDelegate HALSIM_CancelAnalogInInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetAnalogInInitializedDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogInInitializedDelegate HALSIM_GetAnalogInInitialized;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogInInitializedDelegate(int index, bool initialized);
        [NativeDelegate]
        internal static HALSIM_SetAnalogInInitializedDelegate HALSIM_SetAnalogInInitialized;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_initializedCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterInitializedCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogInInitializedCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_initializedCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogInInitializedCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelInitializedCallback(int uid)
        {
            HALSIM_CancelAnalogInInitializedCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_initializedCallbacks.TryRemove(uid, out cb);
        }
        public bool GetInitialized() => HALSIM_GetAnalogInInitialized(Index);
        public void SetInitialized(bool initialized) => HALSIM_SetAnalogInInitialized(Index, initialized);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogInAverageBitsCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogInAverageBitsCallbackDelegate HALSIM_RegisterAnalogInAverageBitsCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogInAverageBitsCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogInAverageBitsCallbackDelegate HALSIM_CancelAnalogInAverageBitsCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetAnalogInAverageBitsDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogInAverageBitsDelegate HALSIM_GetAnalogInAverageBits;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogInAverageBitsDelegate(int index, int averageBits);
        [NativeDelegate]
        internal static HALSIM_SetAnalogInAverageBitsDelegate HALSIM_SetAnalogInAverageBits;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_averageBitsCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAverageBitsCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogInAverageBitsCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_averageBitsCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogInAverageBitsCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAverageBitsCallback(int uid)
        {
            HALSIM_CancelAnalogInAverageBitsCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_averageBitsCallbacks.TryRemove(uid, out cb);
        }
        public int GetAverageBits() => HALSIM_GetAnalogInAverageBits(Index);
        public void SetAverageBits(int averageBits) => HALSIM_SetAnalogInAverageBits(Index, averageBits);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogInOversampleBitsCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogInOversampleBitsCallbackDelegate HALSIM_RegisterAnalogInOversampleBitsCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogInOversampleBitsCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogInOversampleBitsCallbackDelegate HALSIM_CancelAnalogInOversampleBitsCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetAnalogInOversampleBitsDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogInOversampleBitsDelegate HALSIM_GetAnalogInOversampleBits;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogInOversampleBitsDelegate(int index, int oversampleBits);
        [NativeDelegate]
        internal static HALSIM_SetAnalogInOversampleBitsDelegate HALSIM_SetAnalogInOversampleBits;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_oversampleBitsCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterOversampleBitsCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogInOversampleBitsCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_oversampleBitsCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogInOversampleBitsCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelOversampleBitsCallback(int uid)
        {
            HALSIM_CancelAnalogInOversampleBitsCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_oversampleBitsCallbacks.TryRemove(uid, out cb);
        }
        public int GetOversampleBits() => HALSIM_GetAnalogInOversampleBits(Index);
        public void SetOversampleBits(int oversampleBits) => HALSIM_SetAnalogInOversampleBits(Index, oversampleBits);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogInVoltageCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogInVoltageCallbackDelegate HALSIM_RegisterAnalogInVoltageCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogInVoltageCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogInVoltageCallbackDelegate HALSIM_CancelAnalogInVoltageCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetAnalogInVoltageDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogInVoltageDelegate HALSIM_GetAnalogInVoltage;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogInVoltageDelegate(int index, double voltage);
        [NativeDelegate]
        internal static HALSIM_SetAnalogInVoltageDelegate HALSIM_SetAnalogInVoltage;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_voltageCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterVoltageCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogInVoltageCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_voltageCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogInVoltageCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelVoltageCallback(int uid)
        {
            HALSIM_CancelAnalogInVoltageCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_voltageCallbacks.TryRemove(uid, out cb);
        }
        public double GetVoltage() => HALSIM_GetAnalogInVoltage(Index);
        public void SetVoltage(double voltage) => HALSIM_SetAnalogInVoltage(Index, voltage);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogInAccumulatorInitializedCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogInAccumulatorInitializedCallbackDelegate HALSIM_RegisterAnalogInAccumulatorInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogInAccumulatorInitializedCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogInAccumulatorInitializedCallbackDelegate HALSIM_CancelAnalogInAccumulatorInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetAnalogInAccumulatorInitializedDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogInAccumulatorInitializedDelegate HALSIM_GetAnalogInAccumulatorInitialized;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogInAccumulatorInitializedDelegate(int index, bool accumulatorInitialized);
        [NativeDelegate]
        internal static HALSIM_SetAnalogInAccumulatorInitializedDelegate HALSIM_SetAnalogInAccumulatorInitialized;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_accumulatorInitializedCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAccumulatorInitializedCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogInAccumulatorInitializedCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_accumulatorInitializedCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogInAccumulatorInitializedCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAccumulatorInitializedCallback(int uid)
        {
            HALSIM_CancelAnalogInAccumulatorInitializedCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_accumulatorInitializedCallbacks.TryRemove(uid, out cb);
        }
        public bool GetAccumulatorInitialized() => HALSIM_GetAnalogInAccumulatorInitialized(Index);
        public void SetAccumulatorInitialized(bool accumulatorInitialized) => HALSIM_SetAnalogInAccumulatorInitialized(Index, accumulatorInitialized);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogInAccumulatorValueCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogInAccumulatorValueCallbackDelegate HALSIM_RegisterAnalogInAccumulatorValueCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogInAccumulatorValueCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogInAccumulatorValueCallbackDelegate HALSIM_CancelAnalogInAccumulatorValueCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate long HALSIM_GetAnalogInAccumulatorValueDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogInAccumulatorValueDelegate HALSIM_GetAnalogInAccumulatorValue;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogInAccumulatorValueDelegate(int index, long accumulatorValue);
        [NativeDelegate]
        internal static HALSIM_SetAnalogInAccumulatorValueDelegate HALSIM_SetAnalogInAccumulatorValue;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_accumulatorValueCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAccumulatorValueCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogInAccumulatorValueCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_accumulatorValueCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogInAccumulatorValueCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAccumulatorValueCallback(int uid)
        {
            HALSIM_CancelAnalogInAccumulatorValueCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_accumulatorValueCallbacks.TryRemove(uid, out cb);
        }
        public long GetAccumulatorValue() => HALSIM_GetAnalogInAccumulatorValue(Index);
        public void SetAccumulatorValue(long accumulatorValue) => HALSIM_SetAnalogInAccumulatorValue(Index, accumulatorValue);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogInAccumulatorCountCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogInAccumulatorCountCallbackDelegate HALSIM_RegisterAnalogInAccumulatorCountCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogInAccumulatorCountCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogInAccumulatorCountCallbackDelegate HALSIM_CancelAnalogInAccumulatorCountCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate long HALSIM_GetAnalogInAccumulatorCountDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogInAccumulatorCountDelegate HALSIM_GetAnalogInAccumulatorCount;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogInAccumulatorCountDelegate(int index, long accumulatorCount);
        [NativeDelegate]
        internal static HALSIM_SetAnalogInAccumulatorCountDelegate HALSIM_SetAnalogInAccumulatorCount;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_accumulatorCountCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAccumulatorCountCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogInAccumulatorCountCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_accumulatorCountCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogInAccumulatorCountCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAccumulatorCountCallback(int uid)
        {
            HALSIM_CancelAnalogInAccumulatorCountCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_accumulatorCountCallbacks.TryRemove(uid, out cb);
        }
        public long GetAccumulatorCount() => HALSIM_GetAnalogInAccumulatorCount(Index);
        public void SetAccumulatorCount(long accumulatorCount) => HALSIM_SetAnalogInAccumulatorCount(Index, accumulatorCount);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogInAccumulatorCenterCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogInAccumulatorCenterCallbackDelegate HALSIM_RegisterAnalogInAccumulatorCenterCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogInAccumulatorCenterCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogInAccumulatorCenterCallbackDelegate HALSIM_CancelAnalogInAccumulatorCenterCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetAnalogInAccumulatorCenterDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogInAccumulatorCenterDelegate HALSIM_GetAnalogInAccumulatorCenter;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogInAccumulatorCenterDelegate(int index, int accumulatorCenter);
        [NativeDelegate]
        internal static HALSIM_SetAnalogInAccumulatorCenterDelegate HALSIM_SetAnalogInAccumulatorCenter;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_accumulatorCenterCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAccumulatorCenterCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogInAccumulatorCenterCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_accumulatorCenterCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogInAccumulatorCenterCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAccumulatorCenterCallback(int uid)
        {
            HALSIM_CancelAnalogInAccumulatorCenterCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_accumulatorCenterCallbacks.TryRemove(uid, out cb);
        }
        public int GetAccumulatorCenter() => HALSIM_GetAnalogInAccumulatorCenter(Index);
        public void SetAccumulatorCenter(int accumulatorCenter) => HALSIM_SetAnalogInAccumulatorCenter(Index, accumulatorCenter);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterAnalogInAccumulatorDeadbandCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterAnalogInAccumulatorDeadbandCallbackDelegate HALSIM_RegisterAnalogInAccumulatorDeadbandCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelAnalogInAccumulatorDeadbandCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelAnalogInAccumulatorDeadbandCallbackDelegate HALSIM_CancelAnalogInAccumulatorDeadbandCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetAnalogInAccumulatorDeadbandDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetAnalogInAccumulatorDeadbandDelegate HALSIM_GetAnalogInAccumulatorDeadband;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetAnalogInAccumulatorDeadbandDelegate(int index, int accumulatorDeadband);
        [NativeDelegate]
        internal static HALSIM_SetAnalogInAccumulatorDeadbandDelegate HALSIM_SetAnalogInAccumulatorDeadband;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_accumulatorDeadbandCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterAccumulatorDeadbandCallback(NotifyCallback callback, bool initialNotify = false)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, value);
            };
            int uid = HALSIM_RegisterAnalogInAccumulatorDeadbandCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_accumulatorDeadbandCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelAnalogInAccumulatorDeadbandCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelAccumulatorDeadbandCallback(int uid)
        {
            HALSIM_CancelAnalogInAccumulatorDeadbandCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_accumulatorDeadbandCallbacks.TryRemove(uid, out cb);
        }
        public int GetAccumulatorDeadband() => HALSIM_GetAnalogInAccumulatorDeadband(Index);
        public void SetAccumulatorDeadband(int accumulatorDeadband) => HALSIM_SetAnalogInAccumulatorDeadband(Index, accumulatorDeadband);
    }
}
