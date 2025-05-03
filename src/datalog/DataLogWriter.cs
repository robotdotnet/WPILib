using WPI.Logging.Natives;

namespace WPI.Logging;

public unsafe class DataLogWriter : DataLog
{
    private static OpaqueDataLog* Create(string filename, string extraHeader)
    {
        var errorCode = 0;
        return DataLogNative.Create(filename, &errorCode, extraHeader);
    }

    public DataLogWriter(string filename, string extraHeader = "") : base(Create(filename, extraHeader))
    {
    }
}
