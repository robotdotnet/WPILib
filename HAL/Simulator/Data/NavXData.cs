using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL.Simulator.Data
{
    public class NavXData : NotifyDataBase
    {
        private double m_gyroRatePitch;
        private double m_gyroRateRoll;
        private double m_gyroRateYaw;

        private double m_gyroAnglePitch;
        private double m_gyroAngleRoll;
        private double m_gyroAngleYaw;

        private double m_accelX;
        private double m_accelY;
        private double m_accelZ;

        public override void ResetData()
        {
            base.ResetData();

            m_gyroRatePitch = 0;
            m_gyroRateRoll = 0;
            m_gyroRateYaw = 0;

            m_gyroAnglePitch = 0;
            m_gyroAngleRoll = 0;
            m_gyroAngleYaw = 0;

            m_accelX = 0;
            m_accelY = 0;
            m_accelZ = 0;
        }

        public double GyroRatePitch
        {
            get { return m_gyroRatePitch; }
            set
            {
                if (value == m_gyroRatePitch) return;
                m_gyroRatePitch = value;
                OnPropertyChanged(value);
            }
        }

        public double GyroRateRoll
        {
            get { return m_gyroRateRoll; }
            set
            {
                if (value == m_gyroRateRoll) return;
                m_gyroRateRoll = value;
                OnPropertyChanged(value);
            }
        }

        public double GyroRateYaw
        {
            get { return m_gyroRateYaw; }
            set
            {
                if (value == m_gyroRateYaw) return;
                m_gyroRateYaw = value;
                OnPropertyChanged(value);
            }
        }

        public double GyroAnglePitch
        {
            get { return m_gyroAnglePitch; }
            set
            {
                if (value == m_gyroAnglePitch) return;
                m_gyroAnglePitch = value;
                OnPropertyChanged(value);
            }
        }

        public double GyroAngleRoll
        {
            get { return m_gyroAngleRoll; }
            set
            {
                if (value == m_gyroAngleRoll) return;
                m_gyroAngleRoll = value;
                OnPropertyChanged(value);
            }
        }

        public double GyroAngleYaw
        {
            get { return m_gyroAngleYaw; }
            set
            {
                if (value == m_gyroAngleYaw) return;
                m_gyroAngleYaw = value;
                OnPropertyChanged(value);
            }
        }

        public double AccelX
        {
            get { return m_accelX; }
            set
            {
                if (value == m_accelX) return;
                m_accelX = value;
                OnPropertyChanged(value);
            }
        }

        public double AccelY
        {
            get { return m_accelY; }
            set
            {
                if (value == m_accelY) return;
                m_accelY = value;
                OnPropertyChanged(value);
            }
        }

        public double AccelZ
        {
            get { return m_accelZ; }
            set
            {
                if (value == m_accelZ) return;
                m_accelZ = value;
                OnPropertyChanged(value);
            }
        }
    }
}
