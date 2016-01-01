using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Base;
using NetworkTables.Tables;
using WPILib.Interfaces;
using WPILib.LiveWindow;
using static HAL.Base.HAL;

namespace WPILib
{
    /// <summary>
    /// ADXL362 SPI Accelerometer
    /// </summary>
    /// <remarks>
    /// This class allows access to an Analog Devices ADXL362 3-axis accelerometer.
    /// </remarks>
    public class ADXL362 : SensorBase, IAccelerometer, ILiveWindowSendable
    {
        private const byte RegWrite = 0x0A;
        private const byte RegRead = 0x0B;

        private const byte PartIdRegister = 0x02;
        private const byte DataRegister = 0x0E;
        private const byte FilterCtlRegister = 0x2C;
        private const byte PowerCtlRegister = 0x2D;

        private const byte FilterCtl_Range2G = 0x00;
        private const byte FilterCtl_Range4G = 0x40;
        private const byte FilterCtl_Range8G = (byte)0x80;
        private const byte FilterCtl_ODR_100Hz = 0x03;

        private const byte PowerCtl_UltraLowNoise = 0x20;
        private const byte PowerCtl_AutoSleep = 0x04;
        private const byte PowerCtl_Measure = 0x02;

        /// <summary>
        /// The Axes for ADXL362 Gyros
        /// </summary>
        public enum Axes
        {
            /// <summary>
            /// The X Axis
            /// </summary>
            X = 0x00,
            /// <summary>
            /// The Y Axis
            /// </summary>
            Y = 0x02,
            /// <summary>
            /// The Z Axis
            /// </summary>
            Z = 0x04
        }

        private readonly SPI m_spi;
        private double m_gsPerLSB = 0.001;

        /// <summary>
        /// Creates a new <see cref="ADXL362"/> with the specified <see cref="SPI.Port">Port</see> 
        /// and <see cref="AccelerometerRange">Range</see>.
        /// </summary>
        /// <param name="port">The SPI Port the accelerometer is connected to.</param>
        /// <param name="range">The range that the accelerometer will measure.</param>
        public ADXL362(SPI.Port port, AccelerometerRange range)
        {
            m_spi = new SPI(port);
            m_spi.SetClockRate(3000000);
            m_spi.SetMSBFirst();
            m_spi.SetSampleDataOnRising();
            m_spi.SetClockActiveHigh();
            m_spi.SetChipSelectActiveLow();

            byte[] commands = new byte[]
            {
                RegRead,
                PartIdRegister,
                0,
            };

            m_spi.Transaction(commands, commands, 3);

            if (commands[2] != 0xF2)
            {
                DriverStation.ReportError("Could not find ADXL362", false);
                m_gsPerLSB = 0.0;
                return;
            }

            AccelerometerRange = range;

            commands[0] = RegWrite;
            commands[1] = PowerCtlRegister;
            commands[2] = PowerCtlRegister | PowerCtl_UltraLowNoise;
            m_spi.Write(commands, 3);

            Report(ResourceType.kResourceType_ADXL362, (byte)port);
            LiveWindow.LiveWindow.AddSensor("ADXL362", port.ToString(), this);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            m_spi.Dispose();
            base.Dispose();
        }

        private AccelerometerRange m_range;

        /// <summary>
        /// Common interface for setting the measuring range of an accelerometer
        /// </summary>
        /// <value>The maximum acceleration, positive or negative, that the 
        /// accelerometer will measure. Not all accelerometers support all ranges
        /// </value>
        public AccelerometerRange AccelerometerRange
        {
            get { return m_range; }
            set
            {
                if (m_gsPerLSB == 0.0) return;
                byte[] commands = new byte[3];
                m_range = value;
                switch (value)
                {
                    case AccelerometerRange.k2G:
                        m_gsPerLSB = 0.001;
                        break;
                    case AccelerometerRange.k4G:
                        m_gsPerLSB = 0.002;
                        break;
                    case AccelerometerRange.k8G:
                    case AccelerometerRange.k16G: //16G not supported, treat as 8G
                        m_gsPerLSB = 0.004;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }

                commands[0] = RegWrite;
                commands[1] = FilterCtlRegister;
                commands[2] = (byte)(FilterCtl_ODR_100Hz | (((int)value & 0x03) << 6));

                m_spi.Write(commands, 3);
            }
        }

        /// <inheritdoc/>
        public virtual double GetX() => GetAcceleration(Axes.X);

        /// <inheritdoc/>
        public virtual double GetY() => GetAcceleration(Axes.Y);

        /// <inheritdoc/>
        public virtual double GetZ() => GetAcceleration(Axes.Z);

        /// <inheritdoc/>
        public virtual AllAxes GetAllAxes() => GetAccelerations();

        /// <summary>
        /// Get the acceleration of one axis in Gs.
        /// </summary>
        /// <param name="axis">The axis to read from</param>
        /// <returns>Acceleration of the ADXL362 in Gs.</returns>
        public double GetAcceleration(Axes axis)
        {
            if (m_gsPerLSB == 0.0) return 0.0;

            byte[] buffer = new byte[4];
            byte[] command = new byte[] {0, 0, 0, 0};
            command[0] = RegRead;
            command[1] = (byte)(DataRegister + (byte) axis);
            m_spi.Transaction(command, buffer, 4);

            short rawAccel = BitConverter.ToInt16(buffer, 2);
            return rawAccel * m_gsPerLSB;
        }

        /// <summary>
        /// Get the acceleration of all axes in Gs.
        /// </summary>
        /// <returns>A structure containing all axes measured in Gs.</returns>
        public AllAxes GetAccelerations()
        {
            if (m_gsPerLSB == 0.0)
            {
                return new AllAxes(0,0,0);
            }

            byte[] dataBuffer = new byte[8];

            int[] rawData = new int[3];

            dataBuffer[0] = RegRead;
            dataBuffer[1] = DataRegister;
            m_spi.Transaction(dataBuffer, dataBuffer, 8);

            for (int i = 0; i < 3; i++)
            {
                short rawAccel = BitConverter.ToInt16(dataBuffer, i * 2 +2);
                rawData[i] = rawAccel;
            }

            return new AllAxes(rawData[0] * m_gsPerLSB, rawData[1] * m_gsPerLSB, rawData[2] * m_gsPerLSB);
        }

        /// <inheritdoc/>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        /// <inheritdoc/>
        public ITable Table { get; private set; }

        /// <inheritdoc/>
        public string SmartDashboardType => "3AxisAccelerometer";

        /// <inheritdoc/>
        public void UpdateTable()
        {
            if (Table != null)
            {
                AllAxes axes = GetAccelerations();
                Table.PutNumber("X", axes.XAxis);
                Table.PutNumber("Y", axes.YAxis);
                Table.PutNumber("Z", axes.ZAxis);
            }
        }

        /// <inheritdoc/>
        public void StartLiveWindowMode()
        {
        }

        /// <inheritdoc/>
        public void StopLiveWindowMode()
        {
        }
    }
}
