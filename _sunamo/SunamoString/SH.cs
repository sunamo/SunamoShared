namespace SunamoShared._sunamo.SunamoString;

internal class SH
{
    #region SH.FirstCharUpper
    internal static void FirstCharUpper(ref string nazevPP)
    {
        nazevPP = FirstCharUpper(nazevPP);
    }


    internal static string FirstCharUpper(string nazevPP)
    {
        if (nazevPP.Length == 1)
        {
            return nazevPP.ToUpper();
        }

        string sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToUpper() + sb;
    }
    #endregion


    internal static string JoinNL(List<string> l)
    {
        StringBuilder sb = new();
        foreach (var item in l) sb.AppendLine(item);
        var r = string.Empty;
        r = sb.ToString();
        return r;
    }


    internal static string FirstCharLower(string nazevPP)
    {
        if (nazevPP.Length < 2) return nazevPP;
        var sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToLower() + sb;
    }
    /// <summary>
    ///     Convert \r\n to NewLine etc.
    /// </summary>
    /// <param name="delimiter"></param>
    internal static string ConvertTypedWhitespaceToString(string delimiter)
    {
        const string nl = @"
";
        switch (delimiter)
        {
            // must use \r\n, not Environment.NewLine (is not constant)
            case "\\r\\n":
            case "\\n":
            case "\\r":
                return nl;
            case "\\t":
                return "\t";
        }
        return delimiter;
    }
    /// <summary>
    ///     Musí tu být. split z .net vrací []
    ///     krom toho je instanční. musel bych měnit hodně kódu kvůli toho
    /// </summary>
    /// <param name="s"></param>
    /// <param name="dot"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    internal static List<string> SplitCharMore(string s, params char[] dot)
    {
        return s.Split(dot, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
    internal static List<string> SplitMore(string s, params string[] dot)
    {
        return s.Split(dot, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
    internal static List<string> SplitNone(string text, params string[] deli)
    {
        return text.Split(deli, StringSplitOptions.None).ToList();
    }
    /// <summary>
    ///     Usage: BadFormatOfElementInList
    ///     If null, return "(null)"
    ///     nemůžu odstranit z sunamo, i tam se používá.
    /// </summary>
    /// <param name="n"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    internal static string NullToStringOrDefault(object n, string v)
    {
        throw new Exception(
            "Tahle metoda vypadala jinak ale jak idiot jsem ji změnil. Tím jak jsem poté přesouval metody tam zpět už je těžké se k tomu dostat.");
        return null;
        //return n == null ? " " + "(null)" : "" + v.ToString();
    }
    /// <summary>
    ///     Usage: BadFormatOfElementInList
    ///     If null, return "(null)"
    ///     jsem
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    internal static string NullToStringOrDefault(object n)
    {
        //return NullToStringOrDefault(n, null);
        return n == null ? " " + "(null)" : " " + n;
    }
    /// <summary>
    ///     Usage: Exceptions.MoreCandidates
    ///     není v .net (pouze char), přes split to taky nedává smysl (dá se to udělat i s .net ale bude to pomalejší)
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ext"></param>
    /// <returns></returns>
    internal static string TrimEnd(string name, string ext)
    {
        while (name.EndsWith(ext)) return name.Substring(0, name.Length - ext.Length);
        return name;
    }


    internal static List<char> ContainsAnyChar(/*T itemT,*/  /*IList<T> containsT,*/
        string item, bool checkInCaseOnlyOneString, IList<char> contains)
    {
        List<char> founded = new List<char>();
        if (contains.Count() == 1 && checkInCaseOnlyOneString)
        {
            item.Contains(contains.First());
        }
        else
        {
            foreach (var c in contains)
            {
                if (item.Contains(c))
                {
                    founded.Add(c);
                }
            }
        }

        return founded;
    }

    internal static bool ContainsBracket(string t, ref List<char> left, ref List<char> right, bool mustBeLeftAndRight = false)
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

    protected static Dictionary<BracketsShared, char> bracketsLeft = null;
    protected static Dictionary<BracketsShared, char> bracketsRight = null;

    protected static List<char> bracketsLeftList = null;
    protected static List<char> bracketsRightList = null;

    protected static void Init()
    {
        if (bracketsLeft == null)
        {
            bracketsLeft = new Dictionary<BracketsShared, char>();
            bracketsLeft.Add(BracketsShared.Curly, '{');
            bracketsLeft.Add(BracketsShared.Square, '[');
            bracketsLeft.Add(BracketsShared.Normal, '(');
            bracketsLeftList = Enumerable.ToList<char>(bracketsLeft.Values);

            bracketsRight = new Dictionary<BracketsShared, char>();
            bracketsRight.Add(BracketsShared.Curly, '}');
            bracketsRight.Add(BracketsShared.Square, ']');
            bracketsRight.Add(BracketsShared.Normal, ')');

            bracketsRightList = Enumerable.ToList<char>(bracketsRight.Values);

        }
    }

    internal static char ClosingBracketFor(char v)
    {
        foreach (var item in bracketsLeft)
        {
            if (item.Value == v)
            {
                return bracketsRight[item.Key];
            }
        }

        ThrowEx.IsNotAllowed(v + " as bracket");
        return char.MaxValue;
    }
    internal static string GetTextBetween(string p, char after, char before, bool throwExceptionIfNotContains = true /*cant have implicit value*/, object notAllowedInRanges = null /*cant have implicit value*/, bool endLastIndexOf = false)
    {
        return GetTextBetweenTwoChars(p, after, before, throwExceptionIfNotContains, notAllowedInRanges, endLastIndexOf);
    }

    internal static bool NotAllowedInRanges(object o, int nt)
    {
        if (o is Func<int, bool>)
        {
            var t = (Func<int, bool>)o;
            return t(nt);
        }

        // nemůže tu být protože SunamoData musí dědit od SunamoStringShared - hodně metod *. 
        //if (o is FromToList)
        //{
        //    var r = (FromToList)o;
        //    return r.IsInRange(nt);
        //}

        ThrowEx.NotImplementedCase("NotAllowedInRanges: " + o);
        return false;
    }

    internal static string GetTextBetweenTwoChars(string p, char beginS, char endS, bool throwExceptionIfNotContains = true, object notAllowedInRanges = null, bool endLastIndexOf = false)
    {
        int num = p.IndexOf(beginS);
        int num2 = -1;
        if (endLastIndexOf)
        {
            num2 = p.LastIndexOf(endS);
        }
        else
        {
            num2 = p.IndexOf(endS, num + 1);
            if (notAllowedInRanges != null)
            {
                while (num2 != -1 && NotAllowedInRanges(notAllowedInRanges, num2))
                {
                    num2 = p.IndexOf(endS, num2 + 1);
                }
            }
        }

        if (num == -1 || num2 == -1)
        {
            if (throwExceptionIfNotContains)
            {
                ThrowEx.NotContains(p, beginS.ToString(), endS.ToString());
            }
            else if (num2 == -1)
            {
                return null;
            }

            return p;
        }

        return GetTextBetweenTwoCharsInts(p, num, num2);
    }

    internal static string GetTextBetweenTwoCharsInts(string p, int begin, int end)
    {
        if (end > begin)
            // a(1) - 1,3
            return p.Substring(begin + 1, end - begin - 1);
        // originally
        //return p.Substring(begin+1, end - begin - 1);
        return p;
    }
    internal static string WrapWith(string value, string h)
    {
        return h + value + h;
    }

    internal static string WrapWithQm(string value)
    {
        var h = "\"";
        return h + value + h;
    }

    internal static string AppendIfDontEndingWith(string text, string append)
    {
        if (text.EndsWith(append))
        {
            return text;
        }
        return text + append;
    }

    internal static string TextWithoutDiacritic(string v)
    {
        return v.RemoveDiacritics();
    }


}