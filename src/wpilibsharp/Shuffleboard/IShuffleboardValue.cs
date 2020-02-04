using NetworkTables;

namespace WPILib.Shuffleboard
{
    public interface IShuffleboardValue
    {
        string Title { get; }

        void BuildInto(NetworkTable parentTable, NetworkTable metaTable);
    }
}
