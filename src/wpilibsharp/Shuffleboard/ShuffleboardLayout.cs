using System.Collections.Generic;
using NetworkTables;

namespace WPILib.Shuffleboard
{
    public class ShuffleboardLayout : ShuffleboardComponent<ShuffleboardLayout>, IShuffleboardContainer
    {
        private readonly ContainerHelper m_helper;

        internal ShuffleboardLayout(IShuffleboardContainer parent, string? name, string type)
          : base(parent, type, name)
        {
            m_helper = new ContainerHelper(this);
        }

        public List<ShuffleboardComponent> Components => m_helper.Components;

        public override void BuildInto(NetworkTable parentTable, NetworkTable metaTable)
        {
            BuildMetadata(metaTable);
            var table = parentTable.GetSubTable(Title);
            table.GetEntry(".type").SetString("ShuffleboardLayout");
            foreach (var component in Components)
            {
                component.BuildInto(table, metaTable.GetSubTable(component.Title));
            }
        }

        public ShuffleboardLayout GetLayout(string title, string type)
        {
            throw new System.NotImplementedException();
        }

        public ShuffleboardLayout GetLayout(string title, ILayoutType layoutType)
        {
            throw new System.NotImplementedException();
        }

        public ShuffleboardLayout GetLayout(string title)
        {
            throw new System.NotImplementedException();
        }
    }
}
