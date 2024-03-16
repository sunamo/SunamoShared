using SunamoStringData;

namespace SunamoShared._sunamo;
internal class SHSplit
{
    internal static List<string> SplitByWhiteSpaces(string innerText)
    {
        return innerText.Split(AllChars.whiteSpacesChars.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitBySpaceAndPunctuationCharsAndWhiteSpaces(string s)
    {
        return s.Split(SHData.s_spaceAndPuntactionCharsAndWhiteSpaces).ToList();
    }

    internal static List<string> SplitChar(string v1, params char[] v2)
    {
        return v1.Split(v2).ToList();
    }

    internal static List<string> Split(string p, params string[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.RemoveEmptyEntries).ToList();
    }


}
