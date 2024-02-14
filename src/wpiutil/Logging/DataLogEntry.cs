using WPIUtil.Handles;

namespace WPIUtil.Logging;

public abstract class DataLogEntry(DataLog log, string name, string type, string metadata = "", long timestamp = 0)
{
    public void SetMetadata(string metadata, long timestamp = 0)
    {
        m_log.SetMetadata(m_entry, metadata, timestamp);
    }

    public void Finish(long timestamp = 0)
    {
        m_log.Finish(m_entry, timestamp);
    }

    protected readonly DataLog m_log = log;
    protected readonly DataLogEntryHandle m_entry = log.Start(name, type, metadata, timestamp);
}
