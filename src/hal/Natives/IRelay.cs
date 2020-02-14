using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IRelay
    {
        int HAL_CheckRelayChannel(int channel);

        void HAL_FreeRelayPort(int relayPortHandle);

        [StatusCheckLastParameter] int HAL_GetRelay(int relayPortHandle);

        [StatusCheckRange(0, typeof(StatusHandling), "RelayStatusCheck")] int HAL_InitializeRelayPort(int portHandle, int fwd);

        [StatusCheckLastParameter] void HAL_SetRelay(int relayPortHandle, int setOn);

    }
}
