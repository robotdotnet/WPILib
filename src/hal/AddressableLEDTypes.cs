using System;
using System.Collections.Generic;
using System.Text;

namespace Hal
{
    public struct AddressableLEDData
    {
        public const int MaxLength = 5460;

        public byte B;
        public byte G;
        public byte R;
        public byte Padding;

    }
}
