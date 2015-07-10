
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace HAL_Simulator
{
    /// Return Type: void
    ///param0: unsigned int
    ///param1: void*
    
    //public delegate void NotifierDelegate(uint param0, System.IntPtr param1);

    

    public class HALNotifier
    {
        private static List<Notifier> Notifiers = new List<Notifier>(); 


        public static IntPtr initializeNotifier(Action<uint, IntPtr> ProcessQueue, ref int status)
        {
            status = 0;
            Notifier notifier = new Notifier();
            notifier.Callback = ProcessQueue;
            Notifiers.Add(notifier);
            return (IntPtr)Notifiers.IndexOf(notifier);
            //IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(int));
            //Marshal.StructureToPtr(notifier, ptr, true);
            //return ptr;
        }

        public static void cleanNotifier(IntPtr notifier_pointer, ref int status)
        {
            status = 0;
            Notifier notifier = (Notifier) Marshal.PtrToStructure(notifier_pointer, typeof (Notifier));
            if (notifier.alarm != null && notifier.alarm.IsAlive)
            {
                notifier.alarm.Abort();
            }
            notifier.alarm?.Join();
            notifier.alarm = null;
            notifier.Callback = null;
            Notifiers.Remove(notifier);
        }


        public static void updateNotifierAlarm(IntPtr notifier_pointer, uint triggerTime, ref int status)
        {
            status = 0;
            Notifier notifier = Notifiers[notifier_pointer.ToInt32()];//(Notifier)Marshal.PtrToStructure(notifier_pointer, typeof(Notifier));
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
                notifier.Callback(0, IntPtr.Zero);
            });
            notifier.alarm.Start();
        }
    }
}
