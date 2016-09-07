using System;
using HAL.Base;
using NetworkTables;
using NetworkTables.Tables;
using WPILib.Exceptions;
using WPILib.Interfaces;
using WPILib.LiveWindow;
using static WPILib.Utility;
using static HAL.Base.HAL;
using static HAL.Base.HALRelay;
using static HAL.Base.HALPorts;

namespace WPILib
{
    /// <summary>
    /// The Relay class is used to interface with the Relay ports on the RoboRIO. 
    /// These are usually used with Spike Relays.
    /// </summary>
    public class Relay : SensorBase, IMotorSafety, ILiveWindowSendable, ITableListener
    {
        /// <summary>
        /// The value to set the relay to.
        /// </summary>
        public enum Value
        {
            /// Off
            Off,
            /// On
            On,
            /// Forward
            Forward,
            /// Reverse
            Reverse,
        }

        /// <summary>
        /// The directions the relay is allowed to output.
        /// </summary>
        public enum Direction
        {
            /// <summary>
            /// Relay allowed in both directions.
            /// </summary>
            Both,
            /// <summary>
            /// Relay allowed only in forward direction.
            /// </summary>
            Forward,
            /// <summary>
            /// Relay allowed only in reverse direction.
            /// </summary>
            Reverse,
        }

        private readonly int m_channel;
        private int m_forwardHandle = 0;
        private int m_reverseHandle = 0;

        private Direction m_direction;

        private MotorSafetyHelper m_safetyHelper;

        private void InitRelay()
        {
            CheckRelayChannel(m_channel);

            int status = 0;
            int portHandle = HAL_GetPort(m_channel);
            if (m_direction == Direction.Both || m_direction == Direction.Forward)
            {
                m_forwardHandle = HAL_InitializeRelayPort(portHandle, true, ref status);
                CheckStatusRange(status, 0, HAL_GetNumRelayHeaders(), m_channel);
                Report(ResourceType.kResourceType_Relay, m_channel);
            }
            if (m_direction == Direction.Both || m_direction == Direction.Reverse)
            {
                m_reverseHandle = HAL_InitializeRelayPort(portHandle, false, ref status);
                CheckStatusRange(status, 0, HAL_GetNumRelayHeaders(), m_channel);
                Report(ResourceType.kResourceType_Relay, m_channel + 128);
            }

            m_safetyHelper = new MotorSafetyHelper(this);
            m_safetyHelper.SafetyEnabled = false;

            LiveWindow.LiveWindow.AddActuator("Relay", m_channel, this);
        }

        /// <summary>
        /// Creates a new relay, specifying the directions allowed.
        /// </summary>
        /// <param name="channel">The relay channel</param>
        /// <param name="direction">The directions allowed.</param>
        public Relay(int channel, Direction direction)
        {
            m_channel = channel;
            m_direction = direction;
            InitRelay();
            Set(Value.Off);
        }

        /// <summary>
        /// Creates a new relay, allowing both directions.
        /// </summary>
        /// <param name="channel"></param>
        public Relay(int channel)
            : this(channel, Direction.Both)
        {

        }

        ///<inheritdoc/>
        public override void Dispose()
        {
            int status = 0;
            HAL_SetRelay(m_forwardHandle, false, ref status);
            // Ignore status
            HAL_SetRelay(m_reverseHandle, false, ref status);

            HAL_FreeRelayPort(m_forwardHandle);
            HAL_FreeRelayPort(m_reverseHandle);
            m_forwardHandle = 0;
            m_reverseHandle = 0;
        }

        /// <summary>
        /// Sets the relay output.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public virtual void Set(Value value)
        {
            int status = 0;
            switch (value)
            {
                case Value.Off:
                    if (m_direction == Direction.Both
                        || m_direction == Direction.Forward)
                    {
                        HAL_SetRelay(m_forwardHandle, false, ref status);
                    }
                    if (m_direction == Direction.Both
                        || m_direction == Direction.Reverse)
                    {
                        HAL_SetRelay(m_reverseHandle, false, ref status);
                    }
                    break;
                case Value.On:
                    if (m_direction == Direction.Both
                        || m_direction == Direction.Forward)
                    {
                        HAL_SetRelay(m_forwardHandle, true, ref status);
                    }
                    if (m_direction == Direction.Both
                        || m_direction == Direction.Reverse)
                    {
                        HAL_SetRelay(m_reverseHandle, true, ref status);
                    }
                    break;
                case Value.Forward:
                    if (m_direction == Direction.Reverse)
                        throw new InvalidValueException(
                            "A relay configured for reverse cannot be set to forward");
                    if (m_direction == Direction.Both
                        || m_direction == Direction.Forward)
                    {
                        HAL_SetRelay(m_forwardHandle, true, ref status);
                    }
                    if (m_direction == Direction.Both)
                    {
                        HAL_SetRelay(m_reverseHandle, false, ref status);
                    }
                    break;
                case Value.Reverse:
                    if (m_direction == Direction.Forward)
                        throw new InvalidValueException(
                            "A relay configured for forward cannot be set to reverse");
                    if (m_direction == Direction.Both)
                    {
                        HAL_SetRelay(m_forwardHandle, false, ref status);
                    }
                    if (m_direction == Direction.Both
                        || m_direction == Direction.Reverse)
                    {
                        HAL_SetRelay(m_reverseHandle, true, ref status);
                    }
                    break;
            }
            CheckStatus(status);
        }

        /// <summary>
        /// Gets the latest relay value sent.
        /// </summary>
        /// <returns>The latest relay value.</returns>
        public virtual Value Get()
        {
            int status = 0;

            if (HAL_GetRelay(m_forwardHandle, ref status))
            {
                if (HAL_GetRelay(m_reverseHandle, ref status))
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
                if (HAL_GetRelay(m_reverseHandle, ref status))
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

        /// <summary>
        /// Sets the direction allowed by the relay.
        /// </summary>
        /// <param name="direction">The directions to allow.</param>
        public void SetDirection(Direction direction)
        {
            if (m_direction == direction)
                return;
            Dispose();
            m_direction = direction;
            InitRelay();
        }
        ///<inheritdoc />
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        ///<inheritdoc />
        public ITable Table { get; private set; }
        ///<inheritdoc />
        public string SmartDashboardType => "Relay";
        ///<inheritdoc />
        public void UpdateTable()
        {
            if (Table != null)
            {
                switch (Get())
                {
                    case Value.On:
                        Table.PutString("Value", "On");
                        break;
                    case Value.Forward:
                        Table.PutString("Value", "Forward");
                        break;
                    case Value.Reverse:
                        Table.PutString("Value", "Reverse");
                        break;
                    default:
                        Table.PutString("Value", "Off");
                        break;
                }
            }
        }

        

        ///<inheritdoc />
        public void StartLiveWindowMode()
        {
            Table.AddTableListener("Value", this, true);
        }
        ///<inheritdoc />
        public void StopLiveWindowMode()
        {
            Table.RemoveTableListener(this);
        }

        ///<inheritdoc/>
        public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
        {
            string val = ((string)value);
            if (val.Equals("Off"))
            {
                Set(Value.Off);
            }
            else if (val.Equals("On"))
            {
                Set(Value.On);
            }
            else if (val.Equals("Forward"))
            {
                Set(Value.Forward);
            }
            else if (val.Equals("Reverse"))
            {
                Set(Value.Reverse);
            }
        }
        /// <summary>
        /// Gets the channel of the relay.
        /// </summary>
        public int Channel => m_channel;
        /// <inheritdoc/>
        public double Expiration {
            get { return m_safetyHelper.Expiration; }
            set { m_safetyHelper.Expiration = value; } }
        /// <inheritdoc/>
        public bool Alive => m_safetyHelper.Alive;
        /// <inheritdoc/>
        public void StopMotor()
        {
            Set(Value.Off);
        }

        /// <inheritdoc/>
        public bool SafetyEnabled {
            get { return m_safetyHelper.SafetyEnabled; }
            set { m_safetyHelper.SafetyEnabled = value; } }
        /// <inheritdoc/>
        public string Description => $"Relay ID {Channel}";
    }
}
