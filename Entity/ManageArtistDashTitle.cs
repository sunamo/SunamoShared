namespace SunamoShared;

public class ManageArtistDashTitle
{
    public static void GetArtistTitleRemix(string item, out string artist, out string song, out string remix)
    {
        var r = GetArtistTitleRemix(item);
        artist = r.Item1;
        song = r.Item2;
        remix = r.Item3;
    }

    public static bool ContainsBracket(string t, ref List<char> left, ref List<char> right, bool mustBeLeftAndRight = false)
    {
        left = SH.ContainsAnyChar(t, false, AllLists.leftBrackets);
        right = SH.ContainsAnyChar(t, false, AllLists.leftBrackets);
        if (mustBeLeftAndRight)
        {
            if (left.Count > 0 && right.Count > 0)
            {
                return true;
            }
        }
        else
        {
            if (left.Count > 0 || right.Count > 0)
            {
                return true;
            }
        }



        return false;
    }

    /// <param name = "item"></param>
    /// <param name = "artist"></param>
    /// <param name = "song"></param>
    /// <param name = "remix"></param>
    public static Tuple<string, string, string> GetArtistTitleRemix(string item)
    {
        string artist; string song; string remix;
        string delimiter = SH.WrapWith(AllStrings.dash, AllStrings.space);

        if (!item.Contains(delimiter))
        {
            delimiter = AllStrings.dash;
        }

        List<string> toks = SHSplit.Split(item, delimiter);
        artist = song = "";
        if (toks.Count == 0)
        {
            artist = song = remix = "";
        }
        else if (toks.Count == 1)
        {
            artist = "";
            VratTitleRemix(toks[0], out song, out remix);
        }
        else
        {

            artist = toks[0];

            List<char> left, right;
            left = right = null;

            if (SH.ContainsBracket(artist, ref left, ref right))
            {
                if (left.Count - 1 == right.Count)
                {
                    var closingBracket = SH.ClosingBracketFor(left[0]);
                    right.Add(closingBracket);
                    artist += closingBracket;
                }
                if (left.Count > 0 && right.Count > 0)
                {
                    var between = SH.GetTextBetween(artist, left[0], right[0]);
                    between = left[0] + between + right[0];
                    item = item.Replace(between, string.Empty);
                    item += " " + between;
                    toks = SHSplit.Split(item, delimiter);
                    if (toks.Count > 0)
                    {
                        artist = toks[0].Trim();
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < toks.Count; i++)
            {
                sb.Append(toks[i]);
            }

            VratTitleRemix(sb.ToString().TrimEnd(AllChars.dash), out song, out remix);
        }
        return new Tuple<string, string, string>(artist, song, remix);
    }

    /// <summary>
    /// IUN
    /// </summary>
    /// <param name = "p"></param>
    /// <param name = "title"></param>
    /// <param name = "remix"></param>
    private static void VratTitleRemix(string p, out string title, out string remix)
    {
        title = p;
        remix = "";
        int firstHranata = p.IndexOf(AllChars.rsqb);
        int firstNormal = p.IndexOf(AllChars.lb);
        if (firstHranata == -1 && firstNormal != -1)
        {
            VratRozdeleneByVcetne(p, firstNormal, out title, out remix);
        }
        else if (firstHranata != -1 && firstNormal == -1)
        {
            VratRozdeleneByVcetne(p, firstHranata, out title, out remix);
        }
        else if (firstHranata != -1 && firstNormal != -1)
        {
            if (firstHranata < firstNormal)
            {
                VratRozdeleneByVcetne(p, firstNormal, out title, out remix);
            }
            else
            {
                VratRozdeleneByVcetne(p, firstHranata, out title, out remix);
            }
        }
    }

    /// <param name = "p"></param>
    /// <param name = "firstNormal"></param>
    /// <param name = "title"></param>
    /// <param name = "remix"></param>
    private static void VratRozdeleneByVcetne(string p, int firstNormal, out string title, out string remix)
    {
        title = p.Substring(0, firstNormal);
        remix = p.Substring(firstNormal);
    }

    /// <summary>
    ///
    /// Získám interpreta
    /// </summary>
    /// <param name = "item"></param>
    public static string GetArtist(string item)
    {
        string nazev, title = null;
        GetArtistTitle(item, out nazev, out title);
        return nazev;
    }

    /// <param name = "item"></param>
    /// <param name = "název"></param>
    /// <param name = "title"></param>
    public static void GetArtistTitle(string item, out string název, out string title)
    {
        // Path.GetFileNameWithoutExtension()
        var fnwoe = Path.GetFileNameWithoutExtension(item);
        List<string> toks = SHSplit.Split(fnwoe, AllStrings.dash);
        název = title = "";
        if (toks.Count == 0)
        {
            název = title = "";
        }
        else if (toks.Count == 1)
        {
            název = "";
            title = toks[0];
        }
        else
        {
            název = toks[0];
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < toks.Count; i++)
            {
                sb.Append(toks[i] + AllStrings.dash);
            }

            title = sb.ToString().TrimEnd(AllChars.dash);
        }
    }

    /// <summary>
    /// První písmeno, písmena po AllChars.space a AllStrings.dash budou velkým.
    /// </summary>
    /// <param name = "názevSouboru"></param>
    /// <param name = "p"></param>
    public static string ArtistAndTitleToUpper(string názevSouboru, string p)
    {
        char[] ch = názevSouboru.ToCharArray();
        ch[0] = char.ToUpper(názevSouboru[0]);
        int dex = názevSouboru.IndexOf(p);
        ch[dex + 1] = char.ToUpper(ch[dex + 1]);
        for (int i = 1; i < ch.Length; i++)
        {
            if (ch[i] == AllChars.space)
            {
                if (CA.IsThereAnotherIndex(ch, i))
                {
                    ch[i + 1] = char.ToUpper(ch[i + 1]);
                }
            }
            else if (ch[i] == AllChars.dash)
            {
                if (CA.IsThereAnotherIndex(ch, i))
                {
                    ch[i + 1] = char.ToUpper(ch[i + 1]);
                }
            }
            else if (ch[i] == AllChars.rsqb)
            {
                if (CA.IsThereAnotherIndex(ch, i))
                {
                    ch[i + 1] = char.ToUpper(ch[i + 1]);
                }
            }
            else if (ch[i] == AllChars.lb)
            {
                if (CA.IsThereAnotherIndex(ch, i))
                {
                    ch[i + 1] = char.ToUpper(ch[i + 1]);
                }
            }
        }

        StringBuilder sb = new StringBuilder();
        sb.Append(ch);
        return sb.ToString();
    }

    /// <param name = "p"></param>
    /// <param name = "cimNahradit"></param>
    public static string ReplaceAllHyphensExceptTheFirst(string p, string cimNahradit)
    {
        int dex = p.IndexOf(AllChars.dash);
        p = p.Replace(AllChars.dash, AllChars.space);
        char[] j = p.ToCharArray();
        j[dex] = AllChars.dash;
        return new string(j);
    }

    /// <summary>
    /// IUN
    ///
    /// </summary>
    /// <param name = "item"></param>
    public string GetTitle(string item)
    {
        string nazev, title = null;
        GetArtistTitle(item, out nazev, out title);
        return title;
    }

    /// <param name = "text"></param>
    public static string Reverse(string text)
    {
        List<string> d = SHSplit.SplitChar(text, AllChars.dash);
        string temp = d[0];
        d[0] = d[d.Count - 1];
        d[d.Count - 1] = temp;
        StringBuilder sb = new StringBuilder();
        foreach (string item in d)
        {
            sb.Append(item + AllStrings.dash);
        }

        return sb.ToString().TrimEnd(AllChars.dash);
    }
}
