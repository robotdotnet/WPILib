using UnitsNet;
using UnitsNet.NumberExtensions.NumberToDuration;
using WPIUtil.Natives;

namespace WPIMath;

public static class MathSharedStore
{
    private static IMathShared? m_mathShared;
    private static readonly object m_lockObject = new();

    private sealed class DefaultMathShared : IMathShared
    {
        public Duration Timestamp => TimestampNative.Now().Microseconds();

        public void ReportError(string error, string stackTrace)
        {
        }

        public void ReportUsage(MathUsageId id, int count)
        {
        }
    }

    public static IMathShared MathShared
    {
        get
        {
            lock (m_lockObject)
            {
                m_mathShared ??= new DefaultMathShared();
                return m_mathShared;
            }
        }
        set
        {
            lock (m_lockObject)
            {
                m_mathShared = value;
            }
        }
    }

    public static void ReportError(string error, string stackTrace)
    {
        MathShared.ReportError(error, stackTrace);
    }

    public static void ReportUsage(MathUsageId id, int count)
    {
        MathShared.ReportUsage(id, count);
    }

    public static Duration Timestamp => MathShared.Timestamp;
}
