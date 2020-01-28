using System;
using System.Collections.Generic;
using System.Text;

namespace Hal.Natives
{
    public unsafe interface IUsageReporting
    {
        int HAL_Report(int resource, int instanceNumber, int context, byte* feature);
    }
}
