// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared._public.SunamoData.Data;


public class FileForSearching
{
    public bool surelyNo = false;
    public List<int> foundedLines = new List<int>();
    public List<string> linesLower = null;
    public List<string> lines = null;
    string path = null;
    public FileForSearching(string path)
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
        lines = (
 await
File.ReadAllLinesAsync(path)).ToList();
        linesLower = new List<string>(lines.Count);
        foreach (var item in lines)
        {
            linesLower.Add(item.ToLower());
        }
    }
}