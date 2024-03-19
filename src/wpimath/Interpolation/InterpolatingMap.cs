using System.Numerics;

namespace WPIMath.Interpolation;

public class InterpolatingMap<T> where T : struct,
                                                         IFloatingPointIeee754<T>
{
    // TODO find a better solution to this
    private readonly List<(T key, T value)> m_map = [];
    private readonly IComparer<(T key, T value)> m_comparer = new KeyCoercingComparer();

    public void Add(T key, T value)
    {
        // Doing this ensures the tree is always sorted.
        int idx = m_map.BinarySearch((key, value), m_comparer);
        m_map.Insert(~idx, (key, value));
    }

    public T this[T key]
    {
        get
        {
            if (m_map.Count == 0)
            {
                return T.Zero;
            }

            int idx = m_map.BinarySearch((key, T.Zero));
            if (idx >= 0)
            {
                return m_map[idx].value;
            }

            int larger = ~idx;

            // Request is smaller than all elements, return smallest
            if (larger == 0)
            {
                return m_map[larger].value;
            }

            // Request is larger than all elements, return largest
            if (larger == m_map.Count)
            {
                return m_map[^1].value;
            }

            (T key, T value) lower = m_map[larger - 1];
            (T key, T value) upper = m_map[larger];

            T delta = (key - lower.key) / (upper.key - lower.key);
            return T.Lerp(lower.value, upper.value, delta);
        }
    }

    public void Clear()
    {
        m_map.Clear();
    }

    private sealed class KeyCoercingComparer : IComparer<(T key, T value)>
    {
        public int Compare((T key, T value) x, (T key, T value) y)
        {
            int result = x.key.CompareTo(y.key);
            return result;
        }
    }
}
