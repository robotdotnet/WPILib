using System.Text;

namespace WPIUtil.Serialization.Struct.Parsing;

public ref struct Parser(ReadOnlySpan<byte> inStr)
{
    private Lexer m_lexer = new(inStr);
    private TokenKind m_token;

    public Parser() : this(default)
    {

    }

    public ParsedSchema Parse()
    {
        ParsedSchema schema = new ParsedSchema();
        do
        {
            GetNextToken();
            if (m_token == TokenKind.Semicolon)
            {
                continue;
            }
            if (m_token == TokenKind.EndOfInput)
            {
                break;
            }
            schema.AddDeclaration(ParseDeclaration());
        } while (m_token != TokenKind.EndOfInput);
        return schema;
    }

    private ParsedDeclaration ParseDeclaration()
    {
        ParsedDeclaration decl = new(null!, null!, null, 1, 0);

        // optional enum specification
        if (m_token == TokenKind.Identifier && "enum"u8.SequenceEqual(m_lexer.GetTokenText()))
        {
            GetNextToken();
            Expect(TokenKind.LeftBrace);
            decl.EnumValues = ParseEnum();
            GetNextToken();
        }
        else if (m_token == TokenKind.LeftBrace)
        {
            decl.EnumValues = ParseEnum();
            GetNextToken();
        }

        // type name
        Expect(TokenKind.Identifier);
        decl.TypeString = Encoding.UTF8.GetString(m_lexer.GetTokenText());
        GetNextToken();

        // identifier name
        Expect(TokenKind.Identifier);
        decl.Name = Encoding.UTF8.GetString(m_lexer.GetTokenText());
        GetNextToken();

        // array or bit field
        if (m_token == TokenKind.LeftBracket)
        {
            GetNextToken();
            Expect(TokenKind.Integer);
            ReadOnlySpan<byte> valueStr = m_lexer.GetTokenText();
            if (!int.TryParse(valueStr, out int value))
            {
                value = 0;
            }
            if (value > 0)
            {
                decl.ArraySize = value;
            }
            else
            {
                throw new ParseException(
                    m_lexer.Position, $"array size '{Encoding.UTF8.GetString(valueStr)}' is not a positive integer");
            }
            GetNextToken();
            Expect(TokenKind.RightBracket);
            GetNextToken();
        }
        else if (m_token == TokenKind.Colon)
        {
            GetNextToken();
            Expect(TokenKind.Integer);
            ReadOnlySpan<byte> valueStr = m_lexer.GetTokenText();
            if (!int.TryParse(valueStr, out int value))
            {
                value = 0;
            }
            if (value > 0)
            {
                decl.BitWidth = value;
            }
            else
            {
                throw new ParseException(
                    m_lexer.Position, $"bitfield width '{Encoding.UTF8.GetString(valueStr)}' is not a positive integer");
            }
            GetNextToken();
        }

        // declaration must end with EOF or semicolon
        if (m_token != TokenKind.EndOfInput)
        {
            Expect(TokenKind.Semicolon);
        }

        return decl;
    }

    private Dictionary<string, long> ParseEnum()
    {
        Dictionary<string, long> map = [];

        // we start with current = '{'
        GetNextToken();
        while (m_token != TokenKind.RightBrace)
        {
            Expect(TokenKind.Identifier);
            ReadOnlySpan<byte> name = m_lexer.GetTokenText();
            GetNextToken();
            Expect(TokenKind.Equals);
            GetNextToken();
            Expect(TokenKind.Integer);
            ReadOnlySpan<byte> valueStr = m_lexer.GetTokenText();
            if (!long.TryParse(valueStr, out long value))
            {
                throw new ParseException(m_lexer.Position, $"could not parse enum value '{Encoding.UTF8.GetString(valueStr)}'");
            }
            map.Add(Encoding.UTF8.GetString(name), value);
            GetNextToken();
            if (m_token == TokenKind.RightBrace)
            {
                break;
            }
            Expect(TokenKind.Comma);
            GetNextToken();
        }
        return map;
    }

    private TokenKind GetNextToken()
    {
        m_token = m_lexer.Scan();
        return m_token;
    }

    private readonly void Expect(TokenKind kind)
    {
        if (m_token != kind)
        {
            throw new ParseException(m_lexer.Position, $"Expected {kind}, got '{Encoding.UTF8.GetString(m_lexer.GetTokenText())}'");
        }
    }
}
