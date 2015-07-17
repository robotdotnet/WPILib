using HAL_Simulator;
using HAL = HAL_Base.HAL;

namespace WPILib.Tests
{
    class TestBase
    {
        public static void StartCode()
        {
            RobotBase.InitializeHardwareConfiguration();
            HAL.Initialize();
            SimData.ResetHALData();
            Resource.RestartProgram();
        }
    }
}
