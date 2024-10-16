namespace SunamoShared._sunamo.SunamoDateTime.DT;
internal class DTHelperMulti
{
    internal static string DateToString(DateTime p, LangsShared l)
    {
        if (l == LangsShared.cs)
        {
            return p.Day + "." + p.Month + "." + p.Year;
        }
        return p.Month + "/" + p.Day + "/" + p.Year;
    }
}
