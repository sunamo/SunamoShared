namespace SunamoShared._sunamo.SunamoDateTime.DT;

internal class DTHelper
{
    public static string DateTimeToFileName(DateTime dt, bool time)
    {
        string dDate = "_";
        string dSpace = "_";
        string dTime = "_";
        string vr = dt.Year + dDate + dt.Month.ToString("D2") + dDate + dt.Day.ToString("D2");
        if (time)
        {
            vr += dSpace + dt.Hour.ToString("D2") + dTime + dt.Minute.ToString("D2");
        }
        return vr;
    }

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
            return d.Day + "." + d.Month + "." + d.Year + " " + d.Hour.ToString("D2") + ":" + d.Hour.ToString("D2");
        }
        else
        {
            // 6/21/1989 11:22 (fill zero)
            return d.Month + "/" + d.Day + "/" + d.Year + " " + d.Hour.ToString("D2") + ":" + d.Minute.ToString("D2");
        }
    }
}
