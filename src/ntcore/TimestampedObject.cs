namespace NetworkTables;

public sealed record class TimestampedObject<T>(long Timestamp, long ServerTime, T Value);
