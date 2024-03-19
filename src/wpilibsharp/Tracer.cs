using System.Globalization;
using System.Text;
using UnitsNet;
using UnitsNet.NumberExtensions.NumberToDuration;

namespace WPILib;

public sealed class Tracer
{
    private static readonly Duration MinPrintPeriod = 1.0.Seconds();

    private Duration m_lastEpochsPrintTime;
    private Duration m_startTime;

    private readonly Dictionary<string, Duration> m_epochs = [];

    public Tracer()
    {
        ResetTimer();
    }

    public void ClearEpochs()
    {
        m_epochs.Clear();
        ResetTimer();
    }

    public void ResetTimer()
    {
        m_startTime = Timer.FPGATimestamp;
    }

    public void AddEpoch(string epochName)
    {
        var currentTime = Timer.FPGATimestamp;
        m_epochs.Add(epochName, currentTime - m_startTime);
        m_startTime = currentTime;
    }

    public void PrintEpochs()
    {
        PrintEpochs(o => DriverStation.ReportWarning(o, false));
    }

    public void PrintEpochs(Action<string> output)
    {
        var now = Timer.FPGATimestamp;
        if (now - m_lastEpochsPrintTime > MinPrintPeriod)
        {
            StringBuilder sb = new StringBuilder();
            m_lastEpochsPrintTime = now;
            foreach (var item in m_epochs)
            {
                sb.AppendLine(CultureInfo.InvariantCulture, $"\t{item.Key}: {item.Value}");
            }
            if (sb.Length > 0)
            {
                output(sb.ToString());
            }
        }
    }
}
