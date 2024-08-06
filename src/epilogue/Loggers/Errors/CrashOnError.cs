
namespace WPILib.Logging.Loggers.Errors;

public class CrashOnError : IErrorHandler
{
    public void Handle(Exception exception, ClassSpecificLogger logger)
    {
#pragma warning disable CA2201 // Do not raise reserved exception types
        throw new Exception($"[EPILOGUE] An error occured while logging an instance of {logger.LoggedType.Name}", exception);
#pragma warning restore CA2201 // Do not raise reserved exception types
    }
}
