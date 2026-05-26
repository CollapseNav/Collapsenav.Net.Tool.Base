using System.Linq.Expressions;

namespace Collapsenav.Net.Tool;

internal class ParameterReplaceVisitor : ExpressionVisitor
{
    private readonly ParameterExpression _target;
    private readonly ParameterExpression _replacement;
    public ParameterReplaceVisitor(ParameterExpression target, ParameterExpression replacement)
    {
        _target = target;
        _replacement = replacement;
    }
    protected override Expression VisitParameter(ParameterExpression node)
        => node == _target ? _replacement : base.VisitParameter(node);
}

public static partial class ExpressionExt
{
    private static Expression ReplaceParameter(this Expression body, ParameterExpression target, ParameterExpression replacement)
        => new ParameterReplaceVisitor(target, replacement).Visit(body);

    public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
    {
        var expBody = exp.Body.ReplaceParameter(exp.Parameters[0], origin.Parameters[0]);
        var sum = Expression.AndAlso(origin.Body, expBody);
        return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
    }
    public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
    {
        var expBody = exp.Body.ReplaceParameter(exp.Parameters[0], origin.Parameters[0]);
        var sum = Expression.OrElse(origin.Body, expBody);
        return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
    }
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
    {
        var expBody = exp.Body.ReplaceParameter(exp.Parameters[0], origin.Parameters[0]);
        var sum = Expression.And(origin.Body, expBody);
        return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
    }
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> origin, Expression<Func<T, bool>> exp)
    {
        var expBody = exp.Body.ReplaceParameter(exp.Parameters[0], origin.Parameters[0]);
        var sum = Expression.Or(origin.Body, expBody);
        return Expression.Lambda<Func<T, bool>>(sum, origin.Parameters);
    }


    public static Expression<Func<T, bool>> AndAlsoIf<T>(this Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
    {
        if (!flag)
            return origin;
        return origin.AndAlso(exp);
    }
    public static Expression<Func<T, bool>> OrElseIf<T>(this Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
    {
        if (!flag)
            return origin;
        return origin.OrElse(exp);
    }
    public static Expression<Func<T, bool>> AndIf<T>(this Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
    {
        if (!flag)
            return origin;
        return origin.And(exp);
    }
    public static Expression<Func<T, bool>> AndIf<T, N>(this Expression<Func<T, bool>> origin, N? flag, Expression<Func<T, bool>> exp) where N : struct
    {
        if (!flag.HasValue)
            return origin;
        return origin.AndIf(true, exp);
    }
    public static Expression<Func<T, bool>> OrIf<T>(this Expression<Func<T, bool>> origin, bool flag, Expression<Func<T, bool>> exp)
    {
        if (!flag)
            return origin;
        return origin.Or(exp);
    }


    public static Expression<Func<T, bool>> AndAlsoIf<T>(this Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
    {
        if (flag.IsEmpty())
            return origin;
        return origin.AndAlso(exp);
    }
    public static Expression<Func<T, bool>> OrElseIf<T>(this Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
    {
        if (flag.IsEmpty())
            return origin;
        return origin.OrElse(exp);
    }
    public static Expression<Func<T, bool>> AndIf<T>(this Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
    {
        if (flag.IsEmpty())
            return origin;
        return origin.And(exp);
    }
    public static Expression<Func<T, bool>> OrIf<T>(this Expression<Func<T, bool>> origin, string flag, Expression<Func<T, bool>> exp)
    {
        if (flag.IsEmpty())
            return origin;
        return origin.Or(exp);
    }
}
