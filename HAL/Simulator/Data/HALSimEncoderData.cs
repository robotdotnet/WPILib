using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using HAL.Base;
using HAL.NativeLoader;
using static HAL.Base.HAL;

namespace HAL.Simulator.Data
{
    public class HALSimEncoderData
    {
        public static void Ping() { }

        static HALSimEncoderData()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSimEncoderData>(LibraryLoaderHolder.NativeLoader);
        }
        public int Index { get; }
        public HALSimEncoderData(int index)
        {
            Index = index;
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_ResetEncoderDataDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_ResetEncoderDataDelegate HALSIM_ResetEncoderData;
        public void ResetData()
        {
            m_initializedCallbacks.Clear();
            m_countCallbacks.Clear();
            m_periodCallbacks.Clear();
            m_resetCallbacks.Clear();
            m_maxPeriodCallbacks.Clear();
            m_directionCallbacks.Clear();
            m_reverseDirectionCallbacks.Clear();
            m_samplesToAverageCallbacks.Clear();
            HALSIM_ResetEncoderData(Index);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterEncoderInitializedCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterEncoderInitializedCallbackDelegate HALSIM_RegisterEncoderInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelEncoderInitializedCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelEncoderInitializedCallbackDelegate HALSIM_CancelEncoderInitializedCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetEncoderInitializedDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetEncoderInitializedDelegate HALSIM_GetEncoderInitialized;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetEncoderInitializedDelegate(int index, bool initialized);
        [NativeDelegate]
        internal static HALSIM_SetEncoderInitializedDelegate HALSIM_SetEncoderInitialized;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_initializedCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterInitializedCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterEncoderInitializedCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_initializedCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelEncoderInitializedCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelInitializedCallback(int uid)
        {
            HALSIM_CancelEncoderInitializedCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_initializedCallbacks.TryRemove(uid, out cb);
        }
        public bool GetInitialized() => HALSIM_GetEncoderInitialized(Index);
        public void SetInitialized(bool initialized) => HALSIM_SetEncoderInitialized(Index, initialized);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterEncoderCountCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterEncoderCountCallbackDelegate HALSIM_RegisterEncoderCountCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelEncoderCountCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelEncoderCountCallbackDelegate HALSIM_CancelEncoderCountCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetEncoderCountDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetEncoderCountDelegate HALSIM_GetEncoderCount;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetEncoderCountDelegate(int index, int count);
        [NativeDelegate]
        internal static HALSIM_SetEncoderCountDelegate HALSIM_SetEncoderCount;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_countCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterCountCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterEncoderCountCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_countCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelEncoderCountCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelCountCallback(int uid)
        {
            HALSIM_CancelEncoderCountCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_countCallbacks.TryRemove(uid, out cb);
        }
        public int GetCount() => HALSIM_GetEncoderCount(Index);
        public void SetCount(int count) => HALSIM_SetEncoderCount(Index, count);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterEncoderPeriodCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterEncoderPeriodCallbackDelegate HALSIM_RegisterEncoderPeriodCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelEncoderPeriodCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelEncoderPeriodCallbackDelegate HALSIM_CancelEncoderPeriodCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetEncoderPeriodDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetEncoderPeriodDelegate HALSIM_GetEncoderPeriod;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetEncoderPeriodDelegate(int index, double period);
        [NativeDelegate]
        internal static HALSIM_SetEncoderPeriodDelegate HALSIM_SetEncoderPeriod;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_periodCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterPeriodCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterEncoderPeriodCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_periodCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelEncoderPeriodCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelPeriodCallback(int uid)
        {
            HALSIM_CancelEncoderPeriodCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_periodCallbacks.TryRemove(uid, out cb);
        }
        public double GetPeriod() => HALSIM_GetEncoderPeriod(Index);
        public void SetPeriod(double period) => HALSIM_SetEncoderPeriod(Index, period);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterEncoderResetCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterEncoderResetCallbackDelegate HALSIM_RegisterEncoderResetCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelEncoderResetCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelEncoderResetCallbackDelegate HALSIM_CancelEncoderResetCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetEncoderResetDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetEncoderResetDelegate HALSIM_GetEncoderReset;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetEncoderResetDelegate(int index, bool reset);
        [NativeDelegate]
        internal static HALSIM_SetEncoderResetDelegate HALSIM_SetEncoderReset;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_resetCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterResetCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterEncoderResetCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_resetCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelEncoderResetCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelResetCallback(int uid)
        {
            HALSIM_CancelEncoderResetCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_resetCallbacks.TryRemove(uid, out cb);
        }
        public bool GetReset() => HALSIM_GetEncoderReset(Index);
        public void SetReset(bool reset) => HALSIM_SetEncoderReset(Index, reset);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterEncoderMaxPeriodCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterEncoderMaxPeriodCallbackDelegate HALSIM_RegisterEncoderMaxPeriodCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelEncoderMaxPeriodCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelEncoderMaxPeriodCallbackDelegate HALSIM_CancelEncoderMaxPeriodCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate double HALSIM_GetEncoderMaxPeriodDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetEncoderMaxPeriodDelegate HALSIM_GetEncoderMaxPeriod;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetEncoderMaxPeriodDelegate(int index, double maxPeriod);
        [NativeDelegate]
        internal static HALSIM_SetEncoderMaxPeriodDelegate HALSIM_SetEncoderMaxPeriod;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_maxPeriodCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterMaxPeriodCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterEncoderMaxPeriodCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_maxPeriodCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelEncoderMaxPeriodCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelMaxPeriodCallback(int uid)
        {
            HALSIM_CancelEncoderMaxPeriodCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_maxPeriodCallbacks.TryRemove(uid, out cb);
        }
        public double GetMaxPeriod() => HALSIM_GetEncoderMaxPeriod(Index);
        public void SetMaxPeriod(double maxPeriod) => HALSIM_SetEncoderMaxPeriod(Index, maxPeriod);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterEncoderDirectionCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterEncoderDirectionCallbackDelegate HALSIM_RegisterEncoderDirectionCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelEncoderDirectionCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelEncoderDirectionCallbackDelegate HALSIM_CancelEncoderDirectionCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetEncoderDirectionDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetEncoderDirectionDelegate HALSIM_GetEncoderDirection;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetEncoderDirectionDelegate(int index, bool direction);
        [NativeDelegate]
        internal static HALSIM_SetEncoderDirectionDelegate HALSIM_SetEncoderDirection;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_directionCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterDirectionCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterEncoderDirectionCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_directionCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelEncoderDirectionCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelDirectionCallback(int uid)
        {
            HALSIM_CancelEncoderDirectionCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_directionCallbacks.TryRemove(uid, out cb);
        }
        public bool GetDirection() => HALSIM_GetEncoderDirection(Index);
        public void SetDirection(bool direction) => HALSIM_SetEncoderDirection(Index, direction);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterEncoderReverseDirectionCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterEncoderReverseDirectionCallbackDelegate HALSIM_RegisterEncoderReverseDirectionCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelEncoderReverseDirectionCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelEncoderReverseDirectionCallbackDelegate HALSIM_CancelEncoderReverseDirectionCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool HALSIM_GetEncoderReverseDirectionDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetEncoderReverseDirectionDelegate HALSIM_GetEncoderReverseDirection;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetEncoderReverseDirectionDelegate(int index, bool reverseDirection);
        [NativeDelegate]
        internal static HALSIM_SetEncoderReverseDirectionDelegate HALSIM_SetEncoderReverseDirection;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_reverseDirectionCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterReverseDirectionCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterEncoderReverseDirectionCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_reverseDirectionCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelEncoderReverseDirectionCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelReverseDirectionCallback(int uid)
        {
            HALSIM_CancelEncoderReverseDirectionCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_reverseDirectionCallbacks.TryRemove(uid, out cb);
        }
        public bool GetReverseDirection() => HALSIM_GetEncoderReverseDirection(Index);
        public void SetReverseDirection(bool reverseDirection) => HALSIM_SetEncoderReverseDirection(Index, reverseDirection);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_RegisterEncoderSamplesToAverageCallbackDelegate(int index, HAL_NotifyCallback callback, IntPtr param, bool initialNotify);
        [NativeDelegate]
        internal static HALSIM_RegisterEncoderSamplesToAverageCallbackDelegate HALSIM_RegisterEncoderSamplesToAverageCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_CancelEncoderSamplesToAverageCallbackDelegate(int index, int uid);
        [NativeDelegate]
        internal static HALSIM_CancelEncoderSamplesToAverageCallbackDelegate HALSIM_CancelEncoderSamplesToAverageCallback;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int HALSIM_GetEncoderSamplesToAverageDelegate(int index);
        [NativeDelegate]
        internal static HALSIM_GetEncoderSamplesToAverageDelegate HALSIM_GetEncoderSamplesToAverage;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void HALSIM_SetEncoderSamplesToAverageDelegate(int index, int samplesToAverage);
        [NativeDelegate]
        internal static HALSIM_SetEncoderSamplesToAverageDelegate HALSIM_SetEncoderSamplesToAverage;
        private readonly ConcurrentDictionary<int, HAL_NotifyCallback> m_samplesToAverageCallbacks = new ConcurrentDictionary<int, HAL_NotifyCallback>();
        public int RegisterSamplesToAverageCallback(NotifyCallback callback, bool initialNotify)
        {
            HAL_NotifyCallback modCallback = (IntPtr namePtr, IntPtr param, ref HAL_Value value) =>
            {
                string varName = ReadUTF8String(namePtr);
                callback?.Invoke(varName, ref value);
            };
            int uid = HALSIM_RegisterEncoderSamplesToAverageCallback(Index, modCallback, IntPtr.Zero, initialNotify);
            if (!m_samplesToAverageCallbacks.TryAdd(uid, modCallback))
            {
                HALSIM_CancelEncoderSamplesToAverageCallback(Index, uid);
                throw new ArgumentException("Key cannot be added multiple times to the dictionary");
            }
            return uid;
        }
        public void CancelSamplesToAverageCallback(int uid)
        {
            HALSIM_CancelEncoderSamplesToAverageCallback(Index, uid);
            HAL_NotifyCallback cb = null;
            m_samplesToAverageCallbacks.TryRemove(uid, out cb);
        }
        public int GetSamplesToAverage() => HALSIM_GetEncoderSamplesToAverage(Index);
        public void SetSamplesToAverage(int samplesToAverage) => HALSIM_SetEncoderSamplesToAverage(Index, samplesToAverage);
    }
}
