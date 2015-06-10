using NetworkTablesDotNet.Tables;
using WPILib.Interfaces;
using WPILib.LiveWindows;

namespace WPILib
{
    public abstract class ADXL345 : SensorBase, IAccelerometer, ILiveWindowSendable
    {
        protected const byte PowerCtlRegister = 0x2D;
        protected const byte DataFormatRegister = 0x31;
        protected const byte DataRegister = 0x32;
        protected const double GsPerLSB = 0.00390625;
        protected enum PowerCtl : byte
        {
            Link = 0x20,
            AutoSleep = 0x10,
            Measure = 0x08,
            Sleep = 0x04
        }
        protected enum DataFormat : byte
        {
            SelfTest = 0x80,
            SPI = 0x40,
            IntInvert = 0x20,
            FullRes = 0x08,
            Justify = 0x04
        }

        public enum Axes
        {
            X = 1,
            Y = 1 << 1,
            Z = 1 << 2
        }

        public struct AllAxes
        {
            public double XAxis;
            public double YAxis;
            public double ZAxis;
        }

        protected abstract void WriteRange(byte value);

        public AccelerometerRange AccelerometerRange
        {
            set
            {
                byte retVal = 0;
                switch (value)
                {
                    case AccelerometerRange.k2G:
                        retVal = 0;
                        break;
                    case AccelerometerRange.k4G:
                        retVal = 1;
                        break;
                    case AccelerometerRange.k8G:
                        retVal = 2;
                        break;
                    case AccelerometerRange.k16G:
                        retVal = 3;
                        break;
                }
                WriteRange(retVal);
            }
        }

        public double GetX() => GetAcceleration(Axes.X);

        public double GetY() => GetAcceleration(Axes.Y);

        public double GetZ() => GetAcceleration(Axes.Z);

        public abstract double GetAcceleration(Axes axis);

        public abstract AllAxes GetAccelerations();


        public string SmartDashboardType => "3AxisAccelerometer";

        private ITable m_table;

        public void InitTable(ITable subtable)
        {
            m_table = subtable;
            UpdateTable();
        }

        public void UpdateTable()
        {
            if (m_table != null)
            {
                m_table.PutNumber("X", GetX());
                m_table.PutNumber("Y", GetY());
                m_table.PutNumber("Z", GetZ());
            }
        }

        public ITable Table => m_table;

        public void StartLiveWindowMode()
        {
        }

        public void StopLiveWindowMode()
        {
        }
    }
}
