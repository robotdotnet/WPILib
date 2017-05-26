using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    interface IIoProvider : IDisposable
    {
        bool IsConnected();
        double GetByteCount();
        double GetUpdateCount();
        void SetUpdateRateHz(byte updateRate);
        void ZeroYaw();
        void ZeroDisplacement();
        void Run();
        void Stop();
    }
}
