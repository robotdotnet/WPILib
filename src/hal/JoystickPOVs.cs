using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace WPIHal;

[StructLayout(LayoutKind.Sequential)]
public readonly struct JoystickPOVs
{
    public const int NumJoystickPOVs = 12;

    [System.Runtime.CompilerServices.InlineArray(NumJoystickPOVs)]
    public struct PovsBuffer
    {
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
        private short _element0;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore IDE0044 // Add readonly modifier
    }

    private readonly short m_count;
    private readonly PovsBuffer m_povs;

    public readonly int Count => m_count;

    [UnscopedRef]
    public ReadOnlySpan<short> PovsSpan => m_povs[..Count];

    public readonly int? this[int index]
    {
        get
        {
            if (index < m_count)
            {
                return m_povs[index];
            }
            else
            {
                return null;
            }
        }
    }

    public bool IsEqual(ref readonly JoystickPOVs other)
    {
        return m_count == other.Count && PovsSpan.SequenceEqual(other.PovsSpan);
    }
}
