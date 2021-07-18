/* 
 * Copyright (C) Pope Games, Inc - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Author: Chandler Pope-Lewis <c.popelewis@gmail.com>
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class CMath
{
    public const double EPSILON = double.Epsilon * 1E100;

    public static IEnumerable<IEnumerable<T>>GetKCombs<T>(IEnumerable<T> list, int length) where T : IComparable
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetKCombs(list, length - 1)
            .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    public static IEnumerable<IEnumerable<T>>GetKCombsWithRept<T>(IEnumerable<T> list, int length) where T : IComparable
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetKCombsWithRept(list, length - 1)
            .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) >= 0),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    public static IEnumerable<IEnumerable<T>>GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(o => !t.Contains(o)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    public static IEnumerable<IEnumerable<T>>GetPermutationsWithRept<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetPermutationsWithRept(list, length - 1)
            .SelectMany(t => list,
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    public static bool ApproxEqual(this double value1, double value2)
    {
        return Math.Abs(value1 - value2) <= EPSILON;
    }

    public static bool ApproxGreaterThanOrEqualTo(this double value1, double value2)
    {
        return value1 > value2 || value1.ApproxEqual(value2);
    }

    public static bool ApproxLessThanOrEqualTo(this double value1, double value2)
    {
        return value1 < value2 || value1.ApproxEqual(value2);
    }

    public static bool ApproxEqual(this float value1, float value2)
    {
        return Math.Abs(value1 - value2) <= EPSILON;
    }

    public static bool ApproxGreaterThanOrEqualTo(this float value1, float value2)
    {
        return value1 > value2 || value1.ApproxEqual(value2);
    }

    public static bool ApproxLessThanOrEqualTo(this float value1, float value2)
    {
        return value1 < value2 || value1.ApproxEqual(value2);
    }
}
