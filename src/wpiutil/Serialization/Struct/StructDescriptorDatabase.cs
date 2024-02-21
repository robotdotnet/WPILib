using System.Runtime.InteropServices;
using WPIUtil.Serialization.Struct.Parsing;

namespace WPIUtil.Serialization.Struct;

public sealed class StructDescriptorDatabase
{
    public StructDescriptor Add(string name, ReadOnlySpan<byte> schema)
    {
        Parser parser = new(schema);
        ParsedSchema parsed;
        try
        {
            parsed = parser.Parse();
        }
        catch (ParseException e)
        {
            throw new BadSchemaException("parse error", e);
        }

        // turn parsed schema into descriptors
        ref StructDescriptor? theStructRef = ref CollectionsMarshal.GetValueRefOrAddDefault(m_structs, name, out bool exists);
        if (!exists)
        {
            theStructRef = new(name);
        }
        // Store non ref in local variable, as we will invalidate the ref later
        StructDescriptor theStruct = theStructRef!;
        theStruct.Fields.Clear();
        bool isValid = true;
        foreach (ref readonly ParsedDeclaration decl in parsed.Declarations)
        {
            ref readonly StructFieldTypeInfo type = ref StructFieldTypeHelpers.GetTypeInfoForString(decl.TypeString);
            int size = type.Size;

            // bitfield checks
            if (decl.BitWidth != 0)
            {
                // only integer or boolean types are allowed
                if (!type.IsInt && !type.IsUint && type.Type != StructFieldType.Bool)
                {
                    throw new BadSchemaException(decl.Name, $"type {decl.TypeString} cannot be bitfield");
                }

                // bit width cannot be larger than field size
                if (decl.BitWidth > (size * 8))
                {
                    throw new BadSchemaException(decl.Name, $"bit width {decl.BitWidth} exceed type size");
                }

                // bit width must be 1 for booleans
                if (type.Type == StructFieldType.Bool && decl.BitWidth != 1)
                {
                    throw new BadSchemaException(decl.Name, "bit width must be 1 for bool type");
                }

                // cannot combine array and bitfield (shouldn't parse, but double-check)
                if (decl.ArraySize > 1)
                {
                    throw new BadSchemaException(decl.Name, "cannot combine array and bitfield");
                }
            }

            // struct handling
            StructDescriptor? structDesc = null;
            if (type.Type == StructFieldType.Struct)
            {
                // recursive definitions are not allowed
                if (decl.TypeString == name)
                {
                    throw new BadSchemaException(decl.Name, "recursive struct reference");
                }

                // cross-reference struct, creating a placeholder if necessary.
                theStructRef = ref CollectionsMarshal.GetValueRefOrAddDefault(m_structs, decl.TypeString, out exists);
                if (!exists)
                {
                    theStructRef = new(name);
                }
                // Store non ref in local variable, as we will invalidate the ref later
                StructDescriptor aStruct = theStructRef!;
                if (aStruct.IsValid)
                {
                    size = aStruct.Size;
                }
                else
                {
                    isValid = false;
                }

                aStruct.m_references.Add(theStruct);
                structDesc = aStruct;
            }

            StructFieldDescriptor fieldDescriptor = new(theStruct, decl.Name, type, size, decl.ArraySize, decl.BitWidth, decl.EnumValues, structDesc);
            if (!theStruct.m_fieldsByName.TryAdd(decl.Name, fieldDescriptor))
            {
                throw new BadSchemaException(decl.Name, "duplicate field name");
            }

            theStruct.Fields.Add(fieldDescriptor);
        }

        theStruct.IsValid = isValid;
        Stack<StructDescriptor> stack = new();
        if (isValid)
        {
            // we have all the info needed, so calculate field offset & shift
            theStruct.CalculateOffsets(stack);
        }
        else
        {
            // check for circular reference
            if (!theStruct.CheckCircular(stack))
            {
                throw new BadSchemaException("Circular reference");
            }
        }

        return theStruct;
    }

    public StructDescriptor? this[string name]
    {
        get
        {
            if (m_structs.TryGetValue(name, out var value))
            {
                return value;
            }
            return null;
        }
    }

    private readonly Dictionary<string, StructDescriptor> m_structs = [];
}
