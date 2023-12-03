using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        public IntRange(IntRange other) : this(other.Start, other.End)
        { }

        public IntRange(int s, int e)
        {
            start = s;
            end = e;

            Debug.Assert(Start <= End);
        }

        public IntRange(string min, string max) : this(min.AsInt(), max.AsInt())
        { }

        public IntRange(string[] vals) : this(vals[0], vals[1])
        { }

        public int Length => End - Start + 1;

        public override string ToString() => $"({Start} <-> {End})";

        public bool Contains(int val)
        {
            return val >= Start && val <= End;
        }

        public bool Contains(IntRange other) => Start < other.Start && End > other.End;

        public bool ContainsInclusive(IntRange other) => Start <= other.Start && End >= other.End;

        public bool Overlap(IntRange other)
        {
            return Contains(other.Start) ||
                Contains(other.End) ||
                other.Contains(Start) ||
                other.Contains(End);
        }

        public void Expand(int val)
        {
            Start = Math.Min(Start, val);
            End = Math.Max(End, val);
        }

        public IEnumerable<int> Range()
        {
            for (int v = Start; v <= End; ++v)
                yield return v;
        }
    }
}
