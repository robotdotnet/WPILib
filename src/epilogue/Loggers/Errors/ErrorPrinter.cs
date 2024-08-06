
namespace WPILib.Logging.Loggers.Errors;

public class ErrorPrinter : IErrorHandler
{
    public void Handle(Exception exception, ClassSpecificLogger logger)
    {
        Console.Error.WriteLine($"[EPILOGUE] An error occured while logging an instance of {logger.LoggedType.Name}: {exception.Message}");
    }
}
