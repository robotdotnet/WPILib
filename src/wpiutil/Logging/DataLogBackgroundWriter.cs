using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil.Natives;

namespace WPIUtil.Logging;

public sealed unsafe class DataLogBackgroundWriter : DataLog
{
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void NativeDataLogCallback(void* ptr, byte* data, nuint len)
    {
        GCHandle handle = GCHandle.FromIntPtr((nint)ptr);
        if (handle.Target is DataLogBackgroundWriter datalog)
        {
            datalog.callback?.Invoke(new ReadOnlySpan<byte>(data, (int)len));
        }
    }

    private GCHandle? gcHandle;
    private readonly DataLogCallback? callback;

    public delegate void DataLogCallback(ReadOnlySpan<byte> data);

    public DataLogBackgroundWriter(string dir = "", string filename = "", double period = 0.25, string extraHeader = "") : base(DataLogNative.CreateBg(dir, filename, period, extraHeader))
    {

    }

    public DataLogBackgroundWriter(DataLogCallback callback, double period = 0.25, string extraHeader = "") : base(null)
    {
        gcHandle = GCHandle.Alloc(this);
        this.callback = callback;
        NativeHandle = DataLogNative.CreateBgFunc(&NativeDataLogCallback, (void*)GCHandle.ToIntPtr(gcHandle.Value), period, extraHeader);
    }

    public override void Dispose()
    {
        base.Dispose();
        if (gcHandle.HasValue)
        {
            gcHandle.Value.Free();
        }
    }

    public string Filename
    {
        set
        {
            DataLogNative.SetFilename(NativeHandle, value);
        }
    }
}
