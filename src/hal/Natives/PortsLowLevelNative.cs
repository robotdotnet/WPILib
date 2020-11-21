using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class PortsLowLevelNative : IPorts
    {
        [NativeFunctionPointer("HAL_GetNumAccumulators")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumAccumulatorsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumAccumulators()
        {
            return HAL_GetNumAccumulatorsFunc();
        }


        [NativeFunctionPointer("HAL_GetNumAddressableLEDs")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumAddressableLEDsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumAddressableLEDs()
        {
            return HAL_GetNumAddressableLEDsFunc();
        }


        [NativeFunctionPointer("HAL_GetNumAnalogInputs")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumAnalogInputsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumAnalogInputs()
        {
            return HAL_GetNumAnalogInputsFunc();
        }


        [NativeFunctionPointer("HAL_GetNumAnalogOutputs")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumAnalogOutputsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumAnalogOutputs()
        {
            return HAL_GetNumAnalogOutputsFunc();
        }


        [NativeFunctionPointer("HAL_GetNumAnalogTriggers")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumAnalogTriggersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumAnalogTriggers()
        {
            return HAL_GetNumAnalogTriggersFunc();
        }


        [NativeFunctionPointer("HAL_GetNumCounters")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumCountersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumCounters()
        {
            return HAL_GetNumCountersFunc();
        }


        [NativeFunctionPointer("HAL_GetNumDigitalChannels")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumDigitalChannelsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumDigitalChannels()
        {
            return HAL_GetNumDigitalChannelsFunc();
        }


        [NativeFunctionPointer("HAL_GetNumDigitalHeaders")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumDigitalHeadersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumDigitalHeaders()
        {
            return HAL_GetNumDigitalHeadersFunc();
        }


        [NativeFunctionPointer("HAL_GetNumDigitalPWMOutputs")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumDigitalPWMOutputsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumDigitalPWMOutputs()
        {
            return HAL_GetNumDigitalPWMOutputsFunc();
        }


        [NativeFunctionPointer("HAL_GetNumDutyCycles")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumDutyCyclesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumDutyCycles()
        {
            return HAL_GetNumDutyCyclesFunc();
        }


        [NativeFunctionPointer("HAL_GetNumEncoders")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumEncodersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumEncoders()
        {
            return HAL_GetNumEncodersFunc();
        }


        [NativeFunctionPointer("HAL_GetNumInterrupts")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumInterruptsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumInterrupts()
        {
            return HAL_GetNumInterruptsFunc();
        }


        [NativeFunctionPointer("HAL_GetNumPCMModules")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumPCMModulesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumPCMModules()
        {
            return HAL_GetNumPCMModulesFunc();
        }


        [NativeFunctionPointer("HAL_GetNumPDPChannels")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumPDPChannelsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumPDPChannels()
        {
            return HAL_GetNumPDPChannelsFunc();
        }


        [NativeFunctionPointer("HAL_GetNumPDPModules")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumPDPModulesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumPDPModules()
        {
            return HAL_GetNumPDPModulesFunc();
        }


        [NativeFunctionPointer("HAL_GetNumPWMChannels")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumPWMChannelsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumPWMChannels()
        {
            return HAL_GetNumPWMChannelsFunc();
        }


        [NativeFunctionPointer("HAL_GetNumPWMHeaders")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumPWMHeadersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumPWMHeaders()
        {
            return HAL_GetNumPWMHeadersFunc();
        }


        [NativeFunctionPointer("HAL_GetNumRelayChannels")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumRelayChannelsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumRelayChannels()
        {
            return HAL_GetNumRelayChannelsFunc();
        }


        [NativeFunctionPointer("HAL_GetNumRelayHeaders")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumRelayHeadersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumRelayHeaders()
        {
            return HAL_GetNumRelayHeadersFunc();
        }


        [NativeFunctionPointer("HAL_GetNumSolenoidChannels")]
        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumSolenoidChannelsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumSolenoidChannels()
        {
            return HAL_GetNumSolenoidChannelsFunc();
        }



    }
}
