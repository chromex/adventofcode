using System.Collections.Generic;
using System.Linq;

namespace AoCUtil
{
    public class RingList<T>
    {
        private List<T> data;

        public RingList(IEnumerable<T> original)
        {
            data = original.ToList();
        }

        public T this[int idx]
        {
            get => data[Loop(idx)];
            set => data[Loop(idx)] = value;
        }

        public int Count => data.Count;

        public List<T> Raw => data;

        public void InsertAt(int idx, T val)
        {
            data.Insert(Loop(idx), val);
        }

        public void InsertRange(int idx, IEnumerable<T> values)
        {
            data.InsertRange(Loop(idx), values);
        }

        public T RemoveAt(int idx)
        {
            T val = this[idx];
            data.RemoveAt(Loop(idx));
            return val;
        }

        public void RotateLeft()
        {
            T val = data[0];
            data.RemoveAt(0);
            data.Add(val);
        }

        private int Loop(int idx)
        {
            return idx % data.Count;
        }
    }
}
