using NetworkTablesDotNet.NetworkTables;

namespace WPILib.Buttons
{
    public class NetworkButton : Button
    {
        private NetworkTable m_table;
        private string m_field;

        
        public NetworkButton(string table, string field) : this(NetworkTable.GetTable(table), field)
        {
        }
        

        public NetworkButton(NetworkTable table, string field)
        {
            m_table = table;
            m_field = field;
        }

        public override bool Get()
        {
            return m_table.IsConnected() && m_table.GetBoolean(m_field, false);
        }
    }
}
