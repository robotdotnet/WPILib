using System;
using System.Collections.Generic;
using System.Text;
using WPILib.ShuffleboardNS;

namespace WPILib.Oblog
{
    class WrappedShuffleboardLayout : IShuffleboardLayoutWrapper
    {
        private readonly ShuffleboardLayout m_layout;

        public WrappedShuffleboardLayout(ShuffleboardLayout layout)
        {
            m_layout = layout;
        }

        public IShuffleboardLayoutWrapper WithPosition(int columnIndex, int rowIndex)
        {
            throw new NotImplementedException();
        }

        public IShuffleboardLayoutWrapper WithProperties(IDictionary<string, object> properties)
        {
            throw new NotImplementedException();
        }

        public IShuffleboardLayoutWrapper WithSize(int width, int height)
        {
            throw new NotImplementedException();
        }
    }
}
