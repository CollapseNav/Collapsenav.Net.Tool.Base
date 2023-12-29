namespace Collapsenav.Net.Tool;

public static partial class DictionaryExt
{
    /// <summary>
    /// 添加或更新
    /// </summary>
    /// <param name="dict">字典</param>
    /// <param name="item">键值对</param>
    public static IDictionary<K, V> AddOrUpdate<K, V>(this IDictionary<K, V> dict, KeyValuePair<K, V> item)
    {
        if (dict.ContainsKey(item.Key))
            dict[item.Key] = item.Value;
        else
            dict.Add(item.Key, item.Value);
        return dict;
    }
    /// <summary>
    /// 添加或更新
    /// </summary>
    /// <param name="dict">字典</param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static IDictionary<K, V> AddOrUpdate<K, V>(this IDictionary<K, V> dict, K key, V value)
    {
        if (dict.ContainsKey(key))
            dict[key] = value;
        else
            dict.Add(key, value);
        return dict;
    }
    /// <summary>
    /// 添加多个字典项
    /// </summary>
    /// <param name="dict">字典</param>
    /// <param name="values">值</param>
    public static IDictionary<K, V> AddRange<K, V>(this IDictionary<K, V> dict, IEnumerable<KeyValuePair<K, V>> values)
    {
        foreach (var item in values)
            dict.AddOrUpdate(item);
        return dict;
    }

}