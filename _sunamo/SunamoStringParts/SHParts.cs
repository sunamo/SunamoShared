namespace SunamoShared._sunamo.SunamoStringParts;
internal class SHParts
{
    internal static string RemoveAfterFirst(string t, char ch)
    {
        int dex = t.IndexOf(ch);
        return dex == -1 || dex == t.Length - 1 ? t : t.Substring(0, dex);
    }


}
