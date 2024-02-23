// Copyright (c) FIRST and other WPILib contributors.
// Open Source Software; you can modify and/or share it under the terms of
// the WPILib BSD license file in the root directory of this project.

namespace WPIUtil.Logging;

/// <summary>
/// Log Raw values.
/// </summary>
/// <remarks>
/// Constructs a Raw log entry.
/// </remarks>
/// <param name="log">datalog</param>
/// <param name="name">name of the entry</param>
/// <param name="metadata">metadata</param>
/// <param name="type">Data type</param>
/// <param name="timestamp">entry creation timestamp (0=now)</param>
public class RawLogEntry(DataLog log, string name, string metadata = "", string type = "raw", long timestamp = 0) : DataLogEntry(log, name, type, metadata, timestamp)
{
    /// <summary>
    /// The data type for Raw values.
    /// </summary>
    public static string DataType => "raw";
    /// <summary>
    /// The data type for Raw values in UTF8 format.
    /// </summary>
    public static ReadOnlySpan<byte> DataTypeUft8 => "raw"u8;

    /// <summary>
    /// Appends a record to the log.
    /// </summary>
    /// <param name="value">Value to record</param>
    /// <param name="timestamp">Time stamp (0 to indicate now)</param>
    public void Append(ReadOnlySpan<byte> value, long timestamp = 0)
    {
        Log.AppendRaw(Entry, value, timestamp);
    }
}
