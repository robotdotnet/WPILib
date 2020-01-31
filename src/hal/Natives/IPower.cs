using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public unsafe interface IPower
    {
        [StatusCheckLastParameter] int HAL_GetUserActive3V3();

        [StatusCheckLastParameter] int HAL_GetUserActive5V();

        [StatusCheckLastParameter] int HAL_GetUserActive6V();

        [StatusCheckLastParameter] double HAL_GetUserCurrent3V3();

        [StatusCheckLastParameter] double HAL_GetUserCurrent5V();

        [StatusCheckLastParameter] double HAL_GetUserCurrent6V();

        [StatusCheckLastParameter] int HAL_GetUserCurrentFaults3V3();

        [StatusCheckLastParameter] int HAL_GetUserCurrentFaults5V();

        [StatusCheckLastParameter] int HAL_GetUserCurrentFaults6V();

        [StatusCheckLastParameter] double HAL_GetUserVoltage3V3();

        [StatusCheckLastParameter] double HAL_GetUserVoltage5V();

        [StatusCheckLastParameter] double HAL_GetUserVoltage6V();

        [StatusCheckLastParameter] double HAL_GetVinCurrent();

        [StatusCheckLastParameter] double HAL_GetVinVoltage();

    }
}
