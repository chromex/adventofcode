using System;
using System.Collections;
using System.Collections.Generic;

namespace AoCUtil
{
    public class StringView : IEnumerable<char>, IEnumerable
    {
        private readonly int _start;

        public StringView(string str)
        {
            Data = str;
            Length = str.Length;
            _start = 0;
        }

        public StringView(string str, int start, int length)
        {
            if (start < 0)
                throw new IndexOutOfRangeException(nameof(start));
            if (length < 0 || (start + length) > str.Length)
                throw new IndexOutOfRangeException(nameof(length));

            Data = str;
            _start = start;
            Length = length;
        }

        public string Data { get; }

        public int Length { get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<char> GetEnumerator()
        {
            int end = _start + Length;
            for (int i = _start; i < end; ++i)
                yield return Data[i];
        }

        public char this[int index]
        {
            get 
            {
                if (index < Length && index >= 0)
                    return Data[_start + index];

                throw new IndexOutOfRangeException();
            }
        }

        public int IndexOf(char c) => IndexOf(c, 0);

        public int IndexOf(char c, int startIndex)
        {
            return Data.IndexOf(c, _start + startIndex, Length - startIndex) - _start;
        }

        public StringView Substring(int start) => Substring(start, Length);

        public StringView Substring(int start, int len)
        {
            if (start >= Length || start < 0)
                throw new IndexOutOfRangeException(nameof(start));

            if (len > (Length - start) || len < 0)
                throw new IndexOutOfRangeException(nameof(start));

            return new StringView(Data, _start + start, len);
        }

        public string GetString() => Data.Substring(_start, Length);
    }
}
