using System;
using System.Collections.Generic;
using System.Text;
using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IUsageReporting
    {
        int HAL_Report(int resource, int instanceNumber, int context, byte* feature);
    }
}
