using System.Reflection;

namespace Collapsenav.Net.Tool;
public static partial class TypeExt
{
    private static Type[] BuildInTypes;

    static TypeExt()
    {
        BuildInTypes = new List<Type> { typeof(Boolean), typeof(Byte), typeof(SByte), typeof(Char), typeof(Decimal), typeof(Double), typeof(Single), typeof(Int32), typeof(UInt32), typeof(IntPtr), typeof(UIntPtr), typeof(Int64), typeof(UInt64), typeof(Int16), typeof(UInt16), typeof(String), typeof(DateTime), typeof(Guid), typeof(Nullable<Boolean>), typeof(Nullable<Byte>), typeof(Nullable<SByte>), typeof(Nullable<Char>), typeof(Nullable<Decimal>), typeof(Nullable<Double>), typeof(Nullable<Single>), typeof(Nullable<Int32>), typeof(Nullable<UInt32>), typeof(Nullable<IntPtr>), typeof(Nullable<UIntPtr>), typeof(Nullable<Int64>), typeof(Nullable<UInt64>), typeof(Nullable<Int16>), typeof(Nullable<UInt16>), typeof(Nullable<DateTime>), typeof(Nullable<Guid>), }.ToArray();
    }

    /// <summary>
    /// 是否内建类型
    /// </summary>
    public static bool IsBaseType(this object obj)
    {
        return obj.GetType().IsBaseType();
    }
    /// <summary>
    /// 是否内建类型
    /// </summary>
    public static bool IsBaseType(this Type type)
    {
        return type.In(BuildInTypes);
    }

    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string>? PropNames<T>(this T obj, int depth = 0)
    {
        return obj?.GetType()?.PropNames(depth);
    }
    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames(this Type type, int depth = 0)
    {
        var props = type.GetProperties();
        if (depth == 0)
            return props.Select(item => item.Name);
        var nameProps = props.Where(item => item.PropertyType.IsBaseType());
        var loopProps = props.Where(item => !item.PropertyType.IsBaseType());
        return depth > 0
            ? loopProps.Select(item => item.PropertyType.PropNames(depth - 1).Select(propName => $@"{item.Name}.{propName}")).Merge(nameProps.Select(item => item.Name))
            : nameProps.Select(item => item.Name).Concat(loopProps.Select(item => item.Name));
    }

    /// <summary>
    /// 设置值
    /// </summary>
    public static void SetValue<T>(this T? obj, string field, object? value) where T : class
    {
        // 判断是否匿名类型, 可能会对性能有影响, 暂时不确定是否应该开启
        // var objType = obj.GetType();
        // if (objType.Name.Contains("AnonymousType"))
        //     obj.SetAnonymousValue(field, value);
        var fieldName = field.FirstTo(".");
        var prop = obj?.GetType().GetProperty(fieldName);
        if (fieldName.Length == field.Length)
            prop?.SetValue(obj, value);
        else if (prop != null)
        {
            var propValue = obj.GetValue(fieldName);
            if (propValue == null)
            {
                propValue = Activator.CreateInstance(prop.PropertyType);
                obj.SetValue(fieldName, propValue);
            }
            propValue.SetValue(field.Last(field.Length - (fieldName.Length + 1)), value);
        }
    }

    /// <summary>
    /// 设置匿名对象的值
    /// </summary>
    public static void SetAnonymousValue(this object obj, string field, object value)
    {
        var fieldName = field.FirstTo(".");
        var runtimeField = obj.GetType().GetRuntimeFields().FirstOrDefault(item => item.Name == $"<{fieldName}>i__Field");
        if (fieldName == field)
            runtimeField?.SetValue(obj, value);
        else if (runtimeField != null)
            obj.GetValue(fieldName)?.SetAnonymousValue(field.Last(field.Length - (fieldName.Length + 1)), value);
    }

    /// <summary>
    /// 获取类型的属性
    /// </summary>
    public static IEnumerable<PropertyInfo> Props(this Type type)
    {
        return type.GetProperties();
    }
    /// <summary>
    /// 获取类型的属性
    /// </summary>
    public static IEnumerable<PropertyInfo> Props<T>(this T obj)
    {
        return obj?.GetType().GetProperties() ?? Enumerable.Empty<PropertyInfo>();
    }

    /// <summary>
    /// 就...GetValue
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="field">属性/字段</param>
    public static object? GetValue<T>(this T obj, string field)
    {
        var fieldName = field.FirstTo(".");
        var prop = obj?.GetType().GetProperty(fieldName);
        if (fieldName.Length == field.Length && prop != null)
            return prop?.GetValue(obj);
        else if (prop != null)
        {
            var propValue = prop?.GetValue(obj);
            return propValue?.GetValue(field.Last(field.Length - (fieldName.Length + 1)));
        }
        return null;
    }
    /// <summary>
    /// 比较两个对象
    /// </summary>
    public static Difference<T> Difference<T>(this T before, T end)
    {
        return new Difference<T>(before, end);
    }

    /// <summary>
    /// 判断是否有指定接口约束
    /// </summary>
    public static bool HasInterface(this Type type, Type interfaceType)
    {
        if (!interfaceType.IsInterface)
            return false;
        return interfaceType.IsAssignableFrom(type);
    }
    /// <summary>
    /// 判断是否有指定接口约束
    /// </summary>
    public static bool HasInterface<T>(this Type type)
    {
        return type.HasInterface(typeof(T));
    }
    /// <summary>
    /// 判断是否有指定接口约束
    /// </summary>
    public static bool IsType(this Type type, Type baseType)
    {
        return baseType.IsAssignableFrom(type);
    }
    /// <summary>
    /// 判断是否有指定接口约束
    /// </summary>
    public static bool IsType<T>(this Type type)
    {
        return type.IsType(typeof(T));
    }

    /// <summary>
    /// 判断是否有泛型接口约束
    /// </summary>
    public static bool HasGenericInterface(this Type type, Type interfaceType)
    {
        return type.GetTypeInfo().ImplementedInterfaces.Any(item => (item.IsGenericType ? item.GetGenericTypeDefinition() : item) == interfaceType);
    }
    /// <summary>
    /// 方法是否包含参数类型
    /// </summary>
    public static bool HasParameter(this MethodInfo method, Type type)
    {
        var pas = method.GetParameters();
        if (pas.Any(item => item.ParameterType == type))
            return true;
        if (type.IsInterface)
            return pas.Any(item => item.ParameterType.HasInterface(type));
        else return pas.Any(item => item.ParameterType.IsSubclassOf(type));
    }
    /// <summary>
    /// 方法是否包含参数类型
    /// </summary>
    public static bool HasParameter<T>(this MethodInfo method)
    {
        return method.HasParameter(typeof(T));
    }

    /// <summary>
    /// 方法是否包含泛型参数类型
    /// </summary>
    public static bool HasGenericParamter(this MethodInfo method, Type type)
    {
        if (!type.IsGenericType)
            return false;
        var pas = method.GetParameters().Where(item => item.ParameterType.IsGenericType);
        if (type.IsInterface)
            if (pas.Any(item => item.ParameterType.GetGenericTypeDefinition() == type))
                return true;
            else
                return pas.Any(item => item.ParameterType.GetGenericTypeDefinition().HasGenericInterface(type));
        return pas.Any(item => item.ParameterType.GetGenericTypeDefinition() == type);
    }

    /// <summary>
    /// 类型中是否包含该名称的方法
    /// </summary>
    public static bool HasMethod(this Type type, string methodName)
    {
        var methods = type.GetMethods();
        return methods.Any(item => item.Name == methodName);
    }
}
