using System.Linq.Expressions;
using Collapsenav.Net.Tool;

public static class JoinExt
{
    public static JoinResult<T> CreateJoin<T>(this IEnumerable<T> query)
    {
        var result = new JoinResult<T>(query);
        return result;
    }
}