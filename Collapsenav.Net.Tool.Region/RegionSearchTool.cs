namespace Collapsenav.Net.Tool.Region;
public static partial class Region
{
    public static IEnumerable<RegionTreeNode> GetRegionsByName(string name, int take = 10)
    {
        return Tree?.GetRegionsByName(name, take) ?? Enumerable.Empty<RegionTreeNode>();
    }
    public static IEnumerable<RegionTreeNode> GetRegionsByName(this RegionTreeNode region, string name, int take = 10)
    {
        if (string.IsNullOrEmpty(name))
            return Tree?.Child?.Take(take) ?? Enumerable.Empty<RegionTreeNode>();

        // if (name.Length > 1)
        // {
        // }
        // else
        // {
        //     return Tree?.Child?.Where(item => item.Name != null && item.Name.Contains(name)).Take(take) ?? Enumerable.Empty<RegionTreeNode>();
        // }
        return Enumerable.Empty<RegionTreeNode>();
    }
    public static RegionTreeNode? GetRegion(string name)
    {
        if (string.IsNullOrEmpty(name))
            return null;
        return null;
    }
    internal static RegionTreeNode? GetProvinceByName(string name, RefInt? re = null)
    {
        int len = name.Length > 1 ? 2 : 1;
        if (string.IsNullOrEmpty(name))
            return null;
        var simpleName = name.Substring(0, len);
        var region = AllTreeProvinces?.FirstOrDefault(item => item.Name != null && item.Name.Contains(simpleName));
        if (region != null)
        {
            return region;
        }
        simpleName = name.Substring(0, 1);
        region = AllTreeProvinces?.FirstOrDefault(item => item.Name != null && item.Name.Contains(simpleName));
        re?.SetValue(1);
        return region;
    }

    internal class RefInt
    {
        public int Value { get; set; }
        public void SetValue(int value)
        {
            Value = value;
        }
    }
}
