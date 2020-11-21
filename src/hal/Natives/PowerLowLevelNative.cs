using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class PowerLowLevelNative
    {
        public PowerLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_GetUserActive3V3Func = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_GetUserActive3V3");
            HAL_GetUserActive5VFunc = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_GetUserActive5V");
            HAL_GetUserActive6VFunc = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_GetUserActive6V");
            HAL_GetUserCurrent3V3Func = (delegate* unmanaged[Cdecl] < int *, System.Double >)loader.GetProcAddress("HAL_GetUserCurrent3V3");
            HAL_GetUserCurrent5VFunc = (delegate* unmanaged[Cdecl] < int *, System.Double >)loader.GetProcAddress("HAL_GetUserCurrent5V");
            HAL_GetUserCurrent6VFunc = (delegate* unmanaged[Cdecl] < int *, System.Double >)loader.GetProcAddress("HAL_GetUserCurrent6V");
            HAL_GetUserCurrentFaults3V3Func = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_GetUserCurrentFaults3V3");
            HAL_GetUserCurrentFaults5VFunc = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_GetUserCurrentFaults5V");
            HAL_GetUserCurrentFaults6VFunc = (delegate* unmanaged[Cdecl] < int *, System.Int32 >)loader.GetProcAddress("HAL_GetUserCurrentFaults6V");
            HAL_GetUserVoltage3V3Func = (delegate* unmanaged[Cdecl] < int *, System.Double >)loader.GetProcAddress("HAL_GetUserVoltage3V3");
            HAL_GetUserVoltage5VFunc = (delegate* unmanaged[Cdecl] < int *, System.Double >)loader.GetProcAddress("HAL_GetUserVoltage5V");
            HAL_GetUserVoltage6VFunc = (delegate* unmanaged[Cdecl] < int *, System.Double >)loader.GetProcAddress("HAL_GetUserVoltage6V");
            HAL_GetVinCurrentFunc = (delegate* unmanaged[Cdecl] < int *, System.Double >)loader.GetProcAddress("HAL_GetVinCurrent");
            HAL_GetVinVoltageFunc = (delegate* unmanaged[Cdecl] < int *, System.Double >)loader.GetProcAddress("HAL_GetVinVoltage");
        }

        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetUserActive3V3Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetUserActive3V3()
        {
            int status = 0;
            var retVal = HAL_GetUserActive3V3Func(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetUserActive5VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetUserActive5V()
        {
            int status = 0;
            var retVal = HAL_GetUserActive5VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetUserActive6VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetUserActive6V()
        {
            int status = 0;
            var retVal = HAL_GetUserActive6VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetUserCurrent3V3Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetUserCurrent3V3()
        {
            int status = 0;
            var retVal = HAL_GetUserCurrent3V3Func(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetUserCurrent5VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetUserCurrent5V()
        {
            int status = 0;
            var retVal = HAL_GetUserCurrent5VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetUserCurrent6VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetUserCurrent6V()
        {
            int status = 0;
            var retVal = HAL_GetUserCurrent6VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetUserCurrentFaults3V3Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetUserCurrentFaults3V3()
        {
            int status = 0;
            var retVal = HAL_GetUserCurrentFaults3V3Func(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetUserCurrentFaults5VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetUserCurrentFaults5V()
        {
            int status = 0;
            var retVal = HAL_GetUserCurrentFaults5VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, int> HAL_GetUserCurrentFaults6VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetUserCurrentFaults6V()
        {
            int status = 0;
            var retVal = HAL_GetUserCurrentFaults6VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetUserVoltage3V3Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetUserVoltage3V3()
        {
            int status = 0;
            var retVal = HAL_GetUserVoltage3V3Func(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetUserVoltage5VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetUserVoltage5V()
        {
            int status = 0;
            var retVal = HAL_GetUserVoltage5VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetUserVoltage6VFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetUserVoltage6V()
        {
            int status = 0;
            var retVal = HAL_GetUserVoltage6VFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetVinCurrentFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetVinCurrent()
        {
            int status = 0;
            var retVal = HAL_GetVinCurrentFunc(&status);
            Hal.StatusHandling.StatusCheck(status);
            return retVal;
        }



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
