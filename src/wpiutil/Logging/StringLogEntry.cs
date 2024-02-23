// Copyright (c) FIRST and other WPILib contributors.
// Open Source Software; you can modify and/or share it under the terms of
// the WPILib BSD license file in the root directory of this project.

namespace WPIUtil.Logging;

/// <summary>
/// Log String values.
/// </summary>
/// <remarks>
/// Constructs a String log entry.
/// </remarks>
/// <param name="log">datalog</param>
/// <param name="name">name of the entry</param>
/// <param name="metadata">metadata</param>
/// <param name="timestamp">entry creation timestamp (0=now)</param>
public class StringLogEntry(DataLog log, string name, string metadata = "", long timestamp = 0) : DataLogEntry(log, name, DataType, metadata, timestamp)
{
    /// <summary>
    /// The data type for String values.
    /// </summary>
    public static string DataType => "string";
    /// <summary>
    /// The data type for String values in UTF8 format.
    /// </summary>
    public static ReadOnlySpan<byte> DataTypeUft8 => "string"u8;

    /// <summary>
    /// Appends a record to the log.
    /// </summary>
    /// <param name="value">Value to record</param>
    /// <param name="timestamp">Time stamp (0 to indicate now)</param>
    public void Append(string value, long timestamp = 0)
    {
        Log.AppendString(Entry, value, timestamp);
    }

    /// <summary>
    /// Appends a record to the log.
    /// </summary>
    /// <param name="value">Value to record</param>
    /// <param name="timestamp">Time stamp (0 to indicate now)</param>
    public void Append(ReadOnlySpan<char> value, long timestamp = 0)
    {
        Log.AppendString(Entry, value, timestamp);
    }

    /// <summary>
    /// Appends a record to the log.
    /// </summary>
    /// <param name="value">Value to record</param>
    /// <param name="timestamp">Time stamp (0 to indicate now)</param>
    public void Append(ReadOnlySpan<byte> value, long timestamp = 0)
    {
        Log.AppendString(Entry, value, timestamp);
    }
}
