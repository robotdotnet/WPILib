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
            ret.control |= (uint)NativeControlWordEnum.Enabled;
        }
        if (managed.Autonomous)
        {
            ret.control |= (uint)NativeControlWordEnum.Autonomous;
        }
        if (managed.Test)
        {
            ret.control |= (uint)NativeControlWordEnum.Test;
        }
        if (managed.EStop)
        {
            ret.control |= (uint)NativeControlWordEnum.EStop;
        }
        if (managed.FmsAttached)
        {
            ret.control |= (uint)NativeControlWordEnum.FmsAttached;
        }
        if (managed.DsAttached)
        {
            ret.control |= (uint)NativeControlWordEnum.DsAttached;
        }
        return ret;
    }

    public static ControlWord ConvertToManaged(NativeControlWord unmanaged)
    {
        return new ControlWord
        {
            Enabled = (unmanaged.control & (uint)NativeControlWordEnum.Enabled) != 0,
            Autonomous = (unmanaged.control & (uint)NativeControlWordEnum.Autonomous) != 0,
            Test = (unmanaged.control & (uint)NativeControlWordEnum.Test) != 0,
            EStop = (unmanaged.control & (uint)NativeControlWordEnum.EStop) != 0,
            FmsAttached = (unmanaged.control & (uint)NativeControlWordEnum.FmsAttached) != 0,
            DsAttached = (unmanaged.control & (uint)NativeControlWordEnum.DsAttached) != 0,
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeControlWord
    {
        public uint control;
    }

    [Flags]
    public enum NativeControlWordEnum : uint
    {
        Enabled = 0x1,
        Autonomous = 0x2,
        Test = 0x4,
        EStop = 0x8,
        FmsAttached = 0x10,
        DsAttached = 0x20,
    }
}
