using System.Collections.Generic;

namespace WPILib.Shuffleboard
{
    internal sealed class ContainerHelper
    {
        private readonly IShuffleboardContainer m_container;
        private readonly List<ShuffleboardComponent> m_components = new List<ShuffleboardComponent>();

        internal ContainerHelper(IShuffleboardContainer container)
        {
            m_container = container;
        }

        public List<ShuffleboardComponent> Components => m_components;
    }
}
