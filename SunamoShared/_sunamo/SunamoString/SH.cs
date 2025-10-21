// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoShared._sunamo.SunamoString;

internal class SH
{
    #region SH.FirstCharUpper


    #endregion

    internal static string FirstCharUpper(ref string result)
    {
        result = SH.FirstCharUpper(result);

        return result;
    }

    internal static string FirstCharUpper(string nazevPP, bool only = false)
    {
        if (nazevPP != null)
        {
            var substring = nazevPP.Substring(1);
            if (only) substring = substring.ToLower();

            return nazevPP[0].ToString().ToUpper() + substring;
        }

        return null;
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

    internal static bool ContainsBracket(string temp, ref List<char> left, ref List<char> right, bool mustBeLeftAndRight = false)
    {
        left = SH.ContainsAnyChar(temp, false, AllLists.leftBrackets);
        right = SH.ContainsAnyChar(temp, false, AllLists.leftBrackets);
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
            var temp = (Func<int, bool>)o;
            return temp(nt);
        }

        // nemůže tu být protože SunamoData musí dědit od SunamoStringShared - hodně metod *. 
        //if (o is FromToList)
        //{
        //    var result = (FromToList)o;
        //    return result.IsInRange(nt);
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



    internal static string TextWithoutDiacritic(string v)
    {
        return v.RemoveDiacritics();
    }


}