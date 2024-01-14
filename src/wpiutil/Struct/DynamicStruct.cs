using System;
using System.Buffers.Binary;
using CommunityToolkit.Diagnostics;

namespace WPIUtil.Struct;

public class DynamicStruct {
    private Memory<byte>? m_writable;

    public ReadOnlyMemory<byte> Buffer {get; private set;}
    public StructDescriptor Descriptor { get;}

    private DynamicStruct(StructDescriptor desc, ReadOnlyMemory<byte> data) {
        Buffer = data;
        Descriptor = desc;
    }

    private DynamicStruct(StructDescriptor desc, Memory<byte> data) {
        Buffer = data;
        Descriptor = desc;
        m_writable = data;
    }

    public static DynamicStruct Allocate(StructDescriptor desc) {
        return new DynamicStruct(desc, new byte[desc.Size]);
    }

    public static DynamicStruct Wrap(StructDescriptor desc, ReadOnlyMemory<byte> data) {
        return new DynamicStruct(desc, data);
    }

    public void SetData(ReadOnlySpan<byte> data) {
        if (data.Length < Descriptor.Size) {
            throw new Exception("TODO Custom exception underflow");
        }
        if (!m_writable.HasValue) {
            Memory<byte> writable = new byte[Descriptor.Size];
            m_writable = writable;
            Buffer = writable;
        }
        data.CopyTo(m_writable.Value.Span);
    }

    public StructPropertyDescriptor? FindProperty(string name) {
        return Descriptor.FindPropertyByName(name);
    }

    private long GetFieldImpl(StructPropertyDescriptor property, int arrIndex) {
        if (!(property.Parent == Descriptor)) {
            ThrowHelper.ThrowArgumentException("Property is not part of this struct");
        }
        if (!Descriptor.IsValid) {
            throw new Exception("TODO better exception");
        }
        if (arrIndex < 0 || arrIndex >= property.ArraySize) {
            throw new IndexOutOfRangeException("TODO");
        }

        ReadOnlySpan<byte> data = Buffer.Span;

        long val = property.Size switch {
            1 => data[property.Offset + arrIndex],
            2 => BinaryPrimitives.ReadInt16BigEndian(data[(property.Offset + arrIndex * 2)..]),
            4 => BinaryPrimitives.ReadInt32BigEndian(data[(property.Offset + arrIndex * 4)..]),
            8 => BinaryPrimitives.ReadInt64BigEndian(data[(property.Offset + arrIndex * 8)..]),
            _ => throw new Exception("Illegal state"),
        };

        return 0;
    }
}
