using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL_Base;

namespace WPILib.Internal
{
    public class HardwareHLUsageReporting : HLUsageReporting.Interface
    {

        public void ReportScheduler()
        {
            HAL.Report(ResourceType.kResourceType_Command, Instances.kCommand_Scheduler);
        }

        public void ReportPIDController(int num)
        {
            HAL.Report(ResourceType.kResourceType_PIDController, (byte)num);
        }

        public void ReportSmartDashboard()
        {
            HAL.Report(ResourceType.kResourceType_SmartDashboard, (byte)0);
        }
    }
}
