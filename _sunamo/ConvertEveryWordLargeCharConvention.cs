using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoShared._sunamo;
internal class ConvertEveryWordLargeCharConvention
{
    private static bool IsSpecialChar(char item)
    {
        return new List<char>([AllChars.bs, AllChars.lb, AllChars.rb, AllChars.rsqb, AllChars.lsqb, AllChars.dot, AllChars.apostrophe]).Any(d => d == item); //CAGSH.IsEqualToAnyElement<char>(item, );
    }

    internal static string ToConvention(string p)
    {
        p = p.ToLower();
        StringBuilder sb = new StringBuilder();
        bool dalsiVelke = true;
        foreach (char item in p)
        {
            if (dalsiVelke)
            {
                if (char.IsUpper(item))
                {
                    dalsiVelke = false;
                    sb.Append(AllChars.space);
                    sb.Append(item);
                    continue;
                }
                else if (char.IsLower(item))
                {
                    dalsiVelke = false;
                    if (sb.Length != 0)
                    {
                        if (!IsSpecialChar(sb[sb.Length - 1]))
                        {
                            sb.Append(AllChars.space);
                        }
                    }
                    sb.Append(char.ToUpper(item));
                    continue;
                }
                else if (IsSpecialChar(item))
                {
                    sb.Append(item);
                    continue;
                }
                else if (char.IsDigit(item))
                {
                    sb.Append(item);
                    continue;
                }
                else
                {
                    sb.Append(AllChars.space);
                    continue;
                }
            }
            if (char.IsUpper(item))
            {
                if (!char.IsUpper(sb[sb.Length - 1]))
                {
                    sb.Append(AllChars.space);
                }
                sb.Append(item);
            }
            else if (char.IsLower(item))
            {
                sb.Append(item);
            }
            else if (char.IsDigit(item))
            {
                dalsiVelke = true;
                sb.Append(item);
                continue;
            }
            else if (IsSpecialChar(item))
            {
                sb.Append(item);
                continue;
            }
            else
            {
                sb.Append(AllChars.space);
                dalsiVelke = true;
            }
        }
        string vr = sb.ToString().Trim();

        vr = vr.Replace("  ", " "); //SHReplace.ReplaceAll(vr, AllStrings.space, AllStrings.doubleSpace);
        return vr;
    }
}
