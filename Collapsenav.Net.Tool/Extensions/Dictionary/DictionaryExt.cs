namespace Collapsenav.Net.Tool;
public static partial class DictionaryExt
{
    /// <summary>
    /// 将键值对集合转为字典
    /// </summary>
    /// <param name="dict"></param>
    public static IDictionary<K, V>? ToDictionary<K, V>(this IEnumerable<KeyValuePair<K, V>>? dict) where K : notnull
    {
        if (dict.IsEmpty())
            return new Dictionary<K, V>();
        return dict.ToDictionary(item => item.Key, item => item.Value);
    }
    /// <summary>
    /// 将集合转为键值对
    /// </summary>
    /// <param name="query">集合</param>
    /// <param name="keySelector">key选择器</param>
    public static IDictionary<K, V>? ToDictionary<K, V>(this IEnumerable<V>? query, Func<V, K>? keySelector) where K : notnull
    {
        if (query.IsEmpty())
            return new Dictionary<K, V>();
        if (keySelector == null)
            throw new NullReferenceException("keySelector");
        return query.ToDictionary(keySelector, item => item);
    }
    /// <summary>
    /// 获取值并且移除字典项
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="key"></param>
    public static V? GetAndRemove<K, V>(this IDictionary<K, V>? dict, K key)
    {
        if (dict.IsEmpty())
            return default;
        return dict.Pop(key);
    }

    /// <summary>
    /// 获取值并且移除字典项
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="key"></param>
    public static V? Pop<K, V>(this IDictionary<K, V>? dict, K key)
    {
        if (dict.IsEmpty())
            return default;
        var flag = dict.TryGetValue(key, out V? value);
        if (flag)
            dict.Remove(key);
        return value;
    }

    /// <summary>
    /// 字典解构
    /// </summary>
    /// <typeparam name="K">Key 作为 index</typeparam>
    /// <typeparam name="V">Value 作为 value</typeparam>
    /// <typeparam name="E">value</typeparam>
    /// <typeparam name="F">index</typeparam>
    public static IEnumerable<KeyValuePair<E, F>> Deconstruct<K, V, E, F>(
        this IDictionary<K, V>? dict,
        Func<K, E> index,
        Func<V, F> value)
    {
        if (dict.NotEmpty())
            foreach (var item in dict)
                yield return new KeyValuePair<E, F>(index(item.Key), value(item.Value));
    }

    /// <summary>
    /// 将字典转换为指定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dict"></param>
    /// <returns></returns>
    public static T? ToObj<T>(this Dictionary<string, object>? dict)
    {
        return dict.ToJson().ToObj<T>();
    }
    /// <summary>
    /// 将字典转换为指定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dict"></param>
    /// <returns></returns>
    public static T? ToObj<T>(this Dictionary<string, string>? dict)
    {
        return dict.ToJson().ToObj<T>();
    }
}