// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoShared._sunamo.SunamoStringJoin;

internal class SHJoin
{
    internal static string JoinFromIndex(int dex, object delimiter2, IList parts)
    {
        string delimiter = delimiter2.ToString();
        StringBuilder stringBuilder = new StringBuilder();
        int i = 0;
        foreach (var item in parts)
        {
            if (i >= dex)
            {
                stringBuilder.Append(item + delimiter);
            }
            i++;
        }
        string vr = stringBuilder.ToString();
        return vr.Substring(0, vr.Length - 1);
        //return SHSubstring.SubstringLength(vr, 0, vr.Length - 1);
    }
}
