﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace AoCUtil
{
    public class FastSet<T> : IEnumerable<T>, IEnumerable
    {
        private readonly T[] _data;
        private readonly int[] _indecies;

        private FastSet(T[] data, int[] subsetIndecies)
        {
            _data = data;
            _indecies = subsetIndecies;
        }

        public FastSet(T[] data)
        {
            _data = data;
            _indecies = new int[data.Length];
            for (int index = 0; index < data.Length; ++index)
            {
                _indecies[index] = index;
            }
        }

        public FastSet(FastSet<T> other)
        {
            _data = other._data;
            _indecies = other._indecies;
        }

        public FastSet(List<T> data) : this(data.ToArray())
        { }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _indecies.Length; ++i)
            {
                yield return _data[_indecies[i]];
            }
        }

        public FastSet<T> Without(T val)
        {
            List<int> newIndecies = new();

            for (int i = 0; i < _indecies.Length; ++i)
            {
                int index = _indecies[i];

                if (!_data[index].Equals(val))
                {
                    newIndecies.Add(index);
                }
            }

            return new(_data, newIndecies.ToArray());
        }

        public bool Contains(T val)
        {
            for (int i = 0; i < _indecies.Length; ++i)
            {
                int index = _indecies[i];

                if (_data[index].Equals(val))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
