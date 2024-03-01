using System.Buffers;
using System.Text;

namespace WPIUtil.Serialization.Struct.Parsing;

public ref struct Utf8CodePointEnumerator(ReadOnlySpan<byte> str)
{
    public Utf8CodePointEnumerator() : this(default)
    {

    }

    private readonly ReadOnlySpan<byte> m_str = str;
    private int m_index;
    public Rune Current { readonly get; private set; } = Rune.ReplacementChar;

    public int CurrentMark { readonly get; private set; }

    public readonly ReadOnlySpan<byte> MarkedSpan => m_str[CurrentMark..m_index];

    public void Mark()
    {
        CurrentMark = m_index;
    }

    public bool MoveNext()
    {
        var status = Rune.DecodeFromUtf8(m_str[m_index..], out var result, out var bytesConsumed);
        m_index += bytesConsumed;
        Current = result;

        return status != OperationStatus.NeedMoreData;
    }

    public bool MovePrevious()
    {
        var status = Rune.DecodeLastFromUtf8(m_str[..m_index], out var result, out var bytesConsumed);
        m_index -= bytesConsumed;
        Current = result;

        return status != OperationStatus.NeedMoreData;
    }
}
