using HAL;

namespace WPILib.Internal
{
    /// <summary>
    /// The RoboRIO Usage Reporting implementation
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class HardwareHLUsageReporting : HLUsageReporting.Interface
    {
        /// <summary>
        /// Report Scheduler
        /// </summary>
        public void ReportScheduler()
        {
            HAL.HAL.Report(ResourceType.kResourceType_Command, Instances.kCommand_Scheduler);
        }
        /// <summary>
        /// Report PID Controller
        /// </summary>
        /// <param name="num">The PID Controller Index</param>
        public void ReportPIDController(int num)
        {
            HAL.HAL.Report(ResourceType.kResourceType_PIDController, (byte)num);
        }
        /// <summary>
        /// Report Smart Dashboard.
        /// </summary>
        public void ReportSmartDashboard()
        {
            HAL.HAL.Report(ResourceType.kResourceType_SmartDashboard, (byte)0);
        }
    }
}
