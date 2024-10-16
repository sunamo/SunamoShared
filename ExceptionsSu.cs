namespace SunamoShared;

public class ExceptionsSu
{
    public static List<string> ParseExceptions(List<string> lines, params string[] trimIfStartWith)
    {
        lines = lines.Where(d => d.StartsWith("Exception:")).ToList();
        CA.TrimStart("Exception:", lines);
        foreach (var item in trimIfStartWith) CA.TrimStart(item, lines);
        return lines;
    }
}