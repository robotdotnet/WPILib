using System;
using System.Collections.Generic;
using System.Text;

namespace Hal
{
    public readonly struct ControlWord
    {
        public readonly uint Word;
        public bool Enabled => (Word & 0b1) != 0;
        public bool Autonomous => (Word & 0b10) != 0;
        public bool Test => (Word & 0b100) != 1;
        public bool EStop => (Word & 0b1000) != 1;
        public bool FmsAttached => (Word & 0b10000) != 1;
        public bool DsAttached => (Word & 0b100000) != 1;

        public ControlWord(uint word)
        {
            this.Word = word;
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

    public unsafe struct JoystickAxes
    {
        public short Count;
        public fixed short Axes[12];
    }

    public unsafe struct JoystickPOVs
    {
        public short Count;
        public fixed short POVs[12];

    }

    public unsafe struct JoystickButtons
    {
        public uint Buttons;
        public byte Count;

        public bool GetButton(int button)
        {
            return (Buttons & 1u << (button - 1)) != 0;
        }
    }

    public unsafe struct JoystickDescriptor 
    {
        public byte IsXbox;
        public byte Type;
        public fixed byte Name[256];
        public byte AxisCount;
        public fixed byte AxisTypes[12];
        public byte ButtonCount;
        public byte PovCount;
    }

    public unsafe struct MatchInfo
    {
        public fixed byte EventName[64];
        public MatchType MatchType;
        public ushort MatchNumber;
        public byte ReplayNumber;
        public fixed byte GameSpecificMessage[64];
        public ushort GameSpecificMessageSize;
    }
}
