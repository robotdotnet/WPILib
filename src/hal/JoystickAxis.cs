using System.Runtime.InteropServices;

namespace WPIHal;

[StructLayout(LayoutKind.Sequential)]
public readonly struct JoystickAxes
{
    [System.Runtime.CompilerServices.InlineArray(12)]
    public struct AxesBuffer
    {
        private float _element0;
    }

    [System.Runtime.CompilerServices.InlineArray(12)]
    public struct RawBuffer
    {
        private byte _element0;
    }
    private readonly short m_count;
    private readonly AxesBuffer m_axes;
    private readonly RawBuffer m_raw;

    public readonly int Count => m_count;

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
}
