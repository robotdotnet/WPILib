using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX
{
    interface IBoardCapabilities
    {
        bool IsOmniMountSupported();
        bool IsBoardYawResetSupported();
        bool IsDisplacementSupported();
    }
}
