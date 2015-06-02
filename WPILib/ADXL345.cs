using NetworkTablesDotNet.Tables;
using WPILib.Interfaces;
using WPILib.livewindow;

namespace WPILib
{
    public abstract class ADXL345 : SensorBase, Accelerometer, LiveWindowSendable
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

        public void SetRange(Interfaces.Range range)
        {
            byte value = 0;
            switch (range)
            {
                case WPILib.Interfaces.Range.k2G:
                    value = 0;
                    break;
                case WPILib.Interfaces.Range.k4G:
                    value = 1;
                    break;
                case WPILib.Interfaces.Range.k8G:
                    value = 2;
                    break;
                case WPILib.Interfaces.Range.k16G:
                    value = 3;
                    break;
            }
            WriteRange(value);
        }

        public double GetX()
        {
            return GetAcceleration(Axes.X);
        }

        public double GetY()
        {
            return GetAcceleration(Axes.Y);
        }

        public double GetZ()
        {
            return GetAcceleration(Axes.Z);
        }

        public abstract double GetAcceleration(Axes axis);

        public abstract AllAxes GetAccelerations();


        public string GetSmartDashboardType()
        {
            return "3AxisAccelerometer";
        }

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

        public ITable GetTable()
        {
            return m_table;
        }

        public void StartLiveWindowMode()
        {
        }

        public void StopLiveWindowMode()
        {
        }
    }
}
