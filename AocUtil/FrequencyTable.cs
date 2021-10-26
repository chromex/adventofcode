using System.Collections.Generic;
using System.Linq;

namespace AoCUtil
{
    public class FrequencyTable<T>
    {
        private Dictionary<T, int> _data = new();

        public void Add(T item)
        {
            _data[item] = _data.GetValueOrDefault(item) + 1;
        }

        public T GetMostFrequent()
        {
            int max = _data.Values.Max();
            return _data.Where(kvp => kvp.Value == max).First().Key;
        }

        public T GetLeastFrequent()
        {
            int min = _data.Values.Min();
            return _data.Where(kvp => kvp.Value == min).First().Key;
        }
    }
}
