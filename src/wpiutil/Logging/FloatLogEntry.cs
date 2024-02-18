// Copyright (c) FIRST and other WPILib contributors.
// Open Source Software; you can modify and/or share it under the terms of
// the WPILib BSD license file in the root directory of this project.

using System;

namespace WPIUtil.Logging;

/// <summary>
/// Log Float values.
/// </summary>
/// <remarks>
/// Constructs a Float log entry.
/// </remarks>
/// <param name="log">datalog</param>
/// <param name="name">name of the entry</param>
/// <param name="metadata">metadata</param>
/// <param name="timestamp">entry creation timestamp (0=now)</param>
public class FloatLogEntry(DataLog log, string name, string metadata = "", long timestamp = 0) : DataLogEntry(log, name, DataType, metadata, timestamp)
{
    /// <summary>
    /// The data type for Float values.
    /// </summary>
    public static string DataType => "float";
    /// <summary>
    /// The data type for Float values in UTF8 format.
    /// </summary>
    public static ReadOnlySpan<byte> DataTypeUft8 => "float"u8;

    /// <summary>
    /// Appends a record to the log.
    /// </summary>
    /// <param name="value">Value to record</param>
    /// <param name="timestamp">Time stamp (0 to indicate now)</param>
    public void Append(float value, long timestamp = 0)
    {
        m_log.AppendFloat(m_entry, value, timestamp);
    }
}