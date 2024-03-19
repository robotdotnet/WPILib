using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace WPIHal;

[StructLayout(LayoutKind.Sequential)]
public readonly struct JoystickAxes
{
    public const int NumJoystickAxes = 12;

    [System.Runtime.CompilerServices.InlineArray(NumJoystickAxes)]
    public struct AxesBuffer
    {
        private float _element0;
    }

    [System.Runtime.CompilerServices.InlineArray(NumJoystickAxes)]
    public struct RawBuffer
    {
        private byte _element0;
    }
    private readonly short m_count;
    private readonly AxesBuffer m_axes;
    private readonly RawBuffer m_raw;

    public readonly int Count => m_count;

    [UnscopedRef]
    public ReadOnlySpan<float> AxesSpan => m_axes[..Count];

    [UnscopedRef]
    public ReadOnlySpan<byte> RawSpan => m_raw[..Count];

    public readonly double? this[int index]
    {
        get
        {
            if (index < m_count)
            {
                return m_axes[index];
            }
            else
            {
                return null;
            }
        }
    }

    public readonly byte? GetRaw(int index)
    {
        if (index < m_count)
        {
            return m_raw[index];
        }
        else
        {
            return null;
        }

    }

    public bool IsEqual(ref readonly JoystickAxes other)
    {
        // Assumed that if raw is equal, floats are equal
        return m_count == other.Count && RawSpan.SequenceEqual(other.RawSpan);
    }
}
