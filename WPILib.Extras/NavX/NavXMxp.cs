using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    public struct NavXBoardInfo
    {
        public byte WhoAmI { get; private set; }
        public byte BoardRevision { get; private set; }
        public byte FirmwareMajorVersion { get; private set; }
        public byte FirmwareMinorVersion { get; private set; }

        public bool BoardYawAxisUp { get; private set; }
        public byte BoardYawAxis { get; private set; }

        public bool Omnimount { get; private set; }
        public bool YawReset { get; private set; }
        public bool VelAndDisp { get; private set; }

        internal NavXBoardInfo(byte whoami, byte rev, byte major, byte minor,
            bool yawup, byte yawAxis, bool omni, bool yreset, bool veldisp)
        {
            WhoAmI = whoami;
            BoardRevision = rev;
            FirmwareMajorVersion = major;
            FirmwareMinorVersion = minor;
            BoardYawAxisUp = yawup;
            BoardYawAxis = yawAxis;
            Omnimount = omni;
            YawReset = yreset;
            VelAndDisp = veldisp;
        }
    }

    public struct NavXStatus
    {
        public byte UpdateRateHz { get; private set; }
        public byte AccelerometerFullScaleRange { get; private set; }
        public ushort GyroFullScaleRange { get; private set; }

        public OpStatus OperationStatus { get; private set; }
        public CalStatus CalibrationStatus { get; private set; }

        public byte SelftestStatus { get; private set; }

        public bool Moving { get; private set; }
        public bool YawStable { get; private set; }
        public bool MagDisturbance { get; private set; }
        public bool AltitudeValid { get; private set; }
        public bool SealevelPressureSet { get; private set; }
        public bool FusedHeadingValid { get; private set; }

        internal NavXStatus(byte upRate, byte acc, ushort gyro, OpStatus op, CalStatus cal,
            byte self, bool moving, bool yaw, bool mag, bool alt, bool sea, bool fused)
        {
            UpdateRateHz = upRate;
            AccelerometerFullScaleRange = acc;
            GyroFullScaleRange = gyro;
            OperationStatus = op;
            CalibrationStatus = cal;
            SelftestStatus = self;
            Moving = moving;
            YawStable = yaw;
            MagDisturbance = mag;
            AltitudeValid = alt;
            SealevelPressureSet = sea;
            FusedHeadingValid = fused;
        }
    }

    public class NavXMxp : IDisposable
    {
        IIo m_io;

        private bool Initialize()
        {
            Thread.Sleep(250);
            m_io.Read(2, DeviceRegisters.WHOAMI);
            m_io.Read(2, DeviceRegisters.WHOAMI);
            Thread.Sleep(250);
            bool boardInfoValid;
            GetBoardInfo(out boardInfoValid);
            Thread.Sleep(250);
            bool statusValid;
            GetStatus(out statusValid);
            Thread.Sleep(250);
            m_io.Write(DeviceRegisters.UPDATE_RATE_HZ, new byte[]{(byte)m_updateRateHz});
            return boardInfoValid && statusValid;

        }

        public NavXMxp(SPI.Port port, int updateRateHz = 50)
        {
            if (updateRateHz > 60)
            {
                updateRateHz = 60;
            }
            if (updateRateHz < 4)
            {
                updateRateHz = 4;
            }
            m_updateRateHz = updateRateHz;
            m_io = new SpiIO(port);
            Initialize();
        }

        public NavXMxp(I2C.Port port, int updateRateHz = 50)
        {
            if (updateRateHz > 60)
            {
                updateRateHz = 60;
            }
            if (updateRateHz < 4)
            {
                updateRateHz = 4;
            }
            m_updateRateHz = updateRateHz;
            m_io = new I2cIO(port);
            Initialize();
        }

        public void Dispose()
        {
            m_io.Dispose();
        }

        long m_lastSampleTimeStamp = 0;
        int m_updateRateHz = 50;
        bool m_lastSampleValid = true;

        byte[] m_cacheArray = new byte[0x6f];

        NavXBoardInfo m_boardInfo;
        NavXStatus m_status;

        private ushort Combine(byte high, byte low)
        {
            return (ushort)(low << 8 | high);
        }
        /*
        private byte[] ReadCached(byte readSize, DeviceRegisters register, out bool valid)
        {
            var currentStamp = Utility.GetFPGATime();
            var deltaStamp = currentStamp - m_lastSampleTimeStamp;
            var updateRate = 1000 / m_updateRateHz;
            bool timespampExpired = deltaStamp * 1000 >= updateRate;

            byte[] read;


            if (timespampExpired)
            {
                byte readS = m_boardInfo.VelAndDisp ? (byte)0x6F : (byte)0x55;

                read = Read(readS, 0, out m_lastSampleValid);
                m_lastSampleTimeStamp = currentStamp;
            }
            else
            {
                read = m_cacheArray;
            }

            valid = m_lastSampleValid;
            if (!m_lastSampleValid) return new byte[readSize];
            m_cacheArray = read;

            byte[] retVal = new byte[readSize];
            Array.Copy(read, (int)register, retVal, 0, readSize);
            return retVal;
        }
        */
        private byte[] Read(byte readSize, DeviceRegisters register, out bool valid)
        {
            byte[] retVal = m_io.Read(readSize, register);
            valid = retVal?.Length > 0;
            if (!valid)
            {
                retVal = new byte[readSize];
            }
            return retVal;
        }

        public NavXBoardInfo GetBoardInfo(out bool valid)
        {
            byte[] read = Read(2, DeviceRegisters.CAPATABILITY_FLAGS_L, out valid);

            if (!valid)
            {
                read = new byte[2];
            }

            byte read0 = read[0];

            bool velAndDisp = (read0 & (byte)CapabilityFlags.VEL_AND_DISP) != 0;
            bool omnimount = (read0 & (byte)CapabilityFlags.OMNIMOUNT) != 0;
            bool yawReset = (read0 & (byte)CapabilityFlags.YAW_RESET) != 0;

            bool boardYawAxisUp = true;
            byte boardYawAxis = 2;

            if (omnimount)
            {
                boardYawAxisUp = (read0 & 0x08) != 0;
                int mask = read0 & (byte)CapabilityFlags.OMNIMOUNT_CONFIG_MASK;

                boardYawAxis = (byte)(mask / 16);
            }

            byte[] whoamiRet = Read(4, DeviceRegisters.WHOAMI, out valid);

            if (whoamiRet == null)
            {
                whoamiRet = new byte[4];
            }

            valid = m_lastSampleValid;
            var info =  new NavXBoardInfo(whoamiRet[0], whoamiRet[1], whoamiRet[2], whoamiRet[3],
                boardYawAxisUp, boardYawAxis, omnimount, yawReset, velAndDisp);
            m_boardInfo = info;
            return info;
        }

        IEnumerable<bool> GetBits(byte b)
        {
            for (int i = 0; i < 16; i++)
            {
                yield return (b & 0x80) != 0;
                b *= 2;
            }
        }

        public NavXStatus GetStatus(out bool valid)
        {
            bool UpdateRateReadValid;
            byte[] read = Read(8, DeviceRegisters.UPDATE_RATE_HZ, out UpdateRateReadValid);
            bool SensorReadValid;
            byte[] sensorStatusRead = Read(2, DeviceRegisters.SENSOR_STATUS, out SensorReadValid);



            valid = UpdateRateReadValid && SensorReadValid;
            if (!valid)
            {
                read = new byte[8];
                sensorStatusRead = new byte[2];
            }

            int calStatus = read[5] & (byte)SelfTestStatus.COMPLETE;
            int self = read[6] & (byte)CalStatus.IMU_CAL_STATE_MASK;

            var bits = GetBits(sensorStatusRead[0]).ToList();

            var status = new NavXStatus(read[0], read[1], Combine(read[2], read[3]), (OpStatus)read[4], (CalStatus)calStatus, 
                (byte)self, bits[0], bits[1], bits[2], bits[3], bits[4], bits[5]);
            m_status = status;
            return status;
        }

        public double Yaw { get; private set; }

        public void ZeroDisplacement()
        {
            m_io.Write(DeviceRegisters.INTEGRATION_CONTROL, new byte[] {0x3f});
        }

        public void ZeroYaw()
        {
            if (m_boardInfo.YawReset)
            {
                //Board supports yaw reset
                m_io.Write(DeviceRegisters.INTEGRATION_CONTROL, new byte[] { 128 });
            }
            else
            {
                Yaw = 0;
                bool valid;
                var yprh = GetYPRH(out valid);
                Yaw = valid ? yprh.Yaw : 0;
            }
        }

        public double GetFusedHeading()
        {
            bool valid;
            byte[] read = Read(2, DeviceRegisters.FUSED_HEADING, out valid);
            return Combine(read[0], read[1])/ 100.0;
        }

        public YPRH GetYPRH(out bool valid)
        {
            byte[] read = Read(8, DeviceRegisters.YAW, out valid);
            int yaw = (short)Combine(read[0], read[1]);
            int pitch = (short)Combine(read[2], read[3]);
            int roll = (short)Combine(read[4], read[5]);
            int heading = Combine(read[6], read[7]);

            double[] yawArray = new double[] { Yaw, Yaw, Yaw};

            yaw = yaw / 100;
            pitch = pitch / 100;
            roll = roll / 100;

            heading = heading / 100;

            return new YPRH(yaw - Yaw, pitch - Yaw, roll - Yaw, heading);
        }
    }

    public struct YPRH
    {
        public double Yaw { get; private set; }
        public double Pitch { get; private set; }
        public double Roll { get; private set; }
        public double Heading { get; private set; }

        public YPRH(double yaw, double pitch, double roll, double heading)
        {
            Yaw = yaw;
            Pitch = pitch;
            Roll = roll;
            Heading = heading;
        }
    }
}
