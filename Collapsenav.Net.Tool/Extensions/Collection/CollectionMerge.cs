namespace Collapsenav.Net.Tool;
public static partial class CollectionExt
{
    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="comparer">怎么去重啊</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>>? querys, Func<T?, T?, bool>? comparer = null)
    {
        if (querys.IsEmpty())
            return Enumerable.Empty<T>();
        var result = querys.First();
        if (comparer == null)
            foreach (var query in querys.Skip(1))
                result = result.Concat(query);
        else
            foreach (var query in querys.Skip(1))
                result = result.Union(query, new CollapseNavEqualityComparer<T>(comparer));
        return result;
    }
    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="hashCodeFunc">hash去重</param>
    public static IEnumerable<T> Merge<T, E>(this IEnumerable<IEnumerable<T>>? querys, Func<T?, E>? hashCodeFunc)
    {
        if (querys.IsEmpty())
            return Enumerable.Empty<T>();
        var result = querys.First();
        if (hashCodeFunc == null)
            foreach (var query in querys.Skip(1))
                result = result.Concat(query);
        else
            foreach (var query in querys.Skip(1))
                result = result.Union(query, new CollapseNavEqualityComparer<T, E>(hashCodeFunc));
        return result;
    }

    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="query">多加一行</param>
    /// <param name="comparer">怎么去重啊</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>>? querys, IEnumerable<T> query, Func<T?, T?, bool>? comparer = null)
    {
        if (querys == null)
            return Enumerable.Empty<T>();
        querys = querys.Append(query);
        return querys.Merge(comparer);
    }
    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="query">多加一行</param>
    /// <param name="hashCodeFunc">hash去重</param>
    public static IEnumerable<T> Merge<T, E>(this IEnumerable<IEnumerable<T>>? querys, IEnumerable<T> query, Func<T?, E>? hashCodeFunc)
    {
        if (querys == null)
            return Enumerable.Empty<T>();
        querys = querys.Append(query);
        return querys.Merge(hashCodeFunc);
    }

    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="concatQuerys">多加一个同级选手</param>
    /// <param name="comparer">怎么去重啊</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>>? querys, IEnumerable<IEnumerable<T>> concatQuerys, Func<T?, T?, bool>? comparer = null)
    {
        if (querys == null)
            return Enumerable.Empty<T>();
        querys = querys.Concat(concatQuerys);
        return querys.Merge(comparer);
    }
    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="concatQuerys">多加一个同级选手</param>
    /// <param name="hashCodeFunc">hash去重</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>>? querys, IEnumerable<IEnumerable<T>> concatQuerys, Func<T?, int>? hashCodeFunc)
    {
        if (querys == null)
            return Enumerable.Empty<T>();
        querys = querys.Concat(concatQuerys);
        return querys.Merge(hashCodeFunc);
    }

    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="unique">是否去重</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>>? querys, bool unique)
    {
        if (querys == null)
            return Enumerable.Empty<T>();
        return unique ? querys.Merge().Unique() : querys.Merge();
    }

    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="query">多加一行</param>
    /// <param name="unique">是否去重</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>>? querys, IEnumerable<T> query, bool unique)
    {
        if (querys == null)
            return Enumerable.Empty<T>();
        querys = querys.Append(query);
        return querys.Merge(unique);
    }

    /// <summary>
    /// 合并集合
    /// </summary>
    /// <param name="querys">合并目标</param>
    /// <param name="concatQuerys">多加一个同级选手</param>
    /// <param name="unique">是否去重</param>
    public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>>? querys, IEnumerable<IEnumerable<T>> concatQuerys, bool unique)
    {
        if (querys == null)
            return Enumerable.Empty<T>();
        querys = querys.Concat(concatQuerys);
        return querys.Merge(unique);
    }
}