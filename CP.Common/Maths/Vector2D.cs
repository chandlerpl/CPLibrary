using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Common.Maths
{
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D Normalized { get => Normalize(); }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public bool Within(Vector2D center, double radius)
        {
            return Math.Abs((this - center).SqrMagnitude) <= radius * radius;
        }
        public bool ApproxEquals(Vector2D other)
        {
            return X.ApproxEqual(other.X) && Y.ApproxEqual(other.Y);
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector2D other)
                return ApproxEquals(other);

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public double Length()
        {
            return Math.Sqrt(SqrMagnitude);
        }
        public double SqrMagnitude { get { return X * X + Y * Y; } }

        public Vector2D Normalize()
        {
            return this / Length();
        }

        public double Cross(Vector2D a)
        {
            return (X * a.Y) - (Y * a.X);
        }

        public override string ToString()
        {
            return "[" + X + ", " + Y + "]";
        }

        public static Vector2D operator +(Vector2D a, Vector2D b) { return new Vector2D(a.X + b.X, a.Y + b.Y); }
        public static Vector2D operator +(Vector2D a, double b) { return new Vector2D(a.X + b, a.Y + b); }
        public static Vector2D operator /(Vector2D a, Vector2D b) {return new Vector2D(a.X / b.X, a.Y / b.Y); }
        public static Vector2D operator *(Vector2D a, double b) { return new Vector2D(a.X * b, a.Y * b); }
        public static Vector2D operator *(Vector2D a, Vector2D b) {return new Vector2D(a.X * b.X, a.Y * b.Y); }

        public static Vector2D operator *(double a, Vector2D b) { return new Vector2D(b.X * a, b.Y * a); }
        public static Vector2D operator /(Vector2D a, double b) {return new Vector2D(a.X / b, a.Y / b); }
        public static Vector2D operator -(Vector2D a, Vector2D b) {return new Vector2D(a.X - b.X, a.Y - b.Y); }
        public static Vector2D operator -(Vector2D a, double b) {return new Vector2D(a.X - b, a.Y - b); }
        public static Vector2D operator -(Vector2D a) {return new Vector2D(a.X = -a.X, a.Y = -a.Y); }
    }
}
