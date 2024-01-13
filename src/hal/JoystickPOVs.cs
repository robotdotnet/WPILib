using System.Runtime.InteropServices;

namespace WPIHal;

[StructLayout(LayoutKind.Sequential)]
public readonly struct JoystickPOVs
{

    [System.Runtime.CompilerServices.InlineArray(12)]
    public struct PovsBuffer
    {
        private short _element0;
    }

    private readonly short m_count;
    private readonly PovsBuffer m_povs;

    public readonly int Count => m_count;

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

}
