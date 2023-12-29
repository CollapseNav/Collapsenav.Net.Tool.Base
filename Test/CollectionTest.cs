using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class UniqueTestModel
{
    public int Index { get; set; } = 1;

    public static IEnumerable<UniqueTestModel> GetEmptyModel(int count = 5)
    {
        IEnumerable<UniqueTestModel> result = Enumerable.Empty<UniqueTestModel>();
        while (count-- > 0)
            result = result.Append(new UniqueTestModel());
        return result;
    }

    public static IEnumerable<UniqueTestModel> GetModels(params int[] values)
    {
        IEnumerable<UniqueTestModel> result = Enumerable.Empty<UniqueTestModel>();
        foreach (var i in values)
            result = result.Append(new UniqueTestModel { Index = i });
        return result;
    }
}
public class CollectionTest
{
    [Fact]
    public void UniqueTest()
    {
        int[] intList = { 1, 1, 2, 2, 3, 3, 4 };
        int[] value = { 1, 2, 3, 4 };
        var uniqueIntList = intList.Unique(item => item);
        Assert.True(uniqueIntList.SequenceEqual(value));
        uniqueIntList = intList.Distinct(item => item);
        Assert.True(uniqueIntList.SequenceEqual(value));

        uniqueIntList = intList.Unique((x, y) => x == y);
        Assert.True(uniqueIntList.SequenceEqual(value));
        uniqueIntList = intList.Distinct((x, y) => x == y);
        Assert.True(uniqueIntList.SequenceEqual(value));

        uniqueIntList = intList.Unique();
        Assert.True(uniqueIntList.SequenceEqual(value));
        intList = null;
        Assert.Empty(intList.Unique(item => item));
        Assert.Empty(intList.Distinct(item => item));
        Assert.Empty(intList.Unique((x, y) => x == y));
        Assert.Empty(intList.Unique());

        var data = UniqueTestModel.GetEmptyModel(6);

        var uniqueData = data.Unique();
        Assert.True(uniqueData.Count() == 6);
        uniqueData = data.Unique((x, y) => x.Index == y.Index);
        Assert.True(uniqueData.Count() == 1);
        uniqueData = data.Unique(item => item.Index.GetHashCode());
        Assert.True(uniqueData.Count() == 1);
        uniqueData = data.Unique(item => item.Index);
        Assert.True(uniqueData.Count() == 1);
    }

    [Fact]
    public void EqualTest()
    {
        int[] intList = new[] { 1, 2, 3, 4 };
        int[] intListCopy = new[] { 1, 2, 3, 4 };
        Assert.True(intList.SequenceEqual(intListCopy));

        IEnumerable<UserInfo> userInfos = new List<UserInfo> {
                new UserInfo{UserName="23331",Age=231 },
                new UserInfo{UserName="23332",Age=232 },
                new UserInfo{UserName="23333",Age=233 },
                new UserInfo{UserName="23334",Age=234 },
            };

        IEnumerable<UserInfo> userInfosCopy = new List<UserInfo> {
                new UserInfo{UserName="23331",Age=231 },
                new UserInfo{UserName="23332",Age=232 },
                new UserInfo{UserName="23333",Age=233 },
                new UserInfo{UserName="23334",Age=234 },
            };
        Assert.False(userInfos.SequenceEqual(userInfosCopy));
        Assert.True(userInfos.SequenceEqual(userInfosCopy, (x, y) => x.UserName == y.UserName));
        Assert.True(userInfos.SequenceEqual(userInfosCopy, item => item.Age));
        Assert.True(userInfos.SequenceEqual(userInfosCopy, item => item.Age.GetHashCode()));

        IEnumerable<UserInfo> userInfosCopy2 = new List<UserInfo> {
                new UserInfo{UserName="23331",Age=23 },
                new UserInfo{UserName="23332",Age=23 },
                new UserInfo{UserName="23333",Age=23 },
                new UserInfo{UserName="23334",Age=23 },
            };
        Assert.True(userInfosCopy.SequenceEqual(userInfosCopy2, (x, y) => x.UserName == y.UserName));
        Assert.True(userInfos.SequenceEqual(userInfosCopy2, (x, y) => x.UserName == y.UserName));
        Assert.False(userInfos.SequenceEqual(userInfosCopy2, item => item.Age.GetHashCode()));
        Assert.False(userInfos.SequenceEqual(userInfosCopy2, item => item.Age));
        userInfos = null;
        userInfosCopy2 = null;
        Assert.False(userInfosCopy.SequenceEqual(userInfosCopy2, (x, y) => x.UserName == y.UserName));
        Assert.False(userInfos.SequenceEqual(userInfosCopy2, (x, y) => x.UserName == y.UserName));
        Assert.False(userInfos.SequenceEqual(userInfosCopy2, item => item.Age.GetHashCode()));
        Assert.False(userInfos.SequenceEqual(userInfosCopy2, item => item.Age));
    }

    [Fact]
    public void ContainAndTest()
    {
        string[] strList = { "1", "2", "3", "4", "5", "6" };
        Assert.True(strList.AllContain(new[] { "2", "6" }));
        Assert.True(strList.AllContain("2", "6"));
        Assert.False(strList.AllContain(new[] { "2", "8" }));
        Assert.True(strList.AllContain((x, y) => x == y, "2", "6"));
        Assert.True(strList.AllContain(x => x, "2", "6"));
        Assert.False(strList.AllContain((x, y) => x == y, "2", "8"));
        Assert.False(strList.AsEnumerable().AllContain(new[] { "2", "8" }, (x, y) => x == y));
        Assert.False(strList.AsEnumerable().AllContain(new[] { "2", "8" }, x => x));
        Assert.True(strList.AsEnumerable().AllContain(null, (x, y) => x == y));
        Assert.True(strList.AsEnumerable().AllContain(null, x => x));
        strList = null;
        Assert.False(strList.AllContain(new[] { "2", "6" }));
        Assert.False(strList.AllContain("2", "6"));
        Assert.False(strList.AllContain(new[] { "2", "8" }));
        Assert.False(strList.AllContain((x, y) => x == y, "2", "6"));
        Assert.False(strList.AllContain(x => x, "2", "6"));
        Assert.False(strList.AllContain((x, y) => x == y, "2", "8"));
        Assert.False(strList.AsEnumerable().AllContain(new[] { "2", "8" }, (x, y) => x == y));
    }

    [Fact]
    public void ContainOrTest()
    {
        string[] strList = { "1", "2", "3", "4", "5", "6" };
        Assert.True(strList.HasContain(new[] { "2", "6" }));
        Assert.True(strList.HasContain(new[] { "2", "8" }.AsEnumerable()));
        Assert.False(strList.HasContain(new[] { "7", "8" }));
        Assert.True(strList.HasContain((x, y) => x == y, "2", "6"));
        Assert.True(strList.HasContain(x => x, "2", "6"));
        Assert.True(strList.HasContain((x, y) => x == y, "2", "8"));
        Assert.False(strList.HasContain((x, y) => x == y, "7", "8"));
        Assert.False(strList.AsEnumerable().HasContain(new[] { "7", "8" }, (x, y) => x == y));
        Assert.False(strList.AsEnumerable().HasContain(new[] { "7", "8" }, x => x));
        Assert.True(strList.AsEnumerable().HasContain(null, (x, y) => x == y));
        Assert.True(strList.AsEnumerable().HasContain(null, x => x));
        strList = null;
        Assert.False(strList.HasContain(new[] { "2", "6" }));
        Assert.False(strList.HasContain((x, y) => x == y, "2", "6"));
        Assert.False(strList.HasContain(x => x, "2", "6"));
        Assert.False(strList.AsEnumerable().HasContain(new[] { "7", "8" }, (x, y) => x == y));
        Assert.False(strList.AsEnumerable().HasContain(new[] { "7", "8" }, x => x));
    }


    [Fact]
    public void IEnumerableWhereIfTest()
    {
        int[] intList = { 1, 1, 2, 2, 3, 3, 4 };
        intList = intList.WhereIf(true, item => item > 1)
        .WhereIf(false, item => item < 3)
        .WhereIf("", item => item != 2)
        .ToArray();
        Assert.True(intList.SequenceEqual(new[] { 2, 2, 3, 3, 4 }));
        Assert.False(new[] { 2, 2, 3, 3, 4 }.Except(intList).Any());
        Assert.True(intList.Length == 5);
        int? input = null;
        intList = intList.WhereIf(input, item => item > input).ToArray();
        Assert.True(intList.SequenceEqual(new[] { 2, 2, 3, 3, 4 }));
        Assert.False(new[] { 2, 2, 3, 3, 4 }.Except(intList).Any());
        Assert.True(intList.Length == 5);
        input = 2;
        intList = intList.WhereIf(input, item => item > input).ToArray();
        Assert.True(intList.SequenceEqual(new[] { 3, 3, 4 }));
        Assert.False(new[] { 3, 3, 4 }.Except(intList).Any());
        Assert.True(intList.Length == 3);

        intList = null;
        input = null;
        Assert.Empty(intList.WhereIf(true, item => item > 1)
        .WhereIf(false, item => item < 3)
        .WhereIf("", item => item != 2)
        .ToArray());
        Assert.Empty(intList.WhereIf(input, item => item > 1)
        .WhereIf(false, item => item < 3)
        .WhereIf("", item => item != 2)
        .ToArray());
    }

    [Fact]
    public void IQueryableWhereIfTest()
    {
        int[] intList = { 1, 1, 2, 2, 3, 3, 4 };
        intList = intList.AsQueryable()
        .WhereIf(true, item => item > 1)
        .WhereIf(false, item => item < 3)
        .WhereIf("", item => item != 2)
        .ToArray();
        Assert.True(intList.SequenceEqual(new[] { 2, 2, 3, 3, 4 }));
        Assert.False(new[] { 2, 2, 3, 3, 4 }.Except(intList).Any());
        Assert.True(intList.Length == 5);

        var query = intList.AsQueryable();
        query = null;
        Assert.Empty(query
        .WhereIf(true, item => item > 1)
        .WhereIf(false, item => item < 3)
        .WhereIf("", item => item != 2)
        .ToArray());
    }

    [Fact]
    public void MergeCollectionTest()
    {
        string[] strs = new[] { "CollapseNav", "Net", "Tool" };
        string uniqueString = "ColapseNvtT";
        var strMergeList = strs.Merge();
        Assert.True(strMergeList.Join("") == "CollapseNavNetTool");
        strMergeList = strs.Merge(true);
        Assert.True(strMergeList.Join("") == uniqueString);

        strMergeList = strs.Merge(strs);
        Assert.True(strMergeList.Join("") == "CollapseNavNetToolCollapseNavNetTool");

        strMergeList = strs.Merge(strs, true);
        Assert.True(strMergeList.Join("") == uniqueString);

        string str = "CollapseNavNetTool";
        strMergeList = strs.Merge(str);
        Assert.True(strMergeList.Join("") == "CollapseNavNetToolCollapseNavNetTool");

        strMergeList = strs.Merge(str, true);
        Assert.True(strMergeList.Join("") == uniqueString);

        IEnumerable<int[]> nums = new List<int[]>()
            {
                new[] {1,2,3},
                new[] {4,5,6},
                new[] {7,8,9},
                new[] {10},
            };
        int[] mergeInts = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var numMergeList = nums.Merge();
        Assert.True(numMergeList.SequenceEqual(mergeInts));
        numMergeList = nums.Merge((x, y) => x == y);
        Assert.True(numMergeList.SequenceEqual(mergeInts));

        numMergeList = nums.Take(2).Merge(nums.TakeLast(2));
        Assert.True(numMergeList.SequenceEqual(mergeInts));

        numMergeList = nums.Take(2).Merge(new[] { 7, 8, 9, 10 });
        Assert.True(numMergeList.SequenceEqual(mergeInts));
        numMergeList = nums.Take(2).Merge(new[] { 7, 8, 9, 10 }, i => i);
        Assert.True(numMergeList.SequenceEqual(mergeInts));
        numMergeList = nums.Take(2).Merge(nums.TakeLast(2), i => i);
        Assert.True(numMergeList.SequenceEqual(mergeInts));
        numMergeList = nums.Take(2).Merge(nums.TakeLast(2), (x, y) => x == y);
        Assert.True(numMergeList.SequenceEqual(mergeInts));
        numMergeList = nums.Take(2).Merge(new[] { 7, 8, 9, 10 }, (x, y) => x == y);
        Assert.True(numMergeList.SequenceEqual(mergeInts));

        Func<int, int> hashFunc = null;
        Assert.True(nums.Merge(hashFunc).SequenceEqual(mergeInts));
        nums = null;
        mergeInts = null;
        IEnumerable<int[]> emptyNums = null;
        Assert.Empty(nums.Merge());
        Assert.Empty(nums.Merge(true));
        Assert.Empty(nums.Merge(mergeInts));
        Assert.Empty(nums.Merge(mergeInts, true));
        Assert.Empty(nums.Merge(mergeInts, i => i));
        Assert.Empty(nums.Merge(mergeInts, (x, y) => x == y));
        Assert.Empty(nums.Merge(emptyNums));
        Assert.Empty(nums.Merge(emptyNums, true));
        Assert.Empty(nums.Merge(emptyNums, i => i));
        Assert.Empty(nums.Merge(emptyNums, (x, y) => x == y));
        Assert.Empty(emptyNums.Merge(item => item.GetHashCode()));

    }

    [Fact]
    public void SpliteCollectionTest()
    {
        var nums = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var numSplit = nums.SpliteCollection(2);
        Assert.True(numSplit.Count() == 5);
        Assert.True(numSplit.First().SequenceEqual(new[] { 1, 2 }));

        numSplit = nums.SpliteCollection(3);
        Assert.True(numSplit.Count() == 4);
        Assert.True(numSplit.First().SequenceEqual(new[] { 1, 2, 3 }));
        Assert.True(numSplit.Last().SequenceEqual(new[] { 10 }));
        nums = null;
        Assert.Empty(nums.SpliteCollection(3));
    }

    [Fact]
    public void EmptyTest()
    {
        string[] nullArray = null;
        Assert.True(nullArray.IsEmpty());
        List<string> emptyList = new();
        Assert.False(emptyList.NotEmpty());
        Assert.True(emptyList.IsEmpty());
        int[] notEmptyArray = new[] { 1, 2, 3, 4 };
        Assert.True(notEmptyArray.NotEmpty());
        Assert.False(notEmptyArray.IsEmpty());
    }

    [Fact]
    public void ShuffleTest()
    {
        var nums = new[] { 1, 2, 3, 4, 5 };
        var oNums = new[] { 1, 2, 3, 4, 5 };
        Assert.True(nums.SequenceEqual(oNums));
        Assert.False(nums.Shuffle().SequenceEqual(oNums));

        nums = null;
        Assert.Empty(nums.Shuffle());
    }

    [Fact]
    public void IEnumerableAddRangeTest()
    {
        IEnumerable<int> nums = new[] { 1, 2, 3 };
        var enums = nums.AddRange(4, 5);
        Assert.True(enums.Count() == 5);
        enums = nums.AddRange(new[] { 4, 5, 6 });
        Assert.True(enums.Count() == 6);
        enums = nums.AddRange((x, y) => x == y, 3, 4, 5);
        Assert.True(enums.Count() == 5);
        enums = nums.AddRange(Enumerable.Empty<int>(), item => item);
        Assert.True(enums.Count() == 3);
        enums = nums.AddRange(x => x.GetHashCode(), 2, 3, 4);
        Assert.True(enums.Count() == 4);

        enums = nums.AddRange(new[] { 4, 5 }.AsEnumerable());
        Assert.True(enums.Count() == 5);
        enums = nums.AddRange(new[] { 4, 5, 6 }.AsEnumerable());
        Assert.True(enums.Count() == 6);
        enums = nums.AddRange(new[] { 3, 4, 5 }, (x, y) => x == y);
        Assert.True(enums.Count() == 5);
        enums = nums.AddRange(new[] { 2, 3, 4 }, x => x.GetHashCode());
        Assert.True(enums.Count() == 4);
    }

    [Fact]
    public void NullIEnumerableAddRangeTest()
    {
        IEnumerable<int> nums = null;
        IEnumerable<int> targets = null;
        Assert.Empty(nums.AddRange());
        Assert.Empty(nums.AddRange(targets));
        Assert.Empty(nums.AddRange(i => i));
        Assert.Empty(nums.AddRange(targets, i => i));
        Assert.Empty(nums.AddRange(Enumerable.Empty<int>(), i => i));
        Assert.Empty(nums.AddRange((x, y) => x == y));
        nums = Enumerable.Empty<int>();
        Assert.Empty(nums.AddRange(targets, (x, y) => x == y));
    }

    public class AddRangeTestModel
    {
        public string Name { get; set; }
    }

    [Fact]
    public void NullIEnumerableAddRangeUniqueObjectTest()
    {
        IEnumerable<AddRangeTestModel> datas = null;
        IEnumerable<AddRangeTestModel> datas2 = null;
        Assert.Empty(datas.AddRange(datas2, item => item.Name));
    }

    [Fact]
    public void IEnumerableAddRangeUniqueObjectTest()
    {
        IEnumerable<AddRangeTestModel> datas = new[] { new AddRangeTestModel { Name = "1" } };
        IEnumerable<AddRangeTestModel> datas2 = new[] { new AddRangeTestModel { Name = "1" } };
        var temps = datas.AddRange(datas2);
        Assert.True(temps.Count() == 2);
        temps = datas.AddRange(datas2, item => item.Name);
        Assert.False(temps.Count() == 2);
        Assert.Single(temps);
    }

    [Fact]
    public void NullICollectionAddRangeTest()
    {
        ICollection<int> nums = null;
        IEnumerable<int> targets = null;
        nums.AddRange();
        Assert.Null(nums);
        nums.AddRange(targets);
        Assert.Null(nums);
        nums.AddRange(i => i);
        Assert.Null(nums);
        nums.AddRange(targets, i => i);
        Assert.Null(nums);
        nums.AddRange((x, y) => x == y);
        Assert.Null(nums);
        nums.AddRange(targets, (x, y) => x == y);
        Assert.Null(nums);
    }
    [Fact]
    public void ICollectionAddRangeTest()
    {
        ICollection<int> nums = new List<int> { 1, 2, 3 };
        nums.AddRange(4, 5, 6);
        Assert.True(nums.Count == 6);
        nums.AddRange(new[] { 4, 5, 6 });
        Assert.True(nums.Count == 9);
        nums.AddRange((x, y) => x == y, 6, 7, 8);
        Assert.True(nums.Count == 11);
        nums.AddRange(x => x.GetHashCode(), 8, 9, 10);
        Assert.True(nums.Count == 13);

        nums.AddRange(new[] { 4, 5, 6 }.AsEnumerable());
        Assert.True(nums.Count == 16);
        nums.AddRange(new[] { 6, 7, 8 }, (x, y) => x == y);
        Assert.True(nums.Count == 16);
        nums.AddRange(new[] { 8, 9, 10 }, x => x.GetHashCode());
        Assert.True(nums.Count == 16);
        Assert.True(nums.Count == 16);
    }

    [Fact]
    public void NullICollectionAddRangeUniqueObjectTest()
    {
        ICollection<AddRangeTestModel> datas = null;
        ICollection<AddRangeTestModel> datas2 = null;
        datas.AddRange(datas2, item => item.Name);
        Assert.Null(datas);
    }

    [Fact]
    public void ICollectionAddRangeUniqueObjectTest()
    {
        ICollection<AddRangeTestModel> datas = new List<AddRangeTestModel> { new AddRangeTestModel { Name = "1" } };
        ICollection<AddRangeTestModel> datas2 = new List<AddRangeTestModel> { new AddRangeTestModel { Name = "1" } };
        datas.AddRange(datas2);
        Assert.True(datas.Count() == 2);
        datas.AddRange(item => item.Name, datas2.ToArray());
        Assert.True(datas.Count() == 2);
        datas.AddRange(new[] { new AddRangeTestModel { Name = "1" }, new AddRangeTestModel { Name = "2" } }, item => item.Name);
        Assert.True(datas.Count() == 3);
    }

    [Fact]
    public void ForEachTest()
    {
        var sum = 0;
        int[] nums = new[] { 1, 2, 3 };
        nums.ForEach(item => sum += item);
        Assert.True(sum == 6);
        sum = 0;
        nums.ForEach(item => sum += item * item);
        Assert.True(sum == 14);

        nums = null;
        Assert.Empty(nums.ForEach(item => sum += item));
    }

    [Fact]
    public void SelectWithIndexTest()
    {
        int[] nums = new[] { 1, 2, 3 };
        foreach (var (item, index) in nums.SelectWithIndex())
            Assert.True(index + 1 == item);

        foreach (var (item, index) in nums.SelectWithIndex(num => num * num))
            Assert.True(index == item * item);
        foreach (var (item, index) in nums.SelectWithIndex(num => num.ToString(), num => num))
        {
            Assert.True(item is not null);
            Assert.True(item.ToInt() == index);
        }

        nums = null;
        Assert.Empty(nums.SelectWithIndex());
        Assert.Empty(nums.SelectWithIndex(num => num * num));
        Assert.Empty(nums.SelectWithIndex(num => num.ToString(), num => num));
    }

    [Fact]
    public void InTest()
    {
        int[] items = { 1, 2, 3, 4, 5 };
        Assert.True(1.In(items));
        Assert.True(4.In(items));
        Assert.False(6.In(items));

        Assert.True(1.In(1, 2, 3, 4, 5));
        Assert.True(4.In(1, 2, 3, 4, 5));
        Assert.False(6.In(1, 2, 3, 4, 5));
    }

    [Fact]
    public void HasComparerInTest()
    {
        var items = UniqueTestModel.GetModels(0, 1, 2, 3, 4);
        Assert.False(new UniqueTestModel { Index = 1 }.In(items));
        Func<UniqueTestModel, int> func = null;
        Assert.False(new UniqueTestModel { Index = 1 }.In(items, func));
        Assert.True(new UniqueTestModel { Index = 1 }.In(items, (x, y) => x.Index == y.Index));
        Assert.True(new UniqueTestModel { Index = 3 }.In(items, (x, y) => x.Index == y.Index));
        Assert.False(new UniqueTestModel { Index = 5 }.In(items, (x, y) => x.Index == y.Index));
        Assert.False(new UniqueTestModel { Index = 5 }.In(items, x => x.Index));

        var values = UniqueTestModel.GetModels(0, 1, 2, 3, 4).ToArray();

        Assert.True(new UniqueTestModel { Index = 1 }.In((x, y) => x.Index == y.Index, values));
        Assert.True(new UniqueTestModel { Index = 3 }.In((x, y) => x.Index == y.Index, values));
        Assert.False(new UniqueTestModel { Index = 5 }.In((x, y) => x.Index == y.Index, values));
        Assert.False(new UniqueTestModel { Index = 5 }.In(x => x.Index, values));
    }

    [Fact]
    public void GetItemInTest()
    {
        var list1 = new[] { 1, 2, 3, 4 };
        var list2 = new[] { 3, 4, 5, 6 };
        Assert.True(list1.GetItemIn(list2).SequenceEqual(new[] { 3, 4 }));
        var model1 = UniqueTestModel.GetModels(0, 1, 2, 3);
        var model2 = UniqueTestModel.GetModels(3, 4, 5, 6, 7);
        Assert.True(model1.GetItemIn(model2, (a, b) => a.Index == b.Index).Select(item => item.Index).SequenceEqual(new[] { 3 }));
        Assert.True(model1.Intersect(model2, (a, b) => a.Index == b.Index).Select(item => item.Index).SequenceEqual(new[] { 3 }));

        model1 = null;
        model2 = null;
        Assert.Empty(model1.GetItemIn(model2, (a, b) => a.Index == b.Index));
        Assert.Empty(model1.Intersect(model2, (a, b) => a.Index == b.Index));
        Assert.Empty(model1.GetItemIn(model2));
    }

    [Fact]
    public void GetItemNotInTest()
    {
        var list1 = new[] { 1, 2, 3, 4 };
        var list2 = new[] { 3, 4, 5, 6 };
        Assert.True(list1.GetItemNotIn(list2).SequenceEqual(new[] { 1, 2 }));
        var model1 = UniqueTestModel.GetModels(0, 1, 2, 3);
        var model2 = UniqueTestModel.GetModels(3, 4, 5, 6, 7);
        Assert.True(model1.GetItemNotIn(model2, (a, b) => a.Index == b.Index).Select(item => item.Index).SequenceEqual(new[] { 0, 1, 2 }));
        Assert.True(model1.Except(model2, (a, b) => a.Index == b.Index).Select(item => item.Index).SequenceEqual(new[] { 0, 1, 2 }));

        model1 = null;
        model2 = null;
        Assert.Empty(model1.GetItemNotIn(model2, (a, b) => a.Index == b.Index));
        Assert.Empty(model1.Except(model2, (a, b) => a.Index == b.Index));
        Assert.Empty(model1.GetItemNotIn(model2));
    }
}
