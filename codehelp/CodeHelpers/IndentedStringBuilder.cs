using System.Text;

namespace WPILib.CodeHelpers;

public enum ScopeType
{
    Namespace,
    Class,
    Struct,
    Interface,
    NonReturningMethod,
    ForEach,
    If,
    Empty
}

public enum LanguageKind
{
    CSharp,
    VisualBasic
}

public class IndentedStringBuilder(LanguageKind language = LanguageKind.CSharp)
{
    private readonly StringBuilder m_builder = new();
    private readonly Stack<ScopeType> m_scopes = new(8);

    public LanguageKind Language { get; } = language;

    private int scopeCount => m_scopes.Count;

    public void StartLine()
    {
        m_builder.Append(' ', scopeCount * 4);
    }

    public void Append(string value)
    {
        m_builder.Append(value);
    }

    public void EndLine()
    {
        m_builder.AppendLine();
    }

    public void AppendFullLine(string line)
    {
        StartLine();
        Append(line);
        EndLine();
    }

    public void Clear()
    {
        m_builder.Clear();
        m_scopes.Clear();
    }

    public void EnterScope(ScopeType type)
    {
        if (Language == LanguageKind.CSharp)
        {
            AppendFullLine("{");
        }
        else if (Language == LanguageKind.VisualBasic)
        {
            // Nothing actually needs to be appended in VB
            // Except for empty, which we fake with an If
            if (type == ScopeType.Empty)
            {
                AppendFullLine("If True");
            }
        }

        m_scopes.Push(type);
    }

    public void ExitScope()
    {
        // TODO VB Support
        var scopeType = m_scopes.Pop();
        if (Language == LanguageKind.CSharp)
        {
            AppendFullLine("}");
        }
        else if (Language == LanguageKind.VisualBasic)
        {
            switch (scopeType)
            {
                case ScopeType.Namespace:
                    AppendFullLine("End Namespace");
                    break;
                case ScopeType.Class:
                    AppendFullLine("End Class");
                    break;
                case ScopeType.Struct:
                    AppendFullLine("End Structure");
                    break;
                case ScopeType.Interface:
                    AppendFullLine("End Interface");
                    break;
                case ScopeType.NonReturningMethod:
                    AppendFullLine("End Sub");
                    break;
                case ScopeType.If:
                    AppendFullLine("End If");
                    break;
                case ScopeType.ForEach:
                    AppendFullLine("Next");
                    break;
                case ScopeType.Empty:
                    AppendFullLine("End If");
                    break;
            }
        }
    }

    public override string ToString()
    {
        return m_builder.ToString();
    }
}
