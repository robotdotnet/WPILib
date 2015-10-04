using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    class BoardState
    {
        public byte OpStatus;
        public short SensorStatus;
        public byte CalStatus;
        public byte SelftestStatus;
        public short CapabilityFlags;
        public byte UpdateRateHz;
        public short AccelFsrG;
        public short GyroFsrDps;
    }
    interface IIoCompleteNotification
    {
        
        void SetYawPitchRoll(IMUProtocol.YprUpdate yprUpdate);
        void SetAHRSData(AHRSProtocol.AHRSUpdate ahrsUpdate);
        void SetAHRSPosData(AHRSProtocol.AHRSPosUpdate ahrsUpdate);
        void SetRawData(IMUProtocol.GyroUpdate rawDataUpdate);
        void SetBoardId(AHRSProtocol.BoardId boardId);
        void SetBoardState(BoardState boardState);
    }
}
