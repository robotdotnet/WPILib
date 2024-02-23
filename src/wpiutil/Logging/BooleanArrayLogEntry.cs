// Copyright (c) FIRST and other WPILib contributors.
// Open Source Software; you can modify and/or share it under the terms of
// the WPILib BSD license file in the root directory of this project.

namespace WPIUtil.Logging;

/// <summary>
/// Log BooleanArray values.
/// </summary>
/// <remarks>
/// Constructs a BooleanArray log entry.
/// </remarks>
/// <remarks>
/// Log BooleanArray values.
/// </remarks>
/// <remarks>
/// Constructs a BooleanArray log entry.
/// </remarks>
/// <param name="log">datalog</param>
/// <param name="name">name of the entry</param>
/// <param name="metadata">metadata</param>
/// <param name="timestamp">entry creation timestamp (0=now)</param>
public class BooleanArrayLogEntry(DataLog log, string name, string metadata = "", long timestamp = 0) : DataLogEntry(log, name, DataType, metadata, timestamp)
{
    /// <summary>
    /// The data type for BooleanArray values.
    /// </summary>
    public static string DataType => "boolean[]";
    /// <summary>
    /// The data type for BooleanArray values in UTF8 format.
    /// </summary>
    public static ReadOnlySpan<byte> DataTypeUft8 => "boolean[]"u8;


    /// <summary>
    /// Appends a record to the log.
    /// </summary>
    /// <param name="value">Value to record</param>
    /// <param name="timestamp">Time stamp (0 to indicate now)</param>
    public void Append(ReadOnlySpan<bool> value, long timestamp = 0)
    {
        Log.AppendBooleanArray(Entry, value, timestamp);
    }
}
