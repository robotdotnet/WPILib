namespace WPIUtil.Serialization.Struct.Parsing;

public record struct ParsedDeclaration(string TypeString, string Name, Dictionary<string, long>? EnumValues, int ArraySize, int BitWidth);
