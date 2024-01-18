namespace Collapsenav.Net.Tool;

/// <summary>
/// 自定义的comparer
/// </summary>
/// <remarks>
/// 建议在需要对自定义对象进行对比时使用<br/>
/// 推荐只使用一种对比方式
/// </remarks>
public class CollapseNavEqualityComparer<T> : IEqualityComparer<T>
{
    public CollapseNavEqualityComparer(Func<T?, int>? hashcodeFunc)
    {
        HashCodeFunc = hashcodeFunc;
    }
    public CollapseNavEqualityComparer(Func<T?, T?, bool>? equalFunc)
    {
        EqualFunc = equalFunc;
    }
    /// <summary>
    /// 提供一个用于对比的委托
    /// </summary>
    private readonly Func<T?, T?, bool>? EqualFunc;
    /// <summary>
    /// 提供一个计算hashcode的委托，然后使用该委托计算对比
    /// </summary>
    private readonly Func<T?, int>? HashCodeFunc;

    public bool Equals(T? x, T? y)
    {
        return EqualFunc == null ? GetHashCode(x) == GetHashCode(y) : EqualFunc(x, y);
    }

    public int GetHashCode(T? obj)
    {
        return HashCodeFunc == null ? 0 : HashCodeFunc(obj);
    }
}

/// <summary>
/// 自定义的comparer
/// </summary>
/// <remarks>
/// 建议在需要对自定义对象进行对比时使用<br/>
/// 推荐只使用一种对比方式
/// </remarks>
public class CollapseNavEqualityComparer<T, E> : IEqualityComparer<T>
{
    /// <summary>
    /// 一个用于对比的委托
    /// </summary>
    /// <param name="keySelector">按照选定字段判断是否重复</param>
    public CollapseNavEqualityComparer(Func<T?, E?>? keySelector)
    {
        KeySelector = keySelector;
    }
    /// <summary>
    /// 提供一个用于对比的委托
    /// </summary>
    private readonly Func<T?, T?, bool>? EqualFunc;
    /// <summary>
    /// 提供一个选择字段/属性的委托
    /// </summary>
    private readonly Func<T?, E?>? KeySelector;

    public bool Equals(T? x, T? y)
    {
        return EqualFunc == null ? GetHashCode(x) == GetHashCode(y) : EqualFunc(x, y);
    }

    public int GetHashCode(T? obj)
    {
        return KeySelector == null ? 0 : (KeySelector(obj)?.GetHashCode() ?? 0);
    }
}
