using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkTablesDotNet.NetworkTables;

namespace WPILib.Buttons
{
    public class NetworkButton : Button
    {
        private NetworkTable m_table;
        private string m_field;

        /*
        public NetworkButton(string table, string field) : this
        {
            //Still needs to be implemented
        }
         * */

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
