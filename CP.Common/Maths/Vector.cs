using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Common.Maths
{
    public class Vector
    {
        private double[] values;
        public int Dimensions { get => values.Length; }
        public double X { get => values[0]; set => values[0] = value; }
        public double Y { get => values.Length > 1 ? values[1] : 0; set { if (values.Length > 1) values[1] = value; } }
        public double Z { get => values.Length > 2 ? values[2] : 0; set { if (values.Length > 2) values[2] = value; } }

        public Vector(double x)
        {
            values = new double[1];
            X = x;
        }

        public Vector(params double[] dimensions)
        {
            values = dimensions;
        }

        public bool Within(Vector center, double radius)
        {
            return Math.Abs((this - center).SqrMagnitude) <= radius * radius;
        }
        public bool ApproxEquals(Vector other)
        {
            if (other.Dimensions != Dimensions)
                return false;

            for(int i = 0; i < Dimensions; ++i)
            {
                if (!other.values[i].ApproxEqual(values[i]))
                    return false;
            }

            return true;
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector other)
                return ApproxEquals(other);

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return values.GetHashCode();
        }

        public double Length()
        {
            return Math.Sqrt(SqrMagnitude);
        }
        public double SqrMagnitude { 
            get {
                double mag = 0;
                foreach (double val in values)
                    mag += val * val;

                return mag;
            } 
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");
            for (int i = 0; i < Dimensions; ++i)
            {
                sb.Append(values[i]);
                if (i < Dimensions - 1)
                    sb.Append(", ");
            }
            sb.Append("]");

            return sb.ToString();
        }

        public static Vector operator +(Vector a, Vector b) 
        {
            int min = a.Dimensions == b.Dimensions || a.Dimensions < b.Dimensions ? a.Dimensions : b.Dimensions;
            double[] n = new double[min];
            for (int i = 0; i < min; ++i)
            {
                n[i] = a.values[i] + b.values[i];
            }

            return new Vector(n);
        }
        public static Vector operator +(Vector a, double b)
        {
            int min = a.Dimensions;
            double[] n = new double[min];
            for (int i = 0; i < min; ++i)
            {
                n[i] = a.values[i] + b;
            }

            return new Vector(n);
        }
        public static Vector operator /(Vector a, Vector b)
        {
            int min = a.Dimensions == b.Dimensions || a.Dimensions < b.Dimensions ? a.Dimensions : b.Dimensions;
            double[] n = new double[min];
            for (int i = 0; i < min; ++i)
            {
                n[i] = b.values[i] / a.values[i];
            }

            return new Vector(n);
        }
        public static Vector operator *(Vector a, double b)
        {
            int min = a.Dimensions;
            double[] n = new double[min];
            for (int i = 0; i < min; ++i)
            {
                n[i] = b * a.values[i];
            }

            return new Vector(n);
        }
        public static Vector operator *(Vector a, Vector b)
        {
            int min = a.Dimensions == b.Dimensions || a.Dimensions < b.Dimensions ? a.Dimensions : b.Dimensions;
            double[] n = new double[min];
            for (int i = 0; i < min; ++i)
            {
                n[i] = a.values[i] * b.values[i];
            }

            return new Vector(n);
        }

        public static Vector operator *(double a, Vector b)
        {
            int min = b.Dimensions;
            double[] n = new double[min];
            for (int i = 0; i < min; ++i)
            {
                n[i] = b.values[i] * a;
            }

            return new Vector(n);
        }
        public static Vector operator /(Vector a, double b)
        {
            int min = a.Dimensions;
            double[] n = new double[min];
            for (int i = 0; i < min; ++i)
            {
                n[i] = a.values[i] / b;
            }

            return new Vector(n);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            int min = a.Dimensions == b.Dimensions || a.Dimensions < b.Dimensions ? a.Dimensions : b.Dimensions;
            double[] n = new double[min];
            for (int i = 0; i < min; ++i)
            {
                n[i] = b.values[i] - a.values[i];
            }

            return new Vector(n);
        }
        public static Vector operator -(Vector a, double b)
        {
            int min = a.Dimensions;
            double[] n = new double[min];
            for (int i = 0; i < min; ++i)
            {
                n[i] = a.values[i] - b;
            }

            return new Vector(n);
        }
        public static Vector operator -(Vector a)
        {
            int min = a.Dimensions;
            double[] n = new double[min];
            for (int i = 0; i < min; ++i)
            {
                n[i] = -a.values[i];
            }

            return new Vector(n);
        }
    }
}
