using System;
using System.Collections.Generic;
using System.Linq;
using EngineLib;
using practice13;

namespace practice14
{
    public static class Extensions
    {
        //счетчик
        public static int Count<TSource>(this  MyNewCollection<TSource> collection, Func<TSource, bool> func)
        {
            int count = 0;
            foreach (var elem in collection)
                if (func(elem))
                    count += 1;
            return count;
        }
        
        //сортировка
        
        public static void CustomOrderBy<TSource,TResult>(this MyNewCollection<TSource> col, Func<TSource, TResult> func) where TResult : IComparable
        {
            int n = 0;
            TSource temp;
            foreach (var e in col)
                n += 1;
            
            for (int i = 0; i < n; i++)
            for (int j = i+1; j < n; j++)
                if (func(col[i]).CompareTo(func(col[j]))>0)
                    {
                        temp = col[i];
                        col[i] = col[j];
                        col[j] = temp;
                    }
        }

        public static void CustomOrderByDescending<TSource,TResult>(this MyNewCollection<TSource> col, Func<TSource, TResult> func) where TResult : IComparable
        {
            int n = 0;
            TSource temp;
            foreach (var e in col)
                n += 1;
            
            for (int i = 0; i < n; i++)
            for (int j = i+1; j < n; j++)
                if (func(col[i]).CompareTo(func(col[j])) < 0)
                {
                    temp = col[i];
                    col[i] = col[j];
                    col[j] = temp;
                }
        }

        //выборка
        public static IEnumerable<TSource> Where<TSource>(this MyNewCollection<TSource> col, Func<TSource, bool> func)
        {
            foreach (TSource t in col)
            {
                if (func(t))
                    yield return t;
            }
        }
        public static IEnumerable<TResult> Select<TSource,TResult>(this IEnumerable<TSource> col, Func<TSource,TResult> func)
        {
            foreach (TSource t in col)
            {
                yield return func(t);
            }
        }

        //аггрегация
        public static TSource CustomMin<TSource,TResult>(this MyNewCollection<TSource> col, Func<TSource, TResult> func) where TResult : IComparable
        {
            TSource min = col[0];
            foreach (TSource elem in col)
            {
                if (func(min).CompareTo(func(elem)) > 0)
                    min = elem;
            }
            return min;
        }
        
        public static TSource CustomMax<TSource,TResult>(this MyNewCollection<TSource> col, Func<TSource, TResult> func) where TResult : IComparable
        {
            TSource min = col[0];
            foreach (TSource elem in col)
            {
                if (func(min).CompareTo(func(elem)) < 0)
                    min = elem;
            }
            return min;
        }

        //public static IEnumerable<IGrouping<TKey, TSource>> GroubBy<TKey, TSource>(this MyNewCollection<TSource>, Func<TSource, TKey> func)
    }
}