using System;

namespace CTRE
{
    using TALON_Control_6_MotProfAddTrajPoint_huff0_t = UInt64;
    using TALON_Control_6_MotProfAddTrajPoint_t = UInt64;

    /**
    * Top level Buffer for motion profile trajectory buffering.
    * Basically this buffers up the eight byte CAN frame payloads that are
    * handshaked into the Talon RAM.
    * TODO: Should this be moved into a separate header, and if so where
    * logically should it reside?
    * TODO: Add compression so that multiple CAN frames can be compressed into
    * one exchange.
    */
    class TrajectoryBuffer
    {
        public void Clear()
        {
            _sz = 0;
            _in = 0;
            _ou = 0;
        }
        /**
         * push caller's uncompressed simple trajectory point.
         */
        public void Push(TALON_Control_6_MotProfAddTrajPoint_huff0_t pt)
        {
            Alloc();

            _motProfTopBuffer[_in] = pt;
            if (++_in >= _cap)
                _in = 0;
            ++_sz;
        }
        /**
         * Get the next trajectory point CAN frame to send.
         * Underlying layer may compress the next few points together
         * into one control_6 frame.
         */
        public TALON_Control_6_MotProfAddTrajPoint_t Front()
        {
            Alloc();

            _lastFront = (ulong)_motProfTopBuffer[_ou];
            return _lastFront;
        }
        public void Pop()
        {
            if (++_ou >= _cap)
                _ou = 0;
            --_sz;
        }
        public uint GetNumTrajectories() { return (uint)_sz; }
        public bool IsEmpty() { return _sz == 0; }

        private UInt64[] _motProfTopBuffer = null;
        private int _cap;
        private int _in = 0;
        private int _ou = 0;
        private int _sz = 0;

        private UInt64 _lastFront;

        public TrajectoryBuffer(int cap)
        {
            if (cap < 1)
                cap = 1;
            _cap = cap;
        }
        private void Alloc()
        {
            if (_motProfTopBuffer == null)
                _motProfTopBuffer = new UInt64[_cap];
        }
    };
}