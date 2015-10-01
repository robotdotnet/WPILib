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

            VInVoltage = 0.0f;
            VInCurrent = 0.0f;
            UserVoltage6V = 0.0f;
            UserCurrent6V = 0.0f;
            UserActive6V = false;
            UserVoltage5V = 0.0f;
            UserCurrent5V = 0.0f;
            UserActive5V = false;
            UserVoltage3V3 = 0.0f;
            UserCurrent3V3 = 0.0f;
            UserActive3V3 = false;
            UserFaults6V = 0;
            UserFaults5V = 0;
            UserFaults3V3 = 0;
        }

        public bool FPGAButton { get; set; } = false;

        public bool HasSource { get; set; } = false;

        public float VInVoltage { get; set; } = 0.0f;
        public float VInCurrent { get; set; } = 0.0f;
        public float UserVoltage6V { get; set; } = 0.0f;
        public float UserCurrent6V { get; set; } = 0.0f;
        public bool UserActive6V { get; set; } = false;
        public float UserVoltage5V { get; set; } = 0.0f;
        public float UserCurrent5V { get; set; } = 0.0f;
        public bool UserActive5V { get; set; } = false;
        public float UserVoltage3V3 { get; set; } = 0.0f;
        public float UserCurrent3V3 { get; set; } = 0.0f;
        public bool UserActive3V3 { get; set; } = false;
        public int UserFaults6V { get; set; } = 0;
        public int UserFaults5V { get; set; } = 0;
        public int UserFaults3V3 { get; set; } = 0;

    }
}
