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
            Data = new T[width, height];
            Marks = new bool[width, height];
        }

        public Matrix(int width, int height, T defaultValue) : this(width, height)
        {
            ForEachCoord((x, y) => Data[x, y] = defaultValue);
        }

        public Matrix(Matrix<T> other) : this(other.Width, other.Height)
        {
            ForEachCoord((col, row) => Data[col, row] = other.Data[col, row]);
        }

        public Matrix(IEnumerable<T[]> data) : this(data.First().Count(), data.Count())
        {
            int row = 0;
            data.ForEach(entry => SetRow(row++, entry));
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public T[,] Data { get; private set; }
        public bool[,] Marks { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new();
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    sb.Append($"{Data[x, y],4}{(Marks[x, y] ? '*' : ' ')} ");
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

        public T[] GetRow(int row)
        {
            T[] ret = new T[Width];
            for (int idx = 0; idx < Width; ++idx)
                ret[idx] = Data[idx, row];
            return ret;
        }

        public void SetRow(int row, T[] d)
        {
            for (int idx = 0; idx < Width; ++idx)
                Data[idx, row] = d[idx];
        }

        public T[] GetCol(int col)
        {
            T[] ret = new T[Height];
            for (int idx = 0; idx < Height; ++idx)
                ret[idx] = Data[col, idx];
            return ret;
        }

        public void SetCol(int col, T[] d)
        {
            for (int idx = 0; idx < Height; ++idx)
                Data[col, idx] = d[idx];
        }

        public bool TryGet(int x, int y, out T ret)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                ret = Data[x, y];
                return true;
            }

            ret = default(T);
            return false;
        }

        public T LooseGet(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                return Data[x, y];
            }

            return default(T);
        }

        public bool IsCoord(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;

        public void Mark(int x, int y) => Marks[x, y] = true;
        public bool IsMarked(int x, int y) => Marks[x, y];
        public void ResetMark(int x, int y) => Marks[x, y] = false;
        public void ResetMarked() => ForEachCoord((x, y) => ResetMark(x, y));

        public int SumMarked()
        {
            int sum = 0;
            
            ForEachCoord((x, y) =>
            {
                if (IsMarked(x, y)) 
                    ++sum;
            });

            return sum;
        }

        public void FlipV()
        {
            T[,] flipped = new T[Width, Height];
            ForEachCoord((col, row) => flipped[col, Height - row - 1] = Data[col, row]);
            Data = flipped;
        }

        public void FlipH()
        {
            T[,] flipped = new T[Width, Height];
            ForEachCoord((col, row) => flipped[Width - col - 1, row] = Data[col, row]);
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
                    action(col, row);
        }

        public void ForEachMarked(Action<int, int> action)
        {
            ForEachCoord((x, y) =>
            {
                if (IsMarked(x, y))
                    action(x, y);
            });
        }
    }
}
