using System;

namespace Hal
{

    public readonly struct SimDevice : IDisposable, IEquatable<SimDevice>
    {
        public static SimDevice Create(string name)
        {
            var handle = SimDeviceLowLevel.Create(name);
            if (handle <= 0) return new SimDevice();
            return new SimDevice(handle);
        }

        public static SimDevice Create(string name, int index)
        {
            return Create($"{name} [{index}]");
        }

        public static SimDevice Create(string name, int index, int channel)
        {
            return Create($"{name} [{index}, {channel}]");
        }

        public int NativeHandle { get; }

#pragma warning disable CA2225 // Operator overloads have named alternates
        public static bool operator true(SimDevice device)
#pragma warning restore CA2225 // Operator overloads have named alternates
        {
            return device.NativeHandle != 0;
        }

        public static bool operator false(SimDevice device)
        {
            return device.NativeHandle == 0;
        }

        public SimDevice(int handle)
        {
            NativeHandle = handle;
        }

        public void Dispose()
        {
            if (NativeHandle != 0)
            {
                SimDeviceLowLevel.Free(NativeHandle);
            }
        }

        public bool Equals(SimDevice other)
        {
            return NativeHandle == other.NativeHandle;
        }

        public static bool operator ==(SimDevice x, SimDevice y) => x.NativeHandle == y.NativeHandle;
        public static bool operator !=(SimDevice x, SimDevice y) => x.NativeHandle != y.NativeHandle;

        public override bool Equals(object? other)
        {
            if (other is SimDevice otherInst)
            {
                return NativeHandle == otherInst.NativeHandle;
            }
            return false;
        }

        public override int GetHashCode() => NativeHandle;
    }
}
