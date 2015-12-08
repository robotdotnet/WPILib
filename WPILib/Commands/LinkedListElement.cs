namespace WPILib.Commands
{
    internal class LinkedListElement
    {
        private LinkedListElement m_next;
        private LinkedListElement m_previous;
        private Command m_data;

        public LinkedListElement()
        {
            
        }

        public void SetData(Command newData)
        {
            m_data = newData;
        }

        public Command GetData()
        {
            return m_data;
        }

        public LinkedListElement GetNext()
        {
            return m_next;
        }

        public LinkedListElement GetPrevious()
        {
            return m_previous;
        }

        public void Add(LinkedListElement l)
        {
            if (m_next == null)
            {
                m_next = l;
                m_next.m_previous = this;
            }
            else
            {
                m_next.m_previous = l;
                l.m_next = m_next;
                l.m_previous = this;
                m_next = l;
            }
        }

        public LinkedListElement Remove()
        {
            if (m_previous == null && m_next == null)
            {
                
            }
            else if (m_next == null)
            {
                m_previous.m_next = null;
            }
            else if (m_previous == null)
            {
                m_next.m_previous = null;
            }
            else
            {
                m_next.m_previous = m_previous;
                m_previous.m_next = m_next;
            }
            LinkedListElement n = m_next;
            m_next = null;
            m_previous = null;
            return n;
        }
    }
}
