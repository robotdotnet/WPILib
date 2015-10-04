using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib.SmartDashboards;

namespace WPILib.Extras.NavX
{
    class SerialIo : IIoProvider
    {
        SerialPort.Port m_serialPortId;
        SerialPort m_serialPort;
        private byte m_nextIntegrationControlAction;
        private bool m_signalTransmitIntegrationControl;
        private bool m_signalRetransmitStreamConfig;
        private bool m_stop;
        private byte m_updateType; //IMUProtocol.MSGID_XXX
        private byte m_updateRateHz;
        int m_byteCount;
        int m_updateCount;
        private IMUProtocol.YprUpdate m_yprUpdateData;
        private IMUProtocol.GyroUpdate m_gyroUpdateData;
        private AHRSProtocol.AHRSUpdate m_ahrsUpdateData;
        private AHRSProtocol.AHRSPosUpdate m_ahrsposUpdateData;
        private AHRSProtocol.BoardId m_boardId;
        IIoCompleteNotification m_notifySink;
        BoardState m_boardState;
        IBoardCapabilities m_boardCapabilities;
        double m_lastValidPacketTime;

        const bool Debug = true; /* Set to true to enable debug output (to smart dashboard) */
    
    public SerialIo(SerialPort.Port portId, byte updateRateHz, bool processedData, IIoCompleteNotification notifySink, IBoardCapabilities boardCapabilities)
        {
            this.m_serialPortId = portId;
            m_yprUpdateData = new IMUProtocol.YprUpdate();
            m_gyroUpdateData = new IMUProtocol.GyroUpdate();
            m_ahrsUpdateData = new AHRSProtocol.AHRSUpdate();
            m_ahrsposUpdateData = new AHRSProtocol.AHRSPosUpdate();
            m_boardId = new AHRSProtocol.BoardId();
            m_boardState = new BoardState();
            this.m_notifySink = notifySink;
            this.m_boardCapabilities = boardCapabilities;
            m_serialPort = GetMaybeCreateSerialPort();
            this.m_updateRateHz = updateRateHz;
            if (processedData)
            {
                m_updateType = AHRSProtocol.MsgidAhrsposUpdate;
            }
            else
            {
                m_updateType = IMUProtocol.MsgidGyroUpdate;
            }
        }

        protected SerialPort ResetSerialPort()
        {
            if (m_serialPort != null)
            {
                try
                {
                    m_serialPort.Dispose();
                }
                catch (Exception ex)
                {
                    // This has been seen to happen before....
                }
                m_serialPort = null;
            }
            m_serialPort = GetMaybeCreateSerialPort();
            return m_serialPort;
        }

        protected SerialPort GetMaybeCreateSerialPort()
        {
            if (m_serialPort == null)
            {
                try
                {
                    m_serialPort = new SerialPort(57600, m_serialPortId);
                    m_serialPort.ReadBufferSize = 256;
                    m_serialPort.Timeout = 1.0;
                    m_serialPort.EnableTermination('\n');
                    m_serialPort.Reset();
                }
                catch (Exception ex)
                {
                    /* Error opening serial port. Perhaps it doesn't exist... */
                    m_serialPort = null;
                }
            }
            return m_serialPort;
        }

        protected void EnqueueIntegrationControlMessage(byte action)
        {
            m_nextIntegrationControlAction = action;
            m_signalTransmitIntegrationControl = true;
        }

        protected void DispatchStreamResponse(IMUProtocol.StreamResponse response)
        {
            m_boardState.CalStatus = (byte)(response.Flags & IMUProtocol.Nav6FlagMaskCalibrationState);
            m_boardState.CapabilityFlags = (short)(response.Flags & ~IMUProtocol.Nav6FlagMaskCalibrationState);
            m_boardState.OpStatus = 0x04; /* TODO:  Create a symbol for this */
            m_boardState.SelftestStatus = 0x07; /* TODO:  Create a symbol for this */
            m_boardState.AccelFsrG = response.AccelFsrG;
            m_boardState.GyroFsrDps = response.GyroFsrDps;
            m_boardState.UpdateRateHz = (byte)response.UpdateRateHz;
            m_notifySink.SetBoardState(m_boardState);
            /* If AHRSPOS is update type is request, but board doesn't support it, */
            /* retransmit the stream config, falling back to AHRS Update mode.     */
            if (this.m_updateType == AHRSProtocol.MsgidAhrsposUpdate)
            {
                if (!m_boardCapabilities.IsDisplacementSupported())
                {
                    this.m_updateType = AHRSProtocol.MsgidAHRSUpdate;
                    m_signalRetransmitStreamConfig = true;
                }
            }
        }

        protected int decodePacketHandler(byte[] receivedData, int offset, int bytesRemaining)
        {

            int packetLength;

            if ((packetLength = IMUProtocol.DecodeYprUpdate(receivedData, offset, bytesRemaining, m_yprUpdateData)) > 0)
            {
                m_notifySink.SetYawPitchRoll(m_yprUpdateData);
            }
            else if ((packetLength = AHRSProtocol.DecodeAHRSPosUpdate(receivedData, offset, bytesRemaining, m_ahrsposUpdateData)) > 0)
            {
                m_notifySink.SetAHRSPosData(m_ahrsposUpdateData);
            }
            else if ((packetLength = AHRSProtocol.DecodeAHRSUpdate(receivedData, offset, bytesRemaining, m_ahrsUpdateData)) > 0)
            {
                m_notifySink.SetAHRSData(m_ahrsUpdateData);
            }
            else if ((packetLength = IMUProtocol.DecodeGyroUpdate(receivedData, offset, bytesRemaining, m_gyroUpdateData)) > 0)
            {
                m_notifySink.SetRawData(m_gyroUpdateData);
            }
            else if ((packetLength = AHRSProtocol.DecodeBoardIdGetResponse(receivedData, offset, bytesRemaining, m_boardId)) > 0)
            {
                m_notifySink.SetBoardId(m_boardId);
            }
            else
            {
                packetLength = 0;
            }
            return packetLength;
        }

        public void Run()
        {

            m_stop = false;
            bool streamResponseReceived = false;
            double lastStreamCommandSentTimestamp = 0.0;
            double lastDataReceivedTimestamp = 0;
            double lastSecondStartTime = 0;

            int partialBinaryPacketCount = 0;
            int streamResponseReceiveCount = 0;
            int timeoutCount = 0;
            int discardedBytesCount = 0;
            int portResetCount = 0;
            int updatesInLastSecond = 0;
            int integrationResponseReceiveCount = 0;

            try
            {
                m_serialPort.ReadBufferSize = (256);
                m_serialPort.Timeout = (1.0);
                m_serialPort.EnableTermination('\n');
                m_serialPort.Flush();
                m_serialPort.Reset();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            byte[] streamCommand = new byte[256];
            byte[] integrationControlCommand = new byte[256];
            IMUProtocol.StreamResponse response = new IMUProtocol.StreamResponse();
            AHRSProtocol.IntegrationControl integrationControl = new AHRSProtocol.IntegrationControl();
            AHRSProtocol.IntegrationControl integrationControlResponse = new AHRSProtocol.IntegrationControl();

            int cmdPacketLength = IMUProtocol.EncodeStreamCommand(streamCommand, m_updateType, m_updateRateHz);
            try
            {
                m_serialPort.Reset();
                m_serialPort.Write(streamCommand, cmdPacketLength);
                cmdPacketLength = AHRSProtocol.EncodeDataGetRequest(streamCommand, AHRSProtocol.AHRSDataType.BoardIdentity, (byte)0);
                m_serialPort.Write(streamCommand, cmdPacketLength);
                m_serialPort.Flush();
                portResetCount++;
                if (Debug)
                {

                    SmartDashboards.SmartDashboard.PutNumber("navX Port Resets", (double)portResetCount);
                }
                lastStreamCommandSentTimestamp = Timer.GetFPGATimestamp();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            int remainderBytes = 0;
            byte[] remainderData = null;

            while (!m_stop)
            {
                try
                {

                    // Wait, with delays to conserve CPU resources, until
                    // bytes have arrived.

                    if (m_signalTransmitIntegrationControl)
                    {
                        integrationControl.Action = m_nextIntegrationControlAction;
                        m_signalTransmitIntegrationControl = false;
                        m_nextIntegrationControlAction = 0;
                        cmdPacketLength = AHRSProtocol.EncodeIntegrationControlCmd(integrationControlCommand, integrationControl);
                        try
                        {
                            m_serialPort.Write(integrationControlCommand, cmdPacketLength);
                        }
                        catch (Exception ex2)
                        {
                            Console.WriteLine(ex2.StackTrace);
                        }
                    }

                    if (!m_stop && (remainderBytes == 0) && (m_serialPort.GetBytesReceived() < 1))
                    {
                        Timer.Delay(1.0 / m_updateRateHz);
                    }

                    int packetsReceived = 0;
                    byte[] receivedData = m_serialPort.Read(256);
                    int bytesRead = receivedData.Length;
                    m_byteCount += bytesRead;

                    /* If a partial packet remains from last iteration, place that at  */
                    /* the start of the data buffer, and append any new data available */
                    /* at the serial port.                                             */

                    if (remainderBytes > 0)
                    {
                        byte[] resizedArray = new byte[remainderBytes + bytesRead];
                        Array.Copy(remainderData, 0, resizedArray, 0 ,remainderBytes);
                        Array.Copy(receivedData, 0, resizedArray, remainderBytes, bytesRead);
                        receivedData = resizedArray;
                        bytesRead += remainderBytes;
                        remainderBytes = 0;
                        remainderData = null;
                    }

                    if (bytesRead > 0)
                    {
                        lastDataReceivedTimestamp = Timer.GetFPGATimestamp();
                        int i = 0;
                        // Scan the buffer looking for valid packets
                        while (i < bytesRead)
                        {

                            // Attempt to decode a packet

                            int bytesRemaining = bytesRead - i;

                            if (receivedData[i] != IMUProtocol.PacketStartChar)
                            {
                                /* Skip over received bytes until a packet start is detected. */
                                i++;
                                discardedBytesCount++;
                                if (Debug)
                                {
                                    SmartDashboards.SmartDashboard.PutNumber("navX Discarded Bytes", (double)discardedBytesCount);
                                }
                                continue;
                            }
                            else
                            {
                                if ((bytesRemaining > 2) &&
                                        (receivedData[i + 1] == AHRSProtocol.BinaryPacketIndicatorChar))
                                {
                                    /* Binary packet received; next byte is packet Length-2 */
                                    byte totalExpectedBinaryDataBytes = receivedData[i + 2];
                                    totalExpectedBinaryDataBytes += 2;
                                    while (bytesRemaining < totalExpectedBinaryDataBytes)
                                    {

                                        /* This binary packet contains an embedded     */
                                        /* end-of-line character.  Continue to receive */
                                        /* more data until entire packet is received.  */
                                        byte[] additionalReceivedData = m_serialPort.Read(256);
                                        m_byteCount += additionalReceivedData.Length;
                                        bytesRemaining += additionalReceivedData.Length;

                                        /* Resize array to hold existing and new data */
                                        byte[] c = new byte[receivedData.Length + additionalReceivedData.Length];
                                        if (c.Length > 0)
                                        {
                                            Array.Copy(receivedData, 0, c, 0, receivedData.Length);
                                            Array.Copy(additionalReceivedData, 0, c, receivedData.Length, additionalReceivedData.Length);
                                            receivedData = c;
                                        }
                                        else
                                        {
                                            /* Timeout waiting for remainder of binary packet */
                                            i++;
                                            bytesRemaining--;
                                            partialBinaryPacketCount++;
                                            if (Debug)
                                            {
                                                SmartDashboard.PutNumber("navX Partial Binary Packets", (double)partialBinaryPacketCount);
                                            }
                                            continue;
                                        }
                                    }
                                }
                            }

                            int packetLength = decodePacketHandler(receivedData, i, bytesRemaining);
                            if (packetLength > 0)
                            {
                                packetsReceived++;
                                m_updateCount++;
                                m_lastValidPacketTime = Timer.GetFPGATimestamp();
                                updatesInLastSecond++;
                                if ((m_lastValidPacketTime - lastSecondStartTime) > 1.0)
                                {
                                    if (Debug)
                                    {
                                        SmartDashboard.PutNumber("navX Updates Per Sec", (double)updatesInLastSecond);
                                    }
                                    updatesInLastSecond = 0;
                                    lastSecondStartTime = m_lastValidPacketTime;
                                }
                                i += packetLength;
                            }
                            else
                            {
                                packetLength = IMUProtocol.DecodeStreamResponse(receivedData, i, bytesRemaining, response);
                                if (packetLength > 0)
                                {
                                    packetsReceived++;
                                    DispatchStreamResponse(response);
                                    streamResponseReceived = true;
                                    i += packetLength;
                                    streamResponseReceiveCount++;
                                    if (Debug)
                                    {
                                        SmartDashboard.PutNumber("navX Stream Responses", (double)streamResponseReceiveCount);
                                    }
                                }
                                else
                                {
                                    packetLength = AHRSProtocol.DecodeIntegrationControlResponse(receivedData, i, bytesRemaining,
                                            integrationControlResponse);
                                    if (packetLength > 0)
                                    {
                                        // Confirmation of integration control
                                        integrationResponseReceiveCount++;
                                        if (Debug)
                                        {
                                            SmartDashboard.PutNumber("navX Integration Control Response Count", integrationResponseReceiveCount);
                                        }
                                        i += packetLength;
                                    }
                                    else
                                    {
                                        /* Even though a start-of-packet indicator was found, the  */
                                        /* current index is not the start of a packet if interest. */
                                        /* Scan to the beginning of the next packet,               */
                                        bool nextPacketStartFound = false;
                                        int x;
                                        for (x = 0; x < bytesRemaining; x++)
                                        {
                                            if (receivedData[i + x] != IMUProtocol.PacketStartChar)
                                            {
                                                x++;
                                            }
                                            else
                                            {
                                                i += x;
                                                bytesRemaining -= x;
                                                if (x != 0)
                                                {
                                                    nextPacketStartFound = true;
                                                }
                                                break;
                                            }
                                        }
                                        bool discardRemainder = false;
                                        if (!nextPacketStartFound && x == bytesRemaining)
                                        {
                                            /* Remaining bytes don't include a start-of-packet */
                                            discardRemainder = true;
                                        }
                                        bool partialPacket = false;
                                        if (discardRemainder)
                                        {
                                            /* Discard the remainder */
                                            i = bytesRemaining;
                                        }
                                        else
                                        {
                                            if (!nextPacketStartFound)
                                            {
                                                /* This occurs when packets are received that are not decoded.   */
                                                /* Bump over this packet and prepare for the next.               */
                                                if ((bytesRemaining > 2) &&
                                                        (receivedData[i + 1] == AHRSProtocol.BinaryPacketIndicatorChar))
                                                {
                                                    /* Binary packet received; next byte is packet Length-2 */
                                                    int pktLen = receivedData[i + 2];
                                                    pktLen += 2;
                                                    if (bytesRemaining >= pktLen)
                                                    {
                                                        bytesRemaining -= pktLen;
                                                        i += pktLen;
                                                        discardedBytesCount += pktLen;
                                                        if (Debug)
                                                        {
                                                            SmartDashboard.PutNumber("navX Discarded Bytes", (double)discardedBytesCount);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        /* This is the initial portion of a partial binary packet. */
                                                        /* Keep this data and attempt to acquire the remainder.    */
                                                        partialPacket = true;
                                                    }
                                                }
                                                else
                                                {
                                                    /* Ascii packet received. */
                                                    /* Scan up to and including next end-of-packet character       */
                                                    /* sequence, or the beginning of a new packet.                 */
                                                    for (x = 0; x < bytesRemaining; x++)
                                                    {
                                                        if (receivedData[i + x] == (byte)'\r')
                                                        {
                                                            i += x + 1;
                                                            bytesRemaining -= (x + 1);
                                                            discardedBytesCount += x + 1;
                                                            if ((bytesRemaining > 0) && receivedData[i] == (byte)'\n')
                                                            {
                                                                bytesRemaining--;
                                                                i++;
                                                                discardedBytesCount++;
                                                            }
                                                            if (Debug)
                                                            {
                                                                SmartDashboard.PutNumber("navX Discarded Bytes", (double)discardedBytesCount);
                                                            }
                                                            break;
                                                        }
                                                        /* If a new start-of-packet is found, discard */
                                                        /* the ascii packet bytes that precede it.    */
                                                        if (receivedData[i + x] == (byte)'!')
                                                        {
                                                            if (x > 0)
                                                            {
                                                                i += x;
                                                                bytesRemaining -= x;
                                                                discardedBytesCount += x;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                /* start of packet found, but no termination     */
                                                                /* Time to get some more data, unless the bytes  */
                                                                /* remaining are larger than a valid packet size */
                                                                if (bytesRemaining < IMUProtocol.IMUProtocolMaxMessageLength)
                                                                {
                                                                    /* Get more data */
                                                                    partialPacket = true;
                                                                }
                                                                else
                                                                {
                                                                    i++;
                                                                    bytesRemaining--;
                                                                }
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    if (x == bytesRemaining)
                                                    {
                                                        /* Partial ascii packet - keep the remainder */
                                                        partialPacket = true;
                                                    }
                                                }
                                            }
                                        }
                                        if (partialPacket)
                                        {
                                            remainderData = new byte[bytesRemaining];
                                            Array.Copy(receivedData, i, remainderData, 0, bytesRemaining);
                                            remainderBytes = bytesRemaining;
                                            i = bytesRead;
                                        }
                                    }
                                }
                            }
                        }

                        if ((packetsReceived == 0) && (bytesRead == 256))
                        {
                            // Workaround for issue found in SerialPort implementation:
                            // No packets received and 256 bytes received; this
                            // condition occurs in the SerialPort.  In this case,
                            // reset the serial port.
                            m_serialPort.Flush();
                            m_serialPort.Reset();
                            portResetCount++;
                            if (Debug)
                            {
                                SmartDashboard.PutNumber("navX Port Resets", (double)portResetCount);
                            }
                        }

                        bool retransmitStreamConfig = false;
                        if (m_signalRetransmitStreamConfig)
                        {
                            retransmitStreamConfig = true;
                            m_signalRetransmitStreamConfig = false;
                        }

                        // If a stream configuration response has not been received within three seconds
                        // of operation, (re)send a stream configuration request

                        if (retransmitStreamConfig ||
                                (!streamResponseReceived && ((Timer.GetFPGATimestamp() - lastStreamCommandSentTimestamp) > 3.0)))
                        {
                            cmdPacketLength = IMUProtocol.EncodeStreamCommand(streamCommand, m_updateType, m_updateRateHz);
                            try
                            {
                                ResetSerialPort();
                                lastStreamCommandSentTimestamp = Timer.GetFPGATimestamp();
                                m_serialPort.Write(streamCommand, cmdPacketLength);
                                cmdPacketLength = AHRSProtocol.EncodeDataGetRequest(streamCommand, AHRSProtocol.AHRSDataType.BoardIdentity, (byte)0);
                                m_serialPort.Write(streamCommand, cmdPacketLength);
                                m_serialPort.Flush();
                            }
                            catch (Exception ex2)
                            {
                                Console.WriteLine(ex2.StackTrace);
                            }
                        }
                        else
                        {
                            // If no bytes remain in the buffer, and not awaiting a response, sleep a bit
                            if (streamResponseReceived && (m_serialPort.GetBytesReceived() == 0))
                            {
                                Timer.Delay(1.0 / m_updateRateHz);
                            }
                        }

                        /* If receiving data, but no valid packets have been received in the last second */
                        /* the navX MXP may have been reset, but no exception has been detected.         */
                        /* In this case , trigger transmission of a new stream_command, to ensure the    */
                        /* streaming packet type is configured correctly.                                */

                        if ((Timer.GetFPGATimestamp() - m_lastValidPacketTime) > 1.0)
                        {
                            lastStreamCommandSentTimestamp = 0.0;
                            streamResponseReceived = false;
                        }
                    }
                    else
                    {
                        /* No data received this time around */
                        if (Timer.GetFPGATimestamp() - lastDataReceivedTimestamp > 1.0)
                        {
                            ResetSerialPort();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // This exception typically indicates a Timeout, but can also be a buffer overrun error.
                    streamResponseReceived = false;
                    timeoutCount++;
                    if (Debug)
                    {
                        SmartDashboard.PutNumber("navX Serial Port Timeout / Buffer Overrun", (double)timeoutCount);
                        SmartDashboard.PutString("navX Last Exception", ex.Message + "; " + ex.ToString());
                    }
                    Console.WriteLine(ex.StackTrace);
                    ResetSerialPort();
                }
            }
        }

        /**
         * Indicates whether the navX MXP is currently connected
         * to the host computer.  A connection is considered established
         * whenever a value update packet has been received from the
         * navX MXP within the last second.
         * @return Returns true if a valid update has been received within the last second.
         */
        public bool IsConnected()
        {
            double timeSinceLastUpdate = Timer.GetFPGATimestamp() - this.m_lastValidPacketTime;
            return timeSinceLastUpdate <= 1.0;
        }

        /**
         * Returns the count in bytes of data received from the
         * navX MXP.  This could can be useful for diagnosing 
         * connectivity issues.
         * 
         * If the byte count is increasing, but the update count
         * (see getUpdateCount()) is not, this indicates a software
         * misconfiguration.
         * @return The number of bytes received from the navX MXP.
         */
        public double GetByteCount()
        {
            return m_byteCount;
        }

        /**
         * Returns the count of valid update packets which have
         * been received from the navX MXP.  This count should increase
         * at the same rate indicated by the configured update rate.
         * @return The number of valid updates received from the navX MXP.
         */
        public double GetUpdateCount()
        {
            return m_updateCount;
        }

        
    public void SetUpdateRateHz(byte updateRate)
        {
            m_updateRateHz = updateRate;
        }

        
    public void ZeroYaw()
        {
            EnqueueIntegrationControlMessage(AHRSProtocol.NavxIntegrationCtlResetYaw);
        }

        
    public void ZeroDisplacement()
        {
            EnqueueIntegrationControlMessage((byte)(AHRSProtocol.NavxIntegrationCtlResetDispX |
                                                     AHRSProtocol.NavxIntegrationCtlResetDispY |
                                                     AHRSProtocol.NavxIntegrationCtlResetDispZ));
        }

        
    public void Stop()
        {
            m_stop = true;
        }
    }
}
