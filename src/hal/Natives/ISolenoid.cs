using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1716 // Identifiers should not match keywords
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface ISolenoid
    {
        int HAL_CheckSolenoidChannel(int channel);


        int HAL_CheckSolenoidModule(int module);

        [StatusCheckLastParameter] void HAL_ClearAllPCMStickyFaults(int module);

        [StatusCheckLastParameter] void HAL_FireOneShot(int solenoidPortHandle);

        void HAL_FreeSolenoidPort(int solenoidPortHandle);

        [StatusCheckLastParameter] int HAL_GetAllSolenoids(int module);

        [StatusCheckLastParameter] int HAL_GetPCMSolenoidBlackList(int module);

        [StatusCheckLastParameter] int HAL_GetPCMSolenoidVoltageFault(int module);

        [StatusCheckLastParameter] int HAL_GetPCMSolenoidVoltageStickyFault(int module);

        [StatusCheckLastParameter] int HAL_GetSolenoid(int solenoidPortHandle);

        [StatusCheckRange(0, typeof(StatusHandling), "SolenoidStatusCheck")] int HAL_InitializeSolenoidPort(int portHandle);

        [StatusCheckLastParameter] void HAL_SetAllSolenoids(int module, int state);

        [StatusCheckLastParameter] void HAL_SetOneShotDuration(int solenoidPortHandle, int durMS);

        [StatusCheckLastParameter] void HAL_SetSolenoid(int solenoidPortHandle, int value);

    }
}
