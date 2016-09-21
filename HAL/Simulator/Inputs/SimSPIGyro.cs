using System;
using HAL.Simulator.Data;

namespace HAL.Simulator.Inputs
{
    /*
    /// <summary>
    /// The SPI ports available to the simulator
    /// </summary>
    public enum SPIPorts : byte
    {
        ///The Onboard CS0 Port
        OnboardCS0 = 0,
        ///The Onboard CS1 Port
        OnboardCS1 = 1,
        ///The Onboard CS2 Port
        OnboardCS2 = 2,
        ///The Onboard CS3 Port
        OnboardCS3 = 3,
        ///The MXP SPI Port
        MXP = 4
    }

    public class SimSPIGyro : IServoFeedback
    {
        private readonly SPIAccumulatorData data;

        public SimSPIGyro(SPIPorts port)
        {
            data = SimData.SPIAccumulator[(int)port];
        }

        public void SetPosition(double value)
        {
            data.AccumulatorValue = BitConverter.DoubleToInt64Bits(value);
        }

        public void SetRate(double rate)
        {
            data.AccumulatorCount = BitConverter.ToUInt32(BitConverter.GetBytes((float)rate), 0);
        }
    }
    */
}
