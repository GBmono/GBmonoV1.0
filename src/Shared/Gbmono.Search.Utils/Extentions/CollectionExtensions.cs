using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.Utils.Extentions
{
    public static class CollectionExtensions
    {
        public static List<T[]> SplitInSize<T>(this List<T> list, int size = 100)
        {
            List<T[]> parts = new List<T[]>();

            int pos = 0;

            while (pos < list.Count)
            {
                parts.Add(list.Skip(pos).Take(size).ToArray());
                pos += size;
            }

            return parts;
        }

        public static void AddIfNotNull<T>(this List<T> list, T item)
        {
            if (list != null && item != null)
            {
                list.Add(item);
            }
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return (enumerable == null || !enumerable.Any());
        }

        public static IEnumerable<T> ExceptIfNotNull<T>(this IEnumerable<T> enumerable, IEnumerable<T> otherEnumerable)
        {
            if (!enumerable.IsNullOrEmpty() && !otherEnumerable.IsNullOrEmpty())
            {
                return enumerable.Except(otherEnumerable);
            }

            return enumerable;
        }

        public static IEnumerable<T> ExceptNonDistinct<T>(this IEnumerable<T> enumerable, IEnumerable<T> otherEnumerable)
        {
            if (!enumerable.IsNullOrEmpty() && !otherEnumerable.IsNullOrEmpty())
            {
                return enumerable.Where(i => !otherEnumerable.Contains(i));
            }

            return enumerable;
        }

        public static IEnumerable<T> IntersectIfNotNull<T>(this IEnumerable<T> enumerable, IEnumerable<T> otherEnumerable)
        {
            if (!enumerable.IsNullOrEmpty() && !otherEnumerable.IsNullOrEmpty())
            {
                return enumerable.Intersect(otherEnumerable);
            }

            return enumerable;
        }

        public static IEnumerable<T> ConcatIfNotNull<T>(this IEnumerable<T> enumerable, IEnumerable<T> otherEnumerable)
        {
            if (enumerable == null)
                enumerable = Enumerable.Empty<T>();

            if (otherEnumerable == null)
                otherEnumerable = Enumerable.Empty<T>();

            return enumerable.Concat(otherEnumerable);
        }



        public static bool ContainsAny<T>(this IEnumerable<T> enumerable, IEnumerable<T> otherEnumerable)
        {
            if (enumerable.IsNullOrEmpty() || otherEnumerable.IsNullOrEmpty())
            {
                return false;
            }

            return enumerable.Intersect(otherEnumerable).Any();

        }
    }
}
