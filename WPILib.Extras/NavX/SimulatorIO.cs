using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Simulator;
using WPILib.Extras.NavX.Protocols;

namespace WPILib.Extras.NavX
{
    class SimulatorIO : IIoProvider
    {
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

        public SimulatorIO(byte update_rate_hz, IIoCompleteNotification notify_sink,
            IBoardCapabilities board_capabilities)
        {
            this.update_rate_hz = update_rate_hz;
            this.board_capabilities = board_capabilities;
            this.notify_sink = notify_sink;
            raw_data_update = new IMUProtocol.GyroUpdate();
            ahrs_update = new AHRSProtocol.AHRSUpdate();
            ahrspos_update = new AHRSProtocol.AHRSPosUpdate();
            board_state = new BoardState();
            board_id = new AHRSProtocol.BoardID();
        }


        public bool IsConnected()
        {
            return true;
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
            //Do Nothing
            update_rate_hz = update_rate;
        }


        public void ZeroYaw()
        {
            SimData.NavXData.GryoAngleYaw = 0;
        }


        public void ZeroDisplacement()
        {
            //TODO: Implement displacement changes
        }

        public void Run()
        {
            SetUpdateRateHz(this.update_rate_hz);

            GetConfiguration();

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
            /*
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
            */

            return true;
        }

        private void GetCurrentData()
        {
            /*
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
            ahrspos_update.quat_w = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_QUAT_W_L - first_address);
            ahrspos_update.quat_x = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_QUAT_X_L - first_address);
            ahrspos_update.quat_y = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_QUAT_Y_L - first_address);
            ahrspos_update.quat_z = AHRSProtocol.decodeBinaryInt16(curr_data, IMURegisters.NAVX_REG_QUAT_Z_L - first_address);

            ahrspos_update.vel_x = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_VEL_X_I_L - first_address);
            ahrspos_update.vel_y = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_VEL_Y_I_L - first_address);
            ahrspos_update.vel_z = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_VEL_Z_I_L - first_address);
            ahrspos_update.disp_x = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_DISP_X_I_L - first_address);
            ahrspos_update.disp_y = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_DISP_Y_I_L - first_address);
            ahrspos_update.disp_z = AHRSProtocol.decodeProtocol1616Float(curr_data, IMURegisters.NAVX_REG_DISP_Z_I_L - first_address);
            notify_sink.SetAHRSPosData(ahrspos_update);


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
            */

            byte_count += 108;
            update_count++;
        }

        public void Stop()
        {
            m_stop = true;
        }
    }
}
