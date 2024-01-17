using System.Reflection;

namespace Collapsenav.Net.Tool;

public static partial class AssemblyExt
{
    /// <summary>
    /// 获取所有 assembly
    /// </summary>
    public static IEnumerable<Assembly> GetAllAssemblies(this Assembly ass, bool withSystem = true)
    {
        var asses = ass.GetReferencedAssemblies().Select(
            item =>
            {
                try
                {
                    return Assembly.Load(item);
                }
                catch
                {
                    return null;
                }
            }).Where(item => item != null && (withSystem || item.IsCustomerAssembly()))
            .Select(i => i!).Concat(new[] { ass });

        return asses;
    }
    /// <summary>
    /// 获取所有自定义 assembly(通过publickeytoken是否为空判断)
    /// </summary>
    public static IEnumerable<Assembly> GetCustomerAssemblies(this Assembly ass)
    {
        return ass.GetAllAssemblies(false);
    }

    /// <summary>
    /// 获取所有assembly
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="withSystem"></param>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetAllAssemblies(this AppDomain domain, bool withSystem = false)
    {
        List<Assembly> asses = new(domain.GetAssemblies());
        var path = domain.BaseDirectory;
        var dllPaths = Directory.GetFiles(path, "*.dll");
        if (dllPaths.NotEmpty())
        {
            asses = asses.Concat(dllPaths.Select(item =>
            {
                try
                {
                    return Assembly.LoadFile(item);
                }
                catch
                {
                    return null;
                }
            }).Where(item => item != null && (withSystem || item.IsCustomerAssembly()))).Distinct(i => i?.FullName).ToList()!;
        }
        return asses.SelectMany(item => item.GetAllAssemblies()).Distinct(i => i?.FullName);
    }

    /// <summary>
    /// 获取自定义的程序集(通过publickeytoken是否为空判断)
    /// </summary>
    public static IEnumerable<Assembly> GetCustomerAssemblies(this AppDomain domain)
    {
        return domain.GetAllAssemblies(false).Distinct(i => i?.FullName);
    }
    /// <summary>
    /// 获取所有接口
    /// </summary>
    public static IEnumerable<Type> GetInterfaces(this Assembly ass)
    {
        return ass.GetTypes().Where(item => item.IsInterface);
    }
    /// <summary>
    /// 获取所有 abstract class
    /// </summary>
    public static IEnumerable<Type> GetAbstracts(this Assembly ass)
    {
        return ass.GetTypes().Where(item => item.IsAbstract);
    }
    /// <summary>
    /// 获取所有枚举
    /// </summary>
    public static IEnumerable<Type> GetEnums(this Assembly ass)
    {
        return ass.GetTypes().Where(item => item.IsEnum);
    }
    public static IEnumerable<Type> GetTypes<T>(this Assembly ass)
    {
        return ass.GetAllAssemblies().SelectMany(item => item.GetTypes()).Where(item => item.IsType<T>());
    }
    /// <summary>
    /// 根据前缀查找type
    /// </summary>
    /// <param name="ass"></param>
    /// <param name="prefixs"></param>
    public static IEnumerable<Type> GetByPrefix(this Assembly ass, params string[] prefixs)
    {
        return ass.GetTypes().Where(item => item.Name.HasStartsWith(prefixs));
    }
    /// <summary>
    /// 根据后缀查找type
    /// </summary>
    /// <param name="ass"></param>
    /// <param name="suffixs"></param>
    public static IEnumerable<Type> GetBySuffix(this Assembly ass, params string[] suffixs)
    {
        return ass.GetTypes().Where(item =>
        {
            if (item.FullName == null)
                return false;
            return (item.IsGenericType ? item.FullName[..item.FullName.IndexOf('`')] : item.FullName).HasEndsWith(suffixs);
        });
    }
    /// <summary>
    /// 根据前后缀查找type
    /// </summary>
    /// <param name="ass"></param>
    /// <param name="prefixAndSuffixs"></param>
    public static IEnumerable<Type> GetByPrefixAndSuffix(this Assembly ass, params string[] prefixAndSuffixs)
    {
        return ass.GetTypes().Where(item =>
        {
            if (item.FullName == null)
                return false;
            return item.Name.HasStartsWith(prefixAndSuffixs) || (item.IsGenericType ? item.FullName[..item.FullName.IndexOf('`')] : item.Name).HasEndsWith(prefixAndSuffixs);
        });
    }
    /// <summary>
    /// 获取appdomain下所有type
    /// </summary>
    public static IEnumerable<Type> GetTypes(this AppDomain domain, bool withSystem = true)
    {
        return domain.GetAllAssemblies(withSystem).SelectMany(item => item.GetTypes());
    }    /// <summary>
         /// 获取appdomain下所有自定义程序集的type
         /// </summary>
    public static IEnumerable<Type> GetCustomerTypes(this AppDomain domain)
    {
        return domain.GetCustomerAssemblies().SelectMany(item => item.GetTypes());
    }
    /// <summary>
    /// 获取appdomain下所有接口
    /// </summary>
    public static IEnumerable<Type> GetInterfaces(this AppDomain domain, bool withSystem = true)
    {
        return domain.GetAllAssemblies(withSystem).SelectMany(item => item.GetInterfaces());
    }    /// <summary>
         /// 获取appdomain下所有自定义程序集的接口
         /// </summary>
    public static IEnumerable<Type> GetCustomerInterfaces(this AppDomain domain)
    {
        return domain.GetCustomerAssemblies().SelectMany(item => item.GetInterfaces());
    }
    public static IEnumerable<Type> GetTypes<T>(this AppDomain domain, bool withSystem = true)
    {
        return domain.GetAllAssemblies(withSystem).SelectMany(item => item.GetTypes()).Where(item => item.IsType<T>());
    }
    public static IEnumerable<Type> GetCustomerTypes<T>(this AppDomain domain)
    {
        return domain.GetTypes<T>(false);
    }
    /// <summary>
    /// 根据前缀查找type
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="prefixs"></param>
    public static IEnumerable<Type> GetCustomerTypesByPrefix(this AppDomain domain, params string[] prefixs)
    {
        return domain.GetTypes(false).Where(item => item.Name.HasStartsWith(prefixs));
    }
    /// <summary>
    /// 根据后缀查找type
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="suffixs"></param>
    public static IEnumerable<Type> GetCustomerTypesBySuffix(this AppDomain domain, params string[] suffixs)
    {
        return domain.GetTypes(false).Where(item =>
        {
            if (item.FullName == null)
                return false;
            return (item.IsGenericType ? item.FullName[..item.FullName.IndexOf('`')] : item.Name).HasEndsWith(suffixs);
        });
    }
    /// <summary>
    /// 根据前后缀查找type
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="prefixAndSuffixs"></param>
    public static IEnumerable<Type> GetCustomerTypesByPrefixAndSuffix(this AppDomain domain, params string[] prefixAndSuffixs)
    {
        return domain.GetTypes(false).Where(item =>
        {
            if (item.FullName == null)
                return false;
            return item.Name.HasStartsWith(prefixAndSuffixs) || (item.IsGenericType ? item.FullName[..item.FullName.IndexOf('`')] : item.Name).HasEndsWith(prefixAndSuffixs);
        });
    }
}