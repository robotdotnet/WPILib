namespace NetworkTables;

public record struct TimestampedObject<T>(long Timestamp, long ServerTime, T Value);
