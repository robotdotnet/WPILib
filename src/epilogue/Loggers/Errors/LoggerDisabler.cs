
using System.Runtime.InteropServices;

namespace WPILib.Logging.Loggers.Errors;

public class LoggerDisabler : IErrorHandler
{
    private readonly int m_threshold;
    private readonly Dictionary<ClassSpecificLogger, int> m_errorCounts = [];

    public LoggerDisabler(int threshold)
    {
        m_threshold = threshold;
    }

    public static LoggerDisabler ForLimit(int threshold) => new(threshold);

    public void Handle(Exception exception, ClassSpecificLogger logger)
    {
        int errorCount = ++CollectionsMarshal.GetValueRefOrAddDefault(m_errorCounts, logger, out var _);

        if (errorCount > m_threshold)
        {
            logger.Disable();
            Console.Error.WriteLine($"[EPILOGUE] Too many errors detected in {logger.GetType().Name} (maximum allowed: {m_threshold}). The most recent error follows:");
            Console.Error.WriteLine(exception.Message);
            Console.Error.WriteLine(exception.StackTrace);
        }
    }

    public void Reset()
    {
        foreach (var logger in m_errorCounts)
        {
            logger.Key.Reenable();
        }
        m_errorCounts.Clear();
    }
}
