using System;

namespace HAL_Base
{
    public interface ISimulator : IDisposable
    {
        string AddArguments();
        void Start();
    }
}
