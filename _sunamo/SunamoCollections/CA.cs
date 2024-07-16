namespace SunamoShared._sunamo.SunamoCollections;

internal class CA
{
    internal static bool IsListStringWrappedInArray<T>(List<T> v2)
    {
        var first = v2.First().ToString();
        if (v2.Count == 1 && (first == "System.Collections.Generic.List`1[System.String]" ||
                              first == "System.Collections.Generic.List`1[System.Object]")) return true;
        return false;
    }
    internal static void InitFillWith(List<string> datas, int pocet, string initWith = Consts.stringEmpty)
    {
        InitFillWith<string>(datas, pocet, initWith);
    }
    internal static void InitFillWith<T>(List<T> datas, int pocet, T initWith)
    {
        for (int i = 0; i < pocet; i++)
        {
            datas.Add(initWith);
        }
    }
    internal static void InitFillWith<T>(List<T> arr, int columns)
    {
        for (int i = 0; i < columns; i++)
        {
            arr.Add(default);
        }
    }

    internal static List<string> TrimStart(string backslash, List<string> s)
    {
        string methodName = "TrimStart";

        ThrowEx.IsNull("backslash", backslash);
        ThrowEx.IsNull("s", s);

        for (int i = 0; i < s.Count; i++)
        {
            if (s[i].StartsWith(backslash))
            {
                s[i] = s[i].Substring(backslash.Length);
            }
        }
        return s;
    }

    internal static List<string> Trim(List<string> l)
    {
        for (var i = 0; i < l.Count; i++) l[i] = l[i].Trim();

        return l;
    }

    internal static List<string> TrimList(List<string> c)
    {
        for (int i = 0; i < c.Count; i++)
        {
            c[i] = c[i].Trim();
        }
        return c;
    }

    internal static string ReplaceAll(string r, List<string> what, string forWhat)
    {
        foreach (var item in what)
        {
            r = r.Replace(item, forWhat);
        }

        return r;
    }
    internal static List<string> WithoutDiacritic(List<string> nazev)
    {
        for (int i = 0; i < nazev.Count; i++)
        {
            nazev[i] = nazev[i].RemoveDiacritics();
        }
        return nazev;
    }
    internal static List<string> RemoveStringsEmpty(List<string> mySites)
    {
        for (int i = mySites.Count - 1; i >= 0; i--)
        {
            if (mySites[i] == string.Empty)
            {
                mySites.RemoveAt(i);
            }
        }
        return mySites;
    }

    internal static bool IsThereAnotherIndex(char[] ch, int i)
    {
        if (ch.Length >= i)
        {
            return true;
        }
        return false;
    }
    internal static bool IsSomethingTheSame(string ext, IList<string> p1, ref string contained)
    {
        foreach (var item in p1)
        {
            if (item == ext)
            {
                contained = item;
                return true;
            }
        }
        return false;
    }
    internal static List<byte> JoinBytesArray(byte[] pass, byte[] salt)
    {
        List<byte> lb = new List<byte>(pass.Length + salt.Length);
        lb.AddRange(pass);
        lb.AddRange(salt);
        return lb;
    }
}