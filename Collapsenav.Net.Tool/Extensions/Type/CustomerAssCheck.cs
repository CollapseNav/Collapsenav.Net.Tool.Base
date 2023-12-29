using System.Reflection;

namespace Collapsenav.Net.Tool;

public static partial class CustomerAssCheck
{
    /// <summary>
    /// 简单规定一下"官方"程序集的前缀
    /// </summary>
    private static string[] MSAssPrefix = new[] {
        "Microsoft",
        "System",
    };
    /// <summary>
    /// 判断是否自定义程序集(通过publickeytoken是否为空判断)
    /// </summary>
    /// <param name="ass"></param>
    public static bool IsCustomerAssembly(this AssemblyName ass)
    {
        return !ass.FullName.HasStartsWith(MSAssPrefix);
    }
    /// <summary>
    /// 判断是否自定义程序集(通过publickeytoken是否为空判断)
    /// </summary>
    /// <param name="ass"></param>
    public static bool IsCustomerAssembly(this Assembly ass)
    {
        return ass.GetName().IsCustomerAssembly() && !ass.IsDynamic;
    }
}