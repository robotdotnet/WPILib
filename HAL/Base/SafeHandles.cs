using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HAL.Base
{
    public class DigitalPortSafeHandle : SafeHandle
    {
        private bool m_simulator;
        private SimulatorHAL.DigitalPort m_digitalPort;
        private bool m_valid = true;

        public DigitalPortSafeHandle() : base(IntPtr.Zero, true)
        {
            m_simulator = false;
        }

        internal DigitalPortSafeHandle(SimulatorHAL.DigitalPort dPort) : base(IntPtr.Zero, true)
        {
            m_valid = true;
            m_digitalPort = dPort;
        }

        internal SimulatorHAL.DigitalPort GetSimulatorPort()
        {
            return m_digitalPort;
        }

        public override bool IsInvalid
        {
            get
            {
                if (m_simulator) return m_valid;
                return handle != IntPtr.Zero;
            }
        }

        protected override bool ReleaseHandle()
        {
            m_valid = false;
            HALDigital.FreeDigitalPort(this);
            handle = IntPtr.Zero;
            return true;
        }
    }


    public class AnalogInputPortSafeHandle : SafeHandle
    {
        private bool m_simulator;
        private SimulatorHAL.AnalogPort m_digitalPort;
        private bool m_valid = true;

        public AnalogInputPortSafeHandle() : base(IntPtr.Zero, true)
        {
            m_simulator = false;
        }

        internal AnalogInputPortSafeHandle(SimulatorHAL.AnalogPort dPort) : base(IntPtr.Zero, true)
        {
            m_valid = true;
            m_digitalPort = dPort;
        }

        public override bool IsInvalid
        {
            get
            {
                if (m_simulator) return m_valid;
                return handle != IntPtr.Zero;
            }
        }

        protected override bool ReleaseHandle()
        {
            m_valid = false;
            HALAnalog.FreeAnalogInputPort(handle);
            handle = IntPtr.Zero;
            return true;
        }
    }
}
