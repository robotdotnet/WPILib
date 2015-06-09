using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Util;
using HAL_Base;

namespace WPILib
{
    public class Relay : SensorBase
    {
        public enum Value
        {
            Off,
            On,
            Forward,
            Reverse,
        }

        public enum Direction
        {
            Both,
            Forward,
            Reverse,
        }

        private int m_channel;
        private IntPtr m_port;

        private Direction m_direction;
        private static Resource s_relayChannels = new Resource(RelayChannels * 2);

        private void InitRelay()
        {
            CheckRelayChannel(m_channel);

            try
            {
                if (m_direction == Direction.Both || m_direction == Direction.Forward)
                {
                    s_relayChannels.Allocate(m_channel * 2);
                    HAL.Report(ResourceType.kResourceType_Relay, (byte)m_channel);
                }
                if (m_direction == Direction.Both || m_direction == Direction.Reverse)
                {
                    s_relayChannels.Allocate(m_channel * 2 + 1);
                    HAL.Report(ResourceType.kResourceType_Relay, (byte)(m_channel + 128));
                }
            }
            catch (CheckedAllocationException)
            {
                throw new AllocationException("Relay channel " + m_channel + " is already allocated");
            }
            int status = 0;
            m_port = HALDigital.InitializeDigitalPort(HAL.GetPort((byte)m_channel), ref status);

        }

        public Relay(int channel, Direction direction)
        {
            m_channel = channel;
            m_direction = direction;
            InitRelay();
            Set(Value.Off);
        }

        public Relay(int channel)
            : this(channel, Direction.Both)
        {

        }

        public override void Dispose()
        {
            if (m_direction == Direction.Both || m_direction == Direction.Forward)
            {
                s_relayChannels.Dispose(m_channel * 2);
            }
            if (m_direction == Direction.Both || m_direction == Direction.Reverse)
            {
                s_relayChannels.Dispose(m_channel * 2 + 1);
            }

            int status = 0;

            HALDigital.SetRelayForward(m_port, false, ref status);
            HALDigital.SetRelayReverse(m_port, false, ref status);

            HALDigital.FreeDIO(m_port, ref status);
        }

        public void Set(Value value)
        {
            int status = 0;
            switch (value)
            {
                case Value.Off:
                    if (m_direction == Direction.Both
                        || m_direction == Direction.Forward)
                    {
                        HALDigital.SetRelayForward(m_port, false, ref status);
                    }
                    if (m_direction == Direction.Both
                        || m_direction == Direction.Reverse)
                    {
                        HALDigital.SetRelayReverse(m_port, false, ref status);
                    }
                    break;
                case Value.On:
                    if (m_direction == Direction.Both
                        || m_direction == Direction.Forward)
                    {
                        HALDigital.SetRelayForward(m_port, true, ref status);
                    }
                    if (m_direction == Direction.Both
                        || m_direction == Direction.Reverse)
                    {
                        HALDigital.SetRelayReverse(m_port, true, ref status);
                    }
                    break;
                case Value.Forward:
                    if (m_direction == Direction.Reverse)
                        throw new InvalidValueException(
                            "A relay configured for reverse cannot be set to forward");
                    if (m_direction == Direction.Both
                        || m_direction == Direction.Forward)
                    {
                        HALDigital.SetRelayForward(m_port, true, ref status);
                    }
                    if (m_direction == Direction.Both)
                    {
                        HALDigital.SetRelayReverse(m_port, false, ref status);
                    }
                    break;
                case Value.Reverse:
                    if (m_direction == Direction.Forward)
                        throw new InvalidValueException(
                            "A relay configured for forward cannot be set to reverse");
                    if (m_direction == Direction.Both)
                    {
                        HALDigital.SetRelayForward(m_port, false, ref status);
                    }
                    if (m_direction == Direction.Both
                        || m_direction == Direction.Reverse)
                    {
                        HALDigital.SetRelayReverse(m_port, true, ref status);
                    }
                    break;
            }
        }

        public Value Get()
        {
            int status = 0;

            if (HALDigital.GetRelayForward(m_port, ref status))
            {
                if (HALDigital.GetRelayReverse(m_port, ref status))
                {
                    return Value.On;
                }
                else
                {
                    if (m_direction == Direction.Forward)
                    {
                        return Value.On;
                    }
                    else
                    {
                        return Value.Forward;
                    }
                }
            }
            else
            {
                if (HALDigital.GetRelayReverse(m_port, ref status))
                {
                    if (m_direction == Direction.Reverse)
                    {
                        return Value.On;
                    }
                    else
                    {
                        return Value.Reverse;
                    }
                }
                else
                {
                    return Value.Off;
                }
            }
        }

        public void SetDirection(Direction direction)
        {
            if (m_direction == direction)
                return;
            Dispose();
            m_direction = direction;
            InitRelay();
        }
    }
}
