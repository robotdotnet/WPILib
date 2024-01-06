namespace NetworkTables;

public record struct LogMessage(int LogLevel, string Filename, int Line, string Message);
