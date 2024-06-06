using Xunit;

namespace Collapsenav.Net.Tool.Test;

public class EnumTest
{
    [Theory]
    [InlineData("测试", TestEnum.Test)]
    [InlineData("测试2", TestEnum.Test2)]
    [InlineData("Test3", null)]
    public void ToEnumTest(string str, TestEnum? testEnum)
    {
        Assert.Equal(testEnum, str.ToEnum<TestEnum>());
        Assert.Equal(testEnum, str.ToEnum(typeof(TestEnum)));
    }

    [Theory]
    [InlineData("测试", TestEnum.Test)]
    [InlineData("测试2", TestEnum.Test2)]
    [InlineData("Test3", TestEnum.Test3)]
    public void ToDescriptionTest(string str, TestEnum? testEnum)
    {
        Assert.Equal(str, testEnum?.Description());
    }
}