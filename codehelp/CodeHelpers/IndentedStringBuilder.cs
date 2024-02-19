using System.Text;

namespace WPILib.CodeHelpers;

public class IndentedStringBuilder
{
    private readonly StringBuilder m_builder = new();
    private int m_scopeCount = 0;

    public void StartLine()
    {
        m_builder.Append(' ', m_scopeCount * 4);
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

    public IndentedScope EnterScope()
    {
        return new(this);
    }

    public void EnterManualScope()
    {
        AppendFullLine("{");
        m_scopeCount++;
    }

    public void ExitManualScope()
    {
        m_scopeCount--;
        AppendFullLine("}");
    }

    public override string ToString()
    {
        return m_builder.ToString();
    }

    public readonly ref struct IndentedScope
    {
        private readonly IndentedStringBuilder m_builder;

        public IndentedScope(IndentedStringBuilder builder)
        {
            m_builder = builder;
            builder.AppendFullLine("{");
            m_builder.m_scopeCount++;
        }

        public void Dispose()
        {
            m_builder.m_scopeCount--;
            m_builder.AppendFullLine("}");
        }
    }
}
