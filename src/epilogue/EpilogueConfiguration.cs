using NetworkTables;
using WPILib.Logging.Loggers;
using WPILib.Logging.Loggers.Errors;

namespace WPILib.Logging;

public class EpilogueConfiguration
{
    public IDataLogger DataLogger { get; set; } = new NTDataLogger(NetworkTableInstance.Default);
    public LogImportance MinimumImportance { get; set; } = LogImportance.Debug;
    public IErrorHandler ErrorHandler { get; set; } = new ErrorPrinter();
    public string Root { get; set; } = "Robot";
}
