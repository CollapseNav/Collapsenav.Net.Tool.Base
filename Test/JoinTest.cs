
using Collapsenav.Net.Tool;
using Xunit;

public class JoinTest
{
    [Fact]
    public void JoinTest2()
    {
        var data1 = Model1.GetDatas();
        var data2 = Model2.GetDatas();
        var result = data1.CreateJoin()
        .LeftJoin(data2, x => x.Value1, y => y.Value1)
        .SelectValue((a, b) => new
        {
            a.Value1,
            b.Value2
        }).ToList();
        Assert.Single(result);
        Assert.Equal("Value1", result[0].Value1);
        Assert.Equal("Value2", result[0].Value2);
    }

    [Fact]
    public void JoinTest3()
    {
        var data1 = Model1.GetDatas();
        var data2 = Model2.GetDatas();
        var data3 = Model3.GetDatas();
        var result = data1.CreateJoin()
        .LeftJoin(data2, x => x.Value1, y => y.Value1)
        .LeftJoin(data3, x => x.Data1.Value1, y => y.Value1)
        .SelectValue((a, b, c) => new
        {
            a.Value1,
            b.Value2,
            c.Value3
        }).ToList();
        Assert.Single(result);
        Assert.Equal("Value1", result[0].Value1);
        Assert.Equal("Value2", result[0].Value2);
        Assert.Equal("Value3", result[0].Value3);
    }

    [Fact]
    public void JoinTest4()
    {
        var data1 = Model1.GetDatas();
        var data2 = Model2.GetDatas();
        var data3 = Model3.GetDatas();
        var data4 = Model4.GetDatas();
        var result = data1.CreateJoin()
        .LeftJoin(data2, x => x.Value1, y => y.Value1)
        .LeftJoin(data3, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data4, x => x.Data1.Value1, y => y.Value1)
        .SelectValue((a, b, c, d) => new
        {
            a.Value1,
            b.Value2,
            c.Value3,
            d.Value4
        }).ToList();
        Assert.Single(result);
        Assert.Equal("Value1", result[0].Value1);
        Assert.Equal("Value2", result[0].Value2);
        Assert.Equal("Value3", result[0].Value3);
        Assert.Equal("Value4", result[0].Value4);
    }

    [Fact]
    public void JoinTest5()
    {
        var data1 = Model1.GetDatas();
        var data2 = Model2.GetDatas();
        var data3 = Model3.GetDatas();
        var data4 = Model4.GetDatas();
        var data5 = Model5.GetDatas();
        var result = data1.CreateJoin()
        .LeftJoin(data2, x => x.Value1, y => y.Value1)
        .LeftJoin(data3, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data4, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data5, x => x.Data1.Value1, y => y.Value1)
        .SelectValue((a, b, c, d, e) => new
        {
            a.Value1,
            b.Value2,
            c.Value3,
            d.Value4,
            e.Value5
        }).ToList();
        Assert.Single(result);
        Assert.Equal("Value1", result[0].Value1);
        Assert.Equal("Value2", result[0].Value2);
        Assert.Equal("Value3", result[0].Value3);
        Assert.Equal("Value4", result[0].Value4);
        Assert.Equal("Value5", result[0].Value5);
    }

    [Fact]
    public void JoinTest6()
    {
        var data1 = Model1.GetDatas();
        var data2 = Model2.GetDatas();
        var data3 = Model3.GetDatas();
        var data4 = Model4.GetDatas();
        var data5 = Model5.GetDatas();
        var data6 = Model6.GetDatas();
        var result = data1.CreateJoin()
        .LeftJoin(data2, x => x.Value1, y => y.Value1)
        .LeftJoin(data3, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data4, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data5, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data6, x => x.Data1.Value1, y => y.Value1)
        .SelectValue((a, b, c, d, e, f) => new
        {
            a.Value1,
            b.Value2,
            c.Value3,
            d.Value4,
            e.Value5,
            f.Value6
        }).ToList();
        Assert.Single(result);
        Assert.Equal("Value1", result[0].Value1);
        Assert.Equal("Value2", result[0].Value2);
        Assert.Equal("Value3", result[0].Value3);
        Assert.Equal("Value4", result[0].Value4);
        Assert.Equal("Value5", result[0].Value5);
        Assert.Equal("Value6", result[0].Value6);
    }

    [Fact]
    public void JoinTest7()
    {
        var data1 = Model1.GetDatas();
        var data2 = Model2.GetDatas();
        var data3 = Model3.GetDatas();
        var data4 = Model4.GetDatas();
        var data5 = Model5.GetDatas();
        var data6 = Model6.GetDatas();
        var data7 = Model7.GetDatas();
        var result = data1.CreateJoin()
        .LeftJoin(data2, x => x.Value1, y => y.Value1)
        .LeftJoin(data3, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data4, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data5, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data6, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data7, x => x.Data1.Value1, y => y.Value1)
        .SelectValue((a, b, c, d, e, f, g) => new
        {
            a.Value1,
            b.Value2,
            c.Value3,
            d.Value4,
            e.Value5,
            f.Value6,
            g.Value7
        }).ToList();
        Assert.Single(result);
        Assert.Equal("Value1", result[0].Value1);
        Assert.Equal("Value2", result[0].Value2);
        Assert.Equal("Value3", result[0].Value3);
        Assert.Equal("Value4", result[0].Value4);
        Assert.Equal("Value5", result[0].Value5);
        Assert.Equal("Value6", result[0].Value6);
        Assert.Equal("Value7", result[0].Value7);
    }

    [Fact]
    public void JoinTest8()
    {
        var data1 = Model1.GetDatas();
        var data2 = Model2.GetDatas();
        var data3 = Model3.GetDatas();
        var data4 = Model4.GetDatas();
        var data5 = Model5.GetDatas();
        var data6 = Model6.GetDatas();
        var data7 = Model7.GetDatas();
        var data8 = Model8.GetDatas();
        var result = data1.CreateJoin()
        .LeftJoin(data2, x => x.Value1, y => y.Value1)
        .LeftJoin(data3, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data4, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data5, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data6, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data7, x => x.Data1.Value1, y => y.Value1)
        .LeftJoin(data8, x => x.Data1.Value1, y => y.Value1)
        .SelectValue((a, b, c, d, e, f, g, h) => new
        {
            a.Value1,
            b.Value2,
            c.Value3,
            d.Value4,
            e.Value5,
            f.Value6,
            g.Value7,
            h.Value8
        }).ToList();
        Assert.Single(result);
        Assert.Equal("Value1", result[0].Value1);
        Assert.Equal("Value2", result[0].Value2);
        Assert.Equal("Value3", result[0].Value3);
        Assert.Equal("Value4", result[0].Value4);
        Assert.Equal("Value5", result[0].Value5);
        Assert.Equal("Value6", result[0].Value6);
        Assert.Equal("Value7", result[0].Value7);
        Assert.Equal("Value8", result[0].Value8);
    }

}

public class Model1
{
    public string Value1 { get; set; }
    public static IEnumerable<Model1> GetDatas()
    {
        return new List<Model1> { new Model1 { Value1 = "Value1" } };
    }
}

public class Model2
{
    public string Value1 { get; set; }
    public string Value2 { get; set; }
    public static IEnumerable<Model2> GetDatas()
    {
        return new List<Model2> { new Model2 { Value1 = "Value1", Value2 = "Value2" } };
    }
}
public class Model3
{
    public string Value1 { get; set; }
    public string Value2 { get; set; }
    public string Value3 { get; set; }
    public static IEnumerable<Model3> GetDatas()
    {
        return new List<Model3> { new Model3 { Value1 = "Value1", Value2 = "Value2", Value3 = "Value3" } };
    }
}
public class Model4
{
    public string Value1 { get; set; }
    public string Value2 { get; set; }
    public string Value3 { get; set; }
    public string Value4 { get; set; }
    public static IEnumerable<Model4> GetDatas()
    {
        return new List<Model4> { new Model4 { Value1 = "Value1", Value2 = "Value2", Value3 = "Value3", Value4 = "Value4" } };
    }
}

public class Model5
{
    public string Value1 { get; set; }
    public string Value2 { get; set; }
    public string Value3 { get; set; }
    public string Value4 { get; set; }
    public string Value5 { get; set; }
    public static IEnumerable<Model5> GetDatas()
    {
        return new List<Model5> { new Model5 { Value1 = "Value1", Value2 = "Value2", Value3 = "Value3", Value4 = "Value4", Value5 = "Value5" } };
    }
}
public class Model6
{
    public string Value1 { get; set; }
    public string Value2 { get; set; }
    public string Value3 { get; set; }
    public string Value4 { get; set; }
    public string Value5 { get; set; }
    public string Value6 { get; set; }
    public static IEnumerable<Model6> GetDatas()
    {
        return new List<Model6> { new Model6 { Value1 = "Value1", Value2 = "Value2", Value3 = "Value3", Value4 = "Value4", Value5 = "Value5", Value6 = "Value6" } };
    }
}
public class Model7
{
    public string Value1 { get; set; }
    public string Value2 { get; set; }
    public string Value3 { get; set; }
    public string Value4 { get; set; }
    public string Value5 { get; set; }
    public string Value6 { get; set; }
    public string Value7 { get; set; }
    public static IEnumerable<Model7> GetDatas()
    {
        return new List<Model7> { new Model7 { Value1 = "Value1", Value2 = "Value2", Value3 = "Value3", Value4 = "Value4", Value5 = "Value5", Value6 = "Value6", Value7 = "Value7" } };
    }
}
public class Model8
{
    public string Value1 { get; set; }
    public string Value2 { get; set; }
    public string Value3 { get; set; }
    public string Value4 { get; set; }
    public string Value5 { get; set; }
    public string Value6 { get; set; }
    public string Value7 { get; set; }
    public string Value8 { get; set; }
    public static IEnumerable<Model8> GetDatas()
    {
        return new List<Model8> { new Model8 { Value1 = "Value1", Value2 = "Value2", Value3 = "Value3", Value4 = "Value4", Value5 = "Value5", Value6 = "Value6", Value7 = "Value7", Value8 = "Value8" } };
    }
}