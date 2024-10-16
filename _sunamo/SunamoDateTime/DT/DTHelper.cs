namespace SunamoShared._sunamo.SunamoDateTime.DT;

internal class DTHelper
{
    internal static string DateToString(DateTime p, LangsShared l)
    {
        return DTHelperMulti.DateToString(p, l);
    }

    internal static string DateTimeToString(DateTime d, LangsShared l, DateTime dtMinVal)
    {
        if (d == dtMinVal)
        {
            if (l == LangsShared.cs)
            {
                return "ItWasNotMentioned";
            }
            else
            {
                return "NotIndicated";
            }
        }

        if (l == LangsShared.cs)
        {
            // 21.6.1989 11:22 (fill zero)
            return d.Day + "." + d.Month + "." + d.Year + "" + d.Hour.ToString("D2") + ":" + d.Hour.ToString("D2");
        }
        else
        {
            // 6/21/1989 11:22 (fill zero)
            return d.Month + "/" + d.Day + "/" + d.Year + "" + d.Hour.ToString("D2") + ":" + d.Minute.ToString("D2");
        }
    }
}
