using WPIUtil.Natives;

namespace WPIUtil.Logging;

public unsafe class DataLogWriter : DataLog
{
    public DataLogWriter(string filename, string extraHeader = "") : base(DataLogNative.Create(filename, extraHeader))
    {
    }
}
