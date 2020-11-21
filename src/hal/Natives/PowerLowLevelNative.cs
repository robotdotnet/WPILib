using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class PowerLowLevelNative
    {
        [NativeFunctionPointer("HAL_GetUserActive3V3")]
        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetUserActive3V3Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetUserActive3V3()
        {
            int status = 0;
            var retVal = HAL_GetUserActive3V3Func(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetUserActive5V")]
        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetUserActive5VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetUserActive5V()
        {
            int status = 0;
            var retVal = HAL_GetUserActive5VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetUserActive6V")]
        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetUserActive6VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetUserActive6V()
        {
            int status = 0;
            var retVal = HAL_GetUserActive6VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetUserCurrent3V3")]
        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetUserCurrent3V3Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetUserCurrent3V3()
        {
            int status = 0;
            var retVal = HAL_GetUserCurrent3V3Func(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetUserCurrent5V")]
        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetUserCurrent5VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetUserCurrent5V()
        {
            int status = 0;
            var retVal = HAL_GetUserCurrent5VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetUserCurrent6V")]
        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetUserCurrent6VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetUserCurrent6V()
        {
            int status = 0;
            var retVal = HAL_GetUserCurrent6VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetUserCurrentFaults3V3")]
        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetUserCurrentFaults3V3Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetUserCurrentFaults3V3()
        {
            int status = 0;
            var retVal = HAL_GetUserCurrentFaults3V3Func(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetUserCurrentFaults5V")]
        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetUserCurrentFaults5VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetUserCurrentFaults5V()
        {
            int status = 0;
            var retVal = HAL_GetUserCurrentFaults5VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetUserCurrentFaults6V")]
        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetUserCurrentFaults6VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetUserCurrentFaults6V()
        {
            int status = 0;
            var retVal = HAL_GetUserCurrentFaults6VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetUserVoltage3V3")]
        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetUserVoltage3V3Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetUserVoltage3V3()
        {
            int status = 0;
            var retVal = HAL_GetUserVoltage3V3Func(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetUserVoltage5V")]
        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetUserVoltage5VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetUserVoltage5V()
        {
            int status = 0;
            var retVal = HAL_GetUserVoltage5VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetUserVoltage6V")]
        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetUserVoltage6VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetUserVoltage6V()
        {
            int status = 0;
            var retVal = HAL_GetUserVoltage6VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetVinCurrent")]
        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetVinCurrentFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetVinCurrent()
        {
            int status = 0;
            var retVal = HAL_GetVinCurrentFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }


        [NativeFunctionPointer("HAL_GetVinVoltage")]
        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetVinVoltageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetVinVoltage()
        {
            int status = 0;
            var retVal = HAL_GetVinVoltageFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



    }
}
