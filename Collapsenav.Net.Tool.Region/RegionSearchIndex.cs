namespace Collapsenav.Net.Tool.Region;


public class RegionSearchIndex
{
    public Dictionary<char, List<char>> Data = new();
    public RegionSearchIndex() { }
    public RegionSearchIndex(IEnumerable<string> input)
    {
        foreach (var item in input)
            Add(item);
    }

    public void Add(string input)
    {
        char last = '\0';
        foreach (var c in input)
        {
            if (Data.TryGetValue(last, out List<char>? value) && !value.Contains(c))
                Data[last].Add(c);
            if (!Data.ContainsKey(c))
                Data.Add(c, new());
            last = c;
        }
    }

    public virtual string? Match(string text)
    {
        text += " ";
        int i, j;
        for (i = 0; i < text.Length - 1; i++)
        {
            if (Data.ContainsKey(text[i]))
            {
                for (j = i + 1; j < text.Length; j++)
                    if (!Data[text[j - 1]].Contains(text[j]))
                        break;
                text = text.Substring(j - i);
            }
            // else
            // {
            //     for (j = i + 1; j < text.Length; j++)
            //         if (Data.ContainsKey(text[j]))
            //             break;
            //     return text.Substring(i, j - i);
            // }
            return string.Empty;
        }
        return string.Empty;
    }
}
