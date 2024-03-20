using UnitsNet;
using UnitsNet.NumberExtensions.NumberToDuration;
using WPIHal.Handles;
using WPIHal.Natives;

namespace WPILib;

public class TimedRobot : IterativeRobotBase
{
    private sealed class Callback(Action func, Duration startTime, Duration period, Duration offset)
    {
        public Action CB { get; } = func;
        public Duration Period { get; } = period;
        public Duration ExpirationTime { get; set; } =
                startTime
                    + offset
                    + Math.Floor((Timer.FPGATimestamp - startTime) / period) * period
                    + period;
    }

    public static readonly Duration DefaultPeriod = 20.Milliseconds();

    private readonly HalNotifierHandle m_notifier = HalNotifier.InitializeNotifier();

    private Duration m_startTime;

    private readonly PriorityQueue<Callback, Duration> m_callbacks = new();

    protected TimedRobot() : this(DefaultPeriod)
    {

    }

    protected TimedRobot(Duration period) : base(period)
    {
        m_startTime = Timer.FPGATimestamp;
        AddPeriodic(LoopFunc, period);
        HalNotifier.SetNotifierName(m_notifier, "TimedRobot");
    }

    public override void Dispose()
    {
        GC.SuppressFinalize(this);
        HalNotifier.StopNotifier(m_notifier);
        HalNotifier.CleanNotifier(m_notifier);
        base.Dispose();
    }

    public override void StartCompetition()
    {
        RobotInit();

        if (IsSimulation)
        {
            SimulationInit();
        }

        // Tell the DS that the robot is ready to be enabled
        Console.WriteLine("********** Robot program startup complete **********");
        HalDriverStation.ObserveUserProgramStarting();

        while (true)
        {
            var callback = m_callbacks.Dequeue();

            HalNotifier.UpdateNotifierAlarm(m_notifier, (ulong)callback.ExpirationTime.Microseconds);

            ulong curTime = HalNotifier.WaitForNotifierAlarm(m_notifier);
            if (curTime == 0)
            {
                break;
            }

            Duration curTimeDur = curTime.Microseconds();

            callback.CB();

            callback.ExpirationTime += callback.Period;

            m_callbacks.Enqueue(callback, callback.ExpirationTime);

            // Process all other callbacks that are ready to run
            while (m_callbacks.Peek().ExpirationTime <= curTimeDur)
            {
                callback = m_callbacks.Dequeue();

                callback.CB();

                callback.ExpirationTime += callback.Period;
                m_callbacks.Enqueue(callback, callback.ExpirationTime);
            }
        }
    }

    public override void EndCompetition()
    {
        HalNotifier.StopNotifier(m_notifier);
    }

    public void AddPeriodic(Action callback, Duration period)
    {
        var cb = new Callback(callback, m_startTime, period, 0.Seconds());
        m_callbacks.Enqueue(cb, cb.ExpirationTime);
    }

    public void AddPeriodic(Action callback, Duration period, Duration offset)
    {
        var cb = new Callback(callback, m_startTime, period, offset);
        m_callbacks.Enqueue(cb, cb.ExpirationTime);
    }
}
