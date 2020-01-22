using Hal.Natives;
using System;
using System.Collections.Generic;
using System.Text;
using WPIUtil;
using WPIUtil.ILGeneration;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(INotifier))]
    public static unsafe class Notifier
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static INotifier notifier;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public static int Initialize()
        {
            return notifier.HAL_InitializeNotifier();
        }

        public static void SetName(int notifierHandle, ReadOnlySpan<char> name)
        {
            UTF8String str = new UTF8String(name);
            fixed (byte* b = str.Buffer)
            {
                notifier.HAL_SetNotifierName(notifierHandle, b);
            }
        }

        public static void SetName(int notifierHandle, string name)
        {
            UTF8String str = new UTF8String(name);
            fixed(byte* b = str.Buffer)
            {
                notifier.HAL_SetNotifierName(notifierHandle, b);
            }
        }

        public static void Stop(int notifierHandle)
        {
            notifier.HAL_StopNotifier(notifierHandle);
        }

        public static void Clean(int notifierHandle)
        {
            notifier.HAL_CleanNotifier(notifierHandle);
        }

        public static void UpdateAlarm(int notifierHandle, ulong triggerTime)
        {
            notifier.HAL_UpdateNotifierAlarm(notifierHandle, triggerTime);
        }

        public static void CancelAlarm(int notifierHandle)
        {
            notifier.HAL_CancelNotifierAlarm(notifierHandle);
        }

        public static ulong WaitForAlarm(int notifierHandle)
        {
            return notifier.HAL_WaitForNotifierAlarm(notifierHandle);
        }

        public static ulong WaitForAlarm(int notifierHandle, out int status)
        {
            int statusPtr = 0;
            var retVal = notifier.HAL_WaitForNotifierAlarm(notifierHandle, &statusPtr);
            status = statusPtr;
            return retVal;
        }
    }
}
