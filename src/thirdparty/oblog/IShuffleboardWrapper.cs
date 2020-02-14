using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.Oblog
{
    public interface IShuffleboardWrapper
    {
        IShuffleboardContainerWrapper GetTab(string title);
    }
}
