using CommunityToolkit.Diagnostics;
using UnitsNet;

namespace WPIMath.Filter;

public class Debouncer
{
    public enum DebounceType
    {
        Rising,
        Falling,
        Both
    }

    private readonly Duration m_debounceTime;
    private readonly DebounceType m_debounceType;
    private bool m_baseline;

    private Duration m_prevTime;

    public Debouncer(Duration debounceTime, DebounceType type = DebounceType.Rising)
    {
        m_debounceTime = debounceTime;
        m_debounceType = type;

        ResetTimer();

        switch (m_debounceType)
        {
            case DebounceType.Both:
            case DebounceType.Rising:
                m_baseline = false;
                break;
            case DebounceType.Falling:
                m_baseline = true;
                break;
            default:
                ThrowHelper.ThrowArgumentOutOfRangeException(nameof(type));
                break;
        }
    }

    private void ResetTimer()
    {
        m_prevTime = MathSharedStore.Timestamp;
    }

    private bool Elapsed => MathSharedStore.Timestamp - m_prevTime >= m_debounceTime;

    public bool Calculate(bool input)
    {
        if (input == m_baseline)
        {
            ResetTimer();
        }

        if (Elapsed)
        {
            if (m_debounceType == DebounceType.Both)
            {
                m_baseline = input;
                ResetTimer();
            }
            return input;
        }
        else
        {
            return m_baseline;
        }
    }
}
