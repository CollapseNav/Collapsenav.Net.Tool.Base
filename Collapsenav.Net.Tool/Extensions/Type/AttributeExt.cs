using System.Reflection;

namespace Collapsenav.Net.Tool;

public static partial class AttributeExt
{
    /// <summary>
    /// 成员 member 拥有对应的attribute类型
    /// </summary>
    public static bool HasAttribute<T>(this MemberInfo info)
    {
        return info.HasAttribute(typeof(T));
    }
    /// <summary>
    /// 成员 member 拥有对应的attribute类型
    /// </summary>
    public static bool HasAttribute(this MemberInfo info, params Type[] attrTypes)
    {
        return info.CustomAttributes.Any(attr => attrTypes.Contains(attr.AttributeType));
    }
    /// <summary>
    /// type中 拥有对应attribute的属性
    /// </summary>
    public static IEnumerable<PropertyInfo> AttrProps(this Type type, params Type[] attrTypes)
    {
        return type.Props().Where(item => item.HasAttribute(attrTypes));
    }
    /// <summary>
    /// type中 拥有对应attribute的属性
    /// </summary>
    public static IEnumerable<PropertyInfo> AttrProps<E>(this Type type) where E : Attribute
    {
        return type.AttrProps(typeof(E));
    }
    /// <summary>
    /// type中 拥有对应attribute的字段
    /// </summary>
    public static IEnumerable<FieldInfo> AttrFields(this Type type, params Type[] attrTypes)
    {
        return type.GetFields().Where(item => item.HasAttribute(attrTypes));
    }
    /// <summary>
    /// type中 拥有对应attribute的字段
    /// </summary>
    public static IEnumerable<FieldInfo> AttrFields<E>(this Type type) where E : Attribute
    {
        return type.AttrFields(typeof(E));
    }
    /// <summary>
    /// type中 拥有对应attribute的成员
    /// </summary>
    public static IEnumerable<MemberInfo> AttrMembers(this Type type, params Type[] attrTypes)
    {
        return type.GetMembers().Where(item => item.HasAttribute(attrTypes));
    }
    /// <summary>
    /// type中 拥有对应attribute的成员
    /// </summary>
    public static IEnumerable<MemberInfo> AttrMembers<E>(this Type type) where E : Attribute
    {
        return type.AttrMembers(typeof(E));
    }


#if NET6_0_OR_GREATER
    /// <summary>
    /// 获取T中存在E的attribute的字段及attribute值
    /// </summary>
    public static Dictionary<FieldInfo, IEnumerable<E>> AttrFieldValues<E>(this Type type) where E : Attribute
    {
        var fields = type.AttrFields<E>();
        return fields.ToDictionary(item => item, item => item.GetCustomAttributes<E>());
    }
    /// <summary>
    /// 获取T中存在E的attribute的属性及attribute值
    /// </summary>
    public static Dictionary<PropertyInfo, IEnumerable<E>> AttrValues<E>(this Type type) where E : Attribute
    {
        return type.AttrPropValues<E>();
    }
    /// <summary>
    /// 获取T中存在E的attribute的属性及attribute值
    /// </summary>
    public static Dictionary<PropertyInfo, IEnumerable<E>> AttrPropValues<E>(this Type type) where E : Attribute
    {
        var props = type.AttrProps<E>();
        return props.ToDictionary(item => item, item => item.GetCustomAttributes<E>());
    }
#else
    /// <summary>
    /// 获取T中存在E的attribute的字段及attribute值
    /// </summary>
    public static Dictionary<FieldInfo, IEnumerable<E>> AttrFieldValues<E>(this Type type) where E : Attribute
    {
        var fields = type.AttrFields<E>();
        return fields.ToDictionary(item => item, item => item.GetCustomAttributes<E>());
    }
    /// <summary>
    /// 获取T中存在E的attribute的属性及attribute值
    /// </summary>
    public static Dictionary<PropertyInfo, IEnumerable<E>> AttrValues<E>(this Type type) where E : Attribute
    {
        return type.AttrPropValues<E>();
    }
    /// <summary>
    /// 获取T中存在E的attribute的属性及attribute值
    /// </summary>
    public static Dictionary<PropertyInfo, IEnumerable<E>> AttrPropValues<E>(this Type type) where E : Attribute
    {
        var props = type.AttrProps<E>();
        return props.ToDictionary(item => item, item => item.GetCustomAttributes<E>());
    }
#endif


    /// <summary>
    /// 获取T中存在E的attribute的成员及attribute值
    /// </summary>
    public static Dictionary<MemberInfo, IEnumerable<E>> AttrMemberValues<E>(this Type type) where E : Attribute
    {
        var props = type.AttrPropValues<E>();
        var fields = type.AttrFieldValues<E>();
        Dictionary<MemberInfo, IEnumerable<E>> result = new();
        if (props is IDictionary<PropertyInfo, IEnumerable<E>> propDict && propDict.NotEmpty())
            propDict.ForEach(dict => result.AddOrUpdate(dict.Key, dict.Value));
        if (fields is IDictionary<FieldInfo, IEnumerable<E>> fieldDict && fieldDict.NotEmpty())
            fieldDict.ForEach(dict => result.AddOrUpdate(dict.Key, dict.Value));
        return result;
    }
    /// <summary>
    /// type中 拥有对应attribute的属性的类型
    /// </summary>
    public static IEnumerable<Type> AttrPropTypes(this Type type, params Type[] attrTypes)
    {
        var props = type.AttrProps(attrTypes);
        return props.Select(prop => prop.PropertyType);
    }
    /// <summary>
    /// type中 拥有对应attribute的属性的类型
    /// </summary>
    public static IEnumerable<Type> AttrPropTypes<E>(this Type type) where E : Attribute
    {
        return type.AttrPropTypes(typeof(E));
    }
    /// <summary>
    /// type中 拥有对应attribute的字段的类型
    /// </summary>
    public static IEnumerable<Type> AttrFieldTypes(this Type type, params Type[] attrTypes)
    {
        var fields = type.AttrFields(attrTypes);
        return fields.Select(field => field.FieldType);
    }
    /// <summary>
    /// type中 拥有对应attribute的字段的类型
    /// </summary>
    public static IEnumerable<Type> AttrFieldTypes<E>(this Type type) where E : Attribute
    {
        return type.AttrFieldTypes(typeof(E));
    }
    /// <summary>
    /// type中 拥有对应attribute的成员的类型
    /// </summary>
    public static IEnumerable<Type> AttrMemberTypes(this Type type, params Type[] attrTypes)
    {
        var props = type.AttrProps(attrTypes);
        var fields = type.AttrFields(attrTypes);
        return props.Select(prop => prop.PropertyType).AddRange(fields.Select(field => field.FieldType));
    }
    /// <summary>
    /// type中 拥有对应attribute的成员的类型
    /// </summary>
    public static IEnumerable<Type> AttrMemberTypes<E>(this Type type) where E : Attribute
    {
        return type.AttrMemberTypes(typeof(E));
    }

}