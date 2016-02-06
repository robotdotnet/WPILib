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

    public class CounterSafeHandle : SafeHandle
    {
        private bool m_simulator;
        private SimulatorHAL.CounterStruct m_digitalPort;
        private bool m_valid = true;

        public CounterSafeHandle() : base(IntPtr.Zero, true)
        {
            m_simulator = false;
        }

        internal CounterSafeHandle(SimulatorHAL.CounterStruct dPort) : base(IntPtr.Zero, true)
        {
            m_valid = true;
            m_digitalPort = dPort;
        }

        internal SimulatorHAL.CounterStruct GetSimulatorPort()
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
            int status = 0;
            HALDigital.FreeCounter(this, ref status);
            handle = IntPtr.Zero;
            return true;
        }
    }

    public class EncoderSafeHandle : SafeHandle
    {
        private bool m_simulator;
        private SimulatorHAL.EncoderStruct m_digitalPort;
        private bool m_valid = true;

        public EncoderSafeHandle() : base(IntPtr.Zero, true)
        {
            m_simulator = false;
        }

        internal EncoderSafeHandle(SimulatorHAL.EncoderStruct dPort) : base(IntPtr.Zero, true)
        {
            m_valid = true;
            m_digitalPort = dPort;
        }

        internal SimulatorHAL.EncoderStruct GetSimulatorPort()
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
            int status = 0;
            HALDigital.FreeEncoder(this, ref status);
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

        internal SimulatorHAL.AnalogPort GetSimulatorPort()
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
            HALAnalog.FreeAnalogInputPort(this);
            handle = IntPtr.Zero;
            return true;
        }
    }

    public class AnalogOutputPortSafeHandle : SafeHandle
    {
        private bool m_simulator;
        private SimulatorHAL.AnalogPort m_digitalPort;
        private bool m_valid = true;

        public AnalogOutputPortSafeHandle() : base(IntPtr.Zero, true)
        {
            m_simulator = false;
        }

        internal AnalogOutputPortSafeHandle(SimulatorHAL.AnalogPort dPort) : base(IntPtr.Zero, true)
        {
            m_valid = true;
            m_digitalPort = dPort;
        }

        internal SimulatorHAL.AnalogPort GetSimulatorPort()
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
            HALAnalog.FreeAnalogOutputPort(this);
            handle = IntPtr.Zero;
            return true;
        }
    }

    public class AnalogTriggerPortSafeHandle : SafeHandle
    {
        private bool m_simulator;
        private SimulatorHAL.AnalogTrigger m_digitalPort;
        private bool m_valid = true;

        public AnalogTriggerPortSafeHandle() : base(IntPtr.Zero, true)
        {
            m_simulator = false;
        }

        internal AnalogTriggerPortSafeHandle(SimulatorHAL.AnalogTrigger dPort) : base(IntPtr.Zero, true)
        {
            m_valid = true;
            m_digitalPort = dPort;
        }

        internal SimulatorHAL.AnalogTrigger GetSimulatorPort()
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
            int status = 0;
            HALAnalog.CleanAnalogTrigger(this, ref status);
            handle = IntPtr.Zero;
            return true;
        }
    }

    public class SolenoidPortSafeHandle : SafeHandle
    {
        private bool m_simulator;
        private SimulatorHAL.SolenoidPort m_digitalPort;
        private bool m_valid = true;

        public SolenoidPortSafeHandle() : base(IntPtr.Zero, true)
        {
            m_simulator = false;
        }

        internal SolenoidPortSafeHandle(SimulatorHAL.SolenoidPort dPort) : base(IntPtr.Zero, true)
        {
            m_valid = true;
            m_digitalPort = dPort;
        }

        internal SimulatorHAL.SolenoidPort GetSimulatorPort()
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
            HALSolenoid.FreeSolenoidPort(this);
            handle = IntPtr.Zero;
            return true;
        }
    }

    public class HALPortSafeHandle : SafeHandle
    {
        private bool m_simulator;
        private SimulatorHAL.Port m_digitalPort;
        private bool m_valid = true;

        public HALPortSafeHandle() : base(IntPtr.Zero, false)
        {
            m_simulator = false;
        }

        internal HALPortSafeHandle(SimulatorHAL.Port dPort) : base(IntPtr.Zero, false)
        {
            m_valid = true;
            m_digitalPort = dPort;
        }

        internal SimulatorHAL.Port GetSimulatorPort()
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
            HAL.FreePort(this);
            handle = IntPtr.Zero;
            return true;
        }
    }

    public class CANTalonSafeHandle : SafeHandle
    {
        private bool m_simulator;
        private SimulatorHAL.TalonSRX m_digitalPort;
        private bool m_valid = true;

        public CANTalonSafeHandle() : base(IntPtr.Zero, true)
        {
            m_simulator = false;
        }

        internal CANTalonSafeHandle(SimulatorHAL.TalonSRX dPort) : base(IntPtr.Zero, true)
        {
            m_valid = true;
            m_digitalPort = dPort;
        }

        internal SimulatorHAL.TalonSRX GetSimulatorPort()
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
            HALCanTalonSRX.C_TalonSRX_Destroy(this);
            handle = IntPtr.Zero;
            return true;
        }
    }


}
