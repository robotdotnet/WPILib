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

    public IndentedScope EnterScope(int count = 0)
    {
        return new(this, count);
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
        private readonly int m_count;

        public IndentedScope(IndentedStringBuilder builder, int count = 0)
        {
            m_builder = builder;
            builder.AppendFullLine("{");
            m_count = count + 1;
            m_builder.m_scopeCount++;
        }

        public void Dispose()
        {
            for (int i = 0; i < m_count; i++)
            {
                m_builder.m_scopeCount--;
                m_builder.AppendFullLine("}");
            }
        }
    }
}
