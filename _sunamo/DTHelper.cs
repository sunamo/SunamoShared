namespace SunamoShared._sunamo;
internal class DTHelper
{
    internal static string DateToString(DateTime p, Langs l)
    {
        return DTHelperMulti.DateToString(p, l);
    }

    internal static string DateTimeToString(DateTime d, Langs l, DateTime dtMinVal)
    {
        if (d == dtMinVal)
        {
            if (l == Langs.cs)
            {
                return "ItWasNotMentioned";
            }
            else
            {
                return "NotIndicated";
            }
        }

        if (l == Langs.cs)
        {
            // 21.6.1989 11:22 (fill zero)
            return d.Day + AllStrings.dot + d.Month + AllStrings.dot + d.Year + AllStrings.space + d.Hour.ToString("D2") + AllStrings.colon + d.Hour.ToString("D2");
        }
        else
        {
            // 6/21/1989 11:22 (fill zero)
            return d.Month + AllStrings.slash + d.Day + AllStrings.slash + d.Year + AllStrings.space + d.Hour.ToString("D2") + AllStrings.colon + d.Minute.ToString("D2");
        }
    }
}
