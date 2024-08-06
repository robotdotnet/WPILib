using Xunit;

namespace WPILib.Logging;

[Logged(Importance = LogImportance.Info)]
public record Point2d(double x, double y, int dim);

[Logged]
public record Other(Point2d point);
