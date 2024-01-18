using System;

namespace WPIUtil.Serialization.Struct.Parsing;

public ref struct Lexer(ReadOnlySpan<char> inStr)
{
    private ReadOnlySpan<char> m_in = inStr;
    private char m_current;
    private int m_tokenStart;
    private int m_pos;

    public TokenKind Scan()
    {
        // skip whitespace
        do
        {
            Get();
        } while (m_current == ' ' || m_current == '\t' || m_current == '\n');
        m_tokenStart = m_pos - 1;

        switch (m_current)
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
                if (char.IsLetter(m_current) || m_current == '_')
                {
                    return ScanIdentifier();
                }
                return TokenKind.Unknown;
        }
    }

    public readonly ReadOnlySpan<char> GetTokenText()
    {
        if (m_tokenStart >= m_in.Length)
        {
            return "";
        }
        return m_in[m_tokenStart..m_pos];
    }

    public readonly int Position => m_tokenStart;

    private TokenKind ScanInteger()
    {
        do
        {
            Get();
        } while (char.IsDigit(m_current));
        Unget();
        return TokenKind.Integer;
    }

    private TokenKind ScanIdentifier()
    {
        do
        {
            Get();
        } while (char.IsLetterOrDigit(m_current) || m_current == '_');
        Unget();
        return TokenKind.Identifier;
    }

    private void Get()
    {
        if (m_pos < m_in.Length)
        {
            m_current = m_in[m_pos];
        }
        else
        {
            m_current = '\0';
        }
        m_pos++;
    }

    private void Unget()
    {
        if (m_pos > 0)
        {
            m_pos--;
            if (m_pos < m_in.Length)
            {
                m_current = m_in[m_pos];
            }
            else
            {
                m_current = '\0';
            }
        }
        else
        {
            m_current = '\0';
        }
    }
}
