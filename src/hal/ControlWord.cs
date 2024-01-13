using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WPIHal;

[NativeMarshalling(typeof(ControlWordMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct ControlWord(bool Enabled, bool Autonomous, bool Test, bool EStop, bool FmsAttached, bool DsAttached);

[CustomMarshaller(typeof(ControlWord), MarshalMode.Default, typeof(ControlWordMarshaller))]
public static class ControlWordMarshaller
{
    public static NativeControlWord ConvertToUnmanaged(ControlWord managed)
    {
        NativeControlWord ret = default;
        if (managed.Enabled)
        {
            ret |= NativeControlWord.Enabled;
        }
        if (managed.Autonomous)
        {
            ret |= NativeControlWord.Autonomous;
        }
        if (managed.Test)
        {
            ret |= NativeControlWord.Test;
        }
        if (managed.EStop)
        {
            ret |= NativeControlWord.EStop;
        }
        if (managed.FmsAttached)
        {
            ret |= NativeControlWord.FmsArrached;
        }
        if (managed.DsAttached)
        {
            ret |= NativeControlWord.DsAttached;
        }
        return ret;
    }

    public static ControlWord ConvertToManaged(NativeControlWord unmanaged)
    {
        return new ControlWord
        {
            Enabled = unmanaged.HasFlag(NativeControlWord.Enabled),
            Autonomous = unmanaged.HasFlag(NativeControlWord.Autonomous),
            Test = unmanaged.HasFlag(NativeControlWord.Test),
            EStop = unmanaged.HasFlag(NativeControlWord.EStop),
            FmsAttached = unmanaged.HasFlag(NativeControlWord.FmsArrached),
            DsAttached = unmanaged.HasFlag(NativeControlWord.DsAttached),
        };
    }

    [Flags]
    public enum NativeControlWord : uint
    {
        Enabled = 0x1,
        Autonomous = 0x2,
        Test = 0x4,
        EStop = 0x8,
        FmsArrached = 0x10,
        DsAttached = 0x20,
    }
}
