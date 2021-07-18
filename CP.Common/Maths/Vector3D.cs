using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Common.Maths
{
    public class Vector3D
    {
        public static Vector3D Zero = new Vector3D(0, 0, 0);

        public static Vector3D Up = new Vector3D(0, 1, 0);
        public static Vector3D Down = new Vector3D(0, -1, 0);
        public static Vector3D Left = new Vector3D(-1, 0, 0);
        public static Vector3D Right = new Vector3D(1, 0, 0);
        public static Vector3D Forward = new Vector3D(0, 0, 1);
        public static Vector3D Back = new Vector3D(0, 0, -1);


        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3D Normalized { get => Normalize(); }

        public Vector3D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Within(Vector3D center, double radius)
        {
            return Math.Abs((this - center).SqrMagnitude) <= radius * radius;
        }

        public bool WithinSqrd(Vector3D center, double radiusSqrd)
        {
            return Math.Abs((this - center).SqrMagnitude) <= radiusSqrd;
        }

        public bool ApproxEquals(Vector3D other)
        {
            return X.ApproxEqual(other.X) && Y.ApproxEqual(other.Y) && Z.ApproxEqual(other.Z);
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector3D other)
                return ApproxEquals(other);

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        }

        public double Length()
        {
            return Math.Sqrt(SqrMagnitude);
        }
        public double SqrMagnitude { get { return X * X + Y * Y + Z * Z; } }

        public Vector3D Normalize()
        {
            return this / Length();
        }

        public Vector3D Clone()
        {
            return new Vector3D(X, Y, Z);
        }

        public Vector3D Cross(Vector3D a)
        {
            return new Vector3D((Y * a.Z) - (Z * a.Y), (Z * a.X) - (X * a.Z), (X * a.Y) - (Y * a.X));
        }

        public double Dot(Vector3D a)
        { 
            return (X * a.X) + (Y * a.Y) + (Z * a.Z);
        }

        public override string ToString()
        {
            return "[" + X + ", " + Y + ", " + Z + "]";
        }

        public static Vector3D Cross(Vector3D a, Vector3D b)
        {
            return new Vector3D((b.Y * a.Z) - (b.Z * a.Y), (b.Z * a.X) - (b.X * a.Z), (b.X * a.Y) - (b.Y * a.X));
        }

        public static Vector3D operator +(Vector3D a, Vector3D b) { return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z); }
        public static Vector3D operator +(Vector3D a, double b) { return new Vector3D(a.X + b, a.Y + b, a.Z + b); }
        public static Vector3D operator /(Vector3D a, Vector3D b) { return new Vector3D(a.X / b.X, a.Y / b.Y, a.Z / b.Z); }
        public static Vector3D operator *(Vector3D a, double b) { return new Vector3D(a.X * b, a.Y * b, a.Z * b); }
        public static Vector3D operator *(Vector3D a, Vector3D b) { return new Vector3D(a.X * b.X, a.Y * b.Y, a.Z * b.Y); }

        public static Vector3D operator *(double a, Vector3D b) { return new Vector3D(b.X * a, b.Y * a, b.Z * a); }
        public static Vector3D operator /(Vector3D a, double b) { return new Vector3D(a.X / b, a.Y / b, a.Z / b); }
        public static Vector3D operator -(Vector3D a, Vector3D b) { return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z); }
        public static Vector3D operator -(Vector3D a, double b) {return new Vector3D(a.X - b, a.Y - b, a.Z - b); }
        public static Vector3D operator -(Vector3D a) {return new Vector3D(a.X = -a.X, a.Y = -a.Y, a.Z = -a.Z);
        }
    }
}
