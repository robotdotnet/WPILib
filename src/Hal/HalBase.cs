using Hal.Natives;
using System;

namespace Hal
{
    [HalInterface(typeof(IHalBase))]
    public static class HalBase
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IHalBase halBase;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public static bool HAL_Initialize()
        {
            if (!HalInitializer.Initialize())
            {
                return false;
            }
            return halBase.HAL_Initialize(500, 0) != 0;
        }
    }
}
