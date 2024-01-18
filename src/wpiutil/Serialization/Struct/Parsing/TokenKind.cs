namespace WPIUtil.Serialization.Struct.Parsing;

public enum TokenKind
{
    Unknown,
    Integer,
    Identifier,
    LeftBracket,
    RightBracket,
    LeftBrace,
    RightBrace,
    Colon,
    Semicolon,
    Comma,
    Equals,
    EndOfInput
}
