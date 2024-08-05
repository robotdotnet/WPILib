using Xunit;

namespace Epilogue;

[Logged(Importance = LogImportance.Info)]
public record Point2d(double x, double y, int dim);
