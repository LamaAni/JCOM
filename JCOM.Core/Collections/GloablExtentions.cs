
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class JCOM_COLLECTIONS_GLOABL_Extentions
{
    /// <summary>
    /// Returns distinct values with comparere.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    /// <param name="col"></param>
    /// <param name="compareValueSelector"></param>
    /// <returns></returns>
    public static IEnumerable<T> Distinct<T, TVal>(this IEnumerable<T> col, Func<T, TVal> compareValueSelector)
    {
        return col.GroupBy<T, TVal>(v => compareValueSelector(v)).Select(grp => grp.First());
    }

    /// <summary>
    /// Interlaces two collections.
    /// </summary>
    /// <typeparam name="TRslt"></typeparam>
    /// <typeparam name="TA"></typeparam>
    /// <typeparam name="TB"></typeparam>
    /// <param name="colA"></param>
    /// <param name="colB"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IEnumerable<TRslt> Interlace<TRslt, TA, TB>(this IEnumerable<TA> colA, IEnumerable<TB> colB, Func<TA, TB, TRslt> selector)
    {
        IEnumerator<TA> aisEnum = colA.GetEnumerator();
        IEnumerator<TB> bisEnum = colB.GetEnumerator();
        List<TRslt> rslt = new List<TRslt>();

        while (aisEnum.MoveNext() && bisEnum.MoveNext())
        {
            rslt.Add(selector(aisEnum.Current, bisEnum.Current));
        }

        return rslt;
    }

    /// <summary>
    /// Returns true if collection is empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="col"></param>
    /// <returns></returns>
    public static bool IsEmpty<T>(this IEnumerable<T> col)
    {
        IEnumerator<T> e = col.GetEnumerator();
        return !e.MoveNext();
    }

    /// <summary>
    /// Executes the foreach command for all values in the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="vals"></param>
    /// <param name="f"></param>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> vals, Action<T> f)
    {
        foreach (T v in vals.ToArray())
            f(v);

        return vals;
    }

    /// <summary>
    /// Executes the foreach command for all values in the collection, has an index i.
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    /// <param name="vals">The values</param>
    /// <param name="f">A function (index, elm)</param>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> vals, Action<T, int> f)
    {
        int i = 0;
        foreach (T v in vals.ToArray())
        {
            f(v, i);
            i += 1;
        }
        return vals;
    }

    /// <summary>
    /// Converts the collection into groups of values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="vals"></param>
    /// <param name="itemsInAGroup"></param>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> ToGroupsOf<T>(this IEnumerable<T> vals, int itemsInAGroup)
    {
        int totalCount = vals.Count();
        List<List<T>> lst = new List<List<T>>();
        List<T> current = null;

        foreach (T val in vals.ToArray())
        {
            if (current == null || current.Count == itemsInAGroup)
            {
                //int curCount = totalCount - totalIndex > itemsInAGroup ? itemsInAGroup : totalCount - totalIndex;
                current = new List<T>();
                lst.Add(current);
            }
            current.Add(val);
        }

        return lst;
    }

    /// <summary>
    /// Orders the collection using a comparer and selects the correct resutlt.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="vals"></param>
    /// <param name="selector"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> vals,
        Func<TSource, TKey> selector, Func<TKey, TKey, int> comparer)
    {
        return vals.OrderBy<TSource, TKey>(selector, new JCOM.Collections.GenericComparer<TKey>(comparer));
    }

    /// <summary>
    /// Orders the collection using a comparer and selects the correct resutlt.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="vals"></param>
    /// <param name="selector"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> vals,
        Func<TSource, TKey> selector, Func<TKey, TKey, int> comparer)
    {
        return vals.OrderByDescending(selector, new JCOM.Collections.GenericComparer<TKey>(comparer));
    }

    /// <summary>
    /// Orders the collection using a comparer and selects the correct resutlt.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="vals"></param>
    /// <param name="selector"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> vals,
        Func<TSource, TKey> selector, Func<TKey, TKey, int> comparer)
    {
        return vals.ThenBy(selector, new JCOM.Collections.GenericComparer<TKey>(comparer));
    }

    /// <summary>
    /// Orders the collection using a comparer and selects the correct resutlt.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="vals"></param>
    /// <param name="selector"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> vals,
        Func<TSource, TKey> selector, Func<TKey, TKey, int> comparer)
    {
        return vals.ThenByDescending(selector, new JCOM.Collections.GenericComparer<TKey>(comparer));
    }

    /// <summary>
    /// True if the collection contains any of the set.
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="dic"></param>
    /// <param name="k"></param>
    /// <returns>True if value has been removed</returns>
    public static bool ContainsAny<T>(this ISet<T> set, IEnumerable<T> vals)
    {
        foreach (T v in vals)
            if (set.Contains(v))
                return true;
        return false;
    }

    /// <summary>
    /// Attempts to add a value, true if value has been added.
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="dic"></param>
    /// <param name="k"></param>
    /// <returns>True if value has been removed</returns>
    public static bool TryAdd<T>(this ICollection<T> set, T val)
    {
        if (set.Contains(val))
            return false;
        set.Add(val);
        return true;
    }

    /// <summary>
    /// Attempts to remove a value.
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="dic"></param>
    /// <param name="k"></param>
    /// <returns>True if value has been removed</returns>
    public static bool TryRemove<T>(this ICollection<T> set, T val)
    {
        if (!set.Contains(val))
            return false;
        set.Remove(val);
        return true;
    }

    /// <summary>
    /// Attempts to add a value, true if value has been added.
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="dic"></param>
    /// <param name="k"></param>
    /// <returns>True if value has been removed</returns>
    public static bool TryAdd<Key, Value>(this IDictionary<Key, Value> dic, Key k, Value v)
    {
        if (dic.ContainsKey(k))
            return false;
        dic.Add(k, v);
        return true;
    }

    /// <summary>
    /// Attempts to remove a value.
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="dic"></param>
    /// <param name="k"></param>
    /// <returns>True if value has been removed</returns>
    public static bool TryRemove<Key, Value>(this IDictionary<Key, Value> dic, Key k)
    {
        if (!dic.ContainsKey(k))
            return false;
        dic.Remove(k);
        return true;
    }

}
