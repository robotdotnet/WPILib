using Epilogue.Logging;
using Epilogue.Logging.Errors;
using NetworkTables;

namespace Epilogue;

public class EpilogueConfiguration
{
    public IDataLogger DataLogger { get; set; } = new NTDataLogger(NetworkTableInstance.Default);
    public LogImportance MinimumImportance { get; set; } = LogImportance.Debug;
    public IErrorHandler ErrorHandler { get; set; } = new ErrorPrinter();
    public string Root { get; set; } = "Robot";
}
