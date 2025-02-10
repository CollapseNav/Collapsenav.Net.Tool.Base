using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class DateTimeTest
{
    [Fact]
    public void TimeStampTest()
    {
        DateTime now = DateTime.Now.ToUniversalTime();
        Assert.True(now.ToString() == now.ToTimestamp().ToDateTime().ToString());
        now = now.AddMilliseconds(-now.Millisecond);
        Assert.True(now.ToString() == now.ToShortTimestamp().ToShortDateTime().ToString());
        now = DateTime.Now;
        var temp = (-1L).ToDateTime();
        Assert.Equal(now.Year, temp.Year);
        Assert.Equal(now.Month, temp.Month);
        Assert.Equal(now.Day, temp.Day);
    }

    [Fact]
    public void DefaultTimeStringTest()
    {
        DateTime date = new(2021, 11, 11, 11, 11, 11);
        Assert.True(date.ToDefaultTimeString() == "2021-11-11 11:11:11");
        Assert.True(date.ToDefaultTimeString("WTF") == "2021WTF11WTF11 11:11:11");
        Assert.True(date.ToDefaultTimeString("WTF", "FFF") == "2021WTF11WTF11 11FFF11FFF11");
    }

    [Fact]
    public void DefaultDateStringTest()
    {
        DateTime date = new(2021, 11, 11, 11, 11, 11);
        Assert.True(date.ToDefaultDateString() == "2021-11-11");
        Assert.True(date.ToDefaultDateString("WTF") == "2021WTF11WTF11");
    }

    [Fact]
    public void DefaultMilliStringTest()
    {
        DateTime date = new(2021, 11, 11, 11, 11, 11, 111);
        Assert.True(date.ToDefaultMilliString() == "2021-11-11 11:11:11.111");
        Assert.True(date.ToDefaultMilliString("WTF") == "2021WTF11WTF11 11:11:11.111");
        Assert.True(date.ToDefaultMilliString("WTF", "FFF") == "2021WTF11WTF11 11FFF11FFF11.111");
        Assert.True(date.ToDefaultMilliString("WTF", "FFF", "MMM") == "2021WTF11WTF11 11FFF11FFF11MMM111");
    }
    [Theory]
    [InlineData("2025-01-01", "2025-01-31", DateTimeType.Year, 1)]
    [InlineData("2025-01-01", "2026-01-31", DateTimeType.Year, 2)]
    [InlineData("2025-01-01", "2026-11-30", DateTimeType.Year, 2)]
    [InlineData("2025-01-01", "2025-01-31", DateTimeType.Month, 1)]
    [InlineData("2025-01-01", "2025-02-27", DateTimeType.Month, 2)]
    [InlineData("2025-01-01", "2025-02-01", DateTimeType.Month, 2)]
    [InlineData("2025-01-01", "2025-01-31", DateTimeType.Day, 31)]
    [InlineData("2025-01-01", "2025-02-01", DateTimeType.Day, 32)]
    [InlineData("2025-01-01", "2025-02-27", DateTimeType.Day, 58)]
    [InlineData("2025-01-01", "2025-01-01 23:00:00", DateTimeType.Hour, 24)]
    [InlineData("2025-01-01", "2025-01-02", DateTimeType.Hour, 25)]
    [InlineData("2025-01-01", "2025-01-01 00:10:00", DateTimeType.Minute, 11)]
    [InlineData("2025-01-01", "2025-01-01 00:00:00", DateTimeType.Minute, 1)]
    [InlineData("2025-01-01", "2025-01-01 00:09:59", DateTimeType.Minute, 10)]
    [InlineData("2025-01-01", "2025-01-01 00:09:59", DateTimeType.Second, 600)]
    [InlineData("2025-01-01", "2025-01-01 00:00:00", DateTimeType.Second, 1)]
    [InlineData("2025-01-01", "2025-01-01 00:10:00", DateTimeType.Second, 601)]
    public void CreateDates(string startStr, string endStr, DateTimeType type, int count)
    {
        var start = DateTime.Parse(startStr);
        var end = DateTime.Parse(endStr);
        var dates = (start, end).CreateDates(type);
        Assert.Equal(count, dates.Count());
    }
}
