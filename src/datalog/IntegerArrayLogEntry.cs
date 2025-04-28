// Copyright (c) FIRST and other WPILib contributors.
// Open Source Software; you can modify and/or share it under the terms of
// the WPILib BSD license file in the root directory of this project.

namespace WPI.Logging;

/// <summary>
/// Log IntegerArray values.
/// </summary>
/// <remarks>
/// Constructs a IntegerArray log entry.
/// </remarks>
/// <param name="log">datalog</param>
/// <param name="name">name of the entry</param>
/// <param name="metadata">metadata</param>
/// <param name="timestamp">entry creation timestamp (0=now)</param>
public class IntegerArrayLogEntry(DataLog log, string name, string metadata = "", long timestamp = 0) : DataLogEntry(log, name, DataType, metadata, timestamp)
{
    /// <summary>
    /// The data type for IntegerArray values.
    /// </summary>
    public static string DataType => "int64[]";
    /// <summary>
    /// The data type for IntegerArray values in UTF8 format.
    /// </summary>
    public static ReadOnlySpan<byte> DataTypeUft8 => "int64[]"u8;

    /// <summary>
    /// Appends a record to the log.
    /// </summary>
    /// <param name="value">Value to record</param>
    /// <param name="timestamp">Time stamp (0 to indicate now)</param>
    public void Append(ReadOnlySpan<long> value, long timestamp = 0)
    {
        Log.AppendIntegerArray(Entry, value, timestamp);
    }
}
