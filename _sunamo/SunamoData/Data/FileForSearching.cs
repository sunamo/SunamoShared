namespace SunamoShared;


public class FileForSearching
{
    internal bool surelyNo = false;
    internal List<int> foundedLines = new List<int>();
    internal List<string> linesLower = null;
    internal List<string> lines = null;
    string path = null;
    internal FileForSearching(string path)
    {
        this.path = path;
    }
    public
#if ASYNC
async Task
#else
void
#endif
        Init()
    {
        lines = SHGetLines.GetLines(
#if ASYNC
await
#endif
File.ReadAllTextAsync(path)).ToList();
        linesLower = new List<string>(lines.Count);
        foreach (var item in lines)
        {
            linesLower.Add(item.ToLower());
        }
    }
}