using System.Text.Json;

namespace Collapsenav.Net.Tool.Region;

internal static class LoadRegionNodeTool
{
    internal const string TreeNodeName = "region-tree.json";
    /// <summary>
    /// 加载树节点
    /// </summary>
    internal static RegionTreeNode? LoadTreeNode()
    {
        var str = File.ReadAllText($"{AppContext.BaseDirectory}/{TreeNodeName}");
        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        var node = JsonSerializer.Deserialize<RegionTreeNode>(str, options);
        if (node == null)
            return null;
        InitParentNode(node);
        return node;
    }
    /// <summary>
    /// 加载父节点
    /// </summary>
    internal static void InitParentNode(RegionTreeNode node)
    {
        if (node.Child == null)
            return;
        foreach (var n in node.Child)
        {
            n.Parent = node;
            InitParentNode(n);
        }
    }
}