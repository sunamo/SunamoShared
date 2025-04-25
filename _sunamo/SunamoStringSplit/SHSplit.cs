namespace SunamoShared._sunamo.SunamoStringSplit;

internal class SHSplit
{


    [Obsolete("Pou��v� SHData.s_spaceAndPuntactionCharsAndWhiteSpaces")]
    internal static List<string> SplitBySpaceAndPunctuationCharsAndWhiteSpaces(string s)
    {
        //return s.Split(SHData.s_spaceAndPuntactionCharsAndWhiteSpaces).ToList();
        return null;
    }

    internal static List<string> SplitChar(string v1, params char[] v2)
    {
        return v1.Split(v2).ToList();
    }

    internal static List<string> SplitMore(string p, params string[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.RemoveEmptyEntries).ToList();
    }


}