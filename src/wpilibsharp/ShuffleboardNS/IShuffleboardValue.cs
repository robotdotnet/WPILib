using NetworkTables;

namespace WPILib.ShuffleboardNS
{
    public interface IShuffleboardValue
    {
        string Title { get; }

        void BuildInto(NetworkTable parentTable, NetworkTable metaTable);
    }
}
