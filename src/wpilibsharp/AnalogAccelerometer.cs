using System;
using WPILib.SmartDashboard;

namespace WPILib {
    public class AnalogAccelerometer : ISendable, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}