namespace WPIUtil.Serialization.Struct.Parsing;

public class ParseException(int pos, string s) : Exception(s)
{
    public int Position { get; } = pos;

    public override string ToString()
    {
        return $"{Position}: {base.Message}";
    }
}
