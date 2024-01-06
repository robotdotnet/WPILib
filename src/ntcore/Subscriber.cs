namespace NetworkTables;

public interface Subscriber : PubSub
{
    bool Exists { get; }
    long LastChange { get; }
}
