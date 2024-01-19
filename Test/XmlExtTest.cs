using Xunit;

namespace Collapsenav.Net.Tool.Test;

public class XmlExtTest
{
    [Fact]
    public void GetXmlDocumentTest()
    {
        var docs = XmlExt.GetXmlDocuments();
        Assert.True(docs.Count() == 2);
        var nodes = docs.GetSummaryNodes();
        var node = nodes.FirstOrDefault(item => item.Summary == "xml对应的注释节点");
        Assert.NotNull(node);
        Assert.True(node.NodeType == XmlNodeMemberTypeEnum.Type);
    }

    [Fact]
    public void GetXmlNodeListTest()
    {
        var docs = XmlExt.GetXmlDocuments();
        var nodeLists = docs.GetNodeLists(XmlExt.SummaryNodePath);
        Assert.True(nodeLists.Count() == 2);
    }

    [Fact]
    public void XmlDocumentRestTest()
    {
        var docs = XmlExt.GetXmlDocuments();
        Assert.True(docs == XmlExt.GetXmlDocuments());

        Assert.False(docs == XmlExt.GetXmlDocuments(true));

        XmlExt.ClearCache();
        Assert.False(docs == XmlExt.GetXmlDocuments());
    }
}