using Hal.Natives;
using System;
using System.Collections.Generic;
using System.Text;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IPWM))]
    public static class PWM
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IPWM pwm;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static int InitializePort(int portHandle)
        {
            return pwm.HAL_InitializePWMPort(portHandle);
        }
    }
}
