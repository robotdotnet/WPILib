using System;
using NetworkTables;

namespace WPILib.Shuffleboard
{
    public sealed class SuppliedValueWidget<T> : ShuffleboardWidget<SuppliedValueWidget<T>>
    {
        private readonly Func<T> m_supplier;
        private readonly Action<NetworkTableEntry, T> m_setter;

        internal SuppliedValueWidget(IShuffleboardContainer parent, string title, Func<T> supplier,
            Action<NetworkTableEntry, T> setter)
                : base(parent, title)
        {
            m_supplier = supplier;
            m_setter = setter;
        }

        public override void BuildInto(NetworkTable parentTable, NetworkTable metaTable)
        {

            BuildMetadata(metaTable);
            metaTable.GetEntry("Controllable").SetBoolean(false);

            var entry = parentTable.GetEntry(Title);
            m_setter(entry, m_supplier());
        }
    }
}
