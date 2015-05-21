using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkTablesDotNet;

namespace WPILib.livewindow
{
    internal class LiveWindowComponent
    {
        private string m_subsystem;
        private string m_name;
        private bool m_isSensor;

        public LiveWindowComponent(string subsystem, string name, bool isSensor)
        {
            m_isSensor = isSensor;
            m_subsystem = subsystem;
            m_name = name;
        }

        public string GetName()
        {
            return m_name;
        }

        public string GetSubsystem()
        {
            return m_subsystem;
        }

        public bool IsSensor()
        {
            return m_isSensor;
        }
    }

    class LiveWindow
    {
        //Need to implement
    }
}
