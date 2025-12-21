namespace SunamoShared.Entity;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public partial class SongFromInternet : IEquatable<SongFromInternet>
{
    List<string> nazev = new List<string>();
    List<string> title = new List<string>();
    List<string> remix = new List<string>();
    List<string> nazevWoDiacritic = new List<string>();
    List<string> titleWoDiacritic = new List<string>();
    List<string> remixWoDiacritic = new List<string>();
    public string ytCode = null;
    public int idInDb = int.MaxValue;
    string _artistS = null;
    string _titleS = null;
    string _remixS = null;
    public string ArtistC
    {
        get
        {
            return _artistS;
        }

        set
        {
            _artistS = value;
        }
    }

    public string TitleC
    {
        get
        {
            return _titleS;
        }

        set
        {
            _titleS = value;
        }
    }

    public string RemixC
    {
        get
        {
            return _remixS;
        }

        set
        {
            _remixS = value;
        }
    }

    public void Artist(string item)
    {
        nazev.Clear();
        nazevWoDiacritic.Clear();
        nazev.AddRange(SplitNazevTitle(item));
        nazevWoDiacritic = CA.WithoutDiacritic(nazev);
        _artistS = ArtistInConvention();
    }

    public SongFromInternet()
    {
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name = "song"></param>
    /// <param name = "ytCode"></param>
    public SongFromInternet(string song, string ytCode = null)
    {
        string n, t, result;
        ManageArtistDashTitle.GetArtistTitleRemix(song, out n, out t, out result);
        Init(n, t, result);
        this.ytCode = ytCode;
    }

    public SongFromInternet(SongFromInternet item2)
    {
        // TODO: Complete member initialization
        nazev = new List<string>(item2.nazev);
        title = new List<string>(item2.title);
        remix = new List<string>(item2.remix);
        SetInConvention();
    }

    public SongFromInternet Init(Tuple<string, string, string> t)
    {
        return Init(t.Item1, t.Item2, t.Item3);
    }

    public SongFromInternet Init(string n, string t, string result)
    {
        Artist(n);
        var splittedNazevTitle = SplitNazevTitle(t);
        var splittedRemix = SplitRemix(result);
        title.AddRange(splittedNazevTitle);
        remix.AddRange(splittedRemix);
        titleWoDiacritic = CA.WithoutDiacritic(new List<string>(title));
        remixWoDiacritic = CA.WithoutDiacritic(new List<string>(remix));
        SetInConvention();
        return this;
    }

    /// <summary>
    /// Pro správné porovnání musí být všechny řetězce jak v A1 tak v A2 lowercase
    /// </summary>
    public static bool IsSimilar(string[] titleArray, string name)
    {
        return IsSimilar(new List<string>(titleArray), name);
    }

    /// <summary>
    /// Pro správné porovnání musí být všechny řetězce jak v A1 tak v A2 lowercase
    /// </summary>
    /// <param name = "titleArray"></param>
    /// <param name = "name"></param>
    public static bool IsSimilar(List<string> titleArray, string name)
    {
        // titleArray - originally entered
        // name,nameArray - from spotify
        int psn, prn;
        var nameArray = SHSplit.SplitBySpaceAndPunctuationCharsAndWhiteSpaces(name);
        SongFromInternet.VratPocetStejnychARozdilnych(titleArray, nameArray, out psn, out prn);
        if (CalculateSimilarity(psn, prn, titleArray, new List<string>(nameArray)) > 0.49f)
        {
            return true;
        }

        return false;
    }

    private void SetInConvention()
    {
        _artistS = ArtistInConvention();
        _titleS = TitleInConvention();
        _remixS = RemixInConvention();
    }

    /// <summary>
    /// Replace without feat etc
    /// </summary>
    public string RemixOnlyContent()
    {
        var result = Remix();
        result = CA.ReplaceAll(result, AllLists.featLower, string.Empty);
        result = CA.ReplaceAll(result, AllLists.featUpper, string.Empty);
        return result;
    }

    /// <summary>
    /// Porovnává s ohledem na diakritiku
    /// </summary>
    /// <param name = "p"></param>
    public float CalculateSimilarity(string p)
    {
        SongFromInternet s = new SongFromInternet(p);
        return CalculateSimilarity(s, false);
    }

    public float CalculateSimilarity(SongFromInternet s, bool woDiacritic)
    {
        float n, t, result;
        if (woDiacritic)
        {
            int psn, prn, pst, prt, psr, prr;
            VratPocetStejnychARozdilnych(s.nazevWoDiacritic, nazevWoDiacritic, out psn, out prn);
            VratPocetStejnychARozdilnych(s.titleWoDiacritic, titleWoDiacritic, out pst, out prt);
            VratPocetStejnychARozdilnych(s.remixWoDiacritic, remixWoDiacritic, out psr, out prr);
            n = CalculateSimilarity(psn, prn, s.nazev, nazevWoDiacritic);
            t = CalculateSimilarity(pst, prt, s.title, titleWoDiacritic);
            result = CalculateSimilarity(psr, prr, s.remix, remixWoDiacritic);
        }
        else
        {
            int psn, prn, pst, prt, psr, prr;
            VratPocetStejnychARozdilnych(s.nazev, nazev, out psn, out prn);
            VratPocetStejnychARozdilnych(s.title, title, out pst, out prt);
            VratPocetStejnychARozdilnych(s.remix, remix, out psr, out prr);
            n = CalculateSimilarity(psn, prn, s.nazev, nazev);
            t = CalculateSimilarity(pst, prt, s.title, title);
            result = CalculateSimilarity(psr, prr, s.remix, remix);
        }

        float vr = (n + t) / 2;
        if (vr == 1)
        {
            vr = (n + t + result) / 3;
        }

        return vr;
    }
}