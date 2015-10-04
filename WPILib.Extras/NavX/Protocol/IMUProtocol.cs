using System.Text;

namespace WPILib.Extras.NavX
{
    public class IMUProtocol
    {
        public const byte PacketStartChar = (byte)'!';
        const int ProtocolFloatLength = 7;
        const int ChecksumLength = 2;
        const int TerminatorLength = 2;

        // Yaw/Pitch/Roll (YPR) Update Packet - e.g., !y[yaw][pitch][roll][compass_heading]
        public const byte MsgidYprUpdate = (byte)'y';
        const int YprUpdateYawValueIndex = 2;
        const int YprUpdatePitchValueIndex = 9;
        const int YprUpdateRollValueIndex = 16;
        const int YprUpdateCompassValueIndex = 23;
        const int YprUpdateChecksumIndex = 30;
        const int YprUpdateTerminatorIndex = 32;
        const int YprUpdateMessageLength = 34;

        // Quaternion Data Update Packet - e.g., !r[q1][q2][q3][q4][accelx][accely][accelz][magx][magy][magz]
        public const byte MsgidQuaternionUpdate = (byte)'q';
        const int QuaternionUpdateMessageLength = 53;
        const int QuaternionUpdateQuat1ValueIndex = 2;
        const int QuaternionUpdateQuat2ValueIndex = 6;
        const int QuaternionUpdateQuat3ValueIndex = 10;
        const int QuaternionUpdateQuat4ValueIndex = 14;
        const int QuaternionUpdateAccelXValueIndex = 18;
        const int QuaternionUpdateAccelYValueIndex = 22;
        const int QuaternionUpdateAccelZValueIndex = 26;
        const int QuaternionUpdateMagXValueIndex = 30;
        const int QuaternionUpdateMagYValueIndex = 34;
        const int QuaternionUpdateMagZValueIndex = 38;
        const int QuaternionUpdateTempValueIndex = 42;
        const int QuaternionUpdateChecksumIndex = 49;
        const int QuaternionUpdateTerminatorIndex = 51;

        // Gyro/Raw Data Update packet - e.g., !g[gx][gy][gz][accelx][accely][accelz][magx][magy][magz][temp_c][cr][lf]

        public const byte MsgidGyroUpdate = (byte)'g';
        const int GyroUpdateGyroXValueIndex = 2;
        const int GyroUpdateGyroYValueIndex = 6;
        const int GyroUpdateGyroZValueIndex = 10;
        const int GyroUpdateAccelXValueIndex = 14;
        const int GyroUpdateAccelYValueIndex = 18;
        const int GyroUpdateAccelZValueIndex = 22;
        const int GyroUpdateMagXValueIndex = 26;
        const int GyroUpdateMagYValueIndex = 30;
        const int GyroUpdateMagZValueIndex = 34;
        const int GyroUpdateTempValueIndex = 38;
        const int GyroUpdateChecksumIndex = 42;
        const int GyroUpdateTerminatorIndex = 44;
        const int GyroUpdateMessageLength = 46;

        // EnableStream Command Packet - e.g., !S[stream type][checksum][cr][lf]
        public const byte MsgidStreamCmd = (byte)'S';
        public const int StreamCmdStreamTypeYpr = MsgidYprUpdate;
        public const int StreamCmdStreamTypeQuaternion = MsgidQuaternionUpdate;
        public const int StreamCmdStreamTypeGyro = MsgidGyroUpdate;
        const int StreamCmdStreamTypeIndex = 2;
        const int StreamCmdUpdateRateHzIndex = 3;
        const int StreamCmdChecksumIndex = 5;
        const int StreamCmdTerminatorIndex = 7;
        const int StreamCmdMessageLength = 9;

        // EnableStream Response Packet - e.g., !s[stream type][gyro full scale range][accel full scale range][update rate hz][yaw_offset_degrees][flags][checksum][cr][lf]
        public const byte MsgIdStreamResponse = (byte)'s';
        const int StreamResponseMessageLength = 46;
        const int StreamResponseStreamTypeIndex = 2;
        const int StreamResponseGyroFullScaleDpsRange = 3;
        const int StreamResponseAccelFullScaleGRange = 7;
        const int StreamResponseUpdateRateHz = 11;
        const int StreamResponseYawOffsetDegrees = 15;
        const int StreamResponseQuat1Offset = 22;
        const int StreamResponseQuat2Offset = 26;
        const int StreamResponseQuat3Offset = 30;
        const int StreamResponseQuat4Offset = 34;
        const int StreamResponseFlags = 38;
        const int StreamResponseChecksumIndex = 42;
        const int StreamResponseTerminatorIndex = 44;

        public const byte StreamMsgTerminationChar = (byte)'\n';

        public const short Nav6FlagMaskCalibrationState = 0x03;

        public const short Nav6CalibrationStateWait = 0x00;
        public const short Nav6CalibrationStateAccumulate = 0x01;
        public const short Nav6CalibrationStateComplete = 0x02;

        public const int IMUProtocolMaxMessageLength = QuaternionUpdateMessageLength;

        public class YprUpdate
        {

            public float Yaw;
            public float Pitch;
            public float Roll;
            public float CompassHeading;
        }

        public class StreamCommand
        {

            public byte StreamType;
        }

        public class StreamResponse
        {

            public byte StreamType;
            public short GyroFsrDps;
            public short AccelFsrG;
            public short UpdateRateHz;
            public float YawOffsetDegrees;
            public short Q1Offset;
            public short Q2Offset;
            public short Q3Offset;
            public short Q4Offset;
            public short Flags;
        }

        public class QuaternionUpdate
        {

            public short Q1;
            public short Q2;
            public short Q3;
            public short Q4;
            public short AccelX;
            public short AccelY;
            public short AccelZ;
            public short MagX;
            public short MagY;
            public short MagZ;
            public float TempC;
        }

        public class GyroUpdate
        {

            public short GyroX;
            public short GyroY;
            public short GyroZ;
            public short AccelX;
            public short AccelY;
            public short AccelZ;
            public short MagX;
            public short MagY;
            public short MagZ;
            public float TempC;
        }

        public static int EncodeStreamCommand(byte[] protocolBuffer, byte streamType, byte updateRateHz)
        {
            // Header
            protocolBuffer[0] = PacketStartChar;
            protocolBuffer[1] = MsgidStreamCmd;

            // Data
            protocolBuffer[StreamCmdStreamTypeIndex] = streamType;
            ByteToHex(updateRateHz, protocolBuffer, StreamCmdUpdateRateHzIndex);

            // Footer
            EncodeTermination(protocolBuffer, StreamCmdMessageLength, StreamCmdMessageLength - 4);

            return StreamCmdMessageLength;
        }

        public static int DecodeStreamResponse(byte[] buffer, int offset, int length, StreamResponse r)
        {

            if (length < StreamResponseMessageLength)
            {
                return 0;
            }
            if ((buffer[offset + 0] == PacketStartChar) && (buffer[offset + 1] == MsgIdStreamResponse))
            {
                if (!VerifyChecksum(buffer, offset, StreamResponseChecksumIndex))
                {
                    return 0;
                }

                r.StreamType = buffer[offset + 2];
                r.GyroFsrDps = DecodeProtocolUint16(buffer, offset + StreamResponseGyroFullScaleDpsRange);
                r.AccelFsrG = DecodeProtocolUint16(buffer, offset + StreamResponseAccelFullScaleGRange);
                r.UpdateRateHz = DecodeProtocolUint16(buffer, offset + StreamResponseUpdateRateHz);
                r.YawOffsetDegrees = DecodeProtocolFloat(buffer, offset + StreamResponseYawOffsetDegrees);
                r.Q1Offset = DecodeProtocolUint16(buffer, offset + StreamResponseQuat1Offset);
                r.Q2Offset = DecodeProtocolUint16(buffer, offset + StreamResponseQuat2Offset);
                r.Q3Offset = DecodeProtocolUint16(buffer, offset + StreamResponseQuat3Offset);
                r.Q4Offset = DecodeProtocolUint16(buffer, offset + StreamResponseQuat4Offset);
                r.Flags = DecodeProtocolUint16(buffer, offset + StreamResponseFlags);

                return StreamResponseMessageLength;
            }
            return 0;
        }

        public static int DecodeStreamCommand(byte[] buffer, int offset, int length, StreamCommand c)
        {
            if (length < StreamCmdMessageLength)
            {
                return 0;
            }
            if ((buffer[offset + 0] == '!') && (buffer[offset + 1] == MsgidStreamCmd))
            {
                if (!VerifyChecksum(buffer, offset, StreamCmdChecksumIndex))
                {
                    return 0;
                }

                c.StreamType = buffer[offset + StreamCmdStreamTypeIndex];
                return StreamCmdMessageLength;
            }
            return 0;
        }

        public static int DecodeYprUpdate(byte[] buffer, int offset, int length, YprUpdate u)
        {
            if (length < YprUpdateMessageLength)
            {
                return 0;
            }
            if ((buffer[offset + 0] == '!') && (buffer[offset + 1] == 'y'))
            {
                if (!VerifyChecksum(buffer, offset, YprUpdateChecksumIndex))
                {
                    return 0;
                }

                u.Yaw = DecodeProtocolFloat(buffer, offset + YprUpdateYawValueIndex);
                u.Pitch = DecodeProtocolFloat(buffer, offset + YprUpdatePitchValueIndex);
                u.Roll = DecodeProtocolFloat(buffer, offset + YprUpdateRollValueIndex);
                u.CompassHeading = DecodeProtocolFloat(buffer, offset + YprUpdateCompassValueIndex);
                return YprUpdateMessageLength;
            }
            return 0;
        }

        public static int DecodeQuaternionUpdate(byte[] buffer, int offset, int length,
            QuaternionUpdate u)
        {
            if (length < QuaternionUpdateMessageLength)
            {
                return 0;
            }
            if ((buffer[offset + 0] == PacketStartChar) && (buffer[offset + 1] == MsgidQuaternionUpdate))
            {
                if (!VerifyChecksum(buffer, offset, QuaternionUpdateChecksumIndex))
                {
                    return 0;
                }

                u.Q1 = DecodeProtocolUint16(buffer, offset + QuaternionUpdateQuat1ValueIndex);
                u.Q2 = DecodeProtocolUint16(buffer, offset + QuaternionUpdateQuat2ValueIndex);
                u.Q3 = DecodeProtocolUint16(buffer, offset + QuaternionUpdateQuat3ValueIndex);
                u.Q4 = DecodeProtocolUint16(buffer, offset + QuaternionUpdateQuat4ValueIndex);
                u.AccelX = DecodeProtocolUint16(buffer, offset + QuaternionUpdateAccelXValueIndex);
                u.AccelY = DecodeProtocolUint16(buffer, offset + QuaternionUpdateAccelYValueIndex);
                u.AccelZ = DecodeProtocolUint16(buffer, offset + QuaternionUpdateAccelZValueIndex);
                u.MagX = DecodeProtocolUint16(buffer, offset + QuaternionUpdateMagXValueIndex);
                u.MagY = DecodeProtocolUint16(buffer, offset + QuaternionUpdateMagYValueIndex);
                u.MagZ = DecodeProtocolUint16(buffer, offset + QuaternionUpdateMagZValueIndex);
                u.TempC = DecodeProtocolFloat(buffer, offset + QuaternionUpdateTempValueIndex);
                return QuaternionUpdateMessageLength;
            }
            return 0;
        }

        public static int DecodeGyroUpdate(byte[] buffer, int offset, int length,
            GyroUpdate u)
        {
            if (length < GyroUpdateMessageLength)
            {
                return 0;
            }
            if ((buffer[offset + 0] == PacketStartChar) && (buffer[offset + 1] == MsgidGyroUpdate))
            {
                if (!VerifyChecksum(buffer, offset, GyroUpdateChecksumIndex))
                {
                    return 0;
                }

                u.GyroX = DecodeProtocolUint16(buffer, offset + GyroUpdateGyroXValueIndex);
                u.GyroY = DecodeProtocolUint16(buffer, offset + GyroUpdateGyroYValueIndex);
                u.GyroZ = DecodeProtocolUint16(buffer, offset + GyroUpdateGyroZValueIndex);
                u.AccelX = DecodeProtocolUint16(buffer, offset + GyroUpdateAccelXValueIndex);
                u.AccelY = DecodeProtocolUint16(buffer, offset + GyroUpdateAccelYValueIndex);
                u.AccelZ = DecodeProtocolUint16(buffer, offset + GyroUpdateAccelZValueIndex);
                u.MagX = DecodeProtocolUint16(buffer, offset + GyroUpdateMagXValueIndex);
                u.MagY = DecodeProtocolUint16(buffer, offset + GyroUpdateMagYValueIndex);
                u.MagZ = DecodeProtocolUint16(buffer, offset + GyroUpdateMagZValueIndex);
                u.TempC = DecodeProtocolUnsignedHundredthsFloat(buffer, offset + GyroUpdateTempValueIndex);
                return GyroUpdateMessageLength;
            }
            return 0;
        }

        public static void EncodeTermination(byte[] buffer, int totalLength, int contentLength)
        {
            if ((totalLength >= (ChecksumLength + TerminatorLength)) && (totalLength >= contentLength + (ChecksumLength + TerminatorLength)))
            {
                // Checksum 
                byte checksum = 0;
                for (int i = 0; i < contentLength; i++)
                {
                    checksum += buffer[i];
                }
                // convert checksum to two ascii bytes

                ByteToHex(checksum, buffer, contentLength);
                // Message Terminator
                buffer[contentLength + ChecksumLength + 0] = (byte)'\r';
                buffer[contentLength + ChecksumLength + 1] = (byte)'\n';
            }
        }

        protected static byte[] HexArray
            = new byte[]{(byte) '0', (byte) '1', (byte) '2', (byte) '3',
                (byte) '4', (byte) '5', (byte) '6', (byte) '7',
                (byte) '8', (byte) '9', (byte) 'A', (byte) 'B',
                (byte) 'C', (byte) 'D', (byte) 'E', (byte) 'F'};

        public static void ByteToHex(byte thebyte, byte[] dest, int offset)
        {
            int v = thebyte & 0xFF;
            dest[offset + 0] = HexArray[v >> 4];
            dest[offset + 1] = HexArray[v & 0x0F];
        }

        public static short DecodeProtocolUint16(byte[] uint16String, int offset)
        {
            short decodedUint16 = 0;
            int shiftLeft = 12;
            for (int i = offset + 0; i < offset + 4; i++)
            {
                byte digit = (byte)(uint16String[i] <= '9' ? uint16String[i] - '0' : ((uint16String[i] - 'A') + 10));
                decodedUint16 += (short)(((short)digit) << shiftLeft);
                shiftLeft -= 4;
            }
            return decodedUint16;
        }

        /* 0 to 655.35 */
        public static float DecodeProtocolUnsignedHundredthsFloat(byte[] uint8UnsignedHundredthsFloat, int offset)
        {
            float unsignedFloat = (float)DecodeProtocolUint16(uint8UnsignedHundredthsFloat, offset);
            unsignedFloat /= 100;
            return unsignedFloat;
        }

        public static bool VerifyChecksum(byte[] buffer, int offset, int contentLength)
        {
            // Calculate Checksum
            byte checksum = 0;
            for (int i = 0; i < contentLength; i++)
            {
                checksum += buffer[offset + i];
            }

            // Decode Checksum
            byte decodedChecksum = DecodeUint8(buffer, offset + contentLength);

            return (checksum == decodedChecksum);
        }

        public static byte DecodeUint8(byte[] checksum, int offset)
        {
            byte firstDigit = (byte)(checksum[0 + offset] <= '9' ? checksum[0 + offset] - '0' : ((checksum[0 + offset] - 'A') + 10));
            byte secondDigit = (byte)(checksum[1 + offset] <= '9' ? checksum[1 + offset] - '0' : ((checksum[1 + offset] - 'A') + 10));
            byte decodedChecksum = (byte)((firstDigit * 16) + secondDigit);
            return decodedChecksum;
        }

        public static float DecodeProtocolFloat(byte[] buffer, int offset)
        {
            //TODO: Figure out what encoding this should be in
            string floatString = Encoding.UTF8.GetString(buffer, offset, ProtocolFloatLength);
            return float.Parse(floatString);
        }
    }
}