using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    public class AHRSProtocol : IMUProtocol
    {
        /* NAVX_CAL_STATUS */

        public const byte NavxCalStatusIMUCalStateMask = 0x03;
        public const byte NavxCalStatusIMUCalInprogress = 0x00;
        public const byte NavxCalStatusIMUCalAccumulate = 0x01;
        public const byte NavxCalStatusIMUCalComplete = 0x02;

        public const byte NavxCalStatusMagCalComplete = 0x04;
        public const byte NavxCalStatusBaroCalComplete = 0x08;

        /* NAVX_SELFTEST_STATUS */

        public const byte NavxSelftestStatusComplete = (byte)0x80;

        public const byte NavxSelftestResultGyroPassed = 0x01;
        public const byte NavxSelftestResultAccelPassed = 0x02;
        public const byte NavxSelftestResultMagPassed = 0x04;
        public const byte NavxSelftestResultBaroPassed = 0x08;

        /* NAVX_OP_STATUS */

        public const byte NavxOpStatusInitializing = 0x00;
        public const byte NavxOpStatusSelftestInProgress = 0x01;
        public const byte NavxOpStatusError = 0x02;
        public const byte NavxOpStatusIMUAutocalInProgress = 0x03;
        public const byte NavxOpStatusNormal = 0x04;

        /* NAVX_SENSOR_STATUS */
        public const byte NavxSensorStatusMoving = 0x01;
        public const byte NavxSensorStatusYawStable = 0x02;
        public const byte NavxSensorStatusMagDisturbance = 0x04;
        public const byte NavxSensorStatusAltitudeValid = 0x08;
        public const byte NavxSensorStatusSealevelPressSet = 0x10;
        public const byte NavxSensorStatusFusedHeadingValid = 0x20;

        /* NAVX_REG_CAPABILITY_FLAGS (Aligned w/NAV6 Flags, see IMUProtocol.h) */

        public const short NavxCapabilityFlagOmnimount = 0x0004;
        public const short NavxCapabilityFlagOmnimountConfigMask = 0x0038;
        public const short NavxCapabilityFlagVelAndDisp = 0x0040;
        public const short NavxCapabilityFlagYawReset = 0x0080;

        /* NAVX_OMNIMOUNT_CONFIG */

        public const byte OmnimountDefault = 0; /* Same as Y_Z_UP */
        public const byte OmnimountYawXUp = 1;
        public const byte OmnimountYawXDown = 2;
        public const byte OmnimountYawYUp = 3;
        public const byte OmnimountYawYDown = 4;
        public const byte OmnimountYawZUp = 5;
        public const byte OmnimountYawZDown = 6;

        /* NAVX_INTEGRATION_CTL */

        public const byte NavxIntegrationCtlResetVelX = 0x01;
        public const byte NavxIntegrationCtlResetVelY = 0x02;
        public const byte NavxIntegrationCtlResetVelZ = 0x04;
        public const byte NavxIntegrationCtlResetDispX = 0x08;
        public const byte NavxIntegrationCtlResetDispY = 0x10;
        public const byte NavxIntegrationCtlResetDispZ = 0x20;
        public const byte NavxIntegrationCtlResetYaw = (byte)0x80;

        public class AHRSTuningVarId
        {
            public const byte Unspecified = 0;
            public const byte MotionThreshold = 1; /* In G */
            public const byte YawStableThreshold = 2; /* In Degrees */
            public const byte MagDisturbanceThreshold = 3; /* Ratio */
            public const byte SeaLevelPressure = 4; /* Millibars */
        };

        public class AHRSDataType
        {
            public const byte TuningVariable = 0;
            public const byte MagCalibration = 1;
            public const byte BoardIdentity = 2;
        };

        public class AHRSDataAction
        {
            public const byte DataGet = 0;
            public const byte DataSet = 1;
        };

        public const byte BinaryPacketIndicatorChar = (byte)'#';

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

        public const byte MsgidAHRSUpdate = (byte)'a';
        const int AHRSUpdateYawValueIndex = 4;  /* Degrees.  Signed Hundredths */
        const int AHRSUpdatePitchValueIndex = 6;  /* Degrees.  Signed Hundredeths */
        const int AHRSUpdateRollValueIndex = 8;  /* Degrees.  Signed Hundredths */
        const int AHRSUpdateHeadingValueIndex = 10;  /* Degrees.  Unsigned Hundredths */
        const int AHRSUpdateAltitudeValueIndex = 12; /* Meters.   Signed 16:16 */
        const int AHRSUpdateFusedHeadingValueIndex = 16; /* Degrees.  Unsigned Hundredths */
        const int AHRSUpdateLinearAccelXValueIndex = 18; /* Inst. G.  Signed Thousandths */
        const int AHRSUpdateLinearAccelYValueIndex = 20; /* Inst. G.  Signed Thousandths */
        const int AHRSUpdateLinearAccelZValueIndex = 22; /* Inst. G.  Signed Thousandths */
        const int AHRSUpdateCalMagXValueIndex = 24; /* Int16 (Device Units) */
        const int AHRSUpdateCalMagYValueIndex = 26; /* Int16 (Device Units) */
        const int AHRSUpdateCalMagZValueIndex = 28; /* Int16 (Device Units) */
        const int AHRSUpdateCalMagNormRatioValueIndex = 30; /* Ratio.  Unsigned Hundredths */
        const int AHRSUpdateCalMagScalarValueIndex = 32; /* Coefficient. Signed 16:16 */
        const int AHRSUpdateMpuTempVaueIndex = 36; /* Centigrade.  Signed Hundredths */
        const int AHRSUpdateRawMagXValueIndex = 38; /* INT16 (Device Units */
        const int AHRSUpdateRawMagYValueIndex = 40; /* INT16 (Device Units */
        const int AHRSUpdateRawMagZValueIndex = 42; /* INT16 (Device Units */
        const int AHRSUpdateQuatWValueIndex = 44; /* INT16 */
        const int AHRSUpdateQuatXValueIndex = 46; /* INT16 */
        const int AHRSUpdateQuatYValueIndex = 48; /* INT16 */
        const int AHRSUpdateQuatZValueIndex = 50; /* INT16 */
        const int AHRSUpdateBaroPressureValueIndex = 52; /* millibar.  Signed 16:16 */
        const int AHRSUpdateBaroTempVaueIndex = 56; /* Centigrade.  Signed  Hundredths */
        const int AHRSUpdateOpstatusValueIndex = 58; /* NAVX_OP_STATUS_XXX */
        const int AHRSUpdateSensorStatusValueIndex = 59; /* NAVX_SENSOR_STATUS_XXX */
        const int AHRSUpdateCalStatusValueIndex = 60; /* NAVX_CAL_STATUS_XXX */
        const int AHRSUpdateSelftestStatusValueIndex = 61; /* NAVX_SELFTEST_STATUS_XXX */
        const int AHRSUpdateMessageChecksumIndex = 62;
        const int AHRSUpdateMessageTerminatorIndex = 64;
        const int AHRSUpdateMessageLength = 66;

        // AHRSAndPositioning Update Packet (similar to AHRS, but removes magnetometer and adds velocity/displacement) */

        public const byte MsgidAhrsposUpdate = (byte)'p';
        const int AhrsposUpdateYawValueIndex = 4; /* Degrees.  Signed Hundredths */
        const int AhrsposUpdatePitchValueIndex = 6; /* Degrees.  Signed Hundredeths */
        const int AhrsposUpdateRollValueIndex = 8; /* Degrees.  Signed Hundredths */
        const int AhrsposUpdateHeadingValueIndex = 10; /* Degrees.  Unsigned Hundredths */
        const int AhrsposUpdateAltitudeValueIndex = 12; /* Meters.   Signed 16:16 */
        const int AhrsposUpdateFusedHeadingValueIndex = 16; /* Degrees.  Unsigned Hundredths */
        const int AhrsposUpdateLinearAccelXValueIndex = 18; /* Inst. G.  Signed Thousandths */
        const int AhrsposUpdateLinearAccelYValueIndex = 20; /* Inst. G.  Signed Thousandths */
        const int AhrsposUpdateLinearAccelZValueIndex = 22; /* Inst. G.  Signed Thousandths */
        const int AhrsposUpdateVelXValueIndex = 24; /* Signed 16:16, in meters/sec */
        const int AhrsposUpdateVelYValueIndex = 28; /* Signed 16:16, in meters/sec */
        const int AhrsposUpdateVelZValueIndex = 32; /* Signed 16:16, in meters/sec */
        const int AhrsposUpdateDispXValueIndex = 36; /* Signed 16:16, in meters */
        const int AhrsposUpdateDispYValueIndex = 40; /* Signed 16:16, in meters */
        const int AhrsposUpdateDispZValueIndex = 44; /* Signed 16:16, in meters */
        const int AhrsposUpdateQuatWValueIndex = 48; /* INT16 */
        const int AhrsposUpdateQuatXValueIndex = 50; /* INT16 */
        const int AhrsposUpdateQuatYValueIndex = 52; /* INT16 */
        const int AhrsposUpdateQuatZValueIndex = 54; /* INT16 */
        const int AhrsposUpdateMpuTempVaueIndex = 56; /* Centigrade.  Signed Hundredths */
        const int AhrsposUpdateOpstatusValueIndex = 58; /* NAVX_OP_STATUS_XXX */
        const int AhrsposUpdateSensorStatusValueIndex = 59; /* NAVX_SENSOR_STATUS_XXX */
        const int AhrsposUpdateCalStatusValueIndex = 60; /* NAVX_CAL_STATUS_XXX */
        const int AhrsposUpdateSelftestStatusValueIndex = 61; /* NAVX_SELFTEST_STATUS_XXX */
        const int AhrsposUpdateMessageChecksumIndex = 62;
        const int AhrsposUpdateMessageTerminatorIndex = 64;
        const int AhrsposUpdateMessageLength = 66;

        // Data Get Request:  Tuning Variable, Mag Cal, Board Identity (Response message depends upon request type)
        public const byte MsgidDataRequest = (byte)'D';
        const int DataRequestDatatypeValueIndex = 4;
        const int DataRequestVariableidValueIndex = 5;
        const int DataRequestChecksumIndex = 6;
        const int DataRequestTerminatorIndex = 8;
        const int DataRequestMessageLength = 10;

        // Data Set Response Packet
        public const byte MsgidDataSetResponse = (byte)'v';
        const int DataSetResponseDatatypeValueIndex = 4;
        const int DataSetResponseVaridValueIndex = 5;
        const int DataSetResponseStatusValueIndex = 6;
        const int DataSetResponseMessageChecksumIndex = 7;
        const int DataSetResponseMessageTerminatorIndex = 9;
        const int DataSetResponseMessageLength = 11;

        /* Integration Control Command Packet */
        public const byte MsgidIntegrationControlCmd = (byte)'I';
        const int IntegrationControlCmdActionIndex = 4;
        const int IntegrationControlCmdParameterIndex = 5;
        const int IntegrationControlCmdMessageChecksumIndex = 9;
        const int IntegrationControlCmdMessageTerminatorIndex = 11;
        const int IntegrationControlCmdMessageLength = 13;

        /* Integration Control Response Packet */
        public const byte MsgidIntegrationControlResp = (byte)'i';
        const int IntegrationControlRespActionIndex = 4;
        const int IntegrationControlRespParameterIndex = 5;
        const int IntegrationControlRespMessageChecksumIndex = 9;
        const int IntegrationControlRespMessageTerminatorIndex = 11;
        const int IntegrationControlRespMessageLength = 13;

        // Magnetometer Calibration Packet - e.g., !m[x_bias][y_bias][z_bias][m1,1 ... m3,3][cr][lf]
        public const byte MsgidMagCalCmd = (byte)'M';
        const int MagCalDataActionValueIndex = 4;
        const int MagXBiasValueIndex = 5; /* signed short */
        const int MagYBiasValueIndex = 7;
        const int MagZBiasValueIndex = 9;
        const int MagXform11ValueIndex = 11; /* signed 16:16 */
        const int MagXform12ValueIndex = 15;
        const int MagXform13ValueIndex = 19;
        const int MagXform21ValueIndex = 23;
        const int MagXform22ValueIndex = 25;
        const int MagXform23ValueIndex = 31;
        const int MagXform31ValueIndex = 35;
        const int MagXform32ValueIndex = 39;
        const int MagXform33ValueIndex = 43;
        const int MagCalEarthMagFieldNormValueIndex = 47;
        const int MagCalCmdMessageChecksumIndex = 51;
        const int MagCalCmdMessageTerminatorIndex = 53;
        const int MagCalCmdMessageLength = 55;

        // Tuning Variable Packet
        public const byte MsgidFusionTuningCmd = (byte)'T';
        const int FusionTuningDataActionValueIndex = 4;
        const int FusionTuningCmdVarIdValueIndex = 5;
        const int FusionTuningCmdVarValueIndex = 6;
        const int FusionTuningCmdMessageChecksumIndex = 10;
        const int FusionTuningCmdMessageTerminatorIndex = 12;
        const int FusionTuningCmdMessageLength = 14;

        // Board Identity Response Packet- e.g., !c[type][hw_rev][fw_major][fw_minor][unique_id[12]]
        public const byte MsgidBoardIdentityResponse = (byte)'i';
        const int BoardIdentityBoardtypeValueIndex = 4;
        const int BoardIdentityHwrevValueIndex = 5;
        const int BoardIdentityFwVerMajor = 6;
        const int BoardIdentityFwVerMinor = 7;
        const int BoardIdentityFwVerRevisionValueIndex = 8;
        const int BoardIdentityUniqueId0 = 10;
        const int BoardIdentityUniqueId1 = 11;
        const int BoardIdentityUniqueId2 = 12;
        const int BoardIdentityUniqueId3 = 13;
        const int BoardIdentityUniqueId4 = 14;
        const int BoardIdentityUniqueId5 = 15;
        const int BoardIdentityUniqueId6 = 16;
        const int BoardIdentityUniqueId7 = 17;
        const int BoardIdentityUniqueId8 = 18;
        const int BoardIdentityUniqueId9 = 19;
        const int BoardIdentityUniqueId10 = 20;
        const int BoardIdentityUniqueId11 = 21;
        const int BoardIdentityResponseChecksumIndex = 22;
        const int BoardIdentityResponseTerminatorIndex = 24;
        const int BoardIdentityResponseMessageLength = 26;

        public const int MaxBinaryMessageLength = AhrsposUpdateMessageLength;

        public class AHRSUpdate
        {
            public float Yaw;
            public float Pitch;
            public float Roll;
            public float CompassHeading;
            public float Altitude;
            public float FusedHeading;
            public float LinearAccelX;
            public float LinearAccelY;
            public float LinearAccelZ;
            public short CalMagX;
            public short CalMagY;
            public short CalMagZ;
            public float MagFieldNormRatio;
            public float MagFieldNormScalar;
            public float MpuTemp;
            public short RawMagX;
            public short RawMagY;
            public short RawMagZ;
            public short QuatW;
            public short QuatX;
            public short QuatY;
            public short QuatZ;
            public float BarometricPressure;
            public float BaroTemp;
            public byte OpStatus;
            public byte SensorStatus;
            public byte CalStatus;
            public byte SelftestStatus;
        }

        public class AHRSPosUpdate
        {
            public float Yaw;
            public float Pitch;
            public float Roll;
            public float CompassHeading;
            public float Altitude;
            public float FusedHeading;
            public float LinearAccelX;
            public float LinearAccelY;
            public float LinearAccelZ;
            public float VelX;
            public float VelY;
            public float VelZ;
            public float DispX;
            public float DispY;
            public float DispZ;
            public float MpuTemp;
            public short QuatW;
            public short QuatX;
            public short QuatY;
            public short QuatZ;
            public float BarometricPressure;
            public float BaroTemp;
            public byte OpStatus;
            public byte SensorStatus;
            public byte CalStatus;
            public byte SelftestStatus;
        }

        public class DataSetResponse
        {
            public byte DataType;
            public byte VarId;       /* If type = TUNING_VARIABLE */
            public byte Status;
        };

        public class IntegrationControl
        {
            public byte Action;
            public int Parameter;
        };

        public class MagCalData
        {
            internal byte Action;
            public short[] MagBias;                /* 3 Values */
            public float[][] MagXform;             /* 3 x 3 Values */
        public float EarthMagFieldNorm;
            public MagCalData()
            {
                MagBias = new short[3];
                MagXform = new float[3][];
                for (int i = 0; i < 3; i++)
                {
                    MagXform[i] = new float[3];
                }
            }
        };

        public class TuningVar
        {
            public byte Action;
            public byte VarId;       /* If type = TUNING_VARIABLE */
            internal float Value;
        };

        public class BoardId
        {
            public byte Type;
            public byte HwRev;
            public byte FwVerMajor;
            public byte FwVerMinor;
            public short FwRevision;
            public byte[] UniqueId;
            public BoardId()
            {
                UniqueId = new byte[12];
            }
        };

        public static int DecodeAHRSUpdate(byte[] buffer,
                                            int offset,
                                            int length,
                                            AHRSUpdate u)
        {
            if (length < AHRSUpdateMessageLength)
            {
                return 0;
            }
            if ((buffer[offset + 0] == PacketStartChar) &&
                 (buffer[offset + 1] == BinaryPacketIndicatorChar) &&
                 (buffer[offset + 2] == AHRSUpdateMessageLength - 2) &&
                 (buffer[offset + 3] == MsgidAHRSUpdate))
            {

                if (!VerifyChecksum(buffer, offset, AHRSUpdateMessageChecksumIndex))
                {
                    return 0;
                }
                u.Yaw = DecodeProtocolSignedHundredthsFloat(buffer, offset + AHRSUpdateYawValueIndex);
                u.Pitch = DecodeProtocolSignedHundredthsFloat(buffer, offset + AHRSUpdatePitchValueIndex);
                u.Roll = DecodeProtocolSignedHundredthsFloat(buffer, offset + AHRSUpdateRollValueIndex);
                u.CompassHeading = decodeProtocolUnsignedHundredthsFloat(buffer, offset + AHRSUpdateHeadingValueIndex);
                u.Altitude = DecodeProtocol1616Float(buffer, offset + AHRSUpdateAltitudeValueIndex);
                u.FusedHeading = decodeProtocolUnsignedHundredthsFloat(buffer, offset + AHRSUpdateFusedHeadingValueIndex);
                u.LinearAccelX = DecodeProtocolSignedThousandthsFloat(buffer, offset + AHRSUpdateLinearAccelXValueIndex);
                u.LinearAccelY = DecodeProtocolSignedThousandthsFloat(buffer, offset + AHRSUpdateLinearAccelYValueIndex);
                u.LinearAccelZ = DecodeProtocolSignedThousandthsFloat(buffer, offset + AHRSUpdateLinearAccelZValueIndex);
                u.CalMagX = DecodeBinaryInt16(buffer, offset + AHRSUpdateCalMagXValueIndex);
                u.CalMagY = DecodeBinaryInt16(buffer, offset + AHRSUpdateCalMagYValueIndex);
                u.CalMagZ = DecodeBinaryInt16(buffer, offset + AHRSUpdateCalMagZValueIndex);
                u.MagFieldNormRatio = decodeProtocolUnsignedHundredthsFloat(buffer, offset + AHRSUpdateCalMagNormRatioValueIndex);
                u.MagFieldNormScalar = DecodeProtocol1616Float(buffer, offset + AHRSUpdateCalMagScalarValueIndex);
                u.MpuTemp = DecodeProtocolSignedHundredthsFloat(buffer, offset + AHRSUpdateMpuTempVaueIndex);
                u.RawMagX = DecodeBinaryInt16(buffer, offset + AHRSUpdateRawMagXValueIndex);
                u.RawMagY = DecodeBinaryInt16(buffer, offset + AHRSUpdateRawMagYValueIndex);
                u.RawMagZ = DecodeBinaryInt16(buffer, offset + AHRSUpdateRawMagZValueIndex);
                u.QuatW = DecodeBinaryInt16(buffer, offset + AHRSUpdateQuatWValueIndex);
                u.QuatX = DecodeBinaryInt16(buffer, offset + AHRSUpdateQuatXValueIndex);
                u.QuatY = DecodeBinaryInt16(buffer, offset + AHRSUpdateQuatYValueIndex);
                u.QuatZ = DecodeBinaryInt16(buffer, offset + AHRSUpdateQuatZValueIndex);
                u.BarometricPressure = DecodeProtocol1616Float(buffer, offset + AHRSUpdateBaroPressureValueIndex);
                u.BaroTemp = DecodeProtocolSignedHundredthsFloat(buffer, offset + AHRSUpdateBaroTempVaueIndex);
                u.OpStatus = buffer[AHRSUpdateOpstatusValueIndex];
                u.SensorStatus = buffer[AHRSUpdateSensorStatusValueIndex];
                u.CalStatus = buffer[AHRSUpdateCalStatusValueIndex];
                u.SelftestStatus = buffer[AHRSUpdateSelftestStatusValueIndex];
                return AHRSUpdateMessageLength;
            }
            return 0;
        }

        public static int DecodeAHRSPosUpdate(byte[] buffer,
                int offset,
                int length,
                AHRSPosUpdate u)
        {
            if (length < AhrsposUpdateMessageLength)
            {
                return 0;
            }
            if ((buffer[offset + 0] == PacketStartChar) &&
                    (buffer[offset + 1] == BinaryPacketIndicatorChar) &&
                    (buffer[offset + 2] == AhrsposUpdateMessageLength - 2) &&
                    (buffer[offset + 3] == MsgidAhrsposUpdate))
            {

                if (!VerifyChecksum(buffer, offset, AhrsposUpdateMessageChecksumIndex))
                {
                    return 0;
                }
                u.Yaw = DecodeProtocolSignedHundredthsFloat(buffer, offset + AhrsposUpdateYawValueIndex);
                u.Pitch = DecodeProtocolSignedHundredthsFloat(buffer, offset + AhrsposUpdatePitchValueIndex);
                u.Roll = DecodeProtocolSignedHundredthsFloat(buffer, offset + AhrsposUpdateRollValueIndex);
                u.CompassHeading = decodeProtocolUnsignedHundredthsFloat(buffer, offset + AhrsposUpdateHeadingValueIndex);
                u.Altitude = DecodeProtocol1616Float(buffer, offset + AhrsposUpdateAltitudeValueIndex);
                u.FusedHeading = decodeProtocolUnsignedHundredthsFloat(buffer, offset + AhrsposUpdateFusedHeadingValueIndex);
                u.LinearAccelX = DecodeProtocolSignedThousandthsFloat(buffer, offset + AhrsposUpdateLinearAccelXValueIndex);
                u.LinearAccelY = DecodeProtocolSignedThousandthsFloat(buffer, offset + AhrsposUpdateLinearAccelYValueIndex);
                u.LinearAccelZ = DecodeProtocolSignedThousandthsFloat(buffer, offset + AhrsposUpdateLinearAccelZValueIndex);
                u.VelX = DecodeProtocol1616Float(buffer, offset + AhrsposUpdateVelXValueIndex);
                u.VelY = DecodeProtocol1616Float(buffer, offset + AhrsposUpdateVelYValueIndex);
                u.VelZ = DecodeProtocol1616Float(buffer, offset + AhrsposUpdateVelZValueIndex);
                u.DispX = DecodeProtocol1616Float(buffer, offset + AhrsposUpdateDispXValueIndex);
                u.DispY = DecodeProtocol1616Float(buffer, offset + AhrsposUpdateDispYValueIndex);
                u.DispZ = DecodeProtocol1616Float(buffer, offset + AhrsposUpdateDispZValueIndex);
                u.MpuTemp = DecodeProtocolSignedHundredthsFloat(buffer, offset + AhrsposUpdateMpuTempVaueIndex);
                u.QuatW = DecodeBinaryInt16(buffer, offset + AhrsposUpdateQuatWValueIndex);
                u.QuatX = DecodeBinaryInt16(buffer, offset + AhrsposUpdateQuatXValueIndex);
                u.QuatY = DecodeBinaryInt16(buffer, offset + AhrsposUpdateQuatYValueIndex);
                u.QuatZ = DecodeBinaryInt16(buffer, offset + AhrsposUpdateQuatZValueIndex);
                u.OpStatus = buffer[AhrsposUpdateOpstatusValueIndex];
                u.SensorStatus = buffer[AhrsposUpdateSensorStatusValueIndex];
                u.CalStatus = buffer[AhrsposUpdateCalStatusValueIndex];
                u.SelftestStatus = buffer[AhrsposUpdateSelftestStatusValueIndex];
                return AhrsposUpdateMessageLength;
            }
            return 0;
        }

        /* Mag Cal, Tuning Variable, or Board ID Retrieval Request */
        public static int EncodeDataGetRequest(byte[] buffer,
                                                byte type,
                                                byte varId)
        {
            // Header
            buffer[0] = PacketStartChar;
            buffer[1] = BinaryPacketIndicatorChar;
            buffer[2] = DataRequestMessageLength - 2;
            buffer[3] = MsgidDataRequest;
            // Data
            buffer[DataRequestDatatypeValueIndex] = type;
            buffer[DataRequestVariableidValueIndex] = varId;
            // Footer
            EncodeTermination(buffer, DataRequestMessageLength, DataRequestMessageLength - 4);
            return DataRequestMessageLength;
        }

        /* Mag Cal Data Storage Request */
        public static int EncodeMagCalDataSetRequest(byte[] buffer,
                                                MagCalData d)
        {
            // Header
            buffer[0] = PacketStartChar;
            buffer[1] = BinaryPacketIndicatorChar;
            buffer[2] = MagCalCmdMessageLength - 2;
            buffer[3] = MsgidMagCalCmd;

            // Data
            buffer[MagCalDataActionValueIndex] = d.Action;
            for (int i = 0; i < 3; i++)
            {
                EncodeBinaryInt16(d.MagBias[i],
                                   buffer, MagXBiasValueIndex + (i * 2));
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    EncodeProtocol1616Float(d.MagXform[i][j],
                            buffer, MagXform11ValueIndex + (i * 6) + (j * 2));
                }
            }
            EncodeProtocol1616Float(d.EarthMagFieldNorm, buffer, MagCalEarthMagFieldNormValueIndex);
            // Footer
            EncodeTermination(buffer, MagCalCmdMessageLength, MagCalCmdMessageLength - 4);
            return MagCalCmdMessageLength;
        }

        /* Mag Cal Data Retrieval Response */
        public static int DecodeMagCalDataGetResponse(byte[] buffer,
                                                        int offset,
                                                        int length,
                                                        MagCalData d)
        {
            if (length < MagCalCmdMessageLength) return 0;
            if ((buffer[0] == PacketStartChar) &&
                 (buffer[1] == BinaryPacketIndicatorChar) &&
                 (buffer[2] == MagCalCmdMessageLength - 2) &&
                 (buffer[3] == MsgidMagCalCmd))
            {
                if (!VerifyChecksum(buffer, offset, MagCalCmdMessageChecksumIndex)) return 0;

                d.Action = buffer[MagCalDataActionValueIndex];
                for (int i = 0; i < 3; i++)
                {
                    d.MagBias[i] = DecodeBinaryInt16(buffer, MagXBiasValueIndex + (i * 2));
                }
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        d.MagXform[i][j] = DecodeProtocol1616Float(buffer, MagXform11ValueIndex + (i * 6) + (j * 2));
                    }
                }
                d.EarthMagFieldNorm = DecodeProtocol1616Float(buffer, MagCalEarthMagFieldNormValueIndex);
                return MagCalCmdMessageLength;
            }
            return 0;
        }

        /* Tuning Variable Storage Request */
        public static int EncodeTuningVarSetRequest(byte[] buffer,
                                                     TuningVar r)
        {
            // Header
            buffer[0] = PacketStartChar;
            buffer[1] = BinaryPacketIndicatorChar;
            buffer[2] = FusionTuningCmdMessageLength - 2;
            buffer[3] = MsgidFusionTuningCmd;
            // Data
            buffer[FusionTuningDataActionValueIndex] = r.Action;
            buffer[FusionTuningCmdVarIdValueIndex] = r.VarId;
            EncodeProtocol1616Float(r.Value, buffer, FusionTuningCmdVarValueIndex);
            // Footer
            EncodeTermination(buffer, FusionTuningCmdMessageLength, FusionTuningCmdMessageLength - 4);
            return FusionTuningCmdMessageLength;
        }

        /* Tuning Variable Retrieval Response */
        public static int DecodeTuningVarGetResponse(byte[] buffer,
                                                        int offset,
                                                        int length,
                                                        TuningVar r)
        {
            if (length < FusionTuningCmdMessageLength) return 0;
            if ((buffer[0] == PacketStartChar) &&
                 (buffer[1] == BinaryPacketIndicatorChar) &&
                 (buffer[2] == FusionTuningCmdMessageLength - 2) &&
                 (buffer[3] == MsgidFusionTuningCmd))
            {
                if (!VerifyChecksum(buffer, offset, FusionTuningCmdMessageChecksumIndex)) return 0;

                // Data
                r.Action = buffer[FusionTuningDataActionValueIndex];
                r.VarId = buffer[FusionTuningCmdVarIdValueIndex];
                r.Value = DecodeProtocol1616Float(buffer, FusionTuningCmdVarValueIndex);
                return FusionTuningCmdMessageLength;
            }
            return 0;
        }

        public static int EncodeIntegrationControlCmd(byte[] buffer, IntegrationControl u)
        {
            // Header
            buffer[0] = PacketStartChar;
            buffer[1] = BinaryPacketIndicatorChar;
            buffer[2] = IntegrationControlCmdMessageLength - 2;
            buffer[3] = MsgidIntegrationControlCmd;
            // Data
            buffer[IntegrationControlCmdActionIndex] = u.Action;
            EncodeBinaryUint32(u.Parameter, buffer, IntegrationControlCmdParameterIndex);
            // Footer
            EncodeTermination(buffer, IntegrationControlCmdMessageLength, IntegrationControlCmdMessageLength - 4);
            return IntegrationControlCmdMessageLength;
        }

        public static int DecodeIntegrationControlResponse(byte[] buffer, int offset, int length, IntegrationControl u)
        {
            if (length < IntegrationControlRespMessageLength) return 0;
            if ((buffer[0] == PacketStartChar) &&
                 (buffer[1] == BinaryPacketIndicatorChar) &&
                 (buffer[2] == IntegrationControlRespMessageLength - 2) &&
                 (buffer[3] == MsgidIntegrationControlResp))
            {
                if (!VerifyChecksum(buffer, offset, IntegrationControlRespMessageChecksumIndex)) return 0;

                // Data
                u.Action = buffer[IntegrationControlRespActionIndex];
                u.Parameter = DecodeBinaryUint32(buffer, IntegrationControlRespParameterIndex);
                return IntegrationControlRespMessageLength;
            }
            return 0;
        }

        /* MagCal or Tuning Variable Storage Response */
        public static int DecodeDataSetResponse(byte[] buffer,
                                                    int offset,
                                                    int length,
                                                    DataSetResponse d)
        {
            if (length < DataSetResponseMessageLength) return 0;
            if ((buffer[0] == PacketStartChar) &&
                 (buffer[1] == BinaryPacketIndicatorChar) &&
                 (buffer[2] == DataSetResponseMessageLength - 2) &&
                 (buffer[3] == MsgidDataSetResponse))
            {
                if (!VerifyChecksum(buffer, offset, DataSetResponseMessageChecksumIndex)) return 0;

                d.DataType = buffer[DataSetResponseDatatypeValueIndex];
                d.VarId = buffer[DataSetResponseVaridValueIndex];
                d.Status = buffer[DataSetResponseStatusValueIndex];
                return DataSetResponseMessageLength;
            }
            return 0;
        }

        /* Board ID Retrieval Response */
        public static int DecodeBoardIdGetResponse(byte[] buffer,
                                                    int offset,
                                                    int length,
                                                    BoardId id)
        {
            if (length < BoardIdentityResponseMessageLength) return 0;
            if ((buffer[0] == PacketStartChar) &&
                 (buffer[1] == BinaryPacketIndicatorChar) &&
                 (buffer[2] == BoardIdentityResponseMessageLength - 2) &&
                 (buffer[3] == MsgidBoardIdentityResponse))
            {
                if (!VerifyChecksum(buffer, offset, BoardIdentityResponseChecksumIndex)) return 0;
                id.Type = buffer[BoardIdentityBoardtypeValueIndex];
                id.HwRev = buffer[BoardIdentityHwrevValueIndex];
                id.FwVerMajor = buffer[BoardIdentityFwVerMajor];
                id.FwVerMinor = buffer[BoardIdentityFwVerMinor];
                id.FwRevision = DecodeBinaryUint16(buffer, BoardIdentityFwVerRevisionValueIndex);
                for (int i = 0; i < 12; i++)
                {
                    id.UniqueId[i] = buffer[BoardIdentityUniqueId0 + i];
                }
                return BoardIdentityResponseMessageLength;
            }
            return 0;
        }

        /* protocol data is encoded little endian, convert to Java's big endian format */
        public static short DecodeBinaryUint16(byte[] buffer, int offset)
        {
            short lowbyte = (short)(((short)buffer[offset]) & 0xff);
            short highbyte = (short)buffer[offset + 1];
            highbyte <<= 8;
            short decodedValue = (short)(highbyte + lowbyte);
            return decodedValue;
        }

        public static void EncodeBinaryUint16(short val, byte[] buffer, int offset)
        {
            buffer[offset + 0] = (byte)(val & 0xFF);
            buffer[offset + 1] = (byte)((val >> 8) & 0xFF);
        }

        public static int DecodeBinaryUint32(byte[] buffer, int offset)
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

        public static void EncodeBinaryUint32(int val, byte[] buffer, int offset)
        {
            buffer[offset + 0] = (byte)(val & 0xFF);
            buffer[offset + 1] = (byte)((val >> 8) & 0xFF);
            buffer[offset + 2] = (byte)((val >> 16) & 0xFF);
            buffer[offset + 3] = (byte)((val >> 24) & 0xFF);
        }

        public static short DecodeBinaryInt16(byte[] buffer, int offset)
        {
            return DecodeBinaryUint16(buffer, offset);
        }

        public static void EncodeBinaryInt16(short val, byte[] buffer, int offset)
        {
            EncodeBinaryUint16(val, buffer, offset);
        }

        /* -327.68 to +327.68 */
        public static float DecodeProtocolSignedHundredthsFloat(byte[] buffer, int offset)
        {
            float signedAngle = (float)DecodeBinaryUint16(buffer, offset);
            signedAngle /= 100;
            return signedAngle;
        }

        public static void EncodeProtocolSignedHundredthsFloat(float input, byte[] buffer, int offset)
        {
            short inputAsInt = (short)(input * 100);
            EncodeBinaryInt16(inputAsInt, buffer, offset);
        }

        public static short EncodeSignedHundredthsFloat(float input)
        {
            return (short)(input * 100);
        }

        public static short EncodeUnsignedHundredthsFloat(float input)
        {
            return (short)(input * 100);
        }

        public static float EncodeRatioFloat(float inputRatio)
        {
            return (float)(inputRatio *= 32768);
        }

        public static float EncodeSignedThousandthsFloat(float input)
        {
            return (float)(input * 1000);
        }

        /* 0 to 655.35 */
        public static float decodeProtocolUnsignedHundredthsFloat(byte[] buffer, int offset)
        {
            int uint16 = (int)DecodeBinaryUint16(buffer, offset);
            if (uint16 < 0)
            {
                uint16 += 65536;
            }
            float unsignedFloat = (float)uint16;
            unsignedFloat /= 100;
            return unsignedFloat;
        }

        public static void EncodeProtocolUnsignedHundredthsFloat(float input, byte[] buffer, int offset)
        {
            short inputAsUint = (short)(input * 100);
            EncodeBinaryUint16(inputAsUint, buffer, offset);
        }

        /* -32.768 to +32.768 */
        public static float DecodeProtocolSignedThousandthsFloat(byte[] buffer, int offset)
        {
            float signedAngle = (float)DecodeBinaryUint16(buffer, offset);
            signedAngle /= 1000;
            return signedAngle;
        }
        public static void EncodeProtocolSignedThousandthsFloat(float input, byte[] buffer, int offset)
        {
            short inputAsInt = (short)(input * 1000);
            EncodeBinaryInt16(inputAsInt, buffer, offset);
        }

        /* In units of -1 to 1, multiplied by 16384 */
        public static float DecodeProtocolRatio(byte[] buffer, int offset)
        {
            float ratio = (float)DecodeBinaryUint16(buffer, offset);
            ratio /= 32768;
            return ratio;
        }

        public static void EncodeProtocolRatio(float ratio, byte[] buffer, int offset)
        {
            ratio *= 32768;
            EncodeBinaryInt16((short)ratio, buffer, offset);
        }

        /* <int16>.<uint16> (-32768.9999 to 32767.9999) */
        public static float DecodeProtocol1616Float(byte[] buffer, int offset)
        {
            float result = (float)DecodeBinaryUint32(buffer, offset);
            result /= 65536;
            return result;
        }
        public static void EncodeProtocol1616Float(float val, byte[] buffer, int offset)
        {
            val *= 65536;
            int intVal = (int)val;
            EncodeBinaryUint32(intVal, buffer, offset);
        }
        const int Crc7Poly = 0x0091;

        public static byte GetCrc(byte[] buffer, int length)
        {
            int i, j, crc = 0;

            for (i = 0; i < length; i++)
            {
                crc ^= (int)(0x00ff & buffer[i]);
                for (j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc ^= Crc7Poly;
                    }
                    crc >>= 1;
                }
            }
            return (byte)crc;
        }
    }
}
