using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IPDP
    {
        int HAL_CheckPDPChannel(int channel);

#pragma warning disable CA1716 // Identifiers should not match keywords
        int HAL_CheckPDPModule(int module);
#pragma warning restore CA1716 // Identifiers should not match keywords

        void HAL_CleanPDP(int handle);

        [StatusCheckLastParameter] void HAL_ClearPDPStickyFaults(int handle);

        [StatusCheckLastParameter] void HAL_GetPDPAllChannelCurrents(int handle, double* currents);

        [StatusCheckLastParameter] double HAL_GetPDPChannelCurrent(int handle, int channel);

        [StatusCheckLastParameter] double HAL_GetPDPTemperature(int handle);

        [StatusCheckLastParameter] double HAL_GetPDPTotalCurrent(int handle);

        [StatusCheckLastParameter] double HAL_GetPDPTotalEnergy(int handle);

        [StatusCheckLastParameter] double HAL_GetPDPTotalPower(int handle);

        [StatusCheckLastParameter] double HAL_GetPDPVoltage(int handle);

#pragma warning disable CA1716 // Identifiers should not match keywords
        [StatusCheckRange(0, typeof(StatusHandling), "PDPStatusCheck")] int HAL_InitializePDP(int module);
#pragma warning restore CA1716 // Identifiers should not match keywords

        [StatusCheckLastParameter] void HAL_ResetPDPTotalEnergy(int handle);

    }
}
