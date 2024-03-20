using UnitsNet;

namespace WPIMath;

public interface IMathShared
{
    void ReportError(string error, string stackTrace);
    void ReportUsage(MathUsageId id, int count);
    Duration Timestamp { get; }
}
