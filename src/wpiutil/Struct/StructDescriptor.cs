using System;
using System.Collections.Generic;

namespace WPIUtil;

public class StructPropertyDescriptor {
    public int Offset {get; internal set;}
    public int Size {get; internal set;}
    public int ArraySize {get; internal set;}
    public bool IsBitfield {get; internal set;}
    public int BitWidth {get; internal set;}
    public int BitShift {get; internal set;}
    public StructPropertyType Type {get; internal set;}
    public StructDescriptor? Struct {get; internal set;}

    public StructDescriptor? Parent {get; internal set;}
}

public class StructDescriptor {
    public string Name {get;}
    public string? Schema {get; internal set;}

    internal StructDescriptor(string name) {
        Name = name;
    }

    public bool IsValid {get; internal set;}

    private int m_size;

    public int Size {
        get {
            if (!IsValid) {
                throw new InvalidOperationException("descriptor is invalid");
            }
            return m_size;
        }
    }

    public StructPropertyDescriptor? FindPropertyByName(string name) {
        return m_propertiesByName.GetValueOrDefault(name);
    }

    public List<StructPropertyDescriptor> Properties {get;} = [];

    internal readonly List<StructDescriptor> m_references = [];
    internal readonly Dictionary<string, StructPropertyDescriptor> m_propertiesByName = [];

    internal bool CheckCircular(Stack<StructDescriptor> stack) {
        stack.Push(this);
        foreach (StructDescriptor desc in m_references) {
            if (stack.Contains(desc)) {
                return false;
            }
            if (!desc.CheckCircular(stack)) {
                return false;
            }
        }
        stack.Pop();
        return true;
    }

    internal void CalculateOffsets(Stack<StructDescriptor> stack) {
        int offset = 0;
        int shift = 0;
        int prevBitfieldSize = 0;
        foreach (StructPropertyDescriptor property in Properties) {
            if (!property.IsBitfield) {
                shift = 0; // reset shift on non-bitfield element
                offset += prevBitfieldSize; // finish bitfield if active
                prevBitfieldSize = 0; // previous is now not bitfield
                property.Offset = offset;
                StructDescriptor? descriptor = property.Struct;
                if (descriptor is not null) {
                    if (!descriptor.IsValid) {
                        IsValid = false;
                        return;
                    }
                    property.Size = descriptor.Size;
                }
                offset += property.Size * property.ArraySize;
            } else {
                int bitWidth = property.BitWidth;
                if (property.Type == StructPropertyType.Bool && prevBitfieldSize != 0 && (shift + 1) <= (prevBitfieldSize * 8)) {
                    // bool takes on size of preceding bitfield type (if it fits)
                    property.Size = prevBitfieldSize;
                } else if (property.Size != prevBitfieldSize || (shift + bitWidth) > (property.Size * 8)) {
                    shift = 0;
                    offset += prevBitfieldSize;
                }
                prevBitfieldSize = property.Size;
                property.Offset = offset;
                property.BitShift = shift;
                shift += bitWidth;
            }
        }

        m_size = offset + prevBitfieldSize;
        IsValid = true;

        stack.Push(this);
        foreach (StructDescriptor desc in m_references)
        {
            if (stack.Contains(desc)) {
                throw new InvalidOperationException($"internal error (inconsistent data): circular struct reference betweent {Name} and {desc.Name}");
            }
            desc.CalculateOffsets(stack);
        }
        stack.Pop();
    }
}
