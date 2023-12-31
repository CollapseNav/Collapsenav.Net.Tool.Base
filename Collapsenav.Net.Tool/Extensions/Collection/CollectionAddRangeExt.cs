namespace Collapsenav.Net.Tool;
public partial class CollectionExt
{
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="values">添加的对象</param>
    /// <param name="field">按照选定字段判断是否重复</param>
    public static IEnumerable<T> AddRange<T, E>(this IEnumerable<T>? query, IEnumerable<T>? values, Func<T?, E?>? field)
    {
        if (query == null)
            return Enumerable.Empty<T>();
        if (values.IsEmpty())
            return query;
        return query.Union(values, new CollapseNavEqualityComparer<T, E>(field));
    }

    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="comparer">去重依据</param>
    /// <param name="values">添加的对象</param>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T>? query, IEnumerable<T>? values, Func<T?, T?, bool>? comparer = null)
    {
        if (query == null)
            return Enumerable.Empty<T>();
        if (values.IsEmpty())
            return query;
        return query.Union(values, comparer != null ? new CollapseNavEqualityComparer<T>(comparer) : null);
    }



    /// <summary>
    /// 向一个集合中添加多个对象
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="values">添加的对象</param>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T>? query, params T[] values)
    {
        if (query == null)
            return Enumerable.Empty<T>();
        return query.AddRange(values, null);
    }

    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="comparer">去重依据</param>
    /// <param name="values">添加的对象</param>
    public static IEnumerable<T> AddRange<T>(this IEnumerable<T>? query, Func<T?, T?, bool>? comparer, params T[] values)
    {
        if (query == null)
            return Enumerable.Empty<T>();
        return query.AddRange(values, comparer);
    }

    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="field">去重依据(hash)</param>
    /// <param name="values">添加的对象</param>
    public static IEnumerable<T> AddRange<T, E>(this IEnumerable<T>? query, Func<T?, E?>? field, params T[] values)
        => query.AddRange(values, field);



    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="comparer">去重依据</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T>(this ICollection<T>? query, IEnumerable<T>? values, Func<T?, T?, bool>? comparer = null)
    {
        if (query == null || values.IsEmpty())
            return;
        if (comparer == null)
        {
            foreach (var item in values)
                query.Add(item);
        }
        else
        {
            var uniqueComparer = new CollapseNavEqualityComparer<T>(comparer);
            var uniqueData = values.Distinct(uniqueComparer);
            foreach (var item in uniqueData)
                if (!query.Contains(item, uniqueComparer))
                    query.Add(item);
        }
    }

    /// <summary>
    /// 向一个集合中添加多个对象
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T>(this ICollection<T>? query, params T[] values)
    {
        query.AddRange(values.AsEnumerable());
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="comparer">去重依据</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T>(this ICollection<T>? query, Func<T?, T?, bool>? comparer, params T[] values)
    {
        query.AddRange(values, comparer);
    }
    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="hashCodeFunc">去重依据(hash)</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T, E>(this ICollection<T>? query, Func<T?, E?>? hashCodeFunc, params T[] values)
    {
        query.AddRange(values, hashCodeFunc);
    }

    /// <summary>
    /// 向一个集合中添加多个对象(带去重)
    /// </summary>
    /// <param name="query">源</param>
    /// <param name="hashCodeFunc">去重依据(hash)</param>
    /// <param name="values">添加的对象</param>
    public static void AddRange<T, E>(this ICollection<T>? query, IEnumerable<T>? values, Func<T?, E?>? hashCodeFunc)
    {
        if (query == null || values.IsEmpty())
            return;
        var uniqueComparer = new CollapseNavEqualityComparer<T, E>(hashCodeFunc);
        var uniqueData = values.Distinct(uniqueComparer);
        foreach (var item in uniqueData)
            if (!query.Contains(item, uniqueComparer))
                query.Add(item);
    }
}