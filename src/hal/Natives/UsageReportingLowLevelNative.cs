using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class UsageReportingLowLevelNative
    {
        private readonly delegate* unmanaged[Cdecl]<int, int, int, byte*, int> HAL_ReportFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_Report(int resource, int instanceNumber, int context, byte* feature)
        {
            return HAL_ReportFunc(resource, instanceNumber, context, feature);
        }

        public UsageReportingLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_ReportFunc = (delegate* unmanaged[Cdecl]<int, int, int, byte*, int>)loader.GetProcAddress("HAL_Report");
        }
    }
}
