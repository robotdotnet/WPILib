using System;
using System.Diagnostics;
using System.Numerics;
using System.Numerics.Tensors;
using System.Runtime.InteropServices;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.HighPerformance;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;
using WPIMath.Numbers;

namespace WPIMath;

public readonly struct Matrix<R, C> where R : class, Nat where C : class, Nat
{
    private readonly DenseMatrix m_storage;

    public Matrix()
    {
        int rows = R.Num;
        int cols = C.Num;
        m_storage = new DenseMatrix(rows, cols);
    }

    private Matrix(DenseMatrix storage)
    {
        Debug.Assert(storage.RowCount == R.Num);
        Debug.Assert(storage.ColumnCount == C.Num);
        m_storage = storage;
    }

    public readonly DenseMatrix Storage => m_storage;

    public readonly MatrixSpan<R, C> Span => new(this);

    public readonly int NumRows => R.Num;
    public readonly int NumColumns => C.Num;

    public readonly double this[int row, int col]
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

    public void SetRow(MatrixSpan<N1, C> val)
    {
        throw new NotImplementedException();
    }

    public void SetColumn(MatrixSpan<R, N1> val)
    {
        throw new NotImplementedException();
    }

    public void Fill(double value)
    {
        m_storage.Values.AsSpan().Fill(value);
    }

    public Matrix<R, C> Diag()
    {
        return new(DenseMatrix.OfDiagonalVector(m_storage.Diagonal()));
    }
}
