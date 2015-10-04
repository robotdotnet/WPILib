using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib.Extras.NavX.Protocol;

namespace WPILib.Extras.NavX
{
    class RegisterIo : IIoProvider
    {
        IRegisterIo m_ioProvider;
        byte m_updateRateHz;
        bool m_stop;
        IMUProtocol.GyroUpdate m_rawDataUpdate;
        AHRSProtocol.AHRSUpdate m_ahrsUpdate;
        AHRSProtocol.AHRSPosUpdate m_ahrsposUpdate;
        IIoCompleteNotification m_notifySink;
        BoardState m_boardState;
        AHRSProtocol.BoardId m_boardId;
        IBoardCapabilities m_boardCapabilities;
        double m_lastUpdateTime;
        int m_byteCount;
        int m_updateCount;

        public RegisterIo(IRegisterIo ioProvider, byte updateRateHz, IIoCompleteNotification notifySink, IBoardCapabilities boardCapabilities)
        {
            this.m_ioProvider = ioProvider;
            this.m_updateRateHz = updateRateHz;
            this.m_boardCapabilities = boardCapabilities;
            this.m_notifySink = notifySink;
            m_rawDataUpdate = new IMUProtocol.GyroUpdate();
            m_ahrsUpdate = new AHRSProtocol.AHRSUpdate();
            m_ahrsposUpdate = new AHRSProtocol.AHRSPosUpdate();
            m_boardState = new BoardState();
            m_boardId = new AHRSProtocol.BoardId();
        }

        private const double IoTimeoutSeconds = 1.0;

        public void Stop()
        {
            m_stop = true;
        }

        public void Run()
        {

            m_ioProvider.Init();

            /* Initial Device Configuration */
            SetUpdateRateHz(this.m_updateRateHz);
            GetConfiguration();

            /* IO Loop */
            while (!m_stop)
            {
                if (m_boardState.UpdateRateHz != this.m_updateRateHz)
                {
                    SetUpdateRateHz(this.m_updateRateHz);
                }
                GetCurrentData();
                Timer.Delay(1.0 / this.m_updateRateHz);
            }
        }

        private bool GetConfiguration()
        {
            bool success = false;
            int retryCount = 0;
            while (retryCount < 3 && !success)
            {
                byte[] config = new byte[IMURegisters.NavxRegSensorStatusH + 1];
                if (m_ioProvider.Read(IMURegisters.NavxRegWhoami, config))
                {
                    m_boardId.HwRev = config[IMURegisters.NavxRegHwRev];
                    m_boardId.FwVerMajor = config[IMURegisters.NavxRegFwVerMajor];
                    m_boardId.FwVerMinor = config[IMURegisters.NavxRegFwVerMinor];
                    m_boardId.Type = config[IMURegisters.NavxRegWhoami];
                    m_notifySink.SetBoardId(m_boardId);

                    m_boardState.CalStatus = config[IMURegisters.NavxRegCalStatus];
                    m_boardState.OpStatus = config[IMURegisters.NavxRegOpStatus];
                    m_boardState.SelftestStatus = config[IMURegisters.NavxRegSelftestStatus];
                    m_boardState.SensorStatus = AHRSProtocol.DecodeBinaryUint16(config, IMURegisters.NavxRegSensorStatusL);
                    m_boardState.GyroFsrDps = AHRSProtocol.DecodeBinaryUint16(config, IMURegisters.NavxRegGyroFsrDpsL);
                    m_boardState.AccelFsrG = (short)config[IMURegisters.NavxRegAccelFsrG];
                    m_boardState.UpdateRateHz = config[IMURegisters.NavxRegUpdateRateHz];
                    m_boardState.CapabilityFlags = AHRSProtocol.DecodeBinaryUint16(config, IMURegisters.NavxRegCapabilityFlagsL);
                    m_notifySink.SetBoardState(m_boardState);
                    success = true;
                }
                else
                {
                    success = false;
                    Timer.Delay(0.05);
                }
                retryCount++;
            }
            return success;
        }


        private void GetCurrentData()
        {
            byte firstAddress = IMURegisters.NavxRegUpdateRateHz;
            bool displacementRegisters = m_boardCapabilities.IsDisplacementSupported();
            byte[] currData;
            /* If firmware supports displacement data, acquire it - otherwise implement */
            /* similar (but potentially less accurate) calculations on this processor.  */
            if (displacementRegisters)
            {
                currData = new byte[IMURegisters.NavxRegLast + 1 - firstAddress];
            }
            else
            {
                currData = new byte[IMURegisters.NavxRegQuatOffsetZH + 1 - firstAddress];
            }
            long timestampLow, timestampHigh;
            long sensorTimestamp;
            if (m_ioProvider.Read(firstAddress, currData))
            {
                timestampLow = (long)AHRSProtocol.DecodeBinaryUint16(currData, IMURegisters.NavxRegTimestampLL - firstAddress);
                timestampHigh = (long)AHRSProtocol.DecodeBinaryUint16(currData, IMURegisters.NavxRegTimestampHL - firstAddress);
                sensorTimestamp = (timestampHigh << 16) + timestampLow;
                m_ahrsposUpdate.OpStatus = currData[IMURegisters.NavxRegOpStatus - firstAddress];
                m_ahrsposUpdate.SelftestStatus = currData[IMURegisters.NavxRegSelftestStatus - firstAddress];
                m_ahrsposUpdate.CalStatus = currData[IMURegisters.NavxRegCalStatus];
                m_ahrsposUpdate.SensorStatus = currData[IMURegisters.NavxRegSensorStatusL - firstAddress];
                m_ahrsposUpdate.Yaw = AHRSProtocol.DecodeProtocolSignedHundredthsFloat(currData, IMURegisters.NavxRegYawL - firstAddress);
                m_ahrsposUpdate.Pitch = AHRSProtocol.DecodeProtocolSignedHundredthsFloat(currData, IMURegisters.NavxRegPitchL - firstAddress);
                m_ahrsposUpdate.Roll = AHRSProtocol.DecodeProtocolSignedHundredthsFloat(currData, IMURegisters.NavxRegRollL - firstAddress);
                m_ahrsposUpdate.CompassHeading = AHRSProtocol.decodeProtocolUnsignedHundredthsFloat(currData, IMURegisters.NavxRegHeadingL - firstAddress);
                m_ahrsposUpdate.MpuTemp = AHRSProtocol.DecodeProtocolSignedHundredthsFloat(currData, IMURegisters.NavxRegMpuTempCL - firstAddress);
                m_ahrsposUpdate.LinearAccelX = AHRSProtocol.DecodeProtocolSignedThousandthsFloat(currData, IMURegisters.NavxRegLinearAccXL - firstAddress);
                m_ahrsposUpdate.LinearAccelY = AHRSProtocol.DecodeProtocolSignedThousandthsFloat(currData, IMURegisters.NavxRegLinearAccYL - firstAddress);
                m_ahrsposUpdate.LinearAccelZ = AHRSProtocol.DecodeProtocolSignedThousandthsFloat(currData, IMURegisters.NavxRegLinearAccZL - firstAddress);
                m_ahrsposUpdate.Altitude = AHRSProtocol.DecodeProtocol1616Float(currData, IMURegisters.NavxRegAltitudeDL - firstAddress);
                m_ahrsposUpdate.BarometricPressure = AHRSProtocol.DecodeProtocol1616Float(currData, IMURegisters.NavxRegPressureDl - firstAddress);
                m_ahrsposUpdate.FusedHeading = AHRSProtocol.decodeProtocolUnsignedHundredthsFloat(currData, IMURegisters.NavxRegFusedHeadingL - firstAddress);
                m_ahrsposUpdate.QuatW = AHRSProtocol.DecodeBinaryInt16(currData, IMURegisters.NavxRegQuatWL - firstAddress);
                m_ahrsposUpdate.QuatX = AHRSProtocol.DecodeBinaryInt16(currData, IMURegisters.NavxRegQuatXL - firstAddress);
                m_ahrsposUpdate.QuatY = AHRSProtocol.DecodeBinaryInt16(currData, IMURegisters.NavxRegQuatYL - firstAddress);
                m_ahrsposUpdate.QuatZ = AHRSProtocol.DecodeBinaryInt16(currData, IMURegisters.NavxRegQuatZL - firstAddress);
                if (displacementRegisters)
                {
                    m_ahrsposUpdate.VelX = AHRSProtocol.DecodeProtocol1616Float(currData, IMURegisters.NavxRegVelXIL - firstAddress);
                    m_ahrsposUpdate.VelY = AHRSProtocol.DecodeProtocol1616Float(currData, IMURegisters.NavxRegVelYIL - firstAddress);
                    m_ahrsposUpdate.VelZ = AHRSProtocol.DecodeProtocol1616Float(currData, IMURegisters.NavxRegVelZIL - firstAddress);
                    m_ahrsposUpdate.DispX = AHRSProtocol.DecodeProtocol1616Float(currData, IMURegisters.NavxRegDispXIL - firstAddress);
                    m_ahrsposUpdate.DispY = AHRSProtocol.DecodeProtocol1616Float(currData, IMURegisters.NavxRegDispYIL - firstAddress);
                    m_ahrsposUpdate.DispZ = AHRSProtocol.DecodeProtocol1616Float(currData, IMURegisters.NavxRegDispZIL - firstAddress);
                    m_notifySink.SetAHRSPosData(m_ahrsposUpdate);
                }
                else
                {
                    m_ahrsUpdate.OpStatus = m_ahrsposUpdate.OpStatus;
                    m_ahrsUpdate.SelftestStatus = m_ahrsposUpdate.SelftestStatus;
                    m_ahrsUpdate.CalStatus = m_ahrsposUpdate.CalStatus;
                    m_ahrsUpdate.SensorStatus = m_ahrsposUpdate.SensorStatus;
                    m_ahrsUpdate.Yaw = m_ahrsposUpdate.Yaw;
                    m_ahrsUpdate.Pitch = m_ahrsposUpdate.Pitch;
                    m_ahrsUpdate.Roll = m_ahrsposUpdate.Roll;
                    m_ahrsUpdate.CompassHeading = m_ahrsposUpdate.CompassHeading;
                    m_ahrsUpdate.MpuTemp = m_ahrsposUpdate.MpuTemp;
                    m_ahrsUpdate.LinearAccelX = m_ahrsposUpdate.LinearAccelX;
                    m_ahrsUpdate.LinearAccelY = m_ahrsposUpdate.LinearAccelY;
                    m_ahrsUpdate.LinearAccelZ = m_ahrsposUpdate.LinearAccelZ;
                    m_ahrsUpdate.Altitude = m_ahrsposUpdate.Altitude;
                    m_ahrsUpdate.BarometricPressure = m_ahrsposUpdate.BarometricPressure;
                    m_ahrsUpdate.FusedHeading = m_ahrsposUpdate.FusedHeading;
                    m_notifySink.SetAHRSData(m_ahrsUpdate);
                }

                m_boardState.CalStatus = currData[IMURegisters.NavxRegCalStatus - firstAddress];
                m_boardState.OpStatus = currData[IMURegisters.NavxRegOpStatus - firstAddress];
                m_boardState.SelftestStatus = currData[IMURegisters.NavxRegSelftestStatus - firstAddress];
                m_boardState.SensorStatus = AHRSProtocol.DecodeBinaryUint16(currData, IMURegisters.NavxRegSensorStatusL - firstAddress);
                m_boardState.UpdateRateHz = currData[IMURegisters.NavxRegUpdateRateHz - firstAddress];
                m_boardState.GyroFsrDps = AHRSProtocol.DecodeBinaryUint16(currData, IMURegisters.NavxRegGyroFsrDpsL);
                m_boardState.AccelFsrG = (short)currData[IMURegisters.NavxRegAccelFsrG];
                m_boardState.CapabilityFlags = AHRSProtocol.DecodeBinaryUint16(currData, IMURegisters.NavxRegCapabilityFlagsL - firstAddress);
                m_notifySink.SetBoardState(m_boardState);

                m_rawDataUpdate.GyroX = AHRSProtocol.DecodeBinaryInt16(currData, IMURegisters.NavxRegGyroXL - firstAddress);
                m_rawDataUpdate.GyroY = AHRSProtocol.DecodeBinaryInt16(currData, IMURegisters.NavxRegGyroYL - firstAddress);
                m_rawDataUpdate.GyroZ = AHRSProtocol.DecodeBinaryInt16(currData, IMURegisters.NavxRegGyroZL - firstAddress);
                m_rawDataUpdate.AccelX = AHRSProtocol.DecodeBinaryInt16(currData, IMURegisters.NavxRegAccXL - firstAddress);
                m_rawDataUpdate.AccelY = AHRSProtocol.DecodeBinaryInt16(currData, IMURegisters.NavxRegAccYL - firstAddress);
                m_rawDataUpdate.AccelZ = AHRSProtocol.DecodeBinaryInt16(currData, IMURegisters.NavxRegAccZL - firstAddress);
                m_rawDataUpdate.MagX = AHRSProtocol.DecodeBinaryInt16(currData, IMURegisters.NavxRegMagXL - firstAddress);
                m_rawDataUpdate.TempC = m_ahrsposUpdate.MpuTemp;
                m_notifySink.SetRawData(m_rawDataUpdate);

                this.m_lastUpdateTime = Timer.GetFPGATimestamp();
                m_byteCount += currData.Length;
                m_updateCount++;
            }
        }

        
    public bool IsConnected()
        {
            double timeSinceLastUpdate = Timer.GetFPGATimestamp() - this.m_lastUpdateTime;
            return timeSinceLastUpdate <= IoTimeoutSeconds;
        }

        
    public double GetByteCount()
        {
            return m_byteCount;
        }

        
    public double GetUpdateCount()
        {
            return m_updateCount;
        }

        
    public void SetUpdateRateHz(byte updateRate)
        {
            m_ioProvider.Write(IMURegisters.NavxRegUpdateRateHz, updateRate);
        }

        
    public void ZeroYaw()
        {
            m_ioProvider.Write(IMURegisters.NavxRegIntegrationCtl,
                                       AHRSProtocol.NavxIntegrationCtlResetYaw);
        }

        
    public void ZeroDisplacement()
        {
            m_ioProvider.Write(IMURegisters.NavxRegIntegrationCtl,
                                (byte)(AHRSProtocol.NavxIntegrationCtlResetDispX |
                                       AHRSProtocol.NavxIntegrationCtlResetDispY |
                                       AHRSProtocol.NavxIntegrationCtlResetDispZ));
        }
    }
}
