namespace WPIUtil.Serialization.Struct.Parsing;

public enum TokenKind
{
    Unknown,
#pragma warning disable CA1720 // Identifier contains type name
    Integer,
#pragma warning restore CA1720 // Identifier contains type name
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
