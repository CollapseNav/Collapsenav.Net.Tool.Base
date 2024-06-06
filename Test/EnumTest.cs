using Xunit;

namespace Collapsenav.Net.Tool.Test;

public class EnumTest
{
    [Fact]
    public void ToEnumTest()
    {
        Assert.Equal(TestEnum.Test, "测试".ToEnum<TestEnum>());
        Assert.Equal(TestEnum.Test2, "测试2".ToEnum<TestEnum>());
        Assert.Null("Test3".ToEnum<TestEnum>());
    }

    [Fact]
    public void ToDescriptionTest()
    {
        TestEnum testEnum = TestEnum.Test;
        Assert.Equal("测试", testEnum.Description());
        testEnum = TestEnum.Test2;
        Assert.Equal("测试2", testEnum.Description());
        testEnum = TestEnum.Test3;
        Assert.Equal("Test3", testEnum.Description());
    }
}