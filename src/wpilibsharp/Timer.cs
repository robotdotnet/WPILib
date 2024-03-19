using UnitsNet;
using UnitsNet.NumberExtensions.NumberToDuration;

namespace WPILib;

public sealed class Timer
{
    public static Duration FPGATimestamp => RobotController.FPGATime.Seconds();

    public static Duration MatchTime => DriverStation.MatchTime.Seconds();

    public static void Delay(Duration time)
    {
        Thread.Sleep((int)time.Milliseconds);
    }

    private Duration m_startTime;
    private Duration m_accumulatedTime;
    private bool m_running;

    public Timer()
    {
        Reset();
    }

    public Duration Elapsed
    {
        get
        {
            if (m_running)
            {
                return m_accumulatedTime + (FPGATimestamp - m_startTime);
            }
            else
            {
                return m_accumulatedTime;
            }
        }
    }

    public void Reset()
    {
        m_accumulatedTime = 0.Seconds();
        m_startTime = FPGATimestamp;
    }

    public void Start()
    {
        if (!m_running)
        {
            m_startTime = FPGATimestamp;
            m_running = true;
        }
    }

    public void Restart()
    {
        if (m_running)
        {
            Stop();
        }
        Reset();
        Start();
    }

    public void Stop()
    {
        m_accumulatedTime = Elapsed;
        m_running = false;
    }

    public bool HasElapsed(Duration time)
    {
        return Elapsed >= time;
    }

    public bool AdvanceIfElapsed(Duration time)
    {
        if (Elapsed >= time)
        {
            m_startTime += time;
            return true;
        }
        else
        {
            return false;
        }
    }
}
