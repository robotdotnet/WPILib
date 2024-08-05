
namespace Epilogue.Logging.Errors;

public class LoggerDisabler : IErrorHandler {
    private readonly int m_threshold;
    private readonly Dictionary<ClassSpecificLogger, int> m_errorCounts = [];

    public LoggerDisabler(int threshold) {
        m_threshold = threshold;
    }

    public static LoggerDisabler ForLimit(int threshold) => new LoggerDisabler(threshold);

    public void Handle(Exception exception, ClassSpecificLogger logger)
    {
        throw new NotImplementedException();
    }
}
