namespace WPIUtil.Serialization.Struct;

public class BadSchemaException : Exception
{
    public string Property { get; }

    public BadSchemaException(string message) : base(message)
    {
        Property = "";
    }

    public BadSchemaException(string message, Exception inner) : base(message, inner)
    {
        Property = "";
    }

    public BadSchemaException(string property, string message) : base(message)
    {
        Property = property;
    }

    public override string ToString()
    {
        return $"Property: {Property}, Exception:\n{base.Message}";
    }
}
