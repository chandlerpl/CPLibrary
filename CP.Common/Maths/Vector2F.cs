using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Common.Maths
{
    public class Vector2F
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2F Normalized { get => Normalize(); }
        public Vector2F(float x, float y)
        {
            X = x;
            Y = y;
        }

        public bool Within(Vector2F center, float radius)
        {
            return Math.Abs((this - center).SqrMagnitude) <= radius * radius;
        }
        public bool ApproxEquals(Vector2F other)
        {
            return X.ApproxEqual(other.X) && Y.ApproxEqual(other.Y);
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector2F other)
                return ApproxEquals(other);

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public float Length()
        {
            return (float)Math.Sqrt(SqrMagnitude);
        }
        public float SqrMagnitude { get { return X * X + Y * Y; } }

        public float Cross(Vector2F a)
        {
            return (X * a.Y) - (Y * a.X);
        }

        public override string ToString()
        {
            return "[" + X + ", " + Y + "]";
        }

        public Vector2F Normalize()
        {
            return this / Length();
        }

        public static Vector2F operator +(Vector2F a, Vector2F b) { return new Vector2F(a.X + b.X, a.Y + b.Y); }
        public static Vector2F operator +(Vector2F a, float b) { return new Vector2F(a.X + b, a.Y + b); }
        public static Vector2F operator /(Vector2F a, Vector2F b) { return new Vector2F(a.X / b.X, a.Y / b.Y); }
        public static Vector2F operator *(Vector2F a, float b) { return new Vector2F(a.X * b, a.Y * b); }
        public static Vector2F operator *(Vector2F a, Vector2F b) { return new Vector2F(a.X * b.X, a.Y * b.Y); }

        public static Vector2F operator *(float a, Vector2F b) { return new Vector2F(b.X * a, b.Y * a); }
        public static Vector2F operator /(Vector2F a, float b) { return new Vector2F(a.X / b, a.Y / b); }
        public static Vector2F operator -(Vector2F a, Vector2F b) { return new Vector2F(a.X - b.X, a.Y - b.Y); }
        public static Vector2F operator -(Vector2F a, float b) { return new Vector2F(a.X - b, a.Y - b); }
        public static Vector2F operator -(Vector2F a) { return new Vector2F(a.X = -a.X, a.Y = -a.Y); }
    }
}
