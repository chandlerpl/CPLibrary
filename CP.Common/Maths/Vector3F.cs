using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Common.Maths
{
    public class Vector3F
    {
        public static Vector3F Zero = new Vector3F(0, 0, 0);

        public static Vector3F Up = new Vector3F(0, 1, 0);
        public static Vector3F Down = new Vector3F(0, -1, 0);
        public static Vector3F Left = new Vector3F(-1, 0, 0);
        public static Vector3F Right = new Vector3F(1, 0, 0);
        public static Vector3F Forward = new Vector3F(0, 0, 1);
        public static Vector3F Back = new Vector3F(0, 0, -1);

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3F Normalized { get => Normalize(); }
        public Vector3F(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector3F(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Within(Vector3F center, float radius)
        {
            return Math.Abs((this - center).SqrMagnitude) <= radius * radius;
        }
        public bool ApproxEquals(Vector3F other)
        {
            return X.ApproxEqual(other.X) && Y.ApproxEqual(other.Y) && Z.ApproxEqual(other.Z);
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector3F other)
                return ApproxEquals(other);

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        }

        public float Length()
        {
            return (float)Math.Sqrt(SqrMagnitude);
        }
        public float SqrMagnitude { get { return X * X + Y * Y + Z * Z; } }

        public Vector3F Normalize()
        {
            return this / Length();
        }

        public Vector3F Cross(Vector3F a)
        {
            return new Vector3F((Y * a.Z) - (Z * a.Y), (Z * a.X) - (X * a.Z), (X * a.Y) - (Y * a.X));
        }

        public override string ToString()
        {
            return "[" + X + ", " + Y + ", " + Z + "]";
        }

        public static Vector3F operator +(Vector3F a, Vector3F b) { return new Vector3F(a.X + b.X, a.Y + b.Y, a.Z + b.Z); }
        public static Vector3F operator +(Vector3F a, float b) { return new Vector3F(a.X + b, a.Y + b, a.Z + b); }
        public static Vector3F operator /(Vector3F a, Vector3F b) { return new Vector3F(a.X / b.X, a.Y / b.Y, a.Z / b.Z); }
        public static Vector3F operator *(Vector3F a, float b) {  return new Vector3F(a.X * b, a.Y * b, a.Z * b); }
        public static Vector3F operator *(Vector3F a, Vector3F b) {return new Vector3F(a.X * b.X, a.Y * b.Y, a.Z * b.Y); }

        public static Vector3F operator *(float a, Vector3F b) { return new Vector3F(b.X * a, b.Y * a, b.Z * a); }
        public static Vector3F operator /(Vector3F a, float b) {return new Vector3F(a.X / b, a.Y / b, a.Z / b); }
        public static Vector3F operator -(Vector3F a, Vector3F b) {return new Vector3F(a.X - b.X, a.Y - b.Y, a.Z - b.Z); }
        public static Vector3F operator -(Vector3F a, float b) {return new Vector3F(a.X - b, a.Y - b, a.Z - b); }
        public static Vector3F operator -(Vector3F a) {return new Vector3F(a.X = -a.X, a.Y = -a.Y, a.Z = -a.Z); }
        
    }
}
