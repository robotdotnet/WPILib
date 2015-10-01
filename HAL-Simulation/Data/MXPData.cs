namespace HAL_Simulator.Data
{
    public class MXPData : DataBase
    {
        private bool m_initialized = false;

        public override void ResetData()
        {
            m_initialized = false;
        }

        public bool Initialized
        {
            get { return m_initialized; }
            internal set
            {
                m_initialized = value;
            }
        }
    }
}
