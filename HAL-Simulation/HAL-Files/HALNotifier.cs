using System;
using System.Collections.Generic;
using System.Threading;
using HAL_Base;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALNotifier
    {
        private static readonly List<Notifier> Notifiers = new List<Notifier>();

        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALNotifier.InitializeNotifier = initializeNotifier;
            HAL_Base.HALNotifier.CleanNotifier = cleanNotifier;
            HAL_Base.HALNotifier.UpdateNotifierAlarm = updateNotifierAlarm;
        }

        [CalledSimFunction]
        public static IntPtr initializeNotifier(Action<uint, IntPtr> ProcessQueue, ref int status)
        {
            status = 0;
            Notifier notifier = new Notifier {Callback = ProcessQueue};
            Notifiers.Add(notifier);
            return (IntPtr)Notifiers.IndexOf(notifier);
            //IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(int));
            //Marshal.StructureToPtr(notifier, ptr, true);
            //return ptr;
        }

        [CalledSimFunction]
        public static void cleanNotifier(IntPtr notifier_pointer, ref int status)
        {
            status = 0;
            Notifier notifier = Notifiers[notifier_pointer.ToInt32()];
            if (notifier.alarm != null && notifier.alarm.IsAlive)
            {
                notifier.alarm.Abort();
            }
            notifier.alarm?.Join();
            notifier.alarm = null;
            notifier.Callback = null;
            Notifiers.Remove(notifier);
        }


        [CalledSimFunction]
        public static void updateNotifierAlarm(IntPtr notifier_pointer, uint triggerTime, ref int status)
        {
            status = 0;
            Notifier notifier = Notifiers[notifier_pointer.ToInt32()];
            if (notifier.alarm != null && notifier.alarm.IsAlive)
            {
                notifier.alarm.Abort();
            }
            notifier.alarm?.Join();
            notifier.alarm = new Thread(() =>
            {
                while (triggerTime > SimHooks.GetFPGATime())
                {
                }
                if (notifier.Callback == null)
                    Console.WriteLine("Callback Null");
                notifier.Callback?.Invoke(0, IntPtr.Zero);
            });
            notifier.alarm.Start();
        }
    }
}
