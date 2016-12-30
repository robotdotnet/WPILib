using System;

namespace CTRE
{
    public abstract class CANBusDevice : CTRECanNode
    {
        protected uint _deviceNumber;

        public const UInt32 kFullMessageIDMask = 0x1fffffff;

        public CANBusDevice(uint deviceNumber) : base((int)deviceNumber)
        {
            _deviceNumber = deviceNumber;
        }
        public uint GetDeviceNumber()
        {
            return _deviceNumber;
        }

        public enum Codes
        {
            CAN_OK = StatusCodes.OK,
            CAN_MSG_STALE = StatusCodes.CAN_MSG_STALE,
            CAN_TX_FULL = StatusCodes.CAN_TX_FULL,
            CAN_INVALID_PARAM = StatusCodes.CAN_INVALID_PARAM,
            CAN_MSG_NOT_FOUND = StatusCodes.CAN_MSG_NOT_FOUND,
            CAN_NO_MORE_TX_JOBS = StatusCodes.CAN_NO_MORE_TX_JOBS,
            CAN_NO_SESSIONS_AVAIL = StatusCodes.CAN_NO_SESSIONS_AVAIL,
            CAN_OVERFLOW = StatusCodes.CAN_OVERFLOW,

            CTR_SigNotUpdated = StatusCodes.SIG_NOT_UPDATED,
        };
    }
}