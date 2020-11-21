using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class PortsLowLevelNative
    {
        public PortsLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_GetNumAccumulatorsFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumAccumulators");
            HAL_GetNumAddressableLEDsFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumAddressableLEDs");
            HAL_GetNumAnalogInputsFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumAnalogInputs");
            HAL_GetNumAnalogOutputsFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumAnalogOutputs");
            HAL_GetNumAnalogTriggersFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumAnalogTriggers");
            HAL_GetNumCountersFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumCounters");
            HAL_GetNumDigitalChannelsFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumDigitalChannels");
            HAL_GetNumDigitalHeadersFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumDigitalHeaders");
            HAL_GetNumDigitalPWMOutputsFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumDigitalPWMOutputs");
            HAL_GetNumDutyCyclesFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumDutyCycles");
            HAL_GetNumEncodersFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumEncoders");
            HAL_GetNumInterruptsFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumInterrupts");
            HAL_GetNumPCMModulesFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumPCMModules");
            HAL_GetNumPDPChannelsFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumPDPChannels");
            HAL_GetNumPDPModulesFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumPDPModules");
            HAL_GetNumPWMChannelsFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumPWMChannels");
            HAL_GetNumPWMHeadersFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumPWMHeaders");
            HAL_GetNumRelayChannelsFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumRelayChannels");
            HAL_GetNumRelayHeadersFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumRelayHeaders");
            HAL_GetNumSolenoidChannelsFunc = (delegate* unmanaged[Cdecl] < System.Int32 >)loader.GetProcAddress("HAL_GetNumSolenoidChannels");
        }

        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumAccumulatorsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumAccumulators()
        {
            return HAL_GetNumAccumulatorsFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumAddressableLEDsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumAddressableLEDs()
        {
            return HAL_GetNumAddressableLEDsFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumAnalogInputsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumAnalogInputs()
        {
            return HAL_GetNumAnalogInputsFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumAnalogOutputsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumAnalogOutputs()
        {
            return HAL_GetNumAnalogOutputsFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumAnalogTriggersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumAnalogTriggers()
        {
            return HAL_GetNumAnalogTriggersFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumCountersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumCounters()
        {
            return HAL_GetNumCountersFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumDigitalChannelsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumDigitalChannels()
        {
            return HAL_GetNumDigitalChannelsFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumDigitalHeadersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumDigitalHeaders()
        {
            return HAL_GetNumDigitalHeadersFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumDigitalPWMOutputsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumDigitalPWMOutputs()
        {
            return HAL_GetNumDigitalPWMOutputsFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumDutyCyclesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumDutyCycles()
        {
            return HAL_GetNumDutyCyclesFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumEncodersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumEncoders()
        {
            return HAL_GetNumEncodersFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumInterruptsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumInterrupts()
        {
            return HAL_GetNumInterruptsFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumPCMModulesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumPCMModules()
        {
            return HAL_GetNumPCMModulesFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumPDPChannelsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumPDPChannels()
        {
            return HAL_GetNumPDPChannelsFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumPDPModulesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumPDPModules()
        {
            return HAL_GetNumPDPModulesFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumPWMChannelsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumPWMChannels()
        {
            return HAL_GetNumPWMChannelsFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumPWMHeadersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumPWMHeaders()
        {
            return HAL_GetNumPWMHeadersFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumRelayChannelsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumRelayChannels()
        {
            return HAL_GetNumRelayChannelsFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumRelayHeadersFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumRelayHeaders()
        {
            return HAL_GetNumRelayHeadersFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<int> HAL_GetNumSolenoidChannelsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetNumSolenoidChannels()
        {
            return HAL_GetNumSolenoidChannelsFunc();
        }



    }
}
