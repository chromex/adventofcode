using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoCUtil
{
    public class IntRange
    {
        private int start, end;

        public int Start
        {
            get { return start; }
            set
            {
                start = value;
                Debug.Assert(start <= end);
            }
        }

        public int End
        {
            get { return end; }
            set
            {
                end = value;
                Debug.Assert(start <= end);
            }
        }

        public IntRange()
        { }

        public IntRange(int s, int e)
        {
            start = s;
            end = e;

            Debug.Assert(Start <= End);
        }

        public IntRange(string s, string e) : this(int.Parse(s), int.Parse(e))
        { }

        public bool Contains(int val)
        {
            return val >= Start && val <= End;
        }

        public bool Contains(IntRange other)
        {
            return Start <= other.Start && End >= other.End;
        }

        public bool Overlaps(IntRange other)
        {
            return Contains(other) || other.Contains(this) || (Start <= other.Start && End >= other.Start) || (Start <= other.End && End >= other.End);
        }

        public void Expand(int val)
        {
            Start = Math.Min(Start, val);
            End = Math.Max(End, val);
        }

        public IEnumerable<int> Range()
        {
            return Enumerable.Range(Start, End - Start + 1);
        }
    }
}
