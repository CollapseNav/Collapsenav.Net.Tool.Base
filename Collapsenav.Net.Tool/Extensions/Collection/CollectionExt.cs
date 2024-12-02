using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Collapsenav.Net.Tool;
public static partial class CollectionExt
{
    /// <summary>
    /// 分割集合
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="size">每片大小</param>
    public static IEnumerable<IEnumerable<T>> SpliteCollection<T>(this IEnumerable<T>? query, int size = 1)
    {
        if (query != null)
        {
            for (int i = 0; i < (query.Count() / size) + (query.Count() % size == 0 ? 0 : 1); i++)
                yield return query.Skip(i * size).Take(size);
        }
    }

    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么比</param>
    public static IEnumerable<T> Distinct<T>(this IEnumerable<T>? query, Func<T?, T?, bool>? comparer)
    {
        return query.Unique(comparer);
    }
    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="comparer">怎么比</param>
    public static IEnumerable<T> Unique<T>(this IEnumerable<T>? query, Func<T?, T?, bool>? comparer)
    {
        if (query == null)
            return Enumerable.Empty<T>();
        return query.Distinct(new CollapseNavEqualityComparer<T>(comparer));
    }

    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="field">hash去重</param>
    public static IEnumerable<T> Distinct<T, E>(this IEnumerable<T>? query, Func<T?, E>? field)
    {
        return query.Unique(field);
    }
    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="field">hash去重</param>
    public static IEnumerable<T> Unique<T, E>(this IEnumerable<T>? query, Func<T?, E>? field)
    {
        if (query == null)
            return Enumerable.Empty<T>();
        return query.Distinct(new CollapseNavEqualityComparer<T, E>(field));
    }

    /// <summary>
    /// 去重
    /// </summary>
    /// <param name="query">源集合</param>
    public static IEnumerable<T> Unique<T>(this IEnumerable<T>? query)
    {
        if (query == null)
            return Enumerable.Empty<T>();
        return query.Distinct();
    }

    /// <summary>
    /// 判断两个集合是否相等
    /// </summary>
    /// <param name="left">集合1</param>
    /// <param name="right">集合2</param>
    /// <param name="comparer">怎么比</param>
    public static bool SequenceEqual<T>(this IEnumerable<T>? left, IEnumerable<T>? right, Func<T?, T?, bool>? comparer)
    {
        if (left == null || right == null)
            return false;
        return left.SequenceEqual(right, new CollapseNavEqualityComparer<T>(comparer));
    }

    /// <summary>
    /// 判断两个集合是否相等
    /// </summary>
    /// <param name="left">集合1</param>
    /// <param name="right">集合2</param>
    /// <param name="field">hash</param>
    public static bool SequenceEqual<T, E>(this IEnumerable<T>? left, IEnumerable<T>? right, Func<T?, E>? field)
    {
        if (left == null || right == null)
            return false;
        return left.SequenceEqual(right, new CollapseNavEqualityComparer<T, E>(field));
    }

    /// <summary>
    /// WhereIf
    /// </summary>
    /// <param name="query">query</param>
    /// <param name="flag">bool 作为标记，true则应用 filter</param>
    /// <param name="filter">筛选条件</param>
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T>? query, bool flag, Func<T, bool> filter)
    {
        if (query == null)
            return Enumerable.Empty<T>();
        return flag ? query.Where(filter) : query;
    }
    /// <summary>
    /// WhereIf
    /// </summary>
    /// <param name="query">query</param>
    /// <param name="flag">bool 作为标记，true则应用 filter</param>
    /// <param name="filter">筛选条件</param>
    public static IEnumerable<T> WhereIf<T, N>(this IEnumerable<T>? query, N? flag, Func<T, bool> filter) where N : struct
    {
        if (query == null)
            return Enumerable.Empty<T>();
        return flag.HasValue ? query.Where(filter) : query;
    }

    /// <summary>
    /// WhereIf
    /// </summary>
    /// <param name="query">query</param>
    /// <param name="input">非空字符串则应用筛选条件</param>
    /// <param name="filter">筛选条件</param>
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T>? query, string? input, Func<T, bool> filter)
    {
        return query.WhereIf(input.NotEmpty(), filter);
    }

    /// <summary>
    /// WhereIf
    /// </summary>
    /// <param name="query">query</param>
    /// <param name="flag">bool 作为标记，true则应用 filter</param>
    /// <param name="filter">筛选条件</param>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T>? query, bool flag, Expression<Func<T, bool>> filter)
    {
        if (query == null)
            return Enumerable.Empty<T>().AsQueryable();
        return flag ? query.Where(filter) : query;
    }

    /// <summary>
    /// WhereIf
    /// </summary>
    /// <param name="query">query</param>
    /// <param name="input">非空字符串则应用筛选条件</param>
    /// <param name="filter">筛选条件</param>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T>? query, string? input, Expression<Func<T, bool>> filter)
    {
        return query.WhereIf(input.NotEmpty(), filter);
    }

    /// <summary>
    /// 空?
    /// </summary>
    /// <param name="query">源集合</param>
    public static bool IsEmpty<T>(
#if (NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER)
        [NotNullWhen(false)] 
#endif
        this IEnumerable<T>? query)
    {
        return query == null || !query.Any();
    }

    /// <summary>
    /// 没空?
    /// </summary>
    /// <param name="query">源集合</param>
    public static bool NotEmpty<T>(
#if (NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER)
        [NotNullWhen(true)] 
#endif
        this IEnumerable<T>? query)
    {
        return query != null && query.Any();
    }
    /// <summary>
    /// 打乱顺序
    /// </summary>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T>? query, int round = 1)
    {
        if (query == null)
            return Enumerable.Empty<T>();
        var random = new Random();
        if (round > 0)
        {
            for (var i = 0; i < round; i++)
                query = query.OrderBy(item => Guid.NewGuid()).ToList();
        }
        return query;
    }


    /// <summary>
    /// 遍历执行(不知道为啥原来的IEnumerable不提供这功能)
    /// </summary>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T>? query, Action<T> action)
    {
        if (query == null)
            return Enumerable.Empty<T>();
        foreach (var item in query)
            action(item);
        return query;
    }
    /// <summary>
    /// 生成包含index的元组集合(value,index)
    /// </summary>
    public static IEnumerable<(T value, int index)> SelectWithIndex<T>(this IEnumerable<T>? origin, int zero = 0)
    {
        if (origin == null)
            return Enumerable.Empty<(T, int)>();
        return origin.Select((value, index) => (value, index + zero));
    }
    /// <summary>
    /// 生成包含index的元组集合(value,index)
    /// </summary>
    /// <param name="origin">源集合</param>
    /// <param name="index">生成index的委托</param>
    public static IEnumerable<(T value, E index)> SelectWithIndex<T, E>(this IEnumerable<T>? origin, Func<T, E> index)
    {
        if (origin == null)
            return Enumerable.Empty<(T, E)>();
        return origin.Select(value => (value, index(value)));
    }
    /// <summary>
    /// 生成包含index的元组集合(value,index)
    /// </summary>
    /// <param name="origin">源集合</param>
    /// <param name="value">生成value的委托</param>
    /// <param name="index">生成index的委托</param>
    public static IEnumerable<(E value, F index)> SelectWithIndex<T, E, F>(this IEnumerable<T>? origin, Func<T, E> value, Func<T, F> index)
    {
        if (origin == null)
            return Enumerable.Empty<(E, F)>();
        return origin.Select(item => (value(item), index(item)));
    }

    public static JoinResult<T1, T2> LeftJoin<T1, T2>(this IEnumerable<T1> left, IEnumerable<T2> right, Expression<Func<T1, object?>> LKey, Expression<Func<T2, object?>> RKey) where T1 : class
    {
        var node = new JoinResult<T1>(left);
        return node.LeftJoin(right, LKey, RKey);
    }
}
