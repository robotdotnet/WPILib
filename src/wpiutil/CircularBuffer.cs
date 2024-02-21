namespace WPIUtil;

public class CircularBuffer<T>(int size)
{
    private T[] m_data = new T[size];
    private int m_front;
    private int m_length;

    public int Size => m_length;

    public T GetFirst() => m_data[m_front];

    public T GetLast() => m_data[(m_front + m_length - 1) % m_data.Length];

    public void AddFirst(T value)
    {
        if (m_data.Length == 0)
        {
            return;
        }

        m_front = ModuloDec(m_front);

        m_data[m_front] = value;

        if (m_length < m_data.Length)
        {
            m_length++;
        }
    }

    public void AddLast(T value)
    {
        if (m_data.Length == 0)
        {
            return;
        }

        m_data[(m_front + m_length) % m_data.Length] = value;

        if (m_length < m_data.Length)
        {
            m_length++;
        }
        else
        {
            m_front = ModuloInc(m_front);
        }
    }

    public T RemoveFirst()
    {

        T temp = m_data[m_front];
        m_front = ModuloInc(m_front);
        m_length--;
        return temp;
    }

    public T RemoveLast()
    {
        m_length--;
        return m_data[(m_front + m_length) % m_data.Length];
    }

    public void Resize(int size)
    {
        T[] newBuffer = new T[size];
        m_length = Math.Min(m_length, size);
        for (int i = 0; i < m_length; i++)
        {
            newBuffer[i] = m_data[(m_front + i) % m_data.Length];
        }
        m_data = newBuffer;
        m_front = 0;
    }

    public void Clear()
    {
        Array.Clear(m_data);
        m_front = 0;
        m_length = 0;
    }

    public T this[int index] => m_data[(m_front + index) % m_data.Length];

    private int ModuloInc(int index)
    {
        return (index + 1) % m_data.Length;
    }

    private int ModuloDec(int index)
    {
        if (index == 0)
        {
            return m_data.Length - 1;
        }
        else
        {
            return index - 1;
        }
    }
}
