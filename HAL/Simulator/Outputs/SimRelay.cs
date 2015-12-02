using HAL.Simulator.Data;

namespace HAL.Simulator.Outputs
{
    public class SimRelay
    {
        readonly RelayData RelayData = null;

        public SimRelay(int port)
        {
            RelayData = SimData.Relay[port];
        }

        public bool GetForward()
        {
            return RelayData.Forward;
        }

        public bool GetReverse()
        {
            return RelayData.Reverse;
        }
    }
}
