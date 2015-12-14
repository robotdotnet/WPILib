using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX.Protocols
{
    public class IMUProtocol
    {
        public const byte PACKET_START_CHAR = (byte)'!';
        const int PROTOCOL_FLOAT_LENGTH = 7;
        const int CHECKSUM_LENGTH = 2;
        const int TERMINATOR_LENGTH = 2;

        // Yaw/Pitch/Roll (YPR) Update Packet - e.g., !y[yaw][pitch][roll][compass_heading]
        public const byte MSGID_YPR_UPDATE = (byte)'y';
        const int YPR_UPDATE_YAW_VALUE_INDEX = 2;
        const int YPR_UPDATE_PITCH_VALUE_INDEX = 9;
        const int YPR_UPDATE_ROLL_VALUE_INDEX = 16;
        const int YPR_UPDATE_COMPASS_VALUE_INDEX = 23;
        const int YPR_UPDATE_CHECKSUM_INDEX = 30;
        const int YPR_UPDATE_TERMINATOR_INDEX = 32;
        const int YPR_UPDATE_MESSAGE_LENGTH = 34;

        // Quaternion Data Update Packet - e.g., !r[q1][q2][q3][q4][accelx][accely][accelz][magx][magy][magz]
        public const byte MSGID_QUATERNION_UPDATE = (byte)'q';
        const int QUATERNION_UPDATE_MESSAGE_LENGTH = 53;
        const int QUATERNION_UPDATE_QUAT1_VALUE_INDEX = 2;
        const int QUATERNION_UPDATE_QUAT2_VALUE_INDEX = 6;
        const int QUATERNION_UPDATE_QUAT3_VALUE_INDEX = 10;
        const int QUATERNION_UPDATE_QUAT4_VALUE_INDEX = 14;
        const int QUATERNION_UPDATE_ACCEL_X_VALUE_INDEX = 18;
        const int QUATERNION_UPDATE_ACCEL_Y_VALUE_INDEX = 22;
        const int QUATERNION_UPDATE_ACCEL_Z_VALUE_INDEX = 26;
        const int QUATERNION_UPDATE_MAG_X_VALUE_INDEX = 30;
        const int QUATERNION_UPDATE_MAG_Y_VALUE_INDEX = 34;
        const int QUATERNION_UPDATE_MAG_Z_VALUE_INDEX = 38;
        const int QUATERNION_UPDATE_TEMP_VALUE_INDEX = 42;
        const int QUATERNION_UPDATE_CHECKSUM_INDEX = 49;
        const int QUATERNION_UPDATE_TERMINATOR_INDEX = 51;

        // Gyro/Raw Data Update packet - e.g., !g[gx][gy][gz][accelx][accely][accelz][magx][magy][magz][temp_c][cr][lf]

        public const byte MSGID_GYRO_UPDATE = (byte)'g';
        const int GYRO_UPDATE_GYRO_X_VALUE_INDEX = 2;
        const int GYRO_UPDATE_GYRO_Y_VALUE_INDEX = 6;
        const int GYRO_UPDATE_GYRO_Z_VALUE_INDEX = 10;
        const int GYRO_UPDATE_ACCEL_X_VALUE_INDEX = 14;
        const int GYRO_UPDATE_ACCEL_Y_VALUE_INDEX = 18;
        const int GYRO_UPDATE_ACCEL_Z_VALUE_INDEX = 22;
        const int GYRO_UPDATE_MAG_X_VALUE_INDEX = 26;
        const int GYRO_UPDATE_MAG_Y_VALUE_INDEX = 30;
        const int GYRO_UPDATE_MAG_Z_VALUE_INDEX = 34;
        const int GYRO_UPDATE_TEMP_VALUE_INDEX = 38;
        const int GYRO_UPDATE_CHECKSUM_INDEX = 42;
        const int GYRO_UPDATE_TERMINATOR_INDEX = 44;
        const int GYRO_UPDATE_MESSAGE_LENGTH = 46;

        // EnableStream Command Packet - e.g., !S[stream type][checksum][cr][lf]
        public const byte MSGID_STREAM_CMD = (byte)'S';
        public const int STREAM_CMD_STREAM_TYPE_YPR = MSGID_YPR_UPDATE;
        public const int STREAM_CMD_STREAM_TYPE_QUATERNION = MSGID_QUATERNION_UPDATE;
        public const int STREAM_CMD_STREAM_TYPE_GYRO = MSGID_GYRO_UPDATE;
        const int STREAM_CMD_STREAM_TYPE_INDEX = 2;
        const int STREAM_CMD_UPDATE_RATE_HZ_INDEX = 3;
        const int STREAM_CMD_CHECKSUM_INDEX = 5;
        const int STREAM_CMD_TERMINATOR_INDEX = 7;
        const int STREAM_CMD_MESSAGE_LENGTH = 9;

        // EnableStream Response Packet - e.g., !s[stream type][gyro full scale range][accel full scale range][update rate hz][yaw_offset_degrees][flags][checksum][cr][lf]
        public const byte MSG_ID_STREAM_RESPONSE = (byte)'s';
        const int STREAM_RESPONSE_MESSAGE_LENGTH = 46;
        const int STREAM_RESPONSE_STREAM_TYPE_INDEX = 2;
        const int STREAM_RESPONSE_GYRO_FULL_SCALE_DPS_RANGE = 3;
        const int STREAM_RESPONSE_ACCEL_FULL_SCALE_G_RANGE = 7;
        const int STREAM_RESPONSE_UPDATE_RATE_HZ = 11;
        const int STREAM_RESPONSE_YAW_OFFSET_DEGREES = 15;
        const int STREAM_RESPONSE_QUAT1_OFFSET = 22;
        const int STREAM_RESPONSE_QUAT2_OFFSET = 26;
        const int STREAM_RESPONSE_QUAT3_OFFSET = 30;
        const int STREAM_RESPONSE_QUAT4_OFFSET = 34;
        const int STREAM_RESPONSE_FLAGS = 38;
        const int STREAM_RESPONSE_CHECKSUM_INDEX = 42;
        const int STREAM_RESPONSE_TERMINATOR_INDEX = 44;

        public const byte STREAM_MSG_TERMINATION_CHAR = (byte)'\n';

        public const short NAV6_FLAG_MASK_CALIBRATION_STATE = 0x03;

        public const short NAV6_CALIBRATION_STATE_WAIT = 0x00;
        public const short NAV6_CALIBRATION_STATE_ACCUMULATE = 0x01;
        public const short NAV6_CALIBRATION_STATE_COMPLETE = 0x02;

        public const int IMU_PROTOCOL_MAX_MESSAGE_LENGTH = QUATERNION_UPDATE_MESSAGE_LENGTH;

        public class YPRUpdate
        {

            public float yaw;
            public float pitch;
            public float roll;
            public float compass_heading;
        }

        public class StreamCommand
        {

            public byte stream_type;
        }

        public class StreamResponse
        {

            public byte stream_type;
            public short gyro_fsr_dps;
            public short accel_fsr_g;
            public short update_rate_hz;
            public float yaw_offset_degrees;
            public short q1_offset;
            public short q2_offset;
            public short q3_offset;
            public short q4_offset;
            public short flags;
        }

        public class QuaternionUpdate
        {

            public short q1;
            public short q2;
            public short q3;
            public short q4;
            public short accel_x;
            public short accel_y;
            public short accel_z;
            public short mag_x;
            public short mag_y;
            public short mag_z;
            public float temp_c;
        }

        public class GyroUpdate
        {

            public short gyro_x;
            public short gyro_y;
            public short gyro_z;
            public short accel_x;
            public short accel_y;
            public short accel_z;
            public short mag_x;
            public short mag_y;
            public short mag_z;
            public float temp_c;
        }

        public static int encodeStreamCommand(byte[] protocol_buffer, byte stream_type, byte update_rate_hz)
        {
            // Header
            protocol_buffer[0] = PACKET_START_CHAR;
            protocol_buffer[1] = MSGID_STREAM_CMD;

            // Data
            protocol_buffer[STREAM_CMD_STREAM_TYPE_INDEX] = stream_type;
            byteToHex(update_rate_hz, protocol_buffer, STREAM_CMD_UPDATE_RATE_HZ_INDEX);

            // Footer
            encodeTermination(protocol_buffer, STREAM_CMD_MESSAGE_LENGTH, STREAM_CMD_MESSAGE_LENGTH - 4);

            return STREAM_CMD_MESSAGE_LENGTH;
        }

        public static int decodeStreamResponse(byte[] buffer, int offset, int length, StreamResponse r)
        {

            if (length < STREAM_RESPONSE_MESSAGE_LENGTH)
            {
                return 0;
            }
            if ((buffer[offset + 0] == PACKET_START_CHAR) && (buffer[offset + 1] == MSG_ID_STREAM_RESPONSE))
            {
                if (!verifyChecksum(buffer, offset, STREAM_RESPONSE_CHECKSUM_INDEX))
                {
                    return 0;
                }

                r.stream_type = buffer[offset + 2];
                r.gyro_fsr_dps = decodeProtocolUint16(buffer, offset + STREAM_RESPONSE_GYRO_FULL_SCALE_DPS_RANGE);
                r.accel_fsr_g = decodeProtocolUint16(buffer, offset + STREAM_RESPONSE_ACCEL_FULL_SCALE_G_RANGE);
                r.update_rate_hz = decodeProtocolUint16(buffer, offset + STREAM_RESPONSE_UPDATE_RATE_HZ);
                r.yaw_offset_degrees = decodeProtocolFloat(buffer, offset + STREAM_RESPONSE_YAW_OFFSET_DEGREES);
                r.q1_offset = decodeProtocolUint16(buffer, offset + STREAM_RESPONSE_QUAT1_OFFSET);
                r.q2_offset = decodeProtocolUint16(buffer, offset + STREAM_RESPONSE_QUAT2_OFFSET);
                r.q3_offset = decodeProtocolUint16(buffer, offset + STREAM_RESPONSE_QUAT3_OFFSET);
                r.q4_offset = decodeProtocolUint16(buffer, offset + STREAM_RESPONSE_QUAT4_OFFSET);
                r.flags = decodeProtocolUint16(buffer, offset + STREAM_RESPONSE_FLAGS);

                return STREAM_RESPONSE_MESSAGE_LENGTH;
            }
            return 0;
        }

        public static int decodeStreamCommand(byte[] buffer, int offset, int length, StreamCommand c)
        {
            if (length < STREAM_CMD_MESSAGE_LENGTH)
            {
                return 0;
            }
            if ((buffer[offset + 0] == '!') && (buffer[offset + 1] == MSGID_STREAM_CMD))
            {
                if (!verifyChecksum(buffer, offset, STREAM_CMD_CHECKSUM_INDEX))
                {
                    return 0;
                }

                c.stream_type = buffer[offset + STREAM_CMD_STREAM_TYPE_INDEX];
                return STREAM_CMD_MESSAGE_LENGTH;
            }
            return 0;
        }

        public static int decodeYPRUpdate(byte[] buffer, int offset, int length, YPRUpdate u)
        {
            if (length < YPR_UPDATE_MESSAGE_LENGTH)
            {
                return 0;
            }
            if ((buffer[offset + 0] == '!') && (buffer[offset + 1] == 'y'))
            {
                if (!verifyChecksum(buffer, offset, YPR_UPDATE_CHECKSUM_INDEX))
                {
                    return 0;
                }

                u.yaw = decodeProtocolFloat(buffer, offset + YPR_UPDATE_YAW_VALUE_INDEX);
                u.pitch = decodeProtocolFloat(buffer, offset + YPR_UPDATE_PITCH_VALUE_INDEX);
                u.roll = decodeProtocolFloat(buffer, offset + YPR_UPDATE_ROLL_VALUE_INDEX);
                u.compass_heading = decodeProtocolFloat(buffer, offset + YPR_UPDATE_COMPASS_VALUE_INDEX);
                return YPR_UPDATE_MESSAGE_LENGTH;
            }
            return 0;
        }

        public static int decodeQuaternionUpdate(byte[] buffer, int offset, int length,
                QuaternionUpdate u)
        {
            if (length < QUATERNION_UPDATE_MESSAGE_LENGTH)
            {
                return 0;
            }
            if ((buffer[offset + 0] == PACKET_START_CHAR) && (buffer[offset + 1] == MSGID_QUATERNION_UPDATE))
            {
                if (!verifyChecksum(buffer, offset, QUATERNION_UPDATE_CHECKSUM_INDEX))
                {
                    return 0;
                }

                u.q1 = decodeProtocolUint16(buffer, offset + QUATERNION_UPDATE_QUAT1_VALUE_INDEX);
                u.q2 = decodeProtocolUint16(buffer, offset + QUATERNION_UPDATE_QUAT2_VALUE_INDEX);
                u.q3 = decodeProtocolUint16(buffer, offset + QUATERNION_UPDATE_QUAT3_VALUE_INDEX);
                u.q4 = decodeProtocolUint16(buffer, offset + QUATERNION_UPDATE_QUAT4_VALUE_INDEX);
                u.accel_x = decodeProtocolUint16(buffer, offset + QUATERNION_UPDATE_ACCEL_X_VALUE_INDEX);
                u.accel_y = decodeProtocolUint16(buffer, offset + QUATERNION_UPDATE_ACCEL_Y_VALUE_INDEX);
                u.accel_z = decodeProtocolUint16(buffer, offset + QUATERNION_UPDATE_ACCEL_Z_VALUE_INDEX);
                u.mag_x = decodeProtocolUint16(buffer, offset + QUATERNION_UPDATE_MAG_X_VALUE_INDEX);
                u.mag_y = decodeProtocolUint16(buffer, offset + QUATERNION_UPDATE_MAG_Y_VALUE_INDEX);
                u.mag_z = decodeProtocolUint16(buffer, offset + QUATERNION_UPDATE_MAG_Z_VALUE_INDEX);
                u.temp_c = decodeProtocolFloat(buffer, offset + QUATERNION_UPDATE_TEMP_VALUE_INDEX);
                return QUATERNION_UPDATE_MESSAGE_LENGTH;
            }
            return 0;
        }

        public static int decodeGyroUpdate(byte[] buffer, int offset, int length,
                GyroUpdate u)
        {
            if (length < GYRO_UPDATE_MESSAGE_LENGTH)
            {
                return 0;
            }
            if ((buffer[offset + 0] == PACKET_START_CHAR) && (buffer[offset + 1] == MSGID_GYRO_UPDATE))
            {
                if (!verifyChecksum(buffer, offset, GYRO_UPDATE_CHECKSUM_INDEX))
                {
                    return 0;
                }

                u.gyro_x = decodeProtocolUint16(buffer, offset + GYRO_UPDATE_GYRO_X_VALUE_INDEX);
                u.gyro_y = decodeProtocolUint16(buffer, offset + GYRO_UPDATE_GYRO_Y_VALUE_INDEX);
                u.gyro_z = decodeProtocolUint16(buffer, offset + GYRO_UPDATE_GYRO_Z_VALUE_INDEX);
                u.accel_x = decodeProtocolUint16(buffer, offset + GYRO_UPDATE_ACCEL_X_VALUE_INDEX);
                u.accel_y = decodeProtocolUint16(buffer, offset + GYRO_UPDATE_ACCEL_Y_VALUE_INDEX);
                u.accel_z = decodeProtocolUint16(buffer, offset + GYRO_UPDATE_ACCEL_Z_VALUE_INDEX);
                u.mag_x = decodeProtocolUint16(buffer, offset + GYRO_UPDATE_MAG_X_VALUE_INDEX);
                u.mag_y = decodeProtocolUint16(buffer, offset + GYRO_UPDATE_MAG_Y_VALUE_INDEX);
                u.mag_z = decodeProtocolUint16(buffer, offset + GYRO_UPDATE_MAG_Z_VALUE_INDEX);
                u.temp_c = decodeProtocolUnsignedHundredthsFloat(buffer, offset + GYRO_UPDATE_TEMP_VALUE_INDEX);
                return GYRO_UPDATE_MESSAGE_LENGTH;
            }
            return 0;
        }

        public static void encodeTermination(byte[] buffer, int total_length, int content_length)
        {
            if ((total_length >= (CHECKSUM_LENGTH + TERMINATOR_LENGTH)) && (total_length >= content_length + (CHECKSUM_LENGTH + TERMINATOR_LENGTH)))
            {
                // Checksum 
                byte checksum = 0;
                for (int i = 0; i < content_length; i++)
                {
                    checksum += buffer[i];
                }
                // convert checksum to two ascii bytes

                byteToHex(checksum, buffer, content_length);
                // Message Terminator
                buffer[content_length + CHECKSUM_LENGTH + 0] = (byte)'\r';
                buffer[content_length + CHECKSUM_LENGTH + 1] = (byte)'\n';
            }
        }

        protected static readonly byte[] hexArray
                = new byte[]{(byte) '0', (byte) '1', (byte) '2', (byte) '3',
                (byte) '4', (byte) '5', (byte) '6', (byte) '7',
                (byte) '8', (byte) '9', (byte) 'A', (byte) 'B',
                (byte) 'C', (byte) 'D', (byte) 'E', (byte) 'F'};

        public static void byteToHex(byte thebyte, byte[] dest, int offset)
        {
            int v = thebyte & 0xFF;
            dest[offset + 0] = hexArray[v >> 4];
            dest[offset + 1] = hexArray[v & 0x0F];
        }

        public static short decodeProtocolUint16(byte[] uint16_string, int offset)
        {
            short decoded_uint16 = 0;
            int shift_left = 12;
            for (int i = offset + 0; i < offset + 4; i++)
            {
                byte digit = (byte)(uint16_string[i] <= '9' ? uint16_string[i] - '0' : ((uint16_string[i] - 'A') + 10));
                decoded_uint16 += (short)(((short)digit) << shift_left);
                shift_left -= 4;
            }
            return decoded_uint16;
        }

        /* 0 to 655.35 */
        public static float decodeProtocolUnsignedHundredthsFloat(byte[] uint8_unsigned_hundredths_float, int offset)
        {
            float unsigned_float = (float)decodeProtocolUint16(uint8_unsigned_hundredths_float, offset);
            unsigned_float /= 100;
            return unsigned_float;
        }

        public static bool verifyChecksum(byte[] buffer, int offset, int content_length)
        {
            // Calculate Checksum
            byte checksum = 0;
            for (int i = 0; i < content_length; i++)
            {
                checksum += buffer[offset + i];
            }

            // Decode Checksum
            byte decoded_checksum = decodeUint8(buffer, offset + content_length);

            return (checksum == decoded_checksum);
        }

        public static byte decodeUint8(byte[] checksum, int offset)
        {
            byte first_digit = (byte)(checksum[0 + offset] <= '9' ? checksum[0 + offset] - '0' : ((checksum[0 + offset] - 'A') + 10));
            byte second_digit = (byte)(checksum[1 + offset] <= '9' ? checksum[1 + offset] - '0' : ((checksum[1 + offset] - 'A') + 10));
            byte decoded_checksum = (byte)((first_digit * 16) + second_digit);
            return decoded_checksum;
        }

        public static float decodeProtocolFloat(byte[] buffer, int offset)
        {
            string float_string = Encoding.UTF8.GetString(buffer, offset, PROTOCOL_FLOAT_LENGTH);
            float toRead;
            if (float.TryParse(float_string, out toRead))
            {
                return toRead;
            }
            return 0.0f;
        }
    }
}
