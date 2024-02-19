using CommunityToolkit.HighPerformance;

namespace WPIMath;

public ref struct MatrixSpan<R, C> where R : class, Nat where C : class, Nat
{
    private Span2D<double> m_storage;

    public MatrixSpan()
    {
        int rows = R.Num;
        int cols = C.Num;
        int length = rows * cols;
        m_storage = new Memory2D<double>(new double[length], rows, cols).Span;
    }

    public MatrixSpan(Matrix<R, C> storage)
    {
        m_storage = storage.Storage.Span;
    }

    public readonly Span2D<double> Storage => m_storage;

    public readonly int NumRows => R.Num;
    public readonly int NumColumns => C.Num;

    public double this[int row, int col]
    {
        get
        {
            return m_storage[row, col];
        }
        set
        {
            m_storage[row, col] = value;
        }
    }
}
