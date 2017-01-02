using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib.Extras.NavX.Protocols;

namespace WPILib.Extras.NavX
{
    class RegisterIO : IIoProvider
    {
        IRegisterIo io_provider;
        byte update_rate_hz;
        bool m_stop;
        IMUProtocol.GyroUpdate raw_data_update;
        AHRSProtocol.AHRSUpdate ahrs_update;
        AHRSProtocol.AHRSPosUpdate ahrspos_update;
        IIoCompleteNotification notify_sink;
        BoardState board_state;
        AHRSProtocol.BoardID board_id;
        IBoardCapabilities board_capabilities;
        double last_update_time;
        int byte_count;
        int update_count;

        public RegisterIO(IRegisterIo io_provider, byte update_rate_hz, IIoCompleteNotification notify_sink, IBoardCapabilities board_capabilities)
        {
            this.io_provider = io_provider;
            this.update_rate_hz = update_rate_hz;
            this.board_capabilities = board_capabilities;
            this.notify_sink = notify_sink;
            raw_data_update = new IMUProtocol.GyroUpdate();
            ahrs_update = new AHRSProtocol.AHRSUpdate();
            ahrspos_update = new AHRSProtocol.AHRSPosUpdate();
            board_state = new BoardState();
            board_id = new AHRSProtocol.BoardID();
        }

        private const double IO_TIMEOUT_SECONDS = 1.0;

        public void Stop()
        {
            m_stop = true;
        }

        public void Run()
        {

            io_provider.Init();

            /* Initial Device Configuration */
            SetUpdateRateHz(this.update_rate_hz);
            GetConfiguration();

            /* IO Loop */
            while (!m_stop)
            {
                if (board_state.UpdateRateHz != this.update_rate_hz)
                {
                    SetUpdateRateHz(this.update_rate_hz);
                }
                GetCurrentData();
                Timer.Delay(1.0 / this.update_rate_hz);
            }
        }

        private bool GetConfiguration()
        {
            bool success = false;
            int retry_count = 0;
            while (retry_count < 3 && !success)
            {
                byte[] config = new byte[IMURegisters.NAVX_REG_SENSOR_STATUS_H + 1];
                if (io_provider.Read(IMURegisters.NAVX_REG_WHOAMI, config))
                {
                    board_id.hw_rev = config[IMURegisters.NAVX_REG_HW_REV];
                    board_id.fw_ver_major = config[IMURegisters.NAVX_REG_FW_VER_MAJOR];
                    board_id.fw_ver_minor = config[IMURegisters.NAVX_REG_FW_VER_MINOR];
                    board_id.type = config[IMURegisters.NAVX_REG_WHOAMI];
                    notify_sink.SetBoardID(board_id);

                    board_state.CalStatus = config[IMURegisters.NAVX_REG_CAL_STATUS];
                    board_state.OpStatus = config[IMURegisters.NAVX_REG_OP_STATUS];
                    board_state.SelftestStatus = config[IMURegisters.NAVX_REG_SELFTEST_STATUS];
                    board_state.SensorStatus = AHRSProtocol.decodeBinaryUint16(config, IMURegisters.NAVX_REG_SENSOR_STATUS_L);
                    board_state.GyroFsrDps = AHRSProtocol.decodeBinaryUint16(config, IMURegisters.NAVX_REG_GYRO_FSR_DPS_L);
                    board_state.AccelFsrG = (short)config[IMURegisters.NAVX_REG_ACCEL_FSR_G];
                    board_state.UpdateRateHz = config[IMURegisters.NAVX_REG_UPDATE_RATE_HZ];
                    board_state.CapabilityFlags = AHRSProtocol.decodeBinaryUint16(config, IMURegisters.NAVX_REG_CAPABILITY_FLAGS_L);
                    notify_sink.SetBoardState(board_state);
                    success = true;
                }
                else {
                    success = false;
                    Timer.Delay(0.05);
                }
                retry_count++;
            }
            return success;
        }


        private void GetCurrentData()
        {
            byte first_address = IMURegisters.NAVX_REG_UPDATE_RATE_HZ;
            bool displacement_registers = board_capabilities.IsDisplacementSupported();
            byte[] curr_data;
            /* If firmware supports displacement data, acquire it - otherwise implement */
            /* similar (but potentially less accurate) calculations on this processor.  */
            if (displacement_registers)
            {
                curr_data = new byte[IMURegisters.NAVX_REG_LAST + 1 - first_address];
            }
            else {
                curr_data = new byte[IMURegisters.NAVX_REG_QUAT_OFFSET_Z_H + 1 - first_address];
            }
            if (io_provider.Read(first_address, curr_data))
            {
                long sensor_timestamp = AHRSProtocol.decodeBinaryUint32(curr_data,
                    IMURegisters.NAVX_REG_TIMESTAMP_L_L - first_address);
                ahrspos_update.op_status = curr_data[IMURegisters.NAVX_REG_OP_STATUS - first_address];
                ahrspos_update.selftest_status = curr_data[IMURegisters.NAVX_REG_SELFTEST_STATUS - first_address];
                ahrspos_update.cal_status = curr_data[IMURegisters.NAVX_REG_CAL_STATUS];
                ahrspos_update.sensor_status = curr_data[IMURegisters.NAVX_REG_SENSOR_STATUS_L - first_address];
                ahrspos_update.yaw = AHRSProtocol.decodeProtocolSignedHundredthsFloat(curr_data, IMURegisters.NAVX_REG_YAW_L - first_address);
                ahrspos_update.pitch = AHRSProtocol.decodeProtocolSignedHundredthsFloat(curr_data, IMURegisters.NAVX_REG_PITCH_L - first_address);
                ahrspos_update.roll = AHRSProtocol.decodeProtocolSignedHundredthsFloat(curr_data, IMURegisters.NAVX_REG_ROLL_L - first_address);
                ahrspos_update.compass_heading = AHRSProtocol.decodeProtocolUnsignedHundredthsFloat(curr_data, IMURegisters.NAVX_REG_HEADING_L - first_address);
                ahrspos_update.mpu_temp = AHRSProtocol.decodeProtocolSignedHundredthsFloat(curr_data, IMURegisters.NAVX_REG_MPU_TEMP_C_L - first_address);
                ahrspos_update.linear_accel_x = AHRSProtocol.decodeProtocolSignedThousandthsFloat(curr_data, IMURegisters.NAVX_REG_LINEAR_ACC_X_L - first_address);
                ahrspos_update.linear_accel_y = AHRSProtocol.decodeProtocolSignedThousandthsFloat(curr_data, IMURegisters.NAVX_REG_LINEAR_ACC_Y_L - first_address);
                ahrspos_update.linear_accel_z = AHRSProtocol.decodeProtocolSignedThousandthsFloat(curr_data, IMURegisters.NAVX_REG_LINEAR_ACC_Z_L - first_address);
                ahrspos_update.altitude = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_ALTITUDE_D_L - first_address);
                ahrspos_update.barometric_pressure = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_PRESSURE_DL - first_address);
                ahrspos_update.fused_heading = AHRSProtocol.decodeProtocolUnsignedHundredthsFloat(curr_data, IMURegisters.NAVX_REG_FUSED_HEADING_L - first_address);
                ahrspos_update.quat_w = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_QUAT_W_L - first_address) / 32768.0f;
                ahrspos_update.quat_x = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_QUAT_X_L - first_address) / 32768.0f;
                ahrspos_update.quat_y = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_QUAT_Y_L - first_address) / 32768.0f;
                ahrspos_update.quat_z = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_QUAT_Z_L - first_address) / 32768.0f;
                if (displacement_registers)
                {
                    ahrspos_update.vel_x = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_VEL_X_I_L - first_address);
                    ahrspos_update.vel_y = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_VEL_Y_I_L - first_address);
                    ahrspos_update.vel_z = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_VEL_Z_I_L - first_address);
                    ahrspos_update.disp_x = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_DISP_X_I_L - first_address);
                    ahrspos_update.disp_y = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_DISP_Y_I_L - first_address);
                    ahrspos_update.disp_z = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_DISP_Z_I_L - first_address);
                    notify_sink.SetAHRSPosData(ahrspos_update);
                }
                else {
                    ahrs_update.op_status = ahrspos_update.op_status;
                    ahrs_update.selftest_status = ahrspos_update.selftest_status;
                    ahrs_update.cal_status = ahrspos_update.cal_status;
                    ahrs_update.sensor_status = ahrspos_update.sensor_status;
                    ahrs_update.yaw = ahrspos_update.yaw;
                    ahrs_update.pitch = ahrspos_update.pitch;
                    ahrs_update.roll = ahrspos_update.roll;
                    ahrs_update.compass_heading = ahrspos_update.compass_heading;
                    ahrs_update.mpu_temp = ahrspos_update.mpu_temp;
                    ahrs_update.linear_accel_x = ahrspos_update.linear_accel_x;
                    ahrs_update.linear_accel_y = ahrspos_update.linear_accel_y;
                    ahrs_update.linear_accel_z = ahrspos_update.linear_accel_z;
                    ahrs_update.altitude = ahrspos_update.altitude;
                    ahrs_update.barometric_pressure = ahrspos_update.barometric_pressure;
                    ahrs_update.fused_heading = ahrspos_update.fused_heading;
                    notify_sink.SetAHRSData(ahrs_update);
                }

                board_state.CalStatus = curr_data[IMURegisters.NAVX_REG_CAL_STATUS - first_address];
                board_state.OpStatus = curr_data[IMURegisters.NAVX_REG_OP_STATUS - first_address];
                board_state.SelftestStatus = curr_data[IMURegisters.NAVX_REG_SELFTEST_STATUS - first_address];
                board_state.SensorStatus = AHRSProtocol.decodeBinaryUint16(curr_data, IMURegisters.NAVX_REG_SENSOR_STATUS_L - first_address);
                board_state.UpdateRateHz = curr_data[IMURegisters.NAVX_REG_UPDATE_RATE_HZ - first_address];
                board_state.GyroFsrDps = AHRSProtocol.decodeBinaryUint16(curr_data, IMURegisters.NAVX_REG_GYRO_FSR_DPS_L);
                board_state.AccelFsrG = (short)curr_data[IMURegisters.NAVX_REG_ACCEL_FSR_G];
                board_state.CapabilityFlags = AHRSProtocol.decodeBinaryUint16(curr_data, IMURegisters.NAVX_REG_CAPABILITY_FLAGS_L - first_address);
                notify_sink.SetBoardState(board_state);

                raw_data_update.gyro_x = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_GYRO_X_L - first_address);
                raw_data_update.gyro_y = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_GYRO_Y_L - first_address);
                raw_data_update.gyro_z = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_GYRO_Z_L - first_address);
                raw_data_update.accel_x = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_ACC_X_L - first_address);
                raw_data_update.accel_y = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_ACC_Y_L - first_address);
                raw_data_update.accel_z = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_ACC_Z_L - first_address);
                raw_data_update.mag_x = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_MAG_X_L - first_address);
                raw_data_update.mag_y = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_MAG_Y_L - first_address);
                raw_data_update.mag_z = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_MAG_Z_L - first_address);
                raw_data_update.temp_c = ahrspos_update.mpu_temp;
                notify_sink.SetRawData(raw_data_update);

                this.last_update_time = Timer.GetFPGATimestamp();
                byte_count += curr_data.Length;
                update_count++;
            }
        }


        public bool IsConnected()
        {
            double time_since_last_update = Timer.GetFPGATimestamp() - this.last_update_time;
            return time_since_last_update <= IO_TIMEOUT_SECONDS;
        }

        public double GetByteCount()
        {
            return byte_count;
        }


        public double GetUpdateCount()
        {
            return update_count;
        }


        public void SetUpdateRateHz(byte update_rate)
        {
            io_provider.Write(IMURegisters.NAVX_REG_UPDATE_RATE_HZ, update_rate);
        }


        public void ZeroYaw()
        {
            io_provider.Write(IMURegisters.NAVX_REG_INTEGRATION_CTL,
                                       AHRSProtocol.NAVX_INTEGRATION_CTL_RESET_YAW);
            this.notify_sink.YawResetComplete();
        }


        public void ZeroDisplacement()
        {
            io_provider.Write(IMURegisters.NAVX_REG_INTEGRATION_CTL,
                                (byte)(AHRSProtocol.NAVX_INTEGRATION_CTL_RESET_DISP_X |
                                       AHRSProtocol.NAVX_INTEGRATION_CTL_RESET_DISP_Y |
                                       AHRSProtocol.NAVX_INTEGRATION_CTL_RESET_DISP_Z));
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
