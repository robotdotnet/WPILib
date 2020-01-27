using WPIUtil.ILGeneration;

namespace Hal.Natives
{
   public unsafe interface IPDP
    {
         int HAL_CheckPDPChannel(int channel);

         int HAL_CheckPDPModule(int module);

         void HAL_CleanPDP(int handle);

        [StatusCheckLastParameter]  void HAL_ClearPDPStickyFaults(int handle);

        [StatusCheckLastParameter]  void HAL_GetPDPAllChannelCurrents(int handle, double* currents);

        [StatusCheckLastParameter]  double HAL_GetPDPChannelCurrent(int handle, int channel);

        [StatusCheckLastParameter]  double HAL_GetPDPTemperature(int handle);

        [StatusCheckLastParameter]  double HAL_GetPDPTotalCurrent(int handle);

        [StatusCheckLastParameter]  double HAL_GetPDPTotalEnergy(int handle);

        [StatusCheckLastParameter]  double HAL_GetPDPTotalPower(int handle);

        [StatusCheckLastParameter]  double HAL_GetPDPVoltage(int handle);

        [StatusCheckLastParameter]  int HAL_InitializePDP(int module);

        [StatusCheckLastParameter]  void HAL_ResetPDPTotalEnergy(int handle);

    }
}
