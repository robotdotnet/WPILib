using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
