namespace SunamoShared._sunamo.SunamoBts;

internal class BTS
{
    internal static bool IntToBool(int v)
    {
        return Convert.ToBoolean(v);
    }

    internal static int ParseInt(string entry, int _default)
    {
        //entry = SH.FromSpace160To32(entry);
        entry = entry.Replace(" ", string.Empty);
        //var ch = entry[3];

        int lastInt2 = 0;
        if (int.TryParse(entry, out lastInt2))
        {
            return lastInt2;
        }
        return _default;
    }
}
