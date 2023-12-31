namespace Collapsenav.Net.Tool;
public static partial class CollectionExt
{
    /// <summary>
    /// 对象是否在集合中
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="items"></param>
    /// <param name="comparer">对比</param>
    public static bool In<T>(this T origin, IEnumerable<T> items, Func<T?, T?, bool>? comparer = null)
    {
        if (comparer == null)
            return items.Contains(origin);
        return items.Contains(origin, new CollapseNavEqualityComparer<T>(comparer));
    }
    /// <summary>
    /// 对象是否在集合中
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="items"></param>
    /// <param name="comparer">对比</param>
    public static bool In<T, E>(this T origin, IEnumerable<T> items, Func<T?, E>? comparer)
    {
        if (comparer == null)
            return items.Contains(origin);
        return items.Contains(origin, new CollapseNavEqualityComparer<T, E>(comparer));
    }
    /// <summary>
    /// 对象是否在集合中
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="items"></param>
    /// <param name="comparer">对比</param>
    public static bool In<T>(this T origin, Func<T?, T?, bool>? comparer, params T[] items)
    {
        return items.Contains(origin, new CollapseNavEqualityComparer<T>(comparer));
    }
    /// <summary>
    /// 对象是否在集合中
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="items"></param>
    /// <param name="comparer">对比</param>
    public static bool In<T, E>(this T origin, Func<T?, E>? comparer, params T[] items)
    {
        return items.Contains(origin, new CollapseNavEqualityComparer<T, E>(comparer));
    }
    /// <summary>
    /// 对象是否在集合中
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="items"></param>
    public static bool In<T>(this T origin, params T[] items)
    {
        return items.Contains(origin);
    }



    /// <summary>
    /// 全包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么去重</param>
    /// <param name="filters">条件</param>
    public static bool AllContain<T>(this IEnumerable<T>? query, IEnumerable<T>? filters, Func<T?, T?, bool>? comparer = null)
    {
        if (query == null)
            return false;
        if (filters.IsEmpty())
            return true;
        foreach (var filter in filters)
            if (!query.Contains(filter, comparer == null ? null : new CollapseNavEqualityComparer<T>(comparer)))
                return false;
        return true;
    }
    /// <summary>
    /// 全包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="filters">条件</param>
    /// <param name="keyselector"></param>
    public static bool AllContain<T, E>(this IEnumerable<T>? query, IEnumerable<T>? filters, Func<T?, E?>? keyselector)
    {
        if (query == null)
            return false;
        if (filters.IsEmpty())
            return true;
        foreach (var filter in filters)
            if (!query.Contains(filter, keyselector == null ? null : new CollapseNavEqualityComparer<T, E>(keyselector)))
                return false;
        return true;
    }
    /// <summary>
    /// 全包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="filters">条件</param>
    public static bool AllContain<T>(this IEnumerable<T>? query, params T[] filters)
    {
        return query.AllContain(filters, null);
    }
    /// <summary>
    /// 全包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么去重</param>
    /// <param name="filters">条件</param>
    public static bool AllContain<T>(this IEnumerable<T>? query, Func<T?, T?, bool>? comparer, params T[] filters)
    {
        return query.AllContain(filters, comparer);
    }
    /// <summary>
    /// 全包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="keyselector">怎么去重</param>
    /// <param name="filters">条件</param>
    public static bool AllContain<T, E>(this IEnumerable<T>? query, Func<T?, E?>? keyselector, params T[] filters)
    {
        return query.AllContain(filters, keyselector);
    }

    /// <summary>
    /// 部分包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么判断重复</param>
    /// <param name="filters">条件</param>
    public static bool HasContain<T>(this IEnumerable<T>? query, IEnumerable<T>? filters, Func<T?, T?, bool>? comparer = null)
    {
        if (query == null)
            return false;
        if (filters.IsEmpty())
            return true;
        foreach (var filter in filters)
            if (query.Contains(filter, comparer == null ? null : new CollapseNavEqualityComparer<T>(comparer)))
                return true;
        return false;
    }
    /// <summary>
    /// 部分包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="keyselector">怎么判断重复</param>
    /// <param name="filters">条件</param>
    public static bool HasContain<T, E>(this IEnumerable<T>? query, IEnumerable<T>? filters, Func<T?, E?>? keyselector)
    {
        if (query == null)
            return false;
        if (filters.IsEmpty())
            return true;
        foreach (var filter in filters)
            if (query.Contains(filter, keyselector == null ? null : new CollapseNavEqualityComparer<T, E>(keyselector)))
                return true;
        return false;
    }
    /// <summary>
    /// 部分包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="filters">条件</param>
    public static bool HasContain<T>(this IEnumerable<T>? query, params T[] filters)
    {
        return query.HasContain(filters, null);
    }
    /// <summary>
    /// 部分包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么去重</param>
    /// <param name="filters">条件</param>
    public static bool HasContain<T>(this IEnumerable<T>? query, Func<T?, T?, bool>? comparer, params T[] filters)
    {
        return query.HasContain(filters, comparer);
    }
    /// <summary>
    /// 部分包含
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="keyselector">怎么去重</param>
    /// <param name="filters">条件</param>
    public static bool HasContain<T, E>(this IEnumerable<T>? query, Func<T?, E?>? keyselector, params T[] filters)
    {
        return query.HasContain(filters, keyselector);
    }
    /// <summary>
    /// 取交集
    /// </summary>
    public static IEnumerable<T> Intersect<T>(this IEnumerable<T>? origin, IEnumerable<T> target, Func<T?, T?, bool>? comparer)
    {
        if (origin == null)
            return Enumerable.Empty<T>();
        return origin.Intersect(target, comparer == null ? null : new CollapseNavEqualityComparer<T>(comparer));
    }
    /// <summary>
    /// 取交集
    /// </summary>
    public static IEnumerable<T> GetItemIn<T>(this IEnumerable<T>? origin, IEnumerable<T> target, Func<T?, T?, bool>? comparer = null)
    {
        return origin.Intersect(target, comparer);
    }
    /// <summary>
    /// 取差集
    /// </summary>
    public static IEnumerable<T> Except<T>(this IEnumerable<T>? origin, IEnumerable<T> target, Func<T?, T?, bool>? comparer)
    {
        if (origin == null)
            return Enumerable.Empty<T>();
        return origin.Except(target, comparer == null ? null : new CollapseNavEqualityComparer<T>(comparer));
    }
    /// <summary>
    /// 取差集
    /// </summary>
    public static IEnumerable<T> GetItemNotIn<T>(this IEnumerable<T>? origin, IEnumerable<T> target, Func<T?, T?, bool>? comparer = null)
    {
        return origin.Except(target, comparer);
    }
}