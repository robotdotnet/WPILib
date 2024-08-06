namespace WPILib.Logging.Loggers.Errors;

public interface IErrorHandler
{
    void Handle(Exception exception, ClassSpecificLogger logger);

    public static IErrorHandler CrashOnError() => new CrashOnError();

    public static IErrorHandler PrintErrorMessages() => new ErrorPrinter();

    public static LoggerDisabler Disabling(int maximumPermissableErrors) => LoggerDisabler.ForLimit(maximumPermissableErrors);
}
