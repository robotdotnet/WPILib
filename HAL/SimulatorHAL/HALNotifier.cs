using System;
using System.Collections.Generic;
using System.Threading;
using HAL.Base;
using HAL.Simulator;
using System.Runtime.InteropServices;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming

#pragma warning disable 1591

namespace HAL.SimulatorHAL
{
    ///<inheritdoc cref="HAL"/>
    internal class HALNotifier
    {
        private const uint TimerInterruptNumber = 28;
        private static readonly object s_notifierInterruptMutex = new object();
        private static readonly object s_notifierMutex = new object();

        private static NotifierAlarm s_alarm;

        private static ulong closestTrigger = ulong.MaxValue;

        private class Notifier
        {
            public Notifier prev, next;
            public IntPtr param;
            public Action<ulong, IntPtr> process;
            public ulong triggerTime = ulong.MaxValue;
            public int index;
        }

        private static Notifier notifiers = null;
        private static int notifierRefCount = 0;

        private static void AlarmCallback(ulong mask, IntPtr o)
        {
            bool lockWasTaken = false;
            var temp = s_notifierMutex;
            try
            {
                Monitor.Enter(temp, ref lockWasTaken);
                int status = 0;
                ulong currentTime = 0;
                closestTrigger = ulong.MaxValue;

                Notifier notifier = notifiers;
                while (notifier != null)
                {
                    if (notifier.triggerTime != ulong.MaxValue)
                    {
                        if (currentTime == 0)
                            currentTime = (ulong)Base.HAL.GetFPGATime(ref status);
                        if (notifier.triggerTime < currentTime)
                        {
                            notifier.triggerTime = ulong.MaxValue;
                            var process = notifier.process;
                            var param = notifier.param;
                            Monitor.Exit(temp);
                            process(currentTime, param);
                            Monitor.Enter(temp);
                        }
                        else if (notifier.triggerTime < closestTrigger)
                        {
                            updateNotifierAlarm((IntPtr)notifier.index, notifier.triggerTime, ref status);
                        }
                    }
                    notifier = notifier.next;
                }
            }
            finally
            {
                if (lockWasTaken)
                {
                    Monitor.Exit(temp);
                }
            }
        }

        static int s_notifierCount = 1;

        private static readonly Dictionary<int, Notifier> Notifiers = new Dictionary<int, Notifier>();

        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALNotifier.InitializeNotifier = initializeNotifier;
            Base.HALNotifier.GetNotifierParam = getNotifierParam;
            Base.HALNotifier.CleanNotifier = cleanNotifier;
            Base.HALNotifier.UpdateNotifierAlarm = updateNotifierAlarm;
            Base.HALNotifier.StopNotifierAlarm = stopNotifierAlarm;
        }

        [CalledSimFunction]
        public static IntPtr initializeNotifier(Action<ulong, IntPtr> process, IntPtr param, ref int status)
        {
            if (process == null)
            {
                status = HALErrorConstants.NULL_PARAMETER;
                return IntPtr.Zero;
            }
            if (Interlocked.Increment(ref notifierRefCount) == 1)
            {
                lock (s_notifierInterruptMutex)
                {
                    //Create manager and alarm if not already created
                    s_alarm = new NotifierAlarm(AlarmCallback);
                }
            }
            lock (s_notifierMutex)
            {
                Notifier notifier = new Notifier();
                notifier.prev = null;
                notifier.next = notifiers;
                if (notifier.next != null) notifier.next.prev = notifier;
                notifier.param = param;
                notifier.process = process;
                notifier.index = s_notifierCount;
                notifiers = notifier;
                

                Notifiers.Add(s_notifierCount, notifier);
                s_notifierCount++;

                return (IntPtr)s_notifierCount - 1;
            }
        }

        public static IntPtr getNotifierParam(IntPtr notifier_pointer, ref int status)
        {
            return Notifiers[notifier_pointer.ToInt32()].param;
        }

        [CalledSimFunction]
        public static void cleanNotifier(IntPtr notifier_pointer, ref int status)
        {
            lock (s_notifierMutex)
            {
                Notifier notifier = Notifiers[notifier_pointer.ToInt32()];
                if (notifier.prev != null) notifier.prev.next = notifier.next;
                if (notifier.next != null) notifier.next.prev = notifier.prev;
                if (notifiers == notifier) notifiers = notifier.next;
                Notifiers.Remove(notifier_pointer.ToInt32());
                s_notifierCount--;
            }
            
            if (Interlocked.Decrement(ref notifierRefCount) == 0)
            {
                lock (s_notifierInterruptMutex)
                {
                    if (s_alarm != null)
                    {
                        //Clean up alarm and manager
                        s_alarm.Dispose();
                        s_alarm = null;
                        //Reset closest trigger to max value for next time.
                        closestTrigger = ulong.MaxValue;
                    }

                }
            }
            
        }

        public static void stopNotifierAlarm(IntPtr notifier_pointer, ref int status)
        {
            lock (s_notifierMutex)
            {
                Notifier notifier = Notifiers[notifier_pointer.ToInt32()];
                notifier.triggerTime = ulong.MaxValue;
            }
        }



        [CalledSimFunction]
        public static void updateNotifierAlarm(IntPtr notifier_pointer, ulong triggerTime, ref int status)
        {
            lock (s_notifierMutex)
            {
                Notifier notifier = Notifiers[notifier_pointer.ToInt32()];
                notifier.triggerTime = triggerTime;
                bool wasActive = (closestTrigger != ulong.MaxValue);

                bool lockWasTaken = false;
                var temp = s_notifierInterruptMutex;
                try
                {
                    Monitor.TryEnter(temp, ref lockWasTaken);
                    if (!lockWasTaken || notifierRefCount == 0 || s_alarm == null)
                    {
                        return;
                    }
                    if (triggerTime < closestTrigger)
                    {
                        closestTrigger = triggerTime;
                        s_alarm.WriteTriggerTime(triggerTime);
                    }
                    if (!wasActive)
                    {
                        //Activate
                        s_alarm.EnableAlarm();
                    }
                }
                finally
                {
                    if (lockWasTaken)
                    {
                        Monitor.Exit(temp);
                    }
                }
            }
        }
    }
}
