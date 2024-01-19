using System;
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
        if (data.Length < Descriptor.Size)
        {
            throw new Exception("TODO Custom exception underflow");
        }
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
        // If string is all zeroes, its empty and return an empty string.
        if (stringLength == 0)
        {
            return "";
        }
        // Check if the end of the string is in the middle of a continuation byte or not.
        if ((bytes[stringLength - 1] & 0x80) != 0)
        {
            // This is a UTF8 continuation byte. Make sure its valid.
            // Walk back until initial byte is found
            int utf8StartByte = stringLength;
            for (; utf8StartByte > 0; utf8StartByte--)
            {
                if ((bytes[utf8StartByte - 1] & 0x40) != 0)
                {
                    // Having 2nd bit set means start byte
                    break;
                }
            }
            if (utf8StartByte == 0)
            {
                // This case means string only contains continuation bytes
                return "";
            }
            utf8StartByte--;
            // Check if its a 2, 3, or 4 byte
            byte checkByte = bytes[utf8StartByte];
            if ((checkByte & 0xE0) == 0xC0 && utf8StartByte != stringLength - 2)
            {
                // 2 byte, need 1 more byte
                stringLength = utf8StartByte;
            }
            else if ((checkByte & 0xF0) == 0xE0 && utf8StartByte != stringLength - 3)
            {
                // 3 byte, need 2 more bytes
                stringLength = utf8StartByte;
            }
            else if ((checkByte & 0xF8) == 0xF0 && utf8StartByte != stringLength - 4)
            {
                // 4 byte, need 3 more bytes
                stringLength = utf8StartByte;
            }
            // If we get here, the string is either completely garbage or fine.
        }

        return Encoding.UTF8.GetString(bytes[..stringLength]);
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
            throw new Exception();
        }
        if (field.Parent != Descriptor)
        {
            ThrowHelper.ThrowArgumentException("Field is not part of this struct");
        }
        if (!Descriptor.IsValid)
        {
            throw new Exception("TODO better exception");
        }
        if (arrIndex < 0 || arrIndex >= field.ArraySize)
        {
            throw new IndexOutOfRangeException("TODO");
        }
        StructDescriptor strct = field.Struct!;
        return Wrap(strct, Buffer[(field.Offset + arrIndex * strct.Size)..]);
    }

    public void SetStructField(StructFieldDescriptor field, DynamicStruct value, int arrIndex)
    {
        if (field.Type.Type != StructFieldType.Struct)
        {
            throw new Exception();
        }
        if (field.Parent != Descriptor)
        {
            ThrowHelper.ThrowArgumentException("Field is not part of this struct");
        }
        if (!Descriptor.IsValid)
        {
            throw new Exception("TODO better exception");
        }
        StructDescriptor strct = field.Struct!;
        if (value.Descriptor != strct)
        {
            throw new Exception();
        }
        if (!value.Descriptor.IsValid)
        {
            throw new Exception();
        }
        if (arrIndex < 0 || arrIndex >= field.ArraySize)
        {
            throw new IndexOutOfRangeException("TODO");
        }

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
            throw new Exception("TODO better exception");
        }
        if (arrIndex < 0 || arrIndex >= field.ArraySize)
        {
            throw new IndexOutOfRangeException("TODO");
        }

        ReadOnlySpan<byte> data = Buffer.Span;

        long val = field.Size switch
        {
            1 => data[field.Offset + arrIndex],
            2 => BinaryPrimitives.ReadInt16LittleEndian(data[(field.Offset + arrIndex * 2)..]),
            4 => BinaryPrimitives.ReadInt32LittleEndian(data[(field.Offset + arrIndex * 4)..]),
            8 => BinaryPrimitives.ReadInt64LittleEndian(data[(field.Offset + arrIndex * 8)..]),
            _ => throw new Exception("Illegal state"),
        };

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
            throw new Exception("TODO better exception");
        }
        if (arrIndex < 0 || arrIndex >= field.ArraySize)
        {
            throw new IndexOutOfRangeException("TODO");
        }

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
                    throw new Exception("invalid field size");
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
                throw new Exception("invalid field size");
        }
    }
}
