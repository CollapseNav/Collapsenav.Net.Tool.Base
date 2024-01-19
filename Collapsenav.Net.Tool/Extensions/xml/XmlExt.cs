using System.Xml;

namespace Collapsenav.Net.Tool;

/// <summary>
/// xml节点的缓存
/// </summary>
/// <remarks>
/// 会在运行的时候加载一次，正常来说运行期间xml内容不会被修改
/// </remarks>
public class XmlNodeCache
{
    public XmlNodeCache(XmlDocument doc, string path)
    {
        Doc = doc;
        Path = path;
        NodeList = doc.SelectNodes(path);
        Nodes = NodeList?.GetNodes() ?? Enumerable.Empty<XmlNode>();
    }
    public XmlDocument Doc { get; private set; }
    public string Path { get; private set; }
    public IEnumerable<XmlNode>? Nodes { get; private set; }
    public XmlNodeList? NodeList { get; set; }
}

public static class XmlExt
{
    /// <summary>
    /// 第一次使用先生成对应缓存
    /// </summary>
    static XmlExt()
    {
        // 获取目录中所有 xmldoc
        docs = Enumerable.Empty<XmlDocument>();
        // 根据默认的xml注释生成规则先生成对应的summarynode
        nodeCaches = new();
        summaryDict = new();
    }
    public const string SummaryNodePath = "doc/members/member";
    /// <summary>
    /// 基本上不太可能会在运行的过程中改变, 所以写个静态存着
    /// </summary>
    private static IEnumerable<XmlDocument> Docs
    {
        get
        {
            if (docs.IsEmpty())
                docs = Directory.GetFiles(AppContext.BaseDirectory, "*.xml").Select(item => GetXmlDocument(item)).ToList();
            return docs;
        }
        set => docs = value;
    }
    private static Dictionary<XmlDocument, IEnumerable<SummaryNode>> summaryDict;
    private static List<XmlNodeCache> nodeCaches;
    private static IEnumerable<XmlDocument>? docs;
    private static Dictionary<XmlDocument, IEnumerable<SummaryNode>> SummaryDict
    {
        get
        {
            if (summaryDict.IsEmpty())
                summaryDict = NodeCaches.ToDictionary(item => item.Doc, item => item.Nodes!.Select(node => new SummaryNode(node)));
            return summaryDict;
        }
        set => summaryDict = value;
    }
    public static List<XmlNodeCache> NodeCaches
    {
        get
        {
            if (Docs.NotEmpty() && nodeCaches.IsEmpty())
                Docs.ForEach(doc => nodeCaches.Add(new XmlNodeCache(doc, SummaryNodePath)));
            return nodeCaches;
        }
        private set => nodeCaches = value;
    }
    private static void AddDocToCache(XmlDocument? doc, string path)
    {
        if (Docs == null || doc == null)
            return;
        if (!doc.In(Docs))
            Docs = Docs.Append(doc);
        if (NodeCaches.Any(item => item.Doc == doc && item.Path == path))
            return;
        var cacheNode = new XmlNodeCache(doc, path);
        NodeCaches.Add(cacheNode);
        if (path == SummaryNodePath)
            SummaryDict.Add(doc, cacheNode.Nodes?.Select(node => new SummaryNode(node)) ?? Enumerable.Empty<SummaryNode>());
    }
    private static void AddDocsToCache(IEnumerable<XmlDocument> docs, string path)
    {
        var notExist = docs.Except(Docs);
        if (notExist.NotEmpty())
            notExist.ForEach(item => AddDocToCache(item, path));
    }
    /// <summary>
    /// 自动搜集目录下的xml文件并转为 xmldoc
    /// </summary>
    /// <param name="reset">如果确实需要重新获取xmldoc</param>
    /// <remarks>在不设置reset的情况下会使用第一次执行时获取的缓存</remarks>
    public static IEnumerable<XmlDocument> GetXmlDocuments(bool reset = false)
    {
        if (reset)
            Docs = Directory.GetFiles(AppContext.BaseDirectory, "*.xml").Select(item => GetXmlDocument(item)).ToList();
        return Docs ?? Enumerable.Empty<XmlDocument>();
    }
    /// <summary>
    /// 从指定路径读取xml文件并转为 xmldoc
    /// </summary>
    public static XmlDocument GetXmlDocument(string path)
    {
        XmlDocument doc = new();
        doc.Load(path);
        return doc;
    }
    /// <summary>
    /// 获取nodelist
    /// </summary>
    public static IEnumerable<XmlNodeList> GetNodeLists(this IEnumerable<XmlDocument> docs, string path)
    {
        AddDocsToCache(docs, path);
        return NodeCaches.Where(item => item.Doc.In(docs)).Select(item => item.NodeList!) ?? Enumerable.Empty<XmlNodeList>();
    }
    /// <summary>
    /// 获取注释node
    /// </summary>
    public static IEnumerable<SummaryNode> GetSummaryNodes(this IEnumerable<XmlDocument> docs)
    {
        AddDocsToCache(docs, SummaryNodePath);
        return SummaryDict.Where(item => item.Key.In(docs)).SelectMany(item => item.Value);
    }
    /// <summary>
    /// 获取xmlnode
    /// </summary>
    public static IEnumerable<XmlNode> GetNodes(this XmlNodeList nodeList)
    {
        List<XmlNode> nodes = new();
        foreach (var node in nodeList)
            if (node is XmlNode xmlNode)
                nodes.Add(xmlNode);
        return nodes;
    }
    /// <summary>
    /// 清空xml缓存
    /// </summary>
    public static void ClearCache()
    {
        Docs = Enumerable.Empty<XmlDocument>();
        summaryDict.Clear();
        NodeCaches.Clear();
    }
}