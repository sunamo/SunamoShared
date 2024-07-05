namespace SunamoShared._sunamo.SunamoDateTime.DT;
internal class DTHelperMulti
{
    internal static string DateToString(DateTime p, LangsShared l)
    {
        if (l == LangsShared.cs)
        {
            return p.Day + AllStrings.dot + p.Month + AllStrings.dot + p.Year;
        }
        return p.Month + AllStrings.slash + p.Day + AllStrings.slash + p.Year;
    }
}
