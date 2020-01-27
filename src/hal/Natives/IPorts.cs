using WPIUtil.ILGeneration;

namespace Hal.Natives
{
   public unsafe interface IPorts
    {
         int HAL_GetNumAccumulators();

         int HAL_GetNumAddressableLEDs();

         int HAL_GetNumAnalogInputs();

         int HAL_GetNumAnalogOutputs();

         int HAL_GetNumAnalogTriggers();

         int HAL_GetNumCounters();

         int HAL_GetNumDigitalChannels();

         int HAL_GetNumDigitalHeaders();

         int HAL_GetNumDigitalPWMOutputs();

         int HAL_GetNumDutyCycles();

         int HAL_GetNumEncoders();

         int HAL_GetNumInterrupts();

         int HAL_GetNumPCMModules();

         int HAL_GetNumPDPChannels();

         int HAL_GetNumPDPModules();

         int HAL_GetNumPWMChannels();

         int HAL_GetNumPWMHeaders();

         int HAL_GetNumRelayChannels();

         int HAL_GetNumRelayHeaders();

         int HAL_GetNumSolenoidChannels();

    }
}
