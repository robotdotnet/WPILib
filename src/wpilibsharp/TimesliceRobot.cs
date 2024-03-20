using CommunityToolkit.Diagnostics;
using UnitsNet;

namespace WPILib;

public class TimesliceRobot : TimedRobot
{
    public TimesliceRobot(Duration robotPeriodicAllocation, Duration controllerPeriod)
    {
        m_nextOffset = robotPeriodicAllocation;
        m_controllerPeriod = controllerPeriod;
    }

    public void Schedule(Action func, Duration allocation)
    {
        if (m_nextOffset + allocation > m_controllerPeriod)
        {
            ThrowHelper.ThrowInvalidOperationException($"Function scheduled at offset {m_nextOffset} with allocation {allocation} exceeded controller period of {m_controllerPeriod}\n");
        }

        AddPeriodic(func, m_controllerPeriod, m_nextOffset);
        m_nextOffset += allocation;
    }

    private Duration m_nextOffset;
    private readonly Duration m_controllerPeriod;
}
