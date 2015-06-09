﻿
using System;
using System.Runtime.InteropServices;

namespace HAL_FRC
{
    /// Return Type: void
    ///param0: unsigned int
    ///param1: void*
    
    //public delegate void NotifierDelegate(uint param0, System.IntPtr param1);

    public class HALNotifier
    {
        /// Return Type: void*
        ///ProcessQueue: Anonymous_bc6469e1_81ca_4ce4_a849_7751f6a8b58e
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeNotifier")]
        public static extern IntPtr initializeNotifier(Action<uint, IntPtr> ProcessQueue, ref int status);


        /// Return Type: void
        ///notifier_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "cleanNotifier")]
        public static extern void cleanNotifier(IntPtr notifier_pointer, ref int status);


        /// Return Type: void
        ///notifier_pointer: void*
        ///triggerTime: unsigned int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "updateNotifierAlarm")]
        public static extern void updateNotifierAlarm(IntPtr notifier_pointer, uint triggerTime, ref int status);
    }
}
