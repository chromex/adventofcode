using System;

namespace AoCUtil
{
    public class Vec3
    {
        public int X;
        public int Y;
        public int Z;

        public Vec3()
        { }

        public Vec3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vec3(Vec3 other) : this(other.X, other.Y, other.Z)
        { }

        public Vec3(string str)
        {
            string[] spl = str.Split(',');
            X = spl[0].AsInt();
            Y = spl[1].AsInt();
            Z = spl[2].AsInt();
        }

        public override string ToString() => $"({X}, {Y}, {Z})";

        public static Vec3 operator+(Vec3 lhs, Vec3 rhs)
        {
            return new Vec3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        public static Vec3 operator-(Vec3 lhs, Vec3 rhs)
        {
            return new Vec3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public static bool operator==(Vec3 lhs, Vec3 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;
        }

        public static bool operator!=(Vec3 lhs, Vec3 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z;
        }

        public int Manhattan => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

        public void RotateAroundX()
        {
            int tmp = Z;
            Z = Y;
            Y = -tmp;
        }

        public void RotateAroundY()
        {
            int tmp = X;
            X = Z;
            Z = -tmp;
        }

        public void RotateAroundZ()
        {
            int tmp = Y;
            Y = X;
            X = -tmp;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (obj is Vec3)
            {
                return this == (obj as Vec3);
            }

            return false;
        }

        public override int GetHashCode() => (X, Y, Z).GetHashCode();
    }
}
