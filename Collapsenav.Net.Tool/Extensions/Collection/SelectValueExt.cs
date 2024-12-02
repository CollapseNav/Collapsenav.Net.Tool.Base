namespace Collapsenav.Net.Tool;

public static class SelectValueExt
{
    public static IQueryable<ReturnValue?> SelectValue<T1, T2, ReturnValue>(this JoinResult<T1, T2> query, Func<T1?, T2?, ReturnValue?> func)
    {
        return query.Select(item => func(item.Data1, item.Data2));
    }
    public static IQueryable<ReturnValue?> SelectValue<T1, T2, T3, ReturnValue>(this JoinResult<T1, T2, T3> query, Func<T1?, T2?, T3?, ReturnValue?> func)
    {
        return query.Select(item => func(item.Data1, item.Data2, item.Data3));
    }
    public static IQueryable<ReturnValue?> SelectValue<T1, T2, T3, T4, ReturnValue>(this JoinResult<T1, T2, T3, T4> query, Func<T1?, T2?, T3?, T4?, ReturnValue?> func)
    {
        return query.Select(item => func(item.Data1, item.Data2, item.Data3, item.Data4));
    }
    public static IQueryable<ReturnValue?> SelectValue<T1, T2, T3, T4, T5, ReturnValue>(this JoinResult<T1, T2, T3, T4, T5> query, Func<T1?, T2?, T3?, T4?, T5?, ReturnValue?> func)
    {
        return query.Select(item => func(item.Data1, item.Data2, item.Data3, item.Data4, item.Data5));
    }
    public static IQueryable<ReturnValue?> SelectValue<T1, T2, T3, T4, T5, T6, ReturnValue>(this JoinResult<T1, T2, T3, T4, T5, T6> query, Func<T1?, T2?, T3?, T4?, T5?, T6?, ReturnValue?> func)
    {
        return query.Select(item => func(item.Data1, item.Data2, item.Data3, item.Data4, item.Data5, item.Data6));
    }
    public static IQueryable<ReturnValue?> SelectValue<T1, T2, T3, T4, T5, T6, T7, ReturnValue>(this JoinResult<T1, T2, T3, T4, T5, T6, T7> query, Func<T1?, T2?, T3?, T4?, T5?, T6?, T7?, ReturnValue?> func)
    {
        return query.Select(item => func(item.Data1, item.Data2, item.Data3, item.Data4, item.Data5, item.Data6, item.Data7));
    }
    public static IQueryable<ReturnValue?> SelectValue<T1, T2, T3, T4, T5, T6, T7, T8, ReturnValue>(this JoinResult<T1, T2, T3, T4, T5, T6, T7, T8> query, Func<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, ReturnValue?> func)
    {
        return query.Select(item => func(item.Data1, item.Data2, item.Data3, item.Data4, item.Data5, item.Data6, item.Data7, item.Data8));
    }
    public static IQueryable<ReturnValue?> SelectValue<T1, T2, T3, T4, T5, T6, T7, T8, T9, ReturnValue>(this JoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> query, Func<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?, ReturnValue?> func)
    {
        return query.Select(item => func(item.Data1, item.Data2, item.Data3, item.Data4, item.Data5, item.Data6, item.Data7, item.Data8, item.Data9));
    }
}