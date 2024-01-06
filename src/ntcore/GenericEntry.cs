namespace NetworkTables;

public interface GenericEntry : GenericSubscriber, GenericPublisher
{
    void Unpublish();
}
