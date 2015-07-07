using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Base
{
    public interface ISimulator : IDisposable
    {
        string AddArguments();
        void Start();
    }
}
