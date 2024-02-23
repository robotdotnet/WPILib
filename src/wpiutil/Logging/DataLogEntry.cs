using WPIUtil.Handles;

namespace WPIUtil.Logging;

public abstract class DataLogEntry(DataLog log, string name, string type, string metadata = "", long timestamp = 0)
{
    public void SetMetadata(string metadata, long timestamp = 0)
    {
        Log.SetMetadata(Entry, metadata, timestamp);
    }

    public void Finish(long timestamp = 0)
    {
        Log.Finish(Entry, timestamp);
    }

    protected DataLog Log { get; } = log;
    protected DataLogEntryHandle Entry { get; } = log.Start(name, type, metadata, timestamp);
}
