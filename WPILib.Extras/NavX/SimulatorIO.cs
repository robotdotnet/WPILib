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
            SimData.NavXData.GyroAngleYaw = 0;
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

            board_id.hw_rev = 33;
            board_id.fw_ver_major = 2;
            board_id.fw_ver_minor = 3;
            board_id.type = 50;
            notify_sink.SetBoardID(board_id);

            board_state.CalStatus = 6;
            board_state.OpStatus = 4;
            board_state.SelftestStatus = 135;
            board_state.SensorStatus = 1542;
            board_state.GyroFsrDps = 2000;
            board_state.AccelFsrG = 2;
            board_state.UpdateRateHz = update_rate_hz;
            board_state.CapabilityFlags = 236;
            notify_sink.SetBoardState(board_state);


            return true;
        }

        public float WrapNeg180To180(float value)
        {
            const float max = 180;
            const float min = -180;
            if (value > max)
                return (value - max) + min;
            if (value < min)
                return max - (min - value);
            return value;
        }

        public float Wrap0To360(float value)
        {
            const float max = 180;
            const float min = -180;
            if (value > max)
                return (value - max) + min;
            if (value < min)
                return max - (min - value);
            return value;
        }

        private void GetCurrentData()
        {

            ahrspos_update.op_status = 4;
            ahrspos_update.selftest_status = 135;
            ahrspos_update.cal_status = 6;
            ahrspos_update.sensor_status = 6;
            ahrspos_update.yaw = WrapNeg180To180((float)SimData.NavXData.GyroAngleYaw);
            ahrspos_update.pitch = WrapNeg180To180((float)SimData.NavXData.GyroAnglePitch);
            ahrspos_update.roll = WrapNeg180To180((float)SimData.NavXData.GyroAngleRoll);
            ahrspos_update.compass_heading = WrapNeg180To180((float)SimData.NavXData.GyroAngleYaw);
            ahrspos_update.mpu_temp = 20.0f;
            ahrspos_update.linear_accel_x = (float)SimData.NavXData.AccelX;
            ahrspos_update.linear_accel_y = (float)SimData.NavXData.AccelY;
            ahrspos_update.linear_accel_z = (float)SimData.NavXData.AccelZ;
            ahrspos_update.altitude = 0.0f;
            ahrspos_update.barometric_pressure = 0.0f;
            ahrspos_update.fused_heading = Wrap0To360((float)SimData.NavXData.GyroAngleYaw);
            ahrspos_update.quat_w = 0;
            ahrspos_update.quat_x = 0;
            ahrspos_update.quat_y = 0;
            ahrspos_update.quat_z = 0;

            ahrspos_update.vel_x = 0;
            ahrspos_update.vel_y = 0;
            ahrspos_update.vel_z = 0;
            ahrspos_update.disp_x = 0;
            ahrspos_update.disp_y = 0;
            ahrspos_update.disp_z = 0;
            notify_sink.SetAHRSPosData(ahrspos_update);


            board_state.CalStatus = 6;
            board_state.OpStatus = 4;
            board_state.SelftestStatus = 135;
            board_state.SensorStatus = 1542;
            board_state.UpdateRateHz = update_rate_hz;
            board_state.GyroFsrDps = 2000;
            board_state.AccelFsrG = 2;
            board_state.CapabilityFlags = 236;
            notify_sink.SetBoardState(board_state);

            raw_data_update.gyro_x = (short)(SimData.NavXData.GyroRatePitch * (DevUnitsMax / 2000.0));
            raw_data_update.gyro_y = (short)(SimData.NavXData.GyroRateRoll * (DevUnitsMax / 2000.0));
            raw_data_update.gyro_z = (short)(SimData.NavXData.GyroRateYaw * (DevUnitsMax / 2000.0));
            raw_data_update.accel_x = (short)(SimData.NavXData.AccelX * (DevUnitsMax / 2.0));
            raw_data_update.accel_y = (short)(SimData.NavXData.AccelY * (DevUnitsMax / 2.0));
            raw_data_update.accel_z = (short)(SimData.NavXData.AccelZ * (DevUnitsMax / 2.0));
            raw_data_update.mag_x = 0;
            raw_data_update.mag_y = 0;
            raw_data_update.mag_z = 0;
            raw_data_update.temp_c = ahrspos_update.mpu_temp;
            notify_sink.SetRawData(raw_data_update);


            byte_count += 108;
            update_count++;
        }

        private const float DevUnitsMax = 32768.0f;

        public void Stop()
        {
            m_stop = true;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
