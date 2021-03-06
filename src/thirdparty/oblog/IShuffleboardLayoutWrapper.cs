﻿using System.Collections.Generic;

namespace WPILib.Oblog
{
    public interface IShuffleboardLayoutWrapper
    {
        IShuffleboardLayoutWrapper WithProperties(IDictionary<string, object> properties);

        IShuffleboardLayoutWrapper WithSize(int width, int height);

        IShuffleboardLayoutWrapper WithPosition(int columnIndex, int rowIndex);
    }
}
