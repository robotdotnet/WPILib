using REV.SparkMax.Natives;
using System;
using System.Collections.Generic;
using System.Text;
using WPILib;

namespace REV.SparkMax
{
    public unsafe class CANSparkMaxLowLevel : /*SpeedController,*/ IDisposable
    {
        public const byte kAPIMajorVersion = 0;
        public const byte kAPIMinorVersion = 0;
        public const byte kAPIBuildVersion = 0;
        public const uint kAPIVersion = 0;

        public CANSparkMaxLowLevel(int deviceId, MotorType type)
        {
            m_deviceId = deviceId;
            m_sparkMax = CANSparkMaxDriver.Create(deviceId, type);
            m_motorType = type;
        }

        public void Dispose()
        {
            CANSparkMaxDriver.Destroy(m_sparkMax);
            m_sparkMax = null;
        }

        public uint FirmwareVersion
        {
            get
            {
                FirmwareVersion version;
                CANSparkMaxDriver.GetFirmwareVersion(m_sparkMax, &version);
                return version.VersionRaw;
            }
        }

        public (uint version, bool isDebug) FirmwareVersionDebug
        {
            get
            {
                FirmwareVersion version;
                CANSparkMaxDriver.GetFirmwareVersion(m_sparkMax, &version);
                return (version.VersionRaw, version.IsDebug);
            }
        }

        public string FirmwareString
        {
            get
            {
                FirmwareVersion version;
                CANSparkMaxDriver.GetFirmwareVersion(m_sparkMax, &version);
                uint firmwareBuild = version.Build;

                bool isDebugBuild = version.IsDebug;

                StringBuilder builder = new StringBuilder();
                builder.Append('v');
                builder.Append((int)version.Major);
                builder.Append('.');
                builder.Append((int)version.Minor);
                builder.Append('.');
                builder.Append(firmwareBuild);
                if (isDebugBuild)
                {
                    builder.Append(" Debug Build");
                }
                return builder.ToString();
            }
        }

        public byte[] SerialNumber => Array.Empty<byte>();

        public int DeviceId => m_deviceId;

        public MotorType InitialMotorType => m_motorType;

        public MotorType MotorType
        {
            get
            {
                MotorType type;
                CANSparkMaxDriver.GetMotorType(m_sparkMax, &type);
                return type;
            }
            set
            {
                CANSparkMaxDriver.SetMotorType(m_sparkMax, value);
            }
        }

        public ErrorCode SetPeriodicFramePeriod(PeriodicFrame frame, TimeSpan period)
        {
            return CANSparkMaxDriver.SetPeriodicFramePeriod(m_sparkMax, frame, (int)period.TotalMilliseconds);
        }

        public void SetControlFramePeriod(TimeSpan period)
        {
            CANSparkMaxDriver.SetControlFramePeriod(m_sparkMax, (int)period.TotalMilliseconds);
        }

        public ErrorCode RestoreFactoryDefaults(bool persist = false)
        {
            return CANSparkMaxDriver.RestoreFactoryDefaults(m_sparkMax, persist ? (byte)1 : (byte)0);
        }

        //public void EnableExternalUSBControl(bool enable)
        //{
        //    CANSparkMaxDriver.EnableExternalControl(enable);
        //}

        //public void SetEnable(bool enable)
        //{

        //}

        protected ErrorCode SetEncPosition(double value)
        {
            return CANSparkMaxDriver.SetEncoderPosition(m_sparkMax, (float)value);
        }

        protected ErrorCode SetIAccum(double value)
        {
            return CANSparkMaxDriver.SetIAccum(m_sparkMax, (float)value);
        }

        protected struct FollowConfig
        {
            public uint LeaderArbId;
            public uint ConfigRaw;
        }

        protected unsafe void* m_sparkMax;

        protected PeriodicStatus0 PeriodicStatus0
        {
            get
            {
                PeriodicStatus0LowLevel cStatus0 = default;
                //CANSparkMaxDriver.GetPeriodicStatus0(m_sparkMax, &cStatus0);

                return new PeriodicStatus0(cStatus0);
            }
        }

        protected PeriodicStatus1 PeriodicStatus1
        {
            get
            {
                return default;
            }
        }

        protected PeriodicStatus2 PeriodicStatus2
        {
            get
            {
                return default;
            }
        }

        protected ErrorCode SetFollow(FollowConfig config)
        {
            return 0;
        }

        protected ErrorCode FollowerInvert(bool invert)
        {
            return 0;
        }

        protected ErrorCode SetpointCommand(double value, ControlType ctrl = ControlType.kDutyCycle, int pidSlot = 0, double arbFeedforward = 0, int arbFFUnits = 0)
        {
            return 0;
        }

        protected float GetSafeFloat(float f)
        {
            return f;
        }

        protected MotorType m_motorType;

        private readonly int m_deviceId;


    }
}
