namespace NetworkTables;

public interface IGenericEntry : IGenericSubscriber, IGenericPublisher
{
    void Unpublish();
}
