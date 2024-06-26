using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class DictionaryTest
{
    [Fact]
    public void AddOrUpdateTest()
    {
        Dictionary<int, string> dict = new();
        dict.AddOrUpdate(new KeyValuePair<int, string>(1, "1"))
        .AddOrUpdate(new KeyValuePair<int, string>(1, "1"))
        .AddOrUpdate(1, "1");
        Assert.True(dict.Count == 1);
    }


    [Fact]
    public void AddRangeTest()
    {
        Dictionary<int, string> dict = new();
        Dictionary<int, string> dict2 = new()
        {
            { 1, "1" },
            { 2, "2" },
            { 3, "3" },
        };
        dict.AddRange(dict2);
        Assert.True(dict.Count == 3);
        List<KeyValuePair<int, string>> value = new()
        {
            new(1, "1"),
            new(2, "2"),
            new(3, "3"),
            new(4, "4"),
            new(5, "5"),
        };
        dict.AddRange(value);
        Assert.True(dict.Count == 5);
    }

    [Fact]
    public void ToDictionaryTest()
    {
        List<KeyValuePair<int, string>> value = new()
        {
            new(1, "1"),
            new(2, "2"),
            new(3, "3"),
        };
        IDictionary<int, string> dict = value.ToDictionary();
        Assert.True(dict[1] == "1");
        Assert.True(dict[3] == "3");

        string[] nums = new[] { "1", "2", "3", "4", "5" };
        dict = nums.ToDictionary(item => int.Parse(item));
        Assert.True(dict[5] == "5");

        value = new List<KeyValuePair<int, string>>();
        Assert.Empty(value.ToDictionary());
        try
        {
            Func<string, int> func = null;
            nums.ToDictionary(func);
        }
        catch (Exception ex)
        {
            Assert.True(ex is NullReferenceException);
            Assert.Equal("keySelector", ex.Message);
        }
        nums = null;
        Assert.Empty(nums.ToDictionary(int.Parse));

    }

    [Fact]
    public void GetAndRemoveTest()
    {
        var dict = new Dictionary<int, string>()
        .AddOrUpdate(1, "1")
        .AddOrUpdate(2, "2")
        .AddOrUpdate(3, "4")
        ;

        var value = dict.GetAndRemove(1);
        Assert.True(value == "1");
        Assert.True(dict.Count == 2);

        dict = null;

        value = dict.GetAndRemove(2);
        Assert.True(value == null);
    }
    [Fact]
    public void PopTest()
    {
        var dict = new Dictionary<int, string>()
        .AddOrUpdate(1, "1")
        .AddOrUpdate(2, "2")
        .AddOrUpdate(3, "4")
        ;

        var value = dict.Pop(1);
        Assert.True(value == "1");
        Assert.True(dict.Count == 2);
        dict = null;

        value = dict.Pop(2);
        Assert.True(value == null);
    }

    [Fact]
    public void DeconstructTest()
    {
        var dict = new Dictionary<int, string>()
        .AddOrUpdate(1, "1")
        .AddOrUpdate(2, "2")
        .AddOrUpdate(3, "4")
        ;
        var values = dict.Deconstruct(i => i + 1000, i => i + i);
        Assert.True(values.Count() == 3);
        Assert.True(values.ElementAt(0).Value == "11");
        Assert.True(values.ElementAt(0).Key == 1001);
        dict = null;
        Assert.Empty(dict.Deconstruct(i => i + 1000, i => i + i));

    }

    [Fact]
    public void DictToObject()
    {
        Dictionary<string, object> dict1 = new Dictionary<string, object>{
            {"username","name"},
            {"age",3},
        };
        var userinfo = dict1.ToObj<UserInfo>();
        Assert.Equal("name", userinfo.UserName);
        Assert.Equal(3, userinfo.Age);


        Dictionary<string, string> dict2 = new Dictionary<string, string>{
            {"username","name"},
            {"age","3"},
        };
        userinfo = dict2.ToObj<UserInfo>();
        Assert.Equal("name", userinfo.UserName);
        Assert.Equal(3, userinfo.Age);
    }

}

