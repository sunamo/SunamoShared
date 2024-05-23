namespace SunamoShared;
public class DTHelperMulti
{
    public static string DateToString(DateTime p, Langs l)
    {
        if (l == Langs.cs)
        {
            return p.Day + AllStrings.dot + p.Month + AllStrings.dot + p.Year;
        }
        return p.Month + AllStrings.slash + p.Day + AllStrings.slash + p.Year;
    }
}
