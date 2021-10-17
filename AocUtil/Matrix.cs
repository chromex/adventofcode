using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoCUtil
{
    public class Matrix<T>
    {
        public Matrix(int width, int height)
        {
            Width = width;
            Height = height;
            Data = new T[height, width];
        }

        public Matrix(Matrix<T> other) : this(other.Width, other.Height)
        {
            ForEachCoord((row, col) => Data[row, col] = other.Data[row, col]);
        }

        public Matrix(IEnumerable<T[]> data) : this(data.First().Count(), data.Count())
        {
            int row = 0;
            data.ForEach(entry => SetRow(row++, entry));
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public T[,] Data { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new();
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    sb.Append(Data[x, y]);
                }

                sb.AppendLine();
            }
            return sb.ToString();
        }

        public IEnumerable<T[]> Rows()
        {
            for (int row = 0; row < Height; ++row)
                yield return GetRow(row);
        }

        public void SetRow(int row, T[] d)
        {
            for (int idx = 0; idx < Width; ++idx)
                Data[row, idx] = d[idx];
        }

        public T[] GetRow(int row)
        {
            T[] ret = new T[Width];
            for (int idx = 0; idx < Width; ++idx)
                ret[idx] = Data[row, idx];
            return ret;
        }

        public T[] GetCol(int col)
        {
            T[] ret = new T[Width];
            for (int idx = 0; idx < Height; ++idx)
                ret[idx] = Data[idx, col];
            return ret;
        }

        public T LoseGet(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                return Data[x, y];
            }

            return default(T);
        }

        public void FlipV()
        {
            T[,] flipped = new T[Width, Height];
            ForEachCoord((row, col) => flipped[Height - row - 1, col] = Data[row, col]);
            Data = flipped;
        }

        public void FlipH()
        {
            T[,] flipped = new T[Width, Height];
            ForEachCoord((row, col) => flipped[row, Width - col - 1] = Data[row, col]);
            Data = flipped;
        }

        // Note: Doesn't handle uneven matrix sizes
        public void RR()
        {
            Matrix<T> dupe = new Matrix<T>(this);
            dupe.FlipV();
            for (int row = 0; row < Height; ++row)
                SetRow(row, dupe.GetCol(row));
        }

        public void ForEachCoord(Action<int, int> action)
        {
            for (int row = 0; row < Height; ++row)
                for (int col = 0; col < Width; ++col)
                    action(row, col);
        }
    }
}
