using System.Runtime.InteropServices;

namespace WPIUtil.Serialization.Struct.Parsing;

public sealed class ParsedSchema()
{
    private List<ParsedDeclaration> m_declarations = [];

    public ReadOnlySpan<ParsedDeclaration> Declarations => CollectionsMarshal.AsSpan(m_declarations);

    public void AddDeclaration(in ParsedDeclaration declaration)
    {
        m_declarations.Add(declaration);
    }
}
