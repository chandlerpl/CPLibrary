using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace CP.Common.Utilities
{
    public class CPNumeric
    {
        private readonly double _value;

        public CPNumeric(double value)
        {
            _value = value;
        }

        public CPNumeric(float value)
        {
            _value = value;
        }

        public static implicit operator CPNumeric(double value)
        {
            return new CPNumeric(value);
        }

        public static implicit operator CPNumeric(float value)
        {
            return new CPNumeric(value);
        }

        public static implicit operator double(CPNumeric value)
        {
            return value._value;
        }

        public static CPNumeric operator +(CPNumeric d1, CPNumeric d2)
        {
            return new CPNumeric(d1._value + d2._value);
        }

        public static CPNumeric operator -(CPNumeric d1, CPNumeric d2)
        {
            return new CPNumeric(d1._value - d2._value);
        }

        public static CPNumeric operator *(CPNumeric d1, CPNumeric d2)
        {
            return new CPNumeric(d1._value * d2._value);
        }

        public static CPNumeric operator /(CPNumeric d1, CPNumeric d2)
        {
            return new CPNumeric(d1._value / d2._value);
        }

        public static bool operator ==(CPNumeric d1, CPNumeric d2)
        {
            return d1._value == d2._value;
        }
        public static bool operator !=(CPNumeric d1, CPNumeric d2)
        {
            return d1._value != d2._value;
        }
        public static bool operator >(CPNumeric d1, CPNumeric d2)
        {
            return d1._value > d2._value;
        }
        public static bool operator >=(CPNumeric d1, CPNumeric d2)
        {
            return d1._value >= d2._value;
        }
        public static bool operator <=(CPNumeric d1, CPNumeric d2)
        {
            return d1._value <= d2._value;
        }
        public static bool operator <(CPNumeric d1, CPNumeric d2)
        {
            return d1._value < d2._value;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
