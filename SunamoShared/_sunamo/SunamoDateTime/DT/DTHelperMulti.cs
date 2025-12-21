namespace SunamoShared._sunamo.SunamoDateTime.DT;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
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