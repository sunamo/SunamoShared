using Diacritics.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoShared;
public class SH
{
    #region SH.FirstCharUpper
    public static void FirstCharUpper(ref string nazevPP)
    {
        nazevPP = FirstCharUpper(nazevPP);
    }


    public static string FirstCharUpper(string nazevPP)
    {
        if (nazevPP.Length == 1)
        {
            return nazevPP.ToUpper();
        }

        string sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToUpper() + sb;
    }
    #endregion

    public static List<char> ContainsAnyChar(/*T itemT,*/  /*IList<T> containsT,*/
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
            bracketsLeftList = bracketsLeft.Values.ToList();

            bracketsRight = new Dictionary<BracketsShared, char>();
            bracketsRight.Add(BracketsShared.Curly, '}');
            bracketsRight.Add(BracketsShared.Square, ']');
            bracketsRight.Add(BracketsShared.Normal, ')');

            bracketsRightList = bracketsRight.Values.ToList();

        }
    }

    public static char ClosingBracketFor(char v)
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
    public static string GetTextBetween(string p, char after, char before, bool throwExceptionIfNotContains = true /*cant have implicit value*/, object notAllowedInRanges = null /*cant have implicit value*/, bool endLastIndexOf = false)
    {
        return GetTextBetweenTwoChars(p, after, before, throwExceptionIfNotContains, notAllowedInRanges, endLastIndexOf);
    }

    public static bool NotAllowedInRanges(object o, int nt)
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

    public static string GetTextBetweenTwoChars(string p, char beginS, char endS, bool throwExceptionIfNotContains = true, object notAllowedInRanges = null, bool endLastIndexOf = false)
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

    public static string GetTextBetweenTwoCharsInts(string p, int begin, int end)
    {
        if (end > begin)
            // a(1) - 1,3
            return p.Substring(begin + 1, end - begin - 1);
        // originally
        //return p.Substring(begin+1, end - begin - 1);
        return p;
    }
    public static string WrapWith(string value, string h)
    {
        return h + value + h;
    }

    public static string WrapWithQm(string value)
    {
        var h = "\"";
        return h + value + h;
    }

    public static string AppendIfDontEndingWith(string text, string append)
    {
        if (text.EndsWith(append))
        {
            return text;
        }
        return text + append;
    }

    public static string TextWithoutDiacritic(string v)
    {
        return v.RemoveDiacritics();
    }
}
