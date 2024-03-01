using System.Text;

namespace WPIUtil.Serialization.Struct.Parsing;

public ref struct Lexer(ReadOnlySpan<byte> inStr)
{
    private Utf8CodePointEnumerator m_enumerator = new(inStr);

    public Lexer() : this(default)
    {

    }

    public readonly int Position => m_enumerator.CurrentMark;

    public TokenKind Scan()
    {
        // skip whitespace
        bool hasMoreData;
        do
        {
            m_enumerator.Mark();
            hasMoreData = m_enumerator.MoveNext();
        } while (hasMoreData && Rune.IsWhiteSpace(m_enumerator.Current));

        if (!hasMoreData)
        {
            // String has nothing left
            return TokenKind.EndOfInput;
        }

        switch (m_enumerator.Current.Value)
        {
            case '[':
                return TokenKind.LeftBracket;
            case ']':
                return TokenKind.RightBracket;
            case '{':
                return TokenKind.LeftBrace;
            case '}':
                return TokenKind.RightBrace;
            case ':':
                return TokenKind.Colon;
            case ';':
                return TokenKind.Semicolon;
            case ',':
                return TokenKind.Comma;
            case '=':
                return TokenKind.Equals;
            case '-':
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                return ScanInteger();
            case '\0':
                return TokenKind.EndOfInput;
            default:
                if (Rune.IsLetter(m_enumerator.Current) || m_enumerator.Current.Value == '_')
                {
                    return ScanIdentifier();
                }
                return TokenKind.Unknown;
        }
    }

    public readonly ReadOnlySpan<byte> GetTokenText()
    {
        return m_enumerator.MarkedSpan;
    }

    private TokenKind ScanInteger()
    {
        bool hasMoreData;
        do
        {
            hasMoreData = m_enumerator.MoveNext();
        } while (hasMoreData && Rune.IsDigit(m_enumerator.Current));

        if (hasMoreData)
        {
            m_enumerator.MovePrevious();
        }
        return TokenKind.Integer;
    }

    private TokenKind ScanIdentifier()
    {
        bool hasMoreData;
        do
        {
            hasMoreData = m_enumerator.MoveNext();
        } while (hasMoreData && (Rune.IsLetterOrDigit(m_enumerator.Current) || m_enumerator.Current.Value == '_'));

        if (hasMoreData)
        {
            m_enumerator.MovePrevious();
        }

        return TokenKind.Identifier;
    }
}
