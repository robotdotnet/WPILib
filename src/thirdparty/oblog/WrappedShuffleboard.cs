﻿using System;
using System.Collections.Generic;
using System.Text;
using WPILib.ShuffleboardNS;

namespace WPILib.Oblog
{
    public class WrappedShuffleboard : IShuffleboardWrapper
    {
        public IShuffleboardContainerWrapper GetTab(string title)
        {
            return new WrappedShuffleboardContainer(Shuffleboard.GetTab(title));
        }
    }
}