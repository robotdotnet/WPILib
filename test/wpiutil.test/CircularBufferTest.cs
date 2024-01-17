using Xunit;

namespace WPIUtil.Test;

public class CircularBufferTest
{
    public const double Epsilon = 1e-9;

    private readonly double[] m_values = [751.848, 766.366, 342.657, 234.252, 716.126, 132.344, 445.697, 22.727, 421.125, 799.913];
    private readonly double[] m_addFirstOut = [799.913, 421.125, 22.727, 445.697, 132.344, 716.126, 234.252, 342.657];
    private readonly double[] m_addLastOut = [342.657, 234.252, 716.126, 132.344, 445.697, 22.727, 421.125, 799.913];

    [Fact]
    public void AddFirstTest()
    {
        var queue = new CircularBuffer<double>(8);

        foreach (var value in m_values)
        {
            queue.AddFirst(value);
        }

        for (int i = 0; i < m_addFirstOut.Length; i++)
        {
            Assert.Equal(m_addFirstOut[i], queue[i], Epsilon);
        }
    }

    [Fact]
    public void AddLastTest()
    {
        var queue = new CircularBuffer<double>(8);

        foreach (var value in m_values)
        {
            queue.AddLast(value);
        }

        for (int i = 0; i < m_addLastOut.Length; i++)
        {
            Assert.Equal(m_addLastOut[i], queue[i], Epsilon);
        }
    }

    [Fact]
    public void PushPopTest()
    {
        var queue = new CircularBuffer<double>(3);

        queue.AddLast(1.0);
        queue.AddLast(2.0);
        queue.AddLast(3.0);

        Assert.Equal(1.0, queue[0], Epsilon);
        Assert.Equal(2.0, queue[1], Epsilon);
        Assert.Equal(3.0, queue[2], Epsilon);

        queue.AddLast(4.0); // Overwrite 1 with 4

        Assert.Equal(2.0, queue[0], Epsilon);
        Assert.Equal(3.0, queue[1], Epsilon);
        Assert.Equal(4.0, queue[2], Epsilon);

        queue.AddLast(5.0); // Overwrite 2 with 5

        Assert.Equal(3.0, queue[0], Epsilon);
        Assert.Equal(4.0, queue[1], Epsilon);
        Assert.Equal(5.0, queue[2], Epsilon);

        Assert.Equal(5.0, queue.RemoveLast(), Epsilon);

        Assert.Equal(3.0, queue[0], Epsilon);
        Assert.Equal(4.0, queue[1], Epsilon);

        Assert.Equal(3.0, queue.RemoveFirst(), Epsilon);

        Assert.Equal(4.0, queue[0], Epsilon);
    }

    [Fact]
    public void ResetTest()
    {
        var queue = new CircularBuffer<double>(5);

        for (int i = 0; i < 6; i++)
        {
            queue.AddLast(i);
        }

        queue.Clear();

        Assert.Equal(0, queue.Size);
    }

    [Fact]
    public void ResizeTest()
    {
        var queue = new CircularBuffer<double>(5);

        /* Buffer contains {1, 2, 3, _, _}
         *                  ^ front
         */
        queue.AddLast(1.0);
        queue.AddLast(2.0);
        queue.AddLast(3.0);

        queue.Resize(2);
        Assert.Equal(1.0, queue[0], 0.00005);
        Assert.Equal(2.0, queue[1], 0.00005);

        queue.Resize(5);
        Assert.Equal(1.0, queue[0], 0.00005);
        Assert.Equal(2.0, queue[1], 0.00005);

        queue.Clear();

        /* Buffer contains {_, 1, 2, 3, _}
         *                     ^ front
         */
        queue.AddLast(0.0);
        queue.AddLast(1.0);
        queue.AddLast(2.0);
        queue.AddLast(3.0);
        queue.RemoveFirst();

        queue.Resize(2);
        Assert.Equal(1.0, queue[0], 0.00005);
        Assert.Equal(2.0, queue[1], 0.00005);

        queue.Resize(5);
        Assert.Equal(1.0, queue[0], 0.00005);
        Assert.Equal(2.0, queue[1], 0.00005);

        queue.Clear();

        /* Buffer contains {_, _, 1, 2, 3}
         *                        ^ front
         */
        queue.AddLast(0.0);
        queue.AddLast(0.0);
        queue.AddLast(1.0);
        queue.AddLast(2.0);
        queue.AddLast(3.0);
        queue.RemoveFirst();
        queue.RemoveFirst();

        queue.Resize(2);
        Assert.Equal(1.0, queue[0], 0.00005);
        Assert.Equal(2.0, queue[1], 0.00005);

        queue.Resize(5);
        Assert.Equal(1.0, queue[0], 0.00005);
        Assert.Equal(2.0, queue[1], 0.00005);

        queue.Clear();

        /* Buffer contains {3, _, _, 1, 2}
         *                           ^ front
         */
        queue.AddLast(3.0);
        queue.AddFirst(2.0);
        queue.AddFirst(1.0);

        queue.Resize(2);
        Assert.Equal(1.0, queue[0], 0.00005);
        Assert.Equal(2.0, queue[1], 0.00005);

        queue.Resize(5);
        Assert.Equal(1.0, queue[0], 0.00005);
        Assert.Equal(2.0, queue[1], 0.00005);

        queue.Clear();

        /* Buffer contains {2, 3, _, _, 1}
         *                              ^ front
         */
        queue.AddLast(2.0);
        queue.AddLast(3.0);
        queue.AddFirst(1.0);

        queue.Resize(2);
        Assert.Equal(1.0, queue[0], 0.00005);
        Assert.Equal(2.0, queue[1], 0.00005);

        queue.Resize(5);
        Assert.Equal(1.0, queue[0], 0.00005);
        Assert.Equal(2.0, queue[1], 0.00005);

        // Test AddLast() after Resize
        queue.AddLast(3.0);
        Assert.Equal(1.0, queue[0], 0.00005);
        Assert.Equal(2.0, queue[1], 0.00005);
        Assert.Equal(3.0, queue[2], 0.00005);

        // Test AddFirst() after Resize
        queue.AddFirst(4.0);
        Assert.Equal(4.0, queue[0], 0.00005);
        Assert.Equal(1.0, queue[1], 0.00005);
        Assert.Equal(2.0, queue[2], 0.00005);
        Assert.Equal(3.0, queue[3], 0.00005);
    }
}
