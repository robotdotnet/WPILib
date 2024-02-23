using System.Runtime.CompilerServices;

namespace WPIUtil.Marshal;

#pragma warning disable CA1720 // Identifier contains type name
public readonly unsafe struct Ptr<T> where T : unmanaged
#pragma warning restore CA1720 // Identifier contains type name
{
    private readonly T* ptr;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Ptr(T* ptr) => this.ptr = ptr;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Ptr<T>(T* p) => new(p);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T*(Ptr<T> p) => p.ptr;

    public ref T this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref *(ptr + index);
    }

    public bool IsNull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ptr is null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref Ptr<byte> GetPinnableByteReference()
    {
        return ref Unsafe.As<Ptr<T>, Ptr<byte>>(ref Unsafe.AsRef(in this));
    }
}
