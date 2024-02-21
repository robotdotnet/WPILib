namespace WPIUtil.Serialization.Struct;

public sealed class StructDescriptor
{
    public string Name { get; }

    internal StructDescriptor(string name)
    {
        Name = name;
    }

    public bool IsValid { get; internal set; }

    private int m_size;

    public int Size
    {
        get
        {
            if (!IsValid)
            {
                throw new InvalidOperationException("descriptor is invalid");
            }
            return m_size;
        }
    }

    public StructFieldDescriptor? FindFieldByName(string name)
    {
        return m_fieldsByName.GetValueOrDefault(name);
    }

    public List<StructFieldDescriptor> Fields { get; } = [];

    internal readonly List<StructDescriptor> m_references = [];
    internal readonly Dictionary<string, StructFieldDescriptor> m_fieldsByName = [];

    internal bool CheckCircular(Stack<StructDescriptor> stack)
    {
        stack.Push(this);
        foreach (StructDescriptor desc in m_references)
        {
            if (stack.Contains(desc))
            {
                return false;
            }
            if (!desc.CheckCircular(stack))
            {
                return false;
            }
        }
        stack.Pop();
        return true;
    }

    internal void CalculateOffsets(Stack<StructDescriptor> stack)
    {
        int offset = 0;
        int shift = 0;
        int prevBitfieldSize = 0;
        foreach (StructFieldDescriptor field in Fields)
        {
            if (!field.IsBitField)
            {
                shift = 0; // reset shift on non-bitfield element
                offset += prevBitfieldSize; // finish bitfield if active
                prevBitfieldSize = 0; // previous is now not bitfield
                field.Offset = offset;
                StructDescriptor? descriptor = field.Struct;
                if (descriptor is not null)
                {
                    if (!descriptor.IsValid)
                    {
                        IsValid = false;
                        return;
                    }
                    field.Size = descriptor.Size;
                }
                offset += field.Size * field.ArraySize;
            }
            else
            {
                int bitWidth = field.BitWidth;
                if (field.Type.Type == StructFieldType.Bool && prevBitfieldSize != 0 && (shift + 1) <= (prevBitfieldSize * 8))
                {
                    // bool takes on size of preceding bitfield type (if it fits)
                    field.Size = prevBitfieldSize;
                }
                else if (field.Size != prevBitfieldSize || (shift + bitWidth) > (field.Size * 8))
                {
                    shift = 0;
                    offset += prevBitfieldSize;
                }
                prevBitfieldSize = field.Size;
                field.Offset = offset;
                field.BitShift = shift;
                shift += bitWidth;
            }
        }

        m_size = offset + prevBitfieldSize;
        IsValid = true;

        stack.Push(this);
        foreach (StructDescriptor desc in m_references)
        {
            if (stack.Contains(desc))
            {
                throw new InvalidOperationException($"internal error (inconsistent data): circular struct reference betweent {Name} and {desc.Name}");
            }
            desc.CalculateOffsets(stack);
        }
        stack.Pop();
    }
}
