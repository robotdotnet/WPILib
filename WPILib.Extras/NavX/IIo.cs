using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    internal interface IIo : IDisposable
    {
        byte[] Read(byte readSize, DeviceRegisters deviceRegister);
        void Write(DeviceRegisters deviceRegister, byte[] message);
    }
}
