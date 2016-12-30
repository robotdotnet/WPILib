using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Base;
using HAL = HAL.Base.HAL;

namespace CTRE
{
    public class CTRECanNode
    {
        public CTRECanNode(int deviceNumber)
        {
            _deviceNumber = deviceNumber;
        }

        public int GetDeviceNumber() => _deviceNumber;

        protected struct txTask
        {
            public uint arbId;
            public byte[] toSend;

            public bool IsEmpty()
            {
                if (toSend == null) return true;
                return false;
            }
        }

        protected struct recMsg
        {
            public uint arbId;
            public ulong bytes;
            public CTR_Code err;
        }

        protected int _deviceNumber;

        protected void RegisterRx(uint arbId)
        {
            // No op
        }

        protected void RegisterTx(uint arbId, uint periodMs)
        {
            RegisterTx(arbId, periodMs, 8, null);
        }

        protected void RegisterTx(uint arbId, uint periodMs, uint dlc, byte[] initialFrame)
        {
            int status = 0;
            if (dlc > 8)
                dlc = 8;
            txJob_t job = new txJob_t();
            job.arbId = arbId;
            job.periodMs = periodMs;
            job.dlc = (byte)dlc;
            job.toSend = new byte[8];
            if (initialFrame != null)
            {
                if (dlc > initialFrame.Length) dlc = (uint)initialFrame.Length;
                Array.Copy(initialFrame, job.toSend, dlc);
            }
            if (!_txJobs.ContainsKey(arbId))
            {
                _txJobs.Add(arbId, job);
            }
            else
            {
                _txJobs[arbId] = job;
            }
            HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage2(job.arbId, job.toSend, job.dlc, (int)job.periodMs,
                ref status);
        }

        protected void UnregisterTx(uint arbId)
        {
            ChangeTxPeriod(arbId, 0);
            _txJobs.Remove(arbId);
        }

        protected CTR_Code GetRx(uint arbId, ref ulong data, uint timeoutMs)
        {
            CTR_Code retVal = CTR_Code.CTR_OKAY;
            int status = 0;
            byte len = 0;
            uint timeStamp = 0;

            if (timeoutMs > 999)
                timeoutMs = 999;
            HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessage(ref arbId, 0x1fffffff, ref data, ref len,
                ref timeStamp, ref status);

            if (status == 0)
            {
                // fresh update
                rxEvent_t r;
                if (_rxRxEvents.TryGetValue(arbId, out r))
                {
                    r.time = global::HAL.Base.HAL.HAL_GetFPGATime(ref status);
                    r.bytes = data;
                }
            }
            else
            {
                // Did not get the message
                rxEvent_t r;
                if (!_rxRxEvents.TryGetValue(arbId, out r))
                {
                    retVal = CTR_Code.CTR_RxTimeout;
                    data = 0;
                }
                else
                {
                    data = r.bytes;

                    ulong temp = global::HAL.Base.HAL.HAL_GetFPGATime(ref status);
                    temp = temp - r.time;
                    if (temp > 1000000)
                    {
                        // Greater then 1 sec
                        retVal = CTR_Code.CTR_RxTimeout;
                    }
                    else if (temp > timeoutMs*1000)
                    {
                        retVal = CTR_Code.CTR_RxTimeout;
                    }
                    else
                    {
                        // Last update was good enough
                    }
                }
            }
            return retVal;
        }

        protected void FlushTx(uint arbId)
        {
            int status = 0;
            if (_txJobs.ContainsKey(arbId))
            {
                txJob_t job = _txJobs[arbId];
                HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage2(job.arbId, job.toSend, job.dlc, (int)job.periodMs,
                    ref status);

            }
        }

        protected bool ChangeTxPeriod(uint arbId, uint periodMs)
        {
            int status = 0;
            if (_txJobs.ContainsKey(arbId))
            {
                txJob_t job = _txJobs[arbId];
                job.periodMs = periodMs;
                _txJobs[arbId] = job;
                HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage2(job.arbId, job.toSend, job.dlc, (int)job.periodMs,
                    ref status);

                return true;
            }
            return false;
        }

        protected txTask GetTx(uint arbId)
        {
            txTask retVal = new txTask();
            if (_txJobs.ContainsKey(arbId))
            {
                txJob_t t = _txJobs[arbId];
                retVal.arbId = t.arbId;
                retVal.toSend = t.toSend;
            }
            return retVal;
        }

        protected void FlushTx(ref uint arbId)
        {
            FlushTx(arbId);
        }

        protected recMsg GetRx(uint arbId, uint timeoutMs)
        {
            recMsg retVal = new recMsg();
            retVal.err = GetRx(arbId, ref retVal.bytes, timeoutMs);
            return retVal;
        }

        private struct txJob_t
        {
            public uint arbId;
            public byte[] toSend;
            public uint periodMs;
            public byte dlc;
        }

        private struct rxEvent_t
        {
            public ulong bytes;
            public ulong time;
        }

        private Dictionary<uint, txJob_t> _txJobs;
        private Dictionary<uint, rxEvent_t> _rxRxEvents;


    }
}
