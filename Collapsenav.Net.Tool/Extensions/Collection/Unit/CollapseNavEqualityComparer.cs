namespace Collapsenav.Net.Tool;

/// <summary>
/// 自定义的comparer
/// </summary>
public class CollapseNavEqualityComparer<T> : IEqualityComparer<T>
{
    public CollapseNavEqualityComparer(Func<T, int> hashcodeFunc)
    {
        HashCodeFunc = hashcodeFunc;
    }
    public CollapseNavEqualityComparer(Func<T, T, bool>? equalFunc)
    {
        EqualFunc = equalFunc;
    }
    /// <summary>
    /// 提供一个用于对比的委托
    /// </summary>
    private readonly Func<T, T, bool>? EqualFunc;
    /// <summary>
    /// 提供一个计算hashcode的委托，然后使用该委托计算对比
    /// </summary>
    private readonly Func<T, int>? HashCodeFunc;

    public bool Equals(T x, T y)
    {
        return EqualFunc == null ? GetHashCode(x) == GetHashCode(y) : EqualFunc(x, y);
    }

    public int GetHashCode(T obj)
    {
        return HashCodeFunc == null ? 0 : HashCodeFunc(obj);
    }
}
