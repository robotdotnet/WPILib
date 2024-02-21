using System.Numerics;

namespace WPIMath.Interpolation;

public class InterpolatingMap<T>(IComparer<T> comparer) where T : notnull,
                                                         IFloatingPointIeee754<T>
{
    // TODO find a better solution to this
    private readonly List<(T key, T value)> m_map = [];
    private readonly Comparison<(T key, T value)> m_comparer = (a, b) => comparer.Compare(a.key, b.key);
    private readonly IComparer<T> m_keyComparer = comparer;

    public InterpolatingMap() : this(Comparer<T>.Default)
    {
    }

    public void Add(T key, T value)
    {
        int idx = m_map.FindIndex(x => m_keyComparer.Compare(x.key, key) == 0);
        if (idx < 0)
        {
            m_map.Add((key, value));
        }
        else
        {
            var val = m_map[idx];
            m_map[idx] = (key, val.value);
        }
        m_map.Sort(m_comparer);
    }

    public T this[T key]
    {
        get
        {
            if (m_map.Count == 0)
            {
                return T.Zero;
            }

            // List is already sorted
            (T key, T value) lower = m_map[0];
            (T key, T value) upper = m_map[^1];

            // Binary search
            int minimum = 0;
            int maximum = m_map.Count - 1;
            while (minimum <= maximum)
            {
                int midpoint = (minimum + maximum) / 2;
                int compare = m_keyComparer.Compare(key, m_map[midpoint].key);
                if (compare == 0)
                {
                    return m_map[midpoint].value;
                }
                else if (compare < 0)
                {
                    upper = m_map[midpoint];
                    maximum = midpoint - 1;
                }
                else
                {
                    lower = m_map[midpoint];
                    minimum = midpoint + 1;
                }
            }

            if (m_comparer(lower, upper) == 0)
            {
                return lower.value;
            }

            T delta = (key - lower.key) / (upper.key - lower.key);
            return T.Lerp(lower.value, upper.value, delta);
        }
    }

    public void Clear()
    {
        m_map.Clear();
    }
}
