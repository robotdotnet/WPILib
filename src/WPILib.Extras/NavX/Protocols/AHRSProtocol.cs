﻿namespace WPILib.Extras.NavX.Protocols
{
    public class AHRSProtocol : IMUProtocol
    {
        /* NAVX_CAL_STATUS */

        public const byte NAVX_CAL_STATUS_IMU_CAL_STATE_MASK = 0x03;
        public const byte NAVX_CAL_STATUS_IMU_CAL_INPROGRESS = 0x00;
        public const byte NAVX_CAL_STATUS_IMU_CAL_ACCUMULATE = 0x01;
        public const byte NAVX_CAL_STATUS_IMU_CAL_COMPLETE = 0x02;

        public const byte NAVX_CAL_STATUS_MAG_CAL_COMPLETE = 0x04;
        public const byte NAVX_CAL_STATUS_BARO_CAL_COMPLETE = 0x08;

        /* NAVX_SELFTEST_STATUS */

        public const byte NAVX_SELFTEST_STATUS_COMPLETE = (byte)0x80;

        public const byte NAVX_SELFTEST_RESULT_GYRO_PASSED = 0x01;
        public const byte NAVX_SELFTEST_RESULT_ACCEL_PASSED = 0x02;
        public const byte NAVX_SELFTEST_RESULT_MAG_PASSED = 0x04;
        public const byte NAVX_SELFTEST_RESULT_BARO_PASSED = 0x08;

        /* NAVX_OP_STATUS */

        public const byte NAVX_OP_STATUS_INITIALIZING = 0x00;
        public const byte NAVX_OP_STATUS_SELFTEST_IN_PROGRESS = 0x01;
        public const byte NAVX_OP_STATUS_ERROR = 0x02;
        public const byte NAVX_OP_STATUS_IMU_AUTOCAL_IN_PROGRESS = 0x03;
        public const byte NAVX_OP_STATUS_NORMAL = 0x04;

        /* NAVX_SENSOR_STATUS */
        public const byte NAVX_SENSOR_STATUS_MOVING = 0x01;
        public const byte NAVX_SENSOR_STATUS_YAW_STABLE = 0x02;
        public const byte NAVX_SENSOR_STATUS_MAG_DISTURBANCE = 0x04;
        public const byte NAVX_SENSOR_STATUS_ALTITUDE_VALID = 0x08;
        public const byte NAVX_SENSOR_STATUS_SEALEVEL_PRESS_SET = 0x10;
        public const byte NAVX_SENSOR_STATUS_FUSED_HEADING_VALID = 0x20;

        /* NAVX_REG_CAPABILITY_FLAGS (Aligned w/NAV6 Flags, see IMUProtocol.h) */

        public const short NAVX_CAPABILITY_FLAG_OMNIMOUNT = 0x0004;
        public const short NAVX_CAPABILITY_FLAG_OMNIMOUNT_CONFIG_MASK = 0x0038;
        public const short NAVX_CAPABILITY_FLAG_VEL_AND_DISP = 0x0040;
        public const short NAVX_CAPABILITY_FLAG_YAW_RESET = 0x0080;

        /* NAVX_OMNIMOUNT_CONFIG */

        public const byte OMNIMOUNT_DEFAULT = 0; /* Same as Y_Z_UP */
        public const byte OMNIMOUNT_YAW_X_UP = 1;
        public const byte OMNIMOUNT_YAW_X_DOWN = 2;
        public const byte OMNIMOUNT_YAW_Y_UP = 3;
        public const byte OMNIMOUNT_YAW_Y_DOWN = 4;
        public const byte OMNIMOUNT_YAW_Z_UP = 5;
        public const byte OMNIMOUNT_YAW_Z_DOWN = 6;

        /* NAVX_INTEGRATION_CTL */

        public const byte NAVX_INTEGRATION_CTL_RESET_VEL_X = 0x01;
        public const byte NAVX_INTEGRATION_CTL_RESET_VEL_Y = 0x02;
        public const byte NAVX_INTEGRATION_CTL_RESET_VEL_Z = 0x04;
        public const byte NAVX_INTEGRATION_CTL_RESET_DISP_X = 0x08;
        public const byte NAVX_INTEGRATION_CTL_RESET_DISP_Y = 0x10;
        public const byte NAVX_INTEGRATION_CTL_RESET_DISP_Z = 0x20;
        public const byte NAVX_INTEGRATION_CTL_RESET_YAW = (byte)0x80;

        public class AHRS_TUNING_VAR_ID
        {
            public const byte UNSPECIFIED = 0;
            public const byte MOTION_THRESHOLD = 1; /* In G */
            public const byte YAW_STABLE_THRESHOLD = 2; /* In Degrees */
            public const byte MAG_DISTURBANCE_THRESHOLD = 3; /* Ratio */
            public const byte SEA_LEVEL_PRESSURE = 4; /* Millibars */
        };

        public class AHRS_DATA_TYPE
        {
            public const byte TUNING_VARIABLE = 0;
            public const byte MAG_CALIBRATION = 1;
            public const byte BOARD_IDENTITY = 2;
        };

        public class AHRS_DATA_ACTION
        {
            public const byte DATA_GET = 0;
            public const byte DATA_SET = 1;
        };

        public const char BINARY_PACKET_INDICATOR_CHAR = '#';

        /* AHRS Protocol encodes certain data in binary format, unlike the IMU  */
        /* protocol, which encodes all data in ASCII characters.  Thus, the     */
        /* packet start and message termination sequences may occur within the  */
        /* message content itself.  To support the binary format, the binary    */
        /* message has this format:                                             */
        /*                                                                      */
        /* [start][binary indicator][len][msgid]<MESSAGE>[checksum][terminator] */
        /*                                                                      */
        /* (The binary indicator and len are not present in the ASCII protocol) */
        /*                                                                      */
        /* The [len] does not include the length of the start and binary        */
        /* indicator characters, but does include all other message items,      */
        /* including the checksum and terminator sequence.                      */

        public const byte MSGID_AHRS_UPDATE = (byte)'a';
        const int AHRS_UPDATE_YAW_VALUE_INDEX = 4;  /* Degrees.  Signed Hundredths */
        const int AHRS_UPDATE_PITCH_VALUE_INDEX = 6;  /* Degrees.  Signed Hundredeths */
        const int AHRS_UPDATE_ROLL_VALUE_INDEX = 8;  /* Degrees.  Signed Hundredths */
        const int AHRS_UPDATE_HEADING_VALUE_INDEX = 10;  /* Degrees.  Unsigned Hundredths */
        const int AHRS_UPDATE_ALTITUDE_VALUE_INDEX = 12; /* Meters.   Signed 16:16 */
        const int AHRS_UPDATE_FUSED_HEADING_VALUE_INDEX = 16; /* Degrees.  Unsigned Hundredths */
        const int AHRS_UPDATE_LINEAR_ACCEL_X_VALUE_INDEX = 18; /* Inst. G.  Signed Thousandths */
        const int AHRS_UPDATE_LINEAR_ACCEL_Y_VALUE_INDEX = 20; /* Inst. G.  Signed Thousandths */
        const int AHRS_UPDATE_LINEAR_ACCEL_Z_VALUE_INDEX = 22; /* Inst. G.  Signed Thousandths */
        const int AHRS_UPDATE_CAL_MAG_X_VALUE_INDEX = 24; /* Int16 (Device Units) */
        const int AHRS_UPDATE_CAL_MAG_Y_VALUE_INDEX = 26; /* Int16 (Device Units) */
        const int AHRS_UPDATE_CAL_MAG_Z_VALUE_INDEX = 28; /* Int16 (Device Units) */
        const int AHRS_UPDATE_CAL_MAG_NORM_RATIO_VALUE_INDEX = 30; /* Ratio.  Unsigned Hundredths */
        const int AHRS_UPDATE_CAL_MAG_SCALAR_VALUE_INDEX = 32; /* Coefficient. Signed 16:16 */
        const int AHRS_UPDATE_MPU_TEMP_VAUE_INDEX = 36; /* Centigrade.  Signed Hundredths */
        const int AHRS_UPDATE_RAW_MAG_X_VALUE_INDEX = 38; /* INT16 (Device Units */
        const int AHRS_UPDATE_RAW_MAG_Y_VALUE_INDEX = 40; /* INT16 (Device Units */
        const int AHRS_UPDATE_RAW_MAG_Z_VALUE_INDEX = 42; /* INT16 (Device Units */
        const int AHRS_UPDATE_QUAT_W_VALUE_INDEX = 44; /* INT16 */
        const int AHRS_UPDATE_QUAT_X_VALUE_INDEX = 46; /* INT16 */
        const int AHRS_UPDATE_QUAT_Y_VALUE_INDEX = 48; /* INT16 */
        const int AHRS_UPDATE_QUAT_Z_VALUE_INDEX = 50; /* INT16 */
        const int AHRS_UPDATE_BARO_PRESSURE_VALUE_INDEX = 52; /* millibar.  Signed 16:16 */
        const int AHRS_UPDATE_BARO_TEMP_VAUE_INDEX = 56; /* Centigrade.  Signed  Hundredths */
        const int AHRS_UPDATE_OPSTATUS_VALUE_INDEX = 58; /* NAVX_OP_STATUS_XXX */
        const int AHRS_UPDATE_SENSOR_STATUS_VALUE_INDEX = 59; /* NAVX_SENSOR_STATUS_XXX */
        const int AHRS_UPDATE_CAL_STATUS_VALUE_INDEX = 60; /* NAVX_CAL_STATUS_XXX */
        const int AHRS_UPDATE_SELFTEST_STATUS_VALUE_INDEX = 61; /* NAVX_SELFTEST_STATUS_XXX */
        const int AHRS_UPDATE_MESSAGE_CHECKSUM_INDEX = 62;
        const int AHRS_UPDATE_MESSAGE_TERMINATOR_INDEX = 64;
        const int AHRS_UPDATE_MESSAGE_LENGTH = 66;

        // AHRSAndPositioning Update Packet (similar to AHRS, but removes magnetometer and adds velocity/displacement) */

        public const byte MSGID_AHRSPOS_UPDATE = (byte)'p';
        const int AHRSPOS_UPDATE_YAW_VALUE_INDEX = 4; /* Degrees.  Signed Hundredths */
        const int AHRSPOS_UPDATE_PITCH_VALUE_INDEX = 6; /* Degrees.  Signed Hundredeths */
        const int AHRSPOS_UPDATE_ROLL_VALUE_INDEX = 8; /* Degrees.  Signed Hundredths */
        const int AHRSPOS_UPDATE_HEADING_VALUE_INDEX = 10; /* Degrees.  Unsigned Hundredths */
        const int AHRSPOS_UPDATE_ALTITUDE_VALUE_INDEX = 12; /* Meters.   Signed 16:16 */
        const int AHRSPOS_UPDATE_FUSED_HEADING_VALUE_INDEX = 16; /* Degrees.  Unsigned Hundredths */
        const int AHRSPOS_UPDATE_LINEAR_ACCEL_X_VALUE_INDEX = 18; /* Inst. G.  Signed Thousandths */
        const int AHRSPOS_UPDATE_LINEAR_ACCEL_Y_VALUE_INDEX = 20; /* Inst. G.  Signed Thousandths */
        const int AHRSPOS_UPDATE_LINEAR_ACCEL_Z_VALUE_INDEX = 22; /* Inst. G.  Signed Thousandths */
        const int AHRSPOS_UPDATE_VEL_X_VALUE_INDEX = 24; /* Signed 16:16, in meters/sec */
        const int AHRSPOS_UPDATE_VEL_Y_VALUE_INDEX = 28; /* Signed 16:16, in meters/sec */
        const int AHRSPOS_UPDATE_VEL_Z_VALUE_INDEX = 32; /* Signed 16:16, in meters/sec */
        const int AHRSPOS_UPDATE_DISP_X_VALUE_INDEX = 36; /* Signed 16:16, in meters */
        const int AHRSPOS_UPDATE_DISP_Y_VALUE_INDEX = 40; /* Signed 16:16, in meters */
        const int AHRSPOS_UPDATE_DISP_Z_VALUE_INDEX = 44; /* Signed 16:16, in meters */
        const int AHRSPOS_UPDATE_QUAT_W_VALUE_INDEX = 48; /* INT16 */
        const int AHRSPOS_UPDATE_QUAT_X_VALUE_INDEX = 50; /* INT16 */
        const int AHRSPOS_UPDATE_QUAT_Y_VALUE_INDEX = 52; /* INT16 */
        const int AHRSPOS_UPDATE_QUAT_Z_VALUE_INDEX = 54; /* INT16 */
        const int AHRSPOS_UPDATE_MPU_TEMP_VAUE_INDEX = 56; /* Centigrade.  Signed Hundredths */
        const int AHRSPOS_UPDATE_OPSTATUS_VALUE_INDEX = 58; /* NAVX_OP_STATUS_XXX */
        const int AHRSPOS_UPDATE_SENSOR_STATUS_VALUE_INDEX = 59; /* NAVX_SENSOR_STATUS_XXX */
        const int AHRSPOS_UPDATE_CAL_STATUS_VALUE_INDEX = 60; /* NAVX_CAL_STATUS_XXX */
        const int AHRSPOS_UPDATE_SELFTEST_STATUS_VALUE_INDEX = 61; /* NAVX_SELFTEST_STATUS_XXX */
        const int AHRSPOS_UPDATE_MESSAGE_CHECKSUM_INDEX = 62;
        const int AHRSPOS_UPDATE_MESSAGE_TERMINATOR_INDEX = 64;
        const int AHRSPOS_UPDATE_MESSAGE_LENGTH = 66;

        // Data Get Request:  Tuning Variable, Mag Cal, Board Identity (Response message depends upon request type)
        public const byte MSGID_DATA_REQUEST = (byte)'D';
        const int DATA_REQUEST_DATATYPE_VALUE_INDEX = 4;
        const int DATA_REQUEST_VARIABLEID_VALUE_INDEX = 5;
        const int DATA_REQUEST_CHECKSUM_INDEX = 6;
        const int DATA_REQUEST_TERMINATOR_INDEX = 8;
        const int DATA_REQUEST_MESSAGE_LENGTH = 10;

        // Data Set Response Packet
        public const byte MSGID_DATA_SET_RESPONSE = (byte)'v';
        const int DATA_SET_RESPONSE_DATATYPE_VALUE_INDEX = 4;
        const int DATA_SET_RESPONSE_VARID_VALUE_INDEX = 5;
        const int DATA_SET_RESPONSE_STATUS_VALUE_INDEX = 6;
        const int DATA_SET_RESPONSE_MESSAGE_CHECKSUM_INDEX = 7;
        const int DATA_SET_RESPONSE_MESSAGE_TERMINATOR_INDEX = 9;
        const int DATA_SET_RESPONSE_MESSAGE_LENGTH = 11;

        /* Integration Control Command Packet */
        public const byte MSGID_INTEGRATION_CONTROL_CMD = (byte)'I';
        const int INTEGRATION_CONTROL_CMD_ACTION_INDEX = 4;
        const int INTEGRATION_CONTROL_CMD_PARAMETER_INDEX = 5;
        const int INTEGRATION_CONTROL_CMD_MESSAGE_CHECKSUM_INDEX = 9;
        const int INTEGRATION_CONTROL_CMD_MESSAGE_TERMINATOR_INDEX = 11;
        const int INTEGRATION_CONTROL_CMD_MESSAGE_LENGTH = 13;

        /* Integration Control Response Packet */
        public const byte MSGID_INTEGRATION_CONTROL_RESP = (byte)'j';
        const int INTEGRATION_CONTROL_RESP_ACTION_INDEX = 4;
        const int INTEGRATION_CONTROL_RESP_PARAMETER_INDEX = 5;
        const int INTEGRATION_CONTROL_RESP_MESSAGE_CHECKSUM_INDEX = 9;
        const int INTEGRATION_CONTROL_RESP_MESSAGE_TERMINATOR_INDEX = 11;
        const int INTEGRATION_CONTROL_RESP_MESSAGE_LENGTH = 13;

        // Magnetometer Calibration Packet - e.g., !m[x_bias][y_bias][z_bias][m1,1 ... m3,3][cr][lf]
        public const byte MSGID_MAG_CAL_CMD = (byte)'M';
        const int MAG_CAL_DATA_ACTION_VALUE_INDEX = 4;
        const int MAG_X_BIAS_VALUE_INDEX = 5; /* signed short */
        const int MAG_Y_BIAS_VALUE_INDEX = 7;
        const int MAG_Z_BIAS_VALUE_INDEX = 9;
        const int MAG_XFORM_1_1_VALUE_INDEX = 11; /* signed 16:16 */
        const int MAG_XFORM_1_2_VALUE_INDEX = 15;
        const int MAG_XFORM_1_3_VALUE_INDEX = 19;
        const int MAG_XFORM_2_1_VALUE_INDEX = 23;
        const int MAG_XFORM_2_2_VALUE_INDEX = 25;
        const int MAG_XFORM_2_3_VALUE_INDEX = 31;
        const int MAG_XFORM_3_1_VALUE_INDEX = 35;
        const int MAG_XFORM_3_2_VALUE_INDEX = 39;
        const int MAG_XFORM_3_3_VALUE_INDEX = 43;
        const int MAG_CAL_EARTH_MAG_FIELD_NORM_VALUE_INDEX = 47;
        const int MAG_CAL_CMD_MESSAGE_CHECKSUM_INDEX = 51;
        const int MAG_CAL_CMD_MESSAGE_TERMINATOR_INDEX = 53;
        const int MAG_CAL_CMD_MESSAGE_LENGTH = 55;

        // Tuning Variable Packet
        public const byte MSGID_FUSION_TUNING_CMD = (byte)'T';
        const int FUSION_TUNING_DATA_ACTION_VALUE_INDEX = 4;
        const int FUSION_TUNING_CMD_VAR_ID_VALUE_INDEX = 5;
        const int FUSION_TUNING_CMD_VAR_VALUE_INDEX = 6;
        const int FUSION_TUNING_CMD_MESSAGE_CHECKSUM_INDEX = 10;
        const int FUSION_TUNING_CMD_MESSAGE_TERMINATOR_INDEX = 12;
        const int FUSION_TUNING_CMD_MESSAGE_LENGTH = 14;

        // Board Identity Response Packet- e.g., !c[type][hw_rev][fw_major][fw_minor][unique_id[12]]
        public const byte MSGID_BOARD_IDENTITY_RESPONSE = (byte)'i';
        const int BOARD_IDENTITY_BOARDTYPE_VALUE_INDEX = 4;
        const int BOARD_IDENTITY_HWREV_VALUE_INDEX = 5;
        const int BOARD_IDENTITY_FW_VER_MAJOR = 6;
        const int BOARD_IDENTITY_FW_VER_MINOR = 7;
        const int BOARD_IDENTITY_FW_VER_REVISION_VALUE_INDEX = 8;
        const int BOARD_IDENTITY_UNIQUE_ID_0 = 10;
        const int BOARD_IDENTITY_UNIQUE_ID_1 = 11;
        const int BOARD_IDENTITY_UNIQUE_ID_2 = 12;
        const int BOARD_IDENTITY_UNIQUE_ID_3 = 13;
        const int BOARD_IDENTITY_UNIQUE_ID_4 = 14;
        const int BOARD_IDENTITY_UNIQUE_ID_5 = 15;
        const int BOARD_IDENTITY_UNIQUE_ID_6 = 16;
        const int BOARD_IDENTITY_UNIQUE_ID_7 = 17;
        const int BOARD_IDENTITY_UNIQUE_ID_8 = 18;
        const int BOARD_IDENTITY_UNIQUE_ID_9 = 19;
        const int BOARD_IDENTITY_UNIQUE_ID_10 = 20;
        const int BOARD_IDENTITY_UNIQUE_ID_11 = 21;
        const int BOARD_IDENTITY_RESPONSE_CHECKSUM_INDEX = 22;
        const int BOARD_IDENTITY_RESPONSE_TERMINATOR_INDEX = 24;
        const int BOARD_IDENTITY_RESPONSE_MESSAGE_LENGTH = 26;

        public const int MAX_BINARY_MESSAGE_LENGTH = AHRSPOS_UPDATE_MESSAGE_LENGTH;

        public class AHRSUpdateBase
        {
            public float yaw;
            public float pitch;
            public float roll;
            public float compass_heading;
            public float altitude;
            public float fused_heading;
            public float linear_accel_x;
            public float linear_accel_y;
            public float linear_accel_z;
            public float mpu_temp;
            public float quat_w;
            public float quat_x;
            public float quat_y;
            public float quat_z;
            public float barometric_pressure;
            public float baro_temp;
            public byte op_status;
            public byte sensor_status;
            public byte cal_status;
            public byte selftest_status;
        }

        public class AHRSUpdate : AHRSUpdateBase
        {
            public short cal_mag_x;
            public short cal_mag_y;
            public short cal_mag_z;
            public float mag_field_norm_ratio;
            public float mag_field_norm_scalar;
            public short raw_mag_x;
            public short raw_mag_y;
            public short raw_mag_z;
        }

        public class AHRSPosUpdate : AHRSUpdateBase
        {
            public float vel_x;
            public float vel_y;
            public float vel_z;
            public float disp_x;
            public float disp_y;
            public float disp_z;
        }

        public class AHRSPosTSUpdate : AHRSPosUpdate
        {
            public long timestamp;
        }

        public class DataSetResponse
        {
            public byte data_type;
            public byte var_id;       /* If type = TUNING_VARIABLE */
            public byte status;
        };

        public class IntegrationControl
        {
            public byte action;
            public int parameter;
        };

        public class MagCalData
        {
            internal byte action;
            public short[] mag_bias;                /* 3 Values */
            public float[][] mag_xform;             /* 3 x 3 Values */
        public float earth_mag_field_norm;
            public MagCalData()
            {
                mag_bias = new short[3];
                mag_xform = new float[3][];
                for (int i = 0; i < mag_xform.Length; i++)
                {
                    mag_xform[i] = new float[3];
                }
            }
        };

        public class TuningVar
        {
            public byte action;
            public byte var_id;       /* If type = TUNING_VARIABLE */
            internal float value;
        };

        public class BoardID
        {
            public byte type;
            public byte hw_rev;
            public byte fw_ver_major;
            public byte fw_ver_minor;
            public short fw_revision;
            public byte[] unique_id;
            public BoardID()
            {
                unique_id = new byte[12];
            }
        };

        public static int decodeAHRSUpdate(byte[] buffer,
                                            int offset,
                                            int length,
                                            AHRSUpdate u)
        {
            if (length < AHRS_UPDATE_MESSAGE_LENGTH)
            {
                return 0;
            }
            if ((buffer[offset + 0] == PACKET_START_CHAR) &&
                 (buffer[offset + 1] == BINARY_PACKET_INDICATOR_CHAR) &&
                 (buffer[offset + 2] == AHRS_UPDATE_MESSAGE_LENGTH - 2) &&
                 (buffer[offset + 3] == MSGID_AHRS_UPDATE))
            {

                if (!verifyChecksum(buffer, offset, AHRS_UPDATE_MESSAGE_CHECKSUM_INDEX))
                {
                    return 0;
                }
                u.yaw = decodeProtocolSignedHundredthsFloat(buffer, offset + AHRS_UPDATE_YAW_VALUE_INDEX);
                u.pitch = decodeProtocolSignedHundredthsFloat(buffer, offset + AHRS_UPDATE_ROLL_VALUE_INDEX); /* FIXME */
                u.roll = decodeProtocolSignedHundredthsFloat(buffer, offset + AHRS_UPDATE_PITCH_VALUE_INDEX); /* FIXME */
                u.compass_heading = decodeProtocolUnsignedHundredthsFloat(buffer, offset + AHRS_UPDATE_HEADING_VALUE_INDEX);
                u.altitude = decodeProtocol1616Float(buffer, offset + AHRS_UPDATE_ALTITUDE_VALUE_INDEX);
                u.fused_heading = decodeProtocolUnsignedHundredthsFloat(buffer, offset + AHRS_UPDATE_FUSED_HEADING_VALUE_INDEX);
                u.linear_accel_x = decodeProtocolSignedThousandthsFloat(buffer, offset + AHRS_UPDATE_LINEAR_ACCEL_X_VALUE_INDEX);
                u.linear_accel_y = decodeProtocolSignedThousandthsFloat(buffer, offset + AHRS_UPDATE_LINEAR_ACCEL_Y_VALUE_INDEX);
                u.linear_accel_z = decodeProtocolSignedThousandthsFloat(buffer, offset + AHRS_UPDATE_LINEAR_ACCEL_Z_VALUE_INDEX);
                u.cal_mag_x = decodeBinaryInt16(buffer, offset + AHRS_UPDATE_CAL_MAG_X_VALUE_INDEX);
                u.cal_mag_y = decodeBinaryInt16(buffer, offset + AHRS_UPDATE_CAL_MAG_Y_VALUE_INDEX);
                u.cal_mag_z = decodeBinaryInt16(buffer, offset + AHRS_UPDATE_CAL_MAG_Z_VALUE_INDEX);
                u.mag_field_norm_ratio = decodeProtocolUnsignedHundredthsFloat(buffer, offset + AHRS_UPDATE_CAL_MAG_NORM_RATIO_VALUE_INDEX);
                u.mag_field_norm_scalar = decodeProtocol1616Float(buffer, offset + AHRS_UPDATE_CAL_MAG_SCALAR_VALUE_INDEX);
                u.mpu_temp = decodeProtocolSignedHundredthsFloat(buffer, offset + AHRS_UPDATE_MPU_TEMP_VAUE_INDEX);
                u.raw_mag_x = decodeBinaryInt16(buffer, offset + AHRS_UPDATE_RAW_MAG_X_VALUE_INDEX);
                u.raw_mag_y = decodeBinaryInt16(buffer, offset + AHRS_UPDATE_RAW_MAG_Y_VALUE_INDEX);
                u.raw_mag_z = decodeBinaryInt16(buffer, offset + AHRS_UPDATE_RAW_MAG_Z_VALUE_INDEX);
                u.quat_w = decodeBinaryInt16(buffer, offset + AHRS_UPDATE_QUAT_W_VALUE_INDEX) / 16384.0f;
                u.quat_x = decodeBinaryInt16(buffer, offset + AHRS_UPDATE_QUAT_X_VALUE_INDEX) / 16384.0f;
                u.quat_y = decodeBinaryInt16(buffer, offset + AHRS_UPDATE_QUAT_Y_VALUE_INDEX) / 16384.0f;
                u.quat_z = decodeBinaryInt16(buffer, offset + AHRS_UPDATE_QUAT_Z_VALUE_INDEX) / 16384.0f;
                u.barometric_pressure = decodeProtocol1616Float(buffer, offset + AHRS_UPDATE_BARO_PRESSURE_VALUE_INDEX);
                u.baro_temp = decodeProtocolSignedHundredthsFloat(buffer, offset + AHRS_UPDATE_BARO_TEMP_VAUE_INDEX);
                u.op_status = buffer[AHRS_UPDATE_OPSTATUS_VALUE_INDEX];
                u.sensor_status = buffer[AHRS_UPDATE_SENSOR_STATUS_VALUE_INDEX];
                u.cal_status = buffer[AHRS_UPDATE_CAL_STATUS_VALUE_INDEX];
                u.selftest_status = buffer[AHRS_UPDATE_SELFTEST_STATUS_VALUE_INDEX];
                return AHRS_UPDATE_MESSAGE_LENGTH;
            }
            return 0;
        }

        public static int decodeAHRSPosUpdate(byte[] buffer,
                int offset,
                int length,
                AHRSPosUpdate u)
        {
            if (length < AHRSPOS_UPDATE_MESSAGE_LENGTH)
            {
                return 0;
            }
            if ((buffer[offset + 0] == PACKET_START_CHAR) &&
                    (buffer[offset + 1] == BINARY_PACKET_INDICATOR_CHAR) &&
                    (buffer[offset + 2] == AHRSPOS_UPDATE_MESSAGE_LENGTH - 2) &&
                    (buffer[offset + 3] == MSGID_AHRSPOS_UPDATE))
            {

                if (!verifyChecksum(buffer, offset, AHRSPOS_UPDATE_MESSAGE_CHECKSUM_INDEX))
                {
                    return 0;
                }
                u.yaw = decodeProtocolSignedHundredthsFloat(buffer, offset + AHRSPOS_UPDATE_YAW_VALUE_INDEX);
                u.pitch = decodeProtocolSignedHundredthsFloat(buffer, offset + AHRSPOS_UPDATE_ROLL_VALUE_INDEX); /* FIXME */
                u.roll = decodeProtocolSignedHundredthsFloat(buffer, offset + AHRSPOS_UPDATE_PITCH_VALUE_INDEX); /* FIXME */
                u.compass_heading = decodeProtocolUnsignedHundredthsFloat(buffer, offset + AHRSPOS_UPDATE_HEADING_VALUE_INDEX);
                u.altitude = decodeProtocol1616Float(buffer, offset + AHRSPOS_UPDATE_ALTITUDE_VALUE_INDEX);
                u.fused_heading = decodeProtocolUnsignedHundredthsFloat(buffer, offset + AHRSPOS_UPDATE_FUSED_HEADING_VALUE_INDEX);
                u.linear_accel_x = decodeProtocolSignedThousandthsFloat(buffer, offset + AHRSPOS_UPDATE_LINEAR_ACCEL_X_VALUE_INDEX);
                u.linear_accel_y = decodeProtocolSignedThousandthsFloat(buffer, offset + AHRSPOS_UPDATE_LINEAR_ACCEL_Y_VALUE_INDEX);
                u.linear_accel_z = decodeProtocolSignedThousandthsFloat(buffer, offset + AHRSPOS_UPDATE_LINEAR_ACCEL_Z_VALUE_INDEX);
                u.vel_x = decodeProtocol1616Float(buffer, offset + AHRSPOS_UPDATE_VEL_X_VALUE_INDEX);
                u.vel_y = decodeProtocol1616Float(buffer, offset + AHRSPOS_UPDATE_VEL_Y_VALUE_INDEX);
                u.vel_z = decodeProtocol1616Float(buffer, offset + AHRSPOS_UPDATE_VEL_Z_VALUE_INDEX);
                u.disp_x = decodeProtocol1616Float(buffer, offset + AHRSPOS_UPDATE_DISP_X_VALUE_INDEX);
                u.disp_y = decodeProtocol1616Float(buffer, offset + AHRSPOS_UPDATE_DISP_Y_VALUE_INDEX);
                u.disp_z = decodeProtocol1616Float(buffer, offset + AHRSPOS_UPDATE_DISP_Z_VALUE_INDEX);
                u.mpu_temp = decodeProtocolSignedHundredthsFloat(buffer, offset + AHRSPOS_UPDATE_MPU_TEMP_VAUE_INDEX);
                u.quat_w = decodeBinaryInt16(buffer, offset + AHRSPOS_UPDATE_QUAT_W_VALUE_INDEX) / 16384.0f;
                u.quat_x = decodeBinaryInt16(buffer, offset + AHRSPOS_UPDATE_QUAT_X_VALUE_INDEX) / 16384.0f;
                u.quat_y = decodeBinaryInt16(buffer, offset + AHRSPOS_UPDATE_QUAT_Y_VALUE_INDEX) / 16384.0f;
                u.quat_z = decodeBinaryInt16(buffer, offset + AHRSPOS_UPDATE_QUAT_Z_VALUE_INDEX) / 16384.0f;
                u.op_status = buffer[AHRSPOS_UPDATE_OPSTATUS_VALUE_INDEX];
                u.sensor_status = buffer[AHRSPOS_UPDATE_SENSOR_STATUS_VALUE_INDEX];
                u.cal_status = buffer[AHRSPOS_UPDATE_CAL_STATUS_VALUE_INDEX];
                u.selftest_status = buffer[AHRSPOS_UPDATE_SELFTEST_STATUS_VALUE_INDEX];
                return AHRSPOS_UPDATE_MESSAGE_LENGTH;
            }
            return 0;
        }

        /* Mag Cal, Tuning Variable, or Board ID Retrieval Request */
        public static int encodeDataGetRequest(byte[] buffer,
                                                byte type,
                                                byte var_id)
        {
            // Header
            buffer[0] = PACKET_START_CHAR;
            buffer[1] = (byte)BINARY_PACKET_INDICATOR_CHAR;
            buffer[2] = DATA_REQUEST_MESSAGE_LENGTH - 2;
            buffer[3] = MSGID_DATA_REQUEST;
            // Data
            buffer[DATA_REQUEST_DATATYPE_VALUE_INDEX] = type;
            buffer[DATA_REQUEST_VARIABLEID_VALUE_INDEX] = var_id;
            // Footer
            encodeTermination(buffer, DATA_REQUEST_MESSAGE_LENGTH, DATA_REQUEST_MESSAGE_LENGTH - 4);
            return DATA_REQUEST_MESSAGE_LENGTH;
        }

        /* Mag Cal Data Storage Request */
        public static int encodeMagCalDataSetRequest(byte[] buffer,
                                                MagCalData d)
        {
            // Header
            buffer[0] = PACKET_START_CHAR;
            buffer[1] = (byte)BINARY_PACKET_INDICATOR_CHAR;
            buffer[2] = MAG_CAL_CMD_MESSAGE_LENGTH - 2;
            buffer[3] = MSGID_MAG_CAL_CMD;

            // Data
            buffer[MAG_CAL_DATA_ACTION_VALUE_INDEX] = d.action;
            for (int i = 0; i < 3; i++)
            {
                encodeBinaryInt16(d.mag_bias[i],
                                   buffer, MAG_X_BIAS_VALUE_INDEX + (i * 2));
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    encodeProtocol1616Float(d.mag_xform[i][j],
                            buffer, MAG_XFORM_1_1_VALUE_INDEX + (i * 6) + (j * 2));
                }
            }
            encodeProtocol1616Float(d.earth_mag_field_norm, buffer, MAG_CAL_EARTH_MAG_FIELD_NORM_VALUE_INDEX);
            // Footer
            encodeTermination(buffer, MAG_CAL_CMD_MESSAGE_LENGTH, MAG_CAL_CMD_MESSAGE_LENGTH - 4);
            return MAG_CAL_CMD_MESSAGE_LENGTH;
        }

        /* Mag Cal Data Retrieval Response */
        public static int decodeMagCalDataGetResponse(byte[] buffer,
                                                        int offset,
                                                        int length,
                                                        MagCalData d)
        {
            if (length < MAG_CAL_CMD_MESSAGE_LENGTH) return 0;
            if ((buffer[0] == PACKET_START_CHAR) &&
                 (buffer[1] == BINARY_PACKET_INDICATOR_CHAR) &&
                 (buffer[2] == MAG_CAL_CMD_MESSAGE_LENGTH - 2) &&
                 (buffer[3] == MSGID_MAG_CAL_CMD))
            {
                if (!verifyChecksum(buffer, offset, MAG_CAL_CMD_MESSAGE_CHECKSUM_INDEX)) return 0;

                d.action = buffer[MAG_CAL_DATA_ACTION_VALUE_INDEX];
                for (int i = 0; i < 3; i++)
                {
                    d.mag_bias[i] = decodeBinaryInt16(buffer, MAG_X_BIAS_VALUE_INDEX + (i * 2));
                }
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        d.mag_xform[i][j] = decodeProtocol1616Float(buffer, MAG_XFORM_1_1_VALUE_INDEX + (i * 6) + (j * 2));
                    }
                }
                d.earth_mag_field_norm = decodeProtocol1616Float(buffer, MAG_CAL_EARTH_MAG_FIELD_NORM_VALUE_INDEX);
                return MAG_CAL_CMD_MESSAGE_LENGTH;
            }
            return 0;
        }

        /* Tuning Variable Storage Request */
        public static int encodeTuningVarSetRequest(byte[] buffer,
                                                     TuningVar r)
        {
            // Header
            buffer[0] = PACKET_START_CHAR;
            buffer[1] = (byte)BINARY_PACKET_INDICATOR_CHAR;
            buffer[2] = FUSION_TUNING_CMD_MESSAGE_LENGTH - 2;
            buffer[3] = MSGID_FUSION_TUNING_CMD;
            // Data
            buffer[FUSION_TUNING_DATA_ACTION_VALUE_INDEX] = r.action;
            buffer[FUSION_TUNING_CMD_VAR_ID_VALUE_INDEX] = r.var_id;
            encodeProtocol1616Float(r.value, buffer, FUSION_TUNING_CMD_VAR_VALUE_INDEX);
            // Footer
            encodeTermination(buffer, FUSION_TUNING_CMD_MESSAGE_LENGTH, FUSION_TUNING_CMD_MESSAGE_LENGTH - 4);
            return FUSION_TUNING_CMD_MESSAGE_LENGTH;
        }

        /* Tuning Variable Retrieval Response */
        public static int decodeTuningVarGetResponse(byte[] buffer,
                                                        int offset,
                                                        int length,
                                                        TuningVar r)
        {
            if (length < FUSION_TUNING_CMD_MESSAGE_LENGTH) return 0;
            if ((buffer[0] == PACKET_START_CHAR) &&
                 (buffer[1] == BINARY_PACKET_INDICATOR_CHAR) &&
                 (buffer[2] == FUSION_TUNING_CMD_MESSAGE_LENGTH - 2) &&
                 (buffer[3] == MSGID_FUSION_TUNING_CMD))
            {
                if (!verifyChecksum(buffer, offset, FUSION_TUNING_CMD_MESSAGE_CHECKSUM_INDEX)) return 0;

                // Data
                r.action = buffer[FUSION_TUNING_DATA_ACTION_VALUE_INDEX];
                r.var_id = buffer[FUSION_TUNING_CMD_VAR_ID_VALUE_INDEX];
                r.value = decodeProtocol1616Float(buffer, FUSION_TUNING_CMD_VAR_VALUE_INDEX);
                return FUSION_TUNING_CMD_MESSAGE_LENGTH;
            }
            return 0;
        }

        public static int encodeIntegrationControlCmd(byte[] buffer, IntegrationControl u)
        {
            // Header
            buffer[0] = PACKET_START_CHAR;
            buffer[1] = (byte)BINARY_PACKET_INDICATOR_CHAR;
            buffer[2] = INTEGRATION_CONTROL_CMD_MESSAGE_LENGTH - 2;
            buffer[3] = MSGID_INTEGRATION_CONTROL_CMD;
            // Data
            buffer[INTEGRATION_CONTROL_CMD_ACTION_INDEX] = u.action;
            encodeBinaryUint32(u.parameter, buffer, INTEGRATION_CONTROL_CMD_PARAMETER_INDEX);
            // Footer
            encodeTermination(buffer, INTEGRATION_CONTROL_CMD_MESSAGE_LENGTH, INTEGRATION_CONTROL_CMD_MESSAGE_LENGTH - 4);
            return INTEGRATION_CONTROL_CMD_MESSAGE_LENGTH;
        }

        public static int decodeIntegrationControlResponse(byte[] buffer, int offset, int length, IntegrationControl u)
        {
            if (length < INTEGRATION_CONTROL_RESP_MESSAGE_LENGTH) return 0;
            if ((buffer[0] == PACKET_START_CHAR) &&
                 (buffer[1] == BINARY_PACKET_INDICATOR_CHAR) &&
                 (buffer[2] == INTEGRATION_CONTROL_RESP_MESSAGE_LENGTH - 2) &&
                 (buffer[3] == MSGID_INTEGRATION_CONTROL_RESP))
            {
                if (!verifyChecksum(buffer, offset, INTEGRATION_CONTROL_RESP_MESSAGE_CHECKSUM_INDEX)) return 0;

                // Data
                u.action = buffer[INTEGRATION_CONTROL_RESP_ACTION_INDEX];
                u.parameter = decodeBinaryUint32(buffer, INTEGRATION_CONTROL_RESP_PARAMETER_INDEX);
                return INTEGRATION_CONTROL_RESP_MESSAGE_LENGTH;
            }
            return 0;
        }

        /* MagCal or Tuning Variable Storage Response */
        public static int decodeDataSetResponse(byte[] buffer,
                                                    int offset,
                                                    int length,
                                                    DataSetResponse d)
        {
            if (length < DATA_SET_RESPONSE_MESSAGE_LENGTH) return 0;
            if ((buffer[0] == PACKET_START_CHAR) &&
                 (buffer[1] == BINARY_PACKET_INDICATOR_CHAR) &&
                 (buffer[2] == DATA_SET_RESPONSE_MESSAGE_LENGTH - 2) &&
                 (buffer[3] == MSGID_DATA_SET_RESPONSE))
            {
                if (!verifyChecksum(buffer, offset, DATA_SET_RESPONSE_MESSAGE_CHECKSUM_INDEX)) return 0;

                d.data_type = buffer[DATA_SET_RESPONSE_DATATYPE_VALUE_INDEX];
                d.var_id = buffer[DATA_SET_RESPONSE_VARID_VALUE_INDEX];
                d.status = buffer[DATA_SET_RESPONSE_STATUS_VALUE_INDEX];
                return DATA_SET_RESPONSE_MESSAGE_LENGTH;
            }
            return 0;
        }

        /* Board ID Retrieval Response */
        public static int decodeBoardIDGetResponse(byte[] buffer,
                                                    int offset,
                                                    int length,
                                                    BoardID id)
        {
            if (length < BOARD_IDENTITY_RESPONSE_MESSAGE_LENGTH) return 0;
            if ((buffer[0] == PACKET_START_CHAR) &&
                 (buffer[1] == BINARY_PACKET_INDICATOR_CHAR) &&
                 (buffer[2] == BOARD_IDENTITY_RESPONSE_MESSAGE_LENGTH - 2) &&
                 (buffer[3] == MSGID_BOARD_IDENTITY_RESPONSE))
            {
                if (!verifyChecksum(buffer, offset, BOARD_IDENTITY_RESPONSE_CHECKSUM_INDEX)) return 0;
                id.type = buffer[BOARD_IDENTITY_BOARDTYPE_VALUE_INDEX];
                id.hw_rev = buffer[BOARD_IDENTITY_HWREV_VALUE_INDEX];
                id.fw_ver_major = buffer[BOARD_IDENTITY_FW_VER_MAJOR];
                id.fw_ver_minor = buffer[BOARD_IDENTITY_FW_VER_MINOR];
                id.fw_revision = decodeBinaryUint16(buffer, BOARD_IDENTITY_FW_VER_REVISION_VALUE_INDEX);
                for (int i = 0; i < 12; i++)
                {
                    id.unique_id[i] = buffer[BOARD_IDENTITY_UNIQUE_ID_0 + i];
                }
                return BOARD_IDENTITY_RESPONSE_MESSAGE_LENGTH;
            }
            return 0;
        }

        /* protocol data is encoded little endian, convert to Java's big endian format */
        public static short decodeBinaryUint16(byte[] buffer, int offset)
        {
            short lowbyte = (short)(((short)buffer[offset]) & 0xff);
            short highbyte = (short)buffer[offset + 1];
            highbyte <<= 8;
            short decoded_value = (short)(highbyte + lowbyte);
            return decoded_value;
        }

        public static void encodeBinaryUint16(short val, byte[] buffer, int offset)
        {
            buffer[offset + 0] = (byte)(val & 0xFF);
            buffer[offset + 1] = (byte)((val >> 8) & 0xFF);
        }

        public static int decodeBinaryUint32(byte[] buffer, int offset)
        {
            int lowlowbyte = (((int)buffer[offset]) & 0xff);
            int lowhighbyte = (((int)buffer[offset + 1]) & 0xff);
            int highlowbyte = (((int)buffer[offset + 2]) & 0xff);
            int highhighbyte = (((int)buffer[offset + 3]));

            lowhighbyte <<= 8;
            highlowbyte <<= 16;
            highhighbyte <<= 24;

            int result = highhighbyte + highlowbyte + lowhighbyte + lowlowbyte;
            return result;
        }

        public static void encodeBinaryUint32(int val, byte[] buffer, int offset)
        {
            buffer[offset + 0] = (byte)(val & 0xFF);
            buffer[offset + 1] = (byte)((val >> 8) & 0xFF);
            buffer[offset + 2] = (byte)((val >> 16) & 0xFF);
            buffer[offset + 3] = (byte)((val >> 24) & 0xFF);
        }

        public static short decodeBinaryInt16(byte[] buffer, int offset)
        {
            return decodeBinaryUint16(buffer, offset);
        }

        public static void encodeBinaryInt16(short val, byte[] buffer, int offset)
        {
            encodeBinaryUint16(val, buffer, offset);
        }

        /* -327.68 to +327.68 */
        public static float decodeProtocolSignedHundredthsFloat(byte[] buffer, int offset)
        {
            float signed_angle = (float)decodeBinaryUint16(buffer, offset);
            signed_angle /= 100;
            return signed_angle;
        }

        public static void encodeProtocolSignedHundredthsFloat(float input, byte[] buffer, int offset)
        {
            short input_as_int = (short)(input * 100);
            encodeBinaryInt16(input_as_int, buffer, offset);
        }

        public static short encodeSignedHundredthsFloat(float input)
        {
            return (short)(input * 100);
        }

        public static short encodeUnsignedHundredthsFloat(float input)
        {
            return (short)(input * 100);
        }

        public static float encodeRatioFloat(float input_ratio)
        {
            return (float)(input_ratio *= 32768);
        }

        public static float encodeSignedThousandthsFloat(float input)
        {
            return (float)(input * 1000);
        }

        /* 0 to 655.35 */
        public new static float decodeProtocolUnsignedHundredthsFloat(byte[] buffer, int offset)
        {
            int uint16 = (int)decodeBinaryUint16(buffer, offset);
            if (uint16 < 0)
            {
                uint16 += 65536;
            }
            float unsigned_float = (float)uint16;
            unsigned_float /= 100;
            return unsigned_float;
        }

        public static void encodeProtocolUnsignedHundredthsFloat(float input, byte[] buffer, int offset)
        {
            short input_as_uint = (short)(input * 100);
            encodeBinaryUint16(input_as_uint, buffer, offset);
        }

        /* -32.768 to +32.768 */
        public static float decodeProtocolSignedThousandthsFloat(byte[] buffer, int offset)
        {
            float signed_angle = (float)decodeBinaryUint16(buffer, offset);
            signed_angle /= 1000;
            return signed_angle;
        }
        public static void encodeProtocolSignedThousandthsFloat(float input, byte[] buffer, int offset)
        {
            short input_as_int = (short)(input * 1000);
            encodeBinaryInt16(input_as_int, buffer, offset);
        }

        /* In units of -1 to 1, multiplied by 16384 */
        public static float decodeProtocolRatio(byte[] buffer, int offset)
        {
            float ratio = (float)decodeBinaryUint16(buffer, offset);
            ratio /= 32768;
            return ratio;
        }

        public static void encodeProtocolRatio(float ratio, byte[] buffer, int offset)
        {
            ratio *= 32768;
            encodeBinaryInt16((short)ratio, buffer, offset);
        }

        /* <int16>.<uint16> (-32768.9999 to 32767.9999) */
        public static float decodeProtocol1616Float(byte[] buffer, int offset)
        {
            float result = (float)decodeBinaryUint32(buffer, offset);
            result /= 65536;
            return result;
        }
        public static void encodeProtocol1616Float(float val, byte[] buffer, int offset)
        {
            val *= 65536;
            int int_val = (int)val;
            encodeBinaryUint32(int_val, buffer, offset);
        }
        const int CRC7_POLY = 0x0091;

        public static byte getCRC(byte[] buffer, int length)
        {
            int i, j, crc = 0;

            for (i = 0; i < length; i++)
            {
                crc ^= (int)(0x00ff & buffer[i]);
                for (j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc ^= CRC7_POLY;
                    }
                    crc >>= 1;
                }
            }
            return (byte)crc;
        }
    }
}