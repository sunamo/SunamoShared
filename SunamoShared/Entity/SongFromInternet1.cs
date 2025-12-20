// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.Entity;
public partial class SongFromInternet : IEquatable<SongFromInternet>
{
    /// <summary>
    /// A1 pSn = count of Same
    /// A2 pRn = count of Rozdílný/different (sum of keep from both collection)
    /// </summary>
    /// <param name = "psn"></param>
    /// <param name = "prn"></param>
    /// <param name = "novy"></param>
    /// <param name = "puvodni"></param>
    public static float CalculateSimilarity(int psn, int prn, List<string> novy, List<string> puvodni)
    {
        if (psn == prn && psn == 0)
        {
            return 1.0f;
        }

        if (psn > prn)
        {
            //prn == 1 ||
            if (prn == 0)
            {
                int uy = 0;
                for (int i = 0; i < novy.Count; i++)
                {
                    if (puvodni.Contains(novy[i]))
                    {
                        uy++;
                    }
                }

                if (uy == novy.Count) // - 1
                {
                    return 1.0f;
                }

                //return 1.0f;
                return 1f / 3f * 2f;
            }

            if (prn != 0)
            {
                if (prn != 1)
                {
                    return psn / prn;
                }

                if (psn > 3)
                {
                    float vr = (psn - prn) / 2f;
                    while (vr > 1f)
                    {
                        vr /= 2f;
                    }

                    return vr;
                }
                else
                {
                    float vr = (psn - psn / (psn - 1f)) / 2;
                    int uy = 0;
                    for (int i = 0; i < novy.Count; i++)
                    {
                        if (puvodni.Contains(novy[i]))
                        {
                            uy++;
                        }
                    }

                    if (uy > 0)
                    {
                        vr = uy / (float)prn / 2f;
                    }

                    if (vr > 0.99f)
                    {
                        vr = vr / 2f;
                    }

                    return vr;
                }
            //return 0.5f;
            }

            return 0f;
        }

        if (psn + 1 > prn && psn < 3)
        {
            return 0.5f;
        }

        return 0f;
    }

    /// <summary>
    /// A2 - compare both after diac trim
    /// A3 - minimal for return true
    /// </summary>
    /// <param name = "s"></param>
    /// <param name = "woDiacritic"></param>
    /// <param name = "minimal"></param>
    /// <returns></returns>
    public float CalculateSimilarityAll(SongFromInternet s, bool woDiacritic, float minimal)
    {
        var _this = this;
        var result = CalculateSimilarity(s, woDiacritic);
        float result2 = 0;
        bool _continue = true;
        if (minimal <= result)
        {
            _continue = false;
        }

        List<string> feats = null;
        if (_continue)
        {
            var song = s.TitleC;
            feats = s.AlternateArtists();
            foreach (var item in feats)
            {
                s = new SongFromInternet(item + "-" + song);
                result2 = CalculateSimilarity(s, true);
                if (breakInCalculateSimilarity)
                {
                    System.Diagnostics.Debugger.Break();
                }

                if (result2 > result)
                {
                    result = result2;
                }

                if (minimal <= result)
                {
                    break;
                }
            }
        }

        if (breakInCalculateSimilarity)
        {
            System.Diagnostics.Debugger.Break();
        }

        return result;
    }

    public static bool breakInCalculateSimilarity = false;
    public string Artist()
    {
        return string.Join(" ", nazev);
    }

    public string ArtistInConvention()
    {
        return ConvertEveryWordLargeCharConvention.ToConvention(Artist());
    }

    public string Title()
    {
        return string.Join(" ", title);
    }

    public string TitleInConvention()
    {
        return ConvertEveryWordLargeCharConvention.ToConvention(Title());
    }

    public string Remix()
    {
        return string.Join(" ", remix);
    }

    public string RemixInConvention()
    {
        return ConvertEveryWordLargeCharConvention.ToConvention(Remix());
    }

    public string TitleAndRemixInConvention()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(TitleInConvention());
        if (remix.Count != 0)
        {
            stringBuilder.Append("[" + RemixInConvention() + "]");
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// A1,2 are list to compare
    /// A3 pSn = count of Same
    /// A4 pRn = count of Rozdílný/different (sum of keep from both collection)
    /// </summary>
    /// <param name = "list"></param>
    /// <param name = "list_2"></param>
    /// <param name = "psn"></param>
    /// <param name = "prn"></param>
    public static void VratPocetStejnychARozdilnych(List<string> list, List<string> list_2, out int psn, out int prn)
    {
        // Create copies of A1,2
        List<string> l1 = new List<string>(list.ToArray());
        List<string> l2 = new List<string>(list_2.ToArray());
        psn = 0;
        // Process all in l1
        for (int i = l1.Count - 1; i >= 0; i--)
        {
            int dex = l2.IndexOf(l1[i]);
            if (dex != -1)
            {
                // if l1 is in l2, remove from both
                l1.RemoveAt(i);
                l2.RemoveAt(dex);
                psn++;
            }
        }

        prn = l1.Count + l2.Count;
    }

    public override string ToString()
    {
        StringBuilder vr = new StringBuilder();
        vr.Append(Artist() + "-" + Title());
        if (remix.Count != 0)
        {
            vr.Append(" [" + Remix() + "]");
        }

        return vr.ToString();
    }

    public string ToConventionString()
    {
        StringBuilder vr = new StringBuilder();
        vr.Append(ArtistInConvention() + "-" + TitleInConvention());
        if (remix.Count != 0)
        {
            vr.Append(" [" + RemixInConvention() + "]");
        }

        return vr.ToString();
    }

    private IList<string> SplitRemix(string u)
    {
        // comma - artists like Hm... or The Academy Is..
        List<string> gg = SHSplit.Split(u, "&", " ", ",", "-", "[", "]", "(", ")");
        //gg.ForEach(g => g.ToLower());
        for (int i = 0; i < gg.Count; i++)
        {
            gg[i] = gg[i].ToLower();
        }

        return gg;
    }

    private IList<string> SplitNazevTitle(string u)
    {
        List<string> gg = SHSplit.Split(u, "&", " ", ",", "-");
        //gg.ForEach(g => g.ToLower());
        for (int i = 0; i < gg.Count; i++)
        {
            gg[i] = gg[i].ToLower();
        }

        return gg;
    }
}