using System;
using UnitsNet;
using WPILib.Interfaces;
using WPILib.SmartDashboardNS;

namespace WPILib
{
    public class AnalogPotentiometer : IPotentiometer, ISendable, IDisposable
    {
        private readonly AnalogInput m_analogInput;
        private readonly bool m_allocatedChannel;
        private readonly Angle m_fullRange;
        private readonly Angle m_offset;

        public AnalogPotentiometer(int channel, Angle fullRange, Angle offset = default)
        : this(new AnalogInput(channel), fullRange, offset)
        {
            m_allocatedChannel = true;
            SendableRegistry.Instance.AddChild(this, m_analogInput);
        }

        public AnalogPotentiometer(AnalogInput analogInput, Angle fullRange, Angle offset = default)
        {
            m_analogInput = analogInput;
            m_fullRange = fullRange;
            m_offset = offset;
        }



        public Angle Angle => (m_analogInput.Voltage / RobotController.Voltage5V) * m_fullRange + m_offset;

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            if (m_allocatedChannel)
            {
                m_analogInput.Dispose();
            }
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            (m_analogInput as ISendable).InitSendable(builder);
        }
    }
}
