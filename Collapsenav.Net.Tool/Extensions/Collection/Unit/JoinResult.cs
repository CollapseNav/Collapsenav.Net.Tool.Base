using System.Collections;
using System.Linq.Expressions;
namespace Collapsenav.Net.Tool;

public class JoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IQueryable<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>>
{
    public IQueryable<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>> Query;

    public JoinResult(IQueryable<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>> query)
    {
        Query = query;
    }

    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    /// <summary>
    /// 求求了，别再联表了
    /// </summary>
    public JoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> LeftJoin<T10>(IEnumerable<T10> query, Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>, object?>> LKey, Expression<Func<T10, object?>> RKey)
    {
        throw new Exception("求求了，别再联表了");
    }
    /// <summary>
    /// 求求了，别再联表了
    /// </summary>
    public JoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> Join<T10>(IEnumerable<T10> query, Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>, object?>> LKey, Expression<Func<T10, object?>> RKey)
    {
        throw new Exception("求求了，别再联表了");
    }
    public IEnumerator<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class JoinResult<T1, T2, T3, T4, T5, T6, T7, T8> : IQueryable<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>>
{
    public IQueryable<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>> Query;

    public JoinResult(IQueryable<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>> query)
    {
        Query = query;
    }

    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public JoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> LeftJoin<T9>(IEnumerable<T9> query, Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>, object?>> LKey, Expression<Func<T9, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, x.Data5, x.Data6, x.Data7, x.Data8, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>
            {
                Data1 = x.Data1,
                Data2 = x.Data2,
                Data3 = x.Data3,
                Data4 = x.Data4,
                Data5 = x.Data5,
                Data6 = x.Data6,
                Data7 = x.Data7,
                Data8 = x.Data8,
                Data9 = y,
            }));
        return result;
    }
    public JoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> Join<T9>(IEnumerable<T9> query, Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>, object?>> LKey, Expression<Func<T9, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Query.Join(rquery, LKey, RKey, (x, y) => new JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>(x) { Data9 = y, }));
        return result;
    }
    public IEnumerator<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class JoinResult<T1, T2, T3, T4, T5, T6, T7> : IQueryable<JoinResultItem<T1, T2, T3, T4, T5, T6, T7>>
{
    public IQueryable<JoinResultItem<T1, T2, T3, T4, T5, T6, T7>> Query;

    public JoinResult(IQueryable<JoinResultItem<T1, T2, T3, T4, T5, T6, T7>> query)
    {
        Query = query;
    }

    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public JoinResult<T1, T2, T3, T4, T5, T6, T7, T8> LeftJoin<T8>(IEnumerable<T8> query, Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6, T7>, object?>> LKey, Expression<Func<T8, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3, T4, T5, T6, T7, T8>(Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, x.Data5, x.Data6, x.Data7, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>
            {
                Data1 = x.Data1,
                Data2 = x.Data2,
                Data3 = x.Data3,
                Data4 = x.Data4,
                Data5 = x.Data5,
                Data6 = x.Data6,
                Data7 = x.Data7,
                Data8 = y,
            }));
        return result;
    }
    public JoinResult<T1, T2, T3, T4, T5, T6, T7, T8> Join<T8>(IEnumerable<T8> query, Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6, T7>, object?>> LKey, Expression<Func<T8, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3, T4, T5, T6, T7, T8>(Query.Join(rquery, LKey, RKey, (x, y) => new JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>(x) { Data8 = y, }));
        return result;
    }
    public IEnumerator<JoinResultItem<T1, T2, T3, T4, T5, T6, T7>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class JoinResult<T1, T2, T3, T4, T5, T6> : IQueryable<JoinResultItem<T1, T2, T3, T4, T5, T6>>
{
    public IQueryable<JoinResultItem<T1, T2, T3, T4, T5, T6>> Query;

    public JoinResult(IQueryable<JoinResultItem<T1, T2, T3, T4, T5, T6>> query)
    {
        Query = query;
    }

    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public JoinResult<T1, T2, T3, T4, T5, T6, T7> LeftJoin<T7>(IEnumerable<T7> query, Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6>, object?>> LKey, Expression<Func<T7, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3, T4, T5, T6, T7>(Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, x.Data5, x.Data6, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new JoinResultItem<T1, T2, T3, T4, T5, T6, T7>
            {
                Data1 = x.Data1,
                Data2 = x.Data2,
                Data3 = x.Data3,
                Data4 = x.Data4,
                Data5 = x.Data5,
                Data6 = x.Data6,
                Data7 = y,
            }));
        return result;
    }
    public JoinResult<T1, T2, T3, T4, T5, T6, T7> Join<T7>(IEnumerable<T7> query, Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6>, object?>> LKey, Expression<Func<T7, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3, T4, T5, T6, T7>(Query.Join(rquery, LKey, RKey, (x, y) => new JoinResultItem<T1, T2, T3, T4, T5, T6, T7>(x) { Data7 = y, }));
        return result;
    }
    public IEnumerator<JoinResultItem<T1, T2, T3, T4, T5, T6>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class JoinResult<T1, T2, T3, T4, T5> : IQueryable<JoinResultItem<T1, T2, T3, T4, T5>>
{
    public IQueryable<JoinResultItem<T1, T2, T3, T4, T5>> Query;

    public JoinResult(IQueryable<JoinResultItem<T1, T2, T3, T4, T5>> query)
    {
        Query = query;
    }

    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public JoinResult<T1, T2, T3, T4, T5, T6> LeftJoin<T6>(IEnumerable<T6> query, Expression<Func<JoinResultItem<T1, T2, T3, T4, T5>, object?>> LKey, Expression<Func<T6, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3, T4, T5, T6>(Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, x.Data5, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new JoinResultItem<T1, T2, T3, T4, T5, T6>
            {
                Data1 = x.Data1,
                Data2 = x.Data2,
                Data3 = x.Data3,
                Data4 = x.Data4,
                Data5 = x.Data5,
                Data6 = y,
            }));
        return result;
    }
    public JoinResult<T1, T2, T3, T4, T5, T6> Join<T6>(IEnumerable<T6> query, Expression<Func<JoinResultItem<T1, T2, T3, T4, T5>, object?>> LKey, Expression<Func<T6, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3, T4, T5, T6>(Query.Join(rquery, LKey, RKey, (x, y) => new JoinResultItem<T1, T2, T3, T4, T5, T6>(x) { Data6 = y, }));
        return result;
    }
    public IEnumerator<JoinResultItem<T1, T2, T3, T4, T5>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class JoinResult<T1, T2, T3, T4> : IQueryable<JoinResultItem<T1, T2, T3, T4>>
{
    public IQueryable<JoinResultItem<T1, T2, T3, T4>> Query;

    public JoinResult(IQueryable<JoinResultItem<T1, T2, T3, T4>> query)
    {
        Query = query;
    }

    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public JoinResult<T1, T2, T3, T4, T5> LeftJoin<T5>(IEnumerable<T5> query, Expression<Func<JoinResultItem<T1, T2, T3, T4>, object?>> LKey, Expression<Func<T5, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3, T4, T5>(Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new JoinResultItem<T1, T2, T3, T4, T5>
            {
                Data1 = x.Data1,
                Data2 = x.Data2,
                Data3 = x.Data3,
                Data4 = x.Data4,
                Data5 = y,
            }));
        return result;
    }
    public JoinResult<T1, T2, T3, T4, T5> Join<T5>(IEnumerable<T5> query, Expression<Func<JoinResultItem<T1, T2, T3, T4>, object?>> LKey, Expression<Func<T5, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3, T4, T5>(Query.Join(rquery, LKey, RKey, (x, y) => new JoinResultItem<T1, T2, T3, T4, T5>(x) { Data5 = y, }));
        return result;
    }
    public IEnumerator<JoinResultItem<T1, T2, T3, T4>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class JoinResult<T1, T2, T3> : IQueryable<JoinResultItem<T1, T2, T3>>
{
    public IQueryable<JoinResultItem<T1, T2, T3>> Query;

    public JoinResult(IQueryable<JoinResultItem<T1, T2, T3>> query)
    {
        Query = query;
    }

    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public JoinResult<T1, T2, T3, T4> LeftJoin<T4>(IEnumerable<T4> query, Expression<Func<JoinResultItem<T1, T2, T3>, object?>> LKey, Expression<Func<T4, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3, T4>(Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new JoinResultItem<T1, T2, T3, T4>
            {
                Data1 = x.Data1,
                Data2 = x.Data2,
                Data3 = x.Data3,
                Data4 = y
            }));
        return result;
    }
    public JoinResult<T1, T2, T3, T4> Join<T4>(IEnumerable<T4> query, Expression<Func<JoinResultItem<T1, T2, T3>, object?>> LKey, Expression<Func<T4, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3, T4>(Query.Join(rquery, LKey, RKey, (x, y) => new JoinResultItem<T1, T2, T3, T4>(x) { Data4 = y }));
        return result;
    }
    public IEnumerator<JoinResultItem<T1, T2, T3>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class JoinResult<T1, T2> : IQueryable<JoinResultItem<T1, T2>>
{
    public JoinResult(IQueryable<JoinResultItem<T1, T2>> query)
    {
        Query = query;
    }

    public IQueryable<JoinResultItem<T1, T2>> Query { get; set; }
    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public JoinResult<T1, T2, T3> LeftJoin<T3>(IEnumerable<T3> query, Expression<Func<JoinResultItem<T1, T2>, object?>> LKey, Expression<Func<T3, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3>(Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new JoinResultItem<T1, T2, T3>
            {
                Data1 = x.Data1,
                Data2 = x.Data2,
                Data3 = y
            }));
        return result;
    }
    public JoinResult<T1, T2, T3> Join<T3>(IEnumerable<T3> query, Expression<Func<JoinResultItem<T1, T2>, object?>> LKey, Expression<Func<T3, object?>> RKey)
    {
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2, T3>(Query.Join(rquery, LKey, RKey, (x, y) => new JoinResultItem<T1, T2, T3>(x) { Data3 = y, }));
        return result;
    }
    public IEnumerator<JoinResultItem<T1, T2>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class JoinResult<T1> : IQueryable<JoinResultItem<T1>>
{
    private IEnumerable<T1> _query;
    public JoinResult(IEnumerable<T1> query)
    {
        _query = query;
        Query = query.Select(i => new JoinResultItem<T1> { Data1 = i }).AsQueryable();
    }
    public IQueryable<JoinResultItem<T1>> Query;
    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public JoinResult<T1, T2> LeftJoin<T2>(IEnumerable<T2> query, Expression<Func<T1, object?>> LKey, Expression<Func<T2, object?>> RKey)
    {
        var lquery = _query.AsQueryable();
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2>(lquery.GroupJoin(rquery, LKey, RKey, (x, y) => new { x, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new JoinResultItem<T1, T2>
            {
                Data1 = x.x,
                Data2 = y
            }));
        return result;
    }
    public JoinResult<T1, T2> Join<T2>(IEnumerable<T2> query, Expression<Func<T1, object?>> LKey, Expression<Func<T2, object?>> RKey)
    {
        var lquery = _query.AsQueryable();
        var rquery = query.AsQueryable();
        var result = new JoinResult<T1, T2>(lquery.Join(rquery, LKey, RKey, (x, y) => new JoinResultItem<T1, T2>(new JoinResultItem<T1> { Data1 = x }) { Data2 = y }));
        return result;
    }
    public IEnumerator<JoinResultItem<T1>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}