using System;
using System.Collections.Generic;
using System.Threading;
using HAL.Base;
using HAL.Simulator;

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


        private static uint closestTrigger = uint.MaxValue;

        private class Notifier
        {
            public Notifier prev, next;
            public IntPtr param;
            public Action<uint, IntPtr> process;
            public uint triggerTime = uint.MaxValue;
        }

        private static Notifier notifiers = null;
        private static int notifierRefCount = 0;

        private static void AlarmCallback(uint mask, IntPtr o)
        {
            bool lockWasTaken = false;
            var temp = s_notifierMutex;
            try
            {
                Monitor.Enter(temp, ref lockWasTaken);
                int status = 0;
                uint currentTime = 0;
                closestTrigger = uint.MaxValue;

                Notifier notifier = notifiers;
                while (notifier != null)
                {
                    if (notifier.triggerTime != uint.MaxValue)
                    {
                        if (currentTime == 0)
                            currentTime = Base.HAL.GetFPGATime(ref status);
                        if (notifier.triggerTime < currentTime)
                        {
                            notifier.triggerTime = uint.MaxValue;
                            var process = notifier.process;
                            var param = notifier.param;
                            Monitor.Exit(temp);
                            process(currentTime, param);
                            Monitor.Enter(temp);
                        }
                        else if (notifier.triggerTime < closestTrigger)
                        {
                            updateNotifierAlarm((IntPtr)Notifiers.IndexOf(notifier), notifier.triggerTime, ref status);
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

        private static readonly List<Notifier> Notifiers = new List<Notifier>();

        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALNotifier.InitializeNotifier = initializeNotifier;
            Base.HALNotifier.GetNotifierParam = getNotifierParam;
            Base.HALNotifier.CleanNotifier = cleanNotifier;
            Base.HALNotifier.UpdateNotifierAlarm = updateNotifierAlarm;
            Base.HALNotifier.StopNotifierAlarm = stopNotifierAlarm;
        }

        [CalledSimFunction]
        public static IntPtr initializeNotifier(Action<uint, IntPtr> process, IntPtr param, ref int status)
        {
            if (process == null)
            {
                status = -1005;
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
                notifiers = notifier;

                Notifiers.Add(notifier);

                return (IntPtr)Notifiers.IndexOf(notifier);
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
                Notifiers.Remove(notifier);
            }

            if (Interlocked.Decrement(ref notifierRefCount) == 0)
            {
                lock (s_notifierInterruptMutex)
                {
                    //Clean up alarm and manager
                    s_alarm.Dispose();
                }
            }
        }

        public static void stopNotifierAlarm(IntPtr notifier_pointer, ref int status)
        {
            lock (s_notifierMutex)
            {
                Notifier notifier = Notifiers[notifier_pointer.ToInt32()];
                notifier.triggerTime = uint.MaxValue;
            }
        }



        [CalledSimFunction]
        public static void updateNotifierAlarm(IntPtr notifier_pointer, uint triggerTime, ref int status)
        {
            lock (s_notifierMutex)
            {
                Notifier notifier = Notifiers[notifier_pointer.ToInt32()];
                notifier.triggerTime = triggerTime;
                bool wasActive = (closestTrigger != uint.MaxValue);

                bool lockWasTaken = false;
                var temp = s_notifierInterruptMutex;
                try
                {
                    lockWasTaken = Monitor.TryEnter(temp);
                    if (!lockWasTaken || notifierRefCount == 0)
                    {
                        return;
                    }
                    if (triggerTime < closestTrigger)
                    {
                        closestTrigger = triggerTime;
                        //WriteTriggerTimeToAlarm
                        s_alarm.WriteTriggerTime(triggerTime);
                    }
                    //Enable the alarm
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
