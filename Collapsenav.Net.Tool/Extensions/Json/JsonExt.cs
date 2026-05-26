using System.Text.Json;
using System.Text.Json.Nodes;

namespace Collapsenav.Net.Tool;
public static partial class JsonExt
{
    public static JsonSerializerOptions DefaultJsonSerializerOption = JsonOptions.BetterUseOption;
    /// <summary>
    /// Json字符串转为对象（via JSON deserialization）
    /// </summary>
    public static T? ToObj<T>(this string str, JsonSerializerOptions? options = null) => JsonSerializer.Deserialize<T>(str, options ?? DefaultJsonSerializerOption);
    /// <summary>
    /// Json字符串转为指定类型对象（via JSON deserialization）
    /// </summary>
    public static object? ToObj(this string str, Type type, JsonSerializerOptions? options = null) => JsonSerializer.Deserialize(str, type, options ?? DefaultJsonSerializerOption);
    /// <summary>
    /// Json字符串转为对象集合（via JSON deserialization）
    /// </summary>
    public static IEnumerable<T>? ToObjCollection<T>(this string str, JsonSerializerOptions? options = null) => JsonSerializer.Deserialize<IEnumerable<T>>(str, options ?? DefaultJsonSerializerOption);
    /// <summary>
    /// 对象转为Json字符串（via JSON serialization）
    /// </summary>
    public static string ToJson<T>(this T obj, JsonSerializerOptions? options = null) => JsonSerializer.Serialize(obj, options ?? DefaultJsonSerializerOption);



    /// <summary>
    /// 通过 JSON 序列化反序列化映射对象
    /// </summary>
    public static T? JsonMap<T>(this object obj) => obj.ToJson().ToObj<T>();
    /// <summary>
    /// 通过 JSON 序列化反序列化映射对象
    /// </summary>
    public static T? JsonMap<S, T>(this S obj) => obj.ToJson().ToObj<T>();

    /// <summary>
    /// 通过反射将左边对象的属性值映射到右边对象
    /// </summary>
    public static T? JsonMap<S, T>(this S origin, T target) where S : class where T : class
    {
        var propNames = origin.PropNames();
        if (propNames == null)
            throw new NullReferenceException();
        foreach (var name in propNames)
            target?.SetValue(name, origin.GetValue(name));
        return target;
    }

    /// <summary>
    /// 通过 JSON 序列化反序列化进行深拷贝
    /// </summary>
    public static T? JsonCopy<T>(this T origin)
    {
        return origin.ToJson().ToObj<T>();
    }

    /// <summary>
    /// 从 JSON 文件中读取 JsonElement（path 为文件路径）
    /// </summary>
    public static JsonElement GetJsonElementFromPath(this string path) => path.GetJsonDocumentFromPath().RootElement;
    /// <summary>
    /// 从 JSON 文件中读取 JsonElement（path 为文件路径，异步）
    /// </summary>
    public static async ValueTask<JsonElement> GetJsonElementFromPathAsync(this string path) => (await path.GetJsonDocumentFromPathAsync()).RootElement;
    /// <summary>
    /// 从 JSON 文件中读取 JsonDocument（path 为文件路径）
    /// </summary>
    public static JsonDocument GetJsonDocumentFromPath(this string path)
    {
        using var stream = path.OpenReadWriteShareStream();
        return JsonDocument.Parse(stream);
    }
    /// <summary>
    /// 从 JSON 文件中读取 JsonDocument（path 为文件路径，异步）
    /// </summary>
    public static async ValueTask<JsonDocument> GetJsonDocumentFromPathAsync(this string path)
    {
        using var stream = path.OpenReadWriteShareStream();
        return await JsonDocument.ParseAsync(stream);
    }
    /// <summary>
    /// 从 JSON 文件中读取 JsonObject（path 为文件路径）
    /// </summary>
    public static JsonObject? GetJsonObjectFromPath(this string path) => JsonObject.Create(path.GetJsonElementFromPath());
    /// <summary>
    /// 从 JSON 文件中读取 JsonObject（path 为文件路径，异步）
    /// </summary>
    public static async ValueTask<JsonObject?> GetJsonObjectFromPathAsync(this string path) => JsonObject.Create(await path.GetJsonElementFromPathAsync());
    /// <summary>
    /// 将json字符串转为 JsonDocument
    /// </summary>
    public static JsonDocument ToJsonDocument(this string jsonStr)
    {
        return JsonDocument.Parse(jsonStr);
    }
    /// <summary>
    /// 将json字符串转为 JsonElement
    /// </summary>
    public static JsonElement ToJsonElement(this string jsonStr)
    {
        return JsonDocument.Parse(jsonStr).RootElement;
    }
    /// <summary>
    /// 将json字符串转为 JsonNode
    /// </summary>
    public static JsonNode? ToJsonNode(this string jsonStr)
    {
        return JsonNode.Parse(jsonStr);
    }
    /// <summary>
    /// 将json字符串转为 JsonObject
    /// </summary>
    public static JsonObject? ToJsonObject(this string jsonStr)
    {
        return JsonObject.Create(jsonStr.ToJsonElement());
    }
    /// <summary>
    /// 将json字符串转为 JsonArray
    /// </summary>
    public static JsonArray? ToJsonArray(this string jsonStr)
    {
        return JsonArray.Create(jsonStr.ToJsonElement());
    }
    /// <summary>
    /// 将 JsonNode 转为指定对象（via JSON deserialization）
    /// </summary>
    public static T? ToObj<T>(this JsonNode node, JsonSerializerOptions? options = null) => JsonSerializer.Deserialize<T>(node, options ?? DefaultJsonSerializerOption);
}
