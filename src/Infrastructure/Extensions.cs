namespace Infrastructure
{
    using System;
    using System.Collections.Generic;

    public static class Extensions
    {
        public static TResult IfNotNull<T, TResult>(this T target, Func<T, TResult> getValue)
        {
            return (target != null) ? getValue(target) : default(TResult);
        }

        public static TSource MinBy1<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> selector)
        {
            return source.MinBy(selector, Comparer<TKey>.Default);
        }

        public static TSource MinBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> selector,
            IComparer<TKey> comparer)
        {
            using (var e = source.GetEnumerator())
            {
                if (!e.MoveNext())
                {
                    throw new InvalidOperationException("Sequence is empty");
                }
                var min = e.Current;
                var minKey = selector(min);
                while (e.MoveNext())
                {
                    var candidate = e.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, minKey) >= 0)
                    {
                        continue;
                    }
                    min = candidate;
                    minKey = candidateProjected;
                }
                return min;
            }
        }
    }
}