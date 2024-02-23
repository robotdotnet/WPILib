using System.Buffers;
using System.Buffers.Binary;
using System.Text;
using CommunityToolkit.Diagnostics;

namespace WPIUtil.Serialization.Struct;

public class DynamicStruct
{
    public Memory<byte> Buffer { get; }
    public StructDescriptor Descriptor { get; }

    private DynamicStruct(StructDescriptor desc, Memory<byte> data)
    {
        Buffer = data;
        Descriptor = desc;
    }

    public static DynamicStruct Allocate(StructDescriptor desc)
    {
        return new DynamicStruct(desc, new byte[desc.Size]);
    }

    public static DynamicStruct Wrap(StructDescriptor desc, Memory<byte> data)
    {
        return new DynamicStruct(desc, data);
    }

    public void SetData(ReadOnlySpan<byte> data)
    {
        Guard.HasSizeGreaterThanOrEqualTo(data, Descriptor.Size);
        data.CopyTo(Buffer.Span);
    }

    public StructFieldDescriptor? FindField(string name)
    {
        return Descriptor.FindFieldByName(name);
    }

    public bool GetBoolField(StructFieldDescriptor field, int arrIndex = 0)
    {
        if (field.Type.Type != StructFieldType.Bool)
        {
            ThrowHelper.ThrowInvalidOperationException("field is not bool type");
        }
        return GetFieldImpl(field, arrIndex) != 0;
    }

    public void SetBoolField(StructFieldDescriptor field, bool value, int arrIndex = 0)
    {
        if (field.Type.Type != StructFieldType.Bool)
        {
            ThrowHelper.ThrowInvalidOperationException("field is not bool type");
        }
        SetFieldImpl(field, value ? 1 : 0, arrIndex);
    }

    public long GetIntField(StructFieldDescriptor field, int arrIndex = 0)
    {
        if (!field.IsInt && !field.IsUint)
        {
            ThrowHelper.ThrowInvalidOperationException("field is not integer type");
        }
        return GetFieldImpl(field, arrIndex);
    }

    public void SetIntField(StructFieldDescriptor field, long value, int arrIndex = 0)
    {
        if (!field.IsInt && !field.IsUint)
        {
            ThrowHelper.ThrowInvalidOperationException("field is not integer type");
        }
        SetFieldImpl(field, value, arrIndex);
    }

    public float GetFloatField(StructFieldDescriptor field, int arrIndex = 0)
    {
        if (field.Type.Type != StructFieldType.Float)
        {
            ThrowHelper.ThrowInvalidOperationException("field is not bool type");
        }
        return BitConverter.SingleToInt32Bits((int)GetFieldImpl(field, arrIndex));
    }

    public void SetFloatField(StructFieldDescriptor field, float value, int arrIndex = 0)
    {
        if (field.Type.Type != StructFieldType.Bool)
        {
            ThrowHelper.ThrowInvalidOperationException("field is not bool type");
        }
        SetFieldImpl(field, BitConverter.SingleToInt32Bits(value), arrIndex);
    }

    public double GetDoubleField(StructFieldDescriptor field, int arrIndex = 0)
    {
        if (field.Type.Type != StructFieldType.Double)
        {
            ThrowHelper.ThrowInvalidOperationException("field is not bool type");
        }
        return BitConverter.DoubleToInt64Bits(GetFieldImpl(field, arrIndex));
    }

    public void SetDoubleField(StructFieldDescriptor field, double value, int arrIndex = 0)
    {
        if (field.Type.Type != StructFieldType.Bool)
        {
            ThrowHelper.ThrowInvalidOperationException("field is not bool type");
        }
        SetFieldImpl(field, BitConverter.DoubleToInt64Bits(value), arrIndex);
    }

    public string GetStringField(StructFieldDescriptor field)
    {
        if (field.Type.Type != StructFieldType.Char)
        {
            ThrowHelper.ThrowInvalidOperationException("field is not char type");
        }
        if (field.Parent != Descriptor)
        {
            ThrowHelper.ThrowArgumentException("field is not part of this struct");
        }
        if (!Descriptor.IsValid)
        {
            ThrowHelper.ThrowInvalidOperationException("struct descriptor is not valid");
        }

        ReadOnlySpan<byte> bytes = Buffer.Span[field.Offset..field.ArraySize];

        // Find last non zero character
        int stringLength = bytes.Length;
        for (; stringLength > 0; stringLength--)
        {
            if (bytes[stringLength - 1] != 0)
            {
                break;
            }
        }

        OperationStatus result = Rune.DecodeLastFromUtf8(bytes[..stringLength], out _, out var consumed);
#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
        ReadOnlySpan<byte> buffer = result switch
        {
            OperationStatus.Done => bytes[..stringLength], // Correct character
            OperationStatus.NeedMoreData => new(), // Standalone Surrogate or empty
            OperationStatus.InvalidData => bytes[..(stringLength - consumed)]
        };
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

        return Encoding.UTF8.GetString(buffer);
    }

    public bool SetStringField(StructFieldDescriptor field, string value)
    {
        if (field.Type.Type != StructFieldType.Char)
        {
            ThrowHelper.ThrowInvalidOperationException("field is not char type");
        }
        if (field.Parent != Descriptor)
        {
            ThrowHelper.ThrowArgumentException("field is not part of this struct");
        }
        if (!Descriptor.IsValid)
        {
            ThrowHelper.ThrowInvalidOperationException("struct descriptor is not valid");
        }

        Span<byte> bytes = Buffer.Span[field.Offset..field.ArraySize];
        Encoding.UTF8.GetEncoder().Convert(value, bytes, false, out int _, out int bytesUsed, out bool complete);
        bytes[bytesUsed..].Clear();
        return complete;
    }

    public DynamicStruct GetStructField(StructFieldDescriptor field, int arrIndex = 0)
    {
        if (field.Type.Type != StructFieldType.Struct)
        {
            throw new StructException("Field is not a struct");
        }
        if (field.Parent != Descriptor)
        {
            ThrowHelper.ThrowArgumentException("Field is not part of this struct");
        }
        if (!Descriptor.IsValid)
        {
            throw new StructException("Invalid descriptor");
        }
        Guard.IsInRange(arrIndex, 0, field.ArraySize);
        StructDescriptor strct = field.Struct!;

        return Wrap(strct, Buffer[(field.Offset + arrIndex * strct.Size)..]);
    }

    public void SetStructField(StructFieldDescriptor field, DynamicStruct value, int arrIndex)
    {
        if (field.Type.Type != StructFieldType.Struct)
        {
            throw new StructException("Field is not a struct");
        }
        if (field.Parent != Descriptor)
        {
            ThrowHelper.ThrowArgumentException("Field is not part of this struct");
        }
        if (!Descriptor.IsValid)
        {
            throw new StructException("Invalid descriptor");
        }
        StructDescriptor strct = field.Struct!;
        if (value.Descriptor != strct)
        {
            throw new StructException("Descriptor does not match");
        }
        if (!value.Descriptor.IsValid)
        {
            throw new StructException("Invalid descriptor");
        }
        Guard.IsInRange(arrIndex, 0, field.ArraySize);

        value.Buffer.Span[..value.Descriptor.Size].CopyTo(Buffer.Span[(field.Offset + arrIndex * strct.Size)..]);
    }

    private long GetFieldImpl(StructFieldDescriptor field, int arrIndex)
    {
        if (!(field.Parent == Descriptor))
        {
            ThrowHelper.ThrowArgumentException("Field is not part of this struct");
        }
        if (!Descriptor.IsValid)
        {
            throw new StructException("Invalid descriptor");
        }
        Guard.IsInRange(arrIndex, 0, field.ArraySize);

        ReadOnlySpan<byte> data = Buffer.Span;

#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
        long val = field.Size switch
        {
            1 => data[field.Offset + arrIndex],
            2 => BinaryPrimitives.ReadInt16LittleEndian(data[(field.Offset + arrIndex * 2)..]),
            4 => BinaryPrimitives.ReadInt32LittleEndian(data[(field.Offset + arrIndex * 4)..]),
            8 => BinaryPrimitives.ReadInt64LittleEndian(data[(field.Offset + arrIndex * 8)..]),
        };
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

        if (field.IsUint || field.Type.Type == StructFieldType.Bool)
        {
            // for unsigned fields, we can simply logical shift and mask
            return (val >>> field.BitShift) & field.BitMask;
        }
        else
        {
            // to get sign extension, shift so the sign bit within the bitfield goes to the long's sign
            // bit (also clearing all higher bits), then shift back down (also clearing all lower bits);
            // since upper and lower bits are cleared with the shifts, the bitmask is unnecessary
            return (val << (64 - field.BitShift - field.BitWidth)) >> (64 - field.BitWidth);
        }
    }

    private void SetFieldImpl(StructFieldDescriptor field, long value, int arrIndex)
    {
        if (!(field.Parent == Descriptor))
        {
            ThrowHelper.ThrowArgumentException("Field is not part of this struct");
        }
        if (!Descriptor.IsValid)
        {
            throw new StructException("Invalid descriptor");
        }
        Guard.IsInRange(arrIndex, 0, field.ArraySize);

        Span<byte> data = Buffer.Span;

        if (!field.IsBitField)
        {
            switch (field.Size)
            {
                case 1:
                    data[field.Offset + arrIndex] = (byte)value;
                    break;
                case 2:
                    BinaryPrimitives.WriteInt16LittleEndian(data[(field.Offset + arrIndex * 2)..], (short)value);
                    break;
                case 4:
                    BinaryPrimitives.WriteInt32LittleEndian(data[(field.Offset + arrIndex * 4)..], (int)value);
                    break;
                case 8:
                    BinaryPrimitives.WriteInt64LittleEndian(data[(field.Offset + arrIndex * 8)..], value);
                    break;
                default:
                    throw new StructException("invalid field size");
            }
            return;
        }

        switch (field.Size)
        {
            case 1:
                {
                    byte val = data[field.Offset + arrIndex];
                    val &= (byte)~(field.BitMask << field.BitShift);
                    val |= (byte)((value & field.BitMask) << field.BitShift);
                    data[field.Offset + arrIndex] = val;
                    break;
                }
            case 2:
                {
                    Span<byte> slice = data[(field.Offset + arrIndex * 2)..];
                    short val = BinaryPrimitives.ReadInt16LittleEndian(slice);
                    val &= (short)~(field.BitMask << field.BitShift);
                    val |= (short)((value & field.BitMask) << field.BitShift);
                    BinaryPrimitives.WriteInt16LittleEndian(slice, val);
                    break;
                }
            case 4:
                {
                    Span<byte> slice = data[(field.Offset + arrIndex * 4)..];
                    int val = BinaryPrimitives.ReadInt16LittleEndian(slice);
                    val &= (int)~(field.BitMask << field.BitShift);
                    val |= (int)((value & field.BitMask) << field.BitShift);
                    BinaryPrimitives.WriteInt32LittleEndian(slice, val);
                    break;
                }
            case 8:
                {
                    Span<byte> slice = data[(field.Offset + arrIndex * 8)..];
                    long val = BinaryPrimitives.ReadInt16LittleEndian(slice);
                    val &= ~(field.BitMask << field.BitShift);
                    val |= (value & field.BitMask) << field.BitShift;
                    BinaryPrimitives.WriteInt64LittleEndian(slice, val);
                    break;
                }
            default:
                throw new StructException("invalid field size");
        }
    }
}
