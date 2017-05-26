using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    interface IRegisterIo
    {
        bool Init();
        bool Write(byte address, byte value);
        bool Read(byte firstAddress, byte[] buffer);
        bool Shutdown();
    }
}
