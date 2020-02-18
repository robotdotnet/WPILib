using NetworkTables;

namespace WPILib.ShuffleboardNS
{
    public sealed class SimpleWidget : ShuffleboardWidget<SimpleWidget>
    {
        private NetworkTableEntry m_entry;

        internal SimpleWidget(IShuffleboardContainer parent, string title)
            : base(parent, title)
        {

        }

        public NetworkTableEntry Entry
        {
            get
            {
                if (!m_entry.IsValid)
                {
                    ForceGenerate();
                }
                return m_entry;
            }
        }

        public override void BuildInto(NetworkTable parentTable, NetworkTable metaTable)
        {
            BuildMetadata(metaTable);
            if (!m_entry.IsValid)
            {
                m_entry = parentTable.GetEntry(Title);
            }
        }

        private void ForceGenerate()
        {
            var parent = Parent;
            while (parent is ShuffleboardLayout layout)
            {
                parent = layout.Parent;
            }
            var tab = (ShuffleboardTab)parent;
            tab.Root.Update();
        }
    }
}
