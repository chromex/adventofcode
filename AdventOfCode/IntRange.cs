﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
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

        public bool Contains(int val)
        {
            return val >= Start && val <= End;
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
