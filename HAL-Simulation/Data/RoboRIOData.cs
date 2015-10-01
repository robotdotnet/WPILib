using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator.Data
{
    public class RoboRioData : DataBase
    {
        public override void ResetData()
        {
            FPGAButton = false;

            HasSource = false;

            VInVoltage = 0.0;
            VInCurrent = 0.0;
            UserVoltage6V = 0.0;
            UserCurrent6V = 0.0;
            UserActive6V = false;
            UserVoltage5V = 0.0;
            UserCurrent5V = 0.0;
            UserActive5V = false;
            UserVoltage3V3 = 0.0;
            UserCurrent3V3 = 0.0;
            UserActive3V3 = false;
            UserFaults6V = 0;
            UserFaults5V = 0;
            UserFaults3V3 = 0;
        }

        public bool FPGAButton { get; set; } = false;

        public bool HasSource { get; set; } = false;

        public double VInVoltage { get; set; } = 0.0;
        public double VInCurrent { get; set; } = 0.0;
        public double UserVoltage6V { get; set; } = 0.0;
        public double UserCurrent6V { get; set; } = 0.0;
        public bool UserActive6V { get; set; } = false;
        public double UserVoltage5V { get; set; } = 0.0;
        public double UserCurrent5V { get; set; } = 0.0;
        public bool UserActive5V { get; set; } = false;
        public double UserVoltage3V3 { get; set; } = 0.0;
        public double UserCurrent3V3 { get; set; } = 0.0;
        public bool UserActive3V3 { get; set; } = false;
        public int UserFaults6V { get; set; } = 0;
        public int UserFaults5V { get; set; } = 0;
        public int UserFaults3V3 { get; set; } = 0;

    }
}
