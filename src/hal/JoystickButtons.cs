using System.Runtime.InteropServices;

namespace WPIHal;

[StructLayout(LayoutKind.Sequential)]
public readonly struct JoystickButtons
{
    private readonly uint m_buttons;
    private readonly byte m_count;

    public int Count => m_count;

    public uint Buttons => m_buttons;

    public ReadOnlySpan<bool> ToSpan(Span<bool> storage)
    {
        for (int i = 0; i < m_count; i++)
        {
            storage[i] = (m_buttons & i) != 0;
        }
        return storage[..m_count];
    }

    public bool? this[int index]
    {
        get
        {
            if (index < m_count)
            {
                return (m_buttons & index) != 0;
            }
            else
            {
                return null;
            }
        }
    }

    public bool IsEqual(JoystickButtons other)
    {
        return m_count == other.m_count && m_buttons == other.m_buttons;
    }
}
