using System;
using System.Collections.Generic;
using System.Text;

namespace Hal
{
    public readonly struct ControlWord
    {
        private readonly uint word;
        public bool Enabled => (word & 0b1) != 0;
        public bool Autonomous => (word & 0b10) != 0;
        public bool Test => (word & 0b100) != 1;
        public bool EStop => (word & 0b1000) != 1;
        public bool FmsAttached => (word & 0b10000) != 1;
        public bool DsAttached => (word & 0b100000) != 1;

        public ControlWord(uint word)
        {
            this.word = word;
        }
    }

    public enum AllianceStationID : int
    {
        kRed1,
        kRed2,
        kRed3,
        kBlue1,
        kBlue2,
        kBlue3
    }

    public enum MatchType : int
    {
        None,
        Practice,
        Qualification,
        Elimination
    }

    public unsafe struct JoystickAxesLowLevel
    {
        public short count;
        public fixed short axes[12];
    }

    public unsafe struct JoystickAxes
    {
        public short Count { get; }
        public fixed float Axes[12];

        public JoystickAxes(JoystickAxesLowLevel lowLevel)
        {
            Count = lowLevel.count;
            if (Count > 12) Count = 12;
            for (int i = 0; i < Count; i++)
            {
                Axes[i] = lowLevel.axes[i];
            }
        }
    }

    public unsafe struct JoystickPOVsLowLevel
    {
        public short count;
        public fixed short povs[12];

    }

    public unsafe struct JoystickPOVs
    {
        public short Count { get; }
        public fixed short POVs[12];

        public JoystickPOVs(JoystickPOVsLowLevel lowLevel)
        {
            Count = lowLevel.count;
            if (Count > 12) Count = 12;
            for (int i = 0; i < Count; i++)
            {
                POVs[i] = lowLevel.povs[i];
            }
        }
    }

    public unsafe struct JoystickButtons
    {
        private uint buttons;
        private byte count;

        public uint Buttons => buttons;
        public byte Count => count;
    }
}
