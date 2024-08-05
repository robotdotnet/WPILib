using NetworkTables;

namespace Epilogue;

public class EpilogueConfiguration {
    public DataLogger DataLogger { get; set; } = new NTDataLogger(NetworkTableInstance.Default);
    public LogImportance MinimumImportance { get; set; } = LogImportance.Debug;
    public ErrorHandler ErrorHandler { get; set; } = new();
    public string Root { get; set; } = "Robot";
}