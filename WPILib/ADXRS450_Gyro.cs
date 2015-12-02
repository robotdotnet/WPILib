using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib.Interfaces;
using WPILib.LiveWindows;

namespace WPILib
{
    public class ADXRS450_Gyro : GyroBase, IGyro, IPIDSource, ILiveWindowSendable
    {
        private const double SamplePeriod = 0.001;
        private const double CalibrationSampleTime = 5.0;
        private const double DegreePerSecondPerLSB = 0.0125;
        private const int RateRegister = 0x00;
        private const int TemRegister = 0x02;
        private const int LoCSTRegister = 0x04;
        private const int HiCSTRegister = 0x06;
        private const int QuadRegister = 0x08;
        private const int FaultRegister = 0x0A;
        private const int PIDRegister = 0x0C;
        private const int SNHighRegister = 0x0E;
        private const int SNLowRegister = 0x10;

        private SPI m_spi;

        /// <summary>
        /// Constructor. Uses the onboard CS0
        /// </summary>
        public ADXRS450_Gyro() : this(SPI.Port.OnboardCS0)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">The <see cref="SPI.Port"/> that the gyro is connected to.</param>
        public ADXRS450_Gyro(SPI.Port port)
        {
            m_spi = new SPI(port);
            if (RobotBase.IsSimulation)
            {
                m_spi.InitAccumulator(SamplePeriod, 0x20000000, 4, 0x0c000000, 0x04000000,
                10, 16, true, true);
                m_spi.ResetAccumulator();
                return;
            }
            m_spi.SetClockRate(3000000);
            m_spi.SetMSBFirst();
            m_spi.SetSampleDataOnRising();
            m_spi.SetClockActiveHigh();
            m_spi.SetChipSelectActiveLow();

            // Validate the part ID 	
            if ((ReadRegister(PIDRegister) & 0xff00) != 0x5200)
            {

                m_spi.Dispose();
                m_spi = null;
                DriverStation.ReportError("could not find ADXRS450 gyro on SPI port " + port.ToString(), false);
                return;
            }


            m_spi.InitAccumulator(SamplePeriod, 0x20000000, 4, 0x0c000000, 0x04000000,
                10, 16, true, true);

            Calibrate();

            //UsageReporting.report(tResourceType.kResourceType_ADXRS450, port.getValue()); 	
            LiveWindow.LiveWindow.AddSensor("ADXRS450_Gyro", port.ToString(), this);
        }

        /// <inheritdoc/>
        public override void Calibrate()
        {
            if (m_spi == null) return;

            Timer.Delay(1.0);

            m_spi.SetAccumulatorCenter(0);
            m_spi.ResetAccumulator();

            Timer.Delay(CalibrationSampleTime);

            m_spi.SetAccumulatorCenter((int)m_spi.GetAccumulatorAverage());
            m_spi.ResetAccumulator();
        }

        public override void Dispose()
        {
            m_spi.FreeAccumulator();
            m_spi?.Dispose();
            base.Dispose();
        }

        private bool CalcParity(uint v)
        {
            bool parity = false;
            while (v != 0)
            {
                parity = !parity;
                v = v & (v - 1);
            }
            return parity;
        }

        private static uint BytesToIntBE(byte[] buf)
        {
            uint result = ((uint)buf[0]) << 24;
            result |= ((uint)buf[1]) << 16;
            result |= ((uint)buf[2]) << 8;
            result |= (uint)buf[3];
            return result;
        }

        private ushort ReadRegister(byte reg)
        {
            uint cmd = 0x80000000 | (((uint)reg) << 17);
            if (!CalcParity(cmd)) cmd |= 1u;

            // big endian 	102
            byte[] buf = new byte[]
            {
                (byte)((cmd >> 24) & 0xff),
                (byte)((cmd >> 16) & 0xff),
                (byte)((cmd >> 8) & 0xff),
                (byte)(cmd & 0xff)
            };

            m_spi.Write(buf, 4);
            m_spi.Read(false, buf, 4);

            if ((buf[0] & 0xe0) == 0) return 0;  // error, return 0 	
            return (ushort)((BytesToIntBE(buf) >> 5) & 0xffff);
        }

        /// <inheritdoc/>
        public override void Reset() => m_spi.ResetAccumulator();

        /// <inheritdoc/>
        public override double GetAngle()
        {
            if (RobotBase.IsSimulation)
            {
                //Use our simulator hack.
                return BitConverter.Int64BitsToDouble(m_spi.GetAccumulatorValue());
            }
            return (m_spi.GetAccumulatorValue() * DegreePerSecondPerLSB * SamplePeriod);
        }

        /// <inheritdoc/>
        public override double GetRate()
        {
            if (RobotBase.IsSimulation)
            {
                //Use our simulator hack
                return BitConverter.ToSingle(BitConverter.GetBytes(m_spi.GetAccumulatorCount()), 0);
            }
            return m_spi.GetAccumulatorLastValue() * DegreePerSecondPerLSB;
        }

        public override string SmartDashboardType => nameof(ADXRS450_Gyro);
    }
}
