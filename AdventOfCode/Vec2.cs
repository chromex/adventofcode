﻿using System;

namespace AdventOfCode
{
    public class Vec2
    {
        public int X;
        public int Y;

        public Vec2()
        { }

        public Vec2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vec2(string str)
        {
            string[] split = str.Split(",");
            X = int.Parse(split[0]);
            Y = int.Parse(split[1]);
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public int Manhattan
        {
            get { return Math.Abs(X) + Math.Abs(Y); }
        }

        public double Length
        {
            get { return Math.Sqrt(X * X + Y * Y); }
        }

        public static Vec2 operator+(Vec2 lhs, Vec2 rhs)
        {
            return new Vec2(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Vec2 operator-(Vec2 lhs, Vec2 rhs)
        {
            return new Vec2(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static Vec2 operator*(Vec2 lhs, Vec2 rhs)
        {
            return new Vec2(lhs.X * rhs.X, lhs.Y * rhs.Y);
        }

        public static Vec2 operator*(Vec2 lhs, int rhs)
        {
            return new Vec2(lhs.X * rhs, lhs.Y * rhs);
        }
    }
}
