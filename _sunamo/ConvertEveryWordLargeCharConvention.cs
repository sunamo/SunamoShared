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
        return new List<char>([AllCharsSE.bs, AllCharsSE.lb, AllCharsSE.rb, AllCharsSE.rsqb, AllCharsSE.lsqb, AllCharsSE.dot, AllCharsSE.apostrophe]).Any(d => d == item); //CAGSH.IsEqualToAnyElement<char>(item, );
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
                    sb.Append(AllCharsSE.space);
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
                            sb.Append(AllCharsSE.space);
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
                    sb.Append(AllCharsSE.space);
                    continue;
                }
            }
            if (char.IsUpper(item))
            {
                if (!char.IsUpper(sb[sb.Length - 1]))
                {
                    sb.Append(AllCharsSE.space);
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
                sb.Append(AllCharsSE.space);
                dalsiVelke = true;
            }
        }
        string vr = sb.ToString().Trim();

        vr = vr.Replace("  ", " "); //SHReplace.ReplaceAll(vr, AllStringsSE.space, AllStringsSE.doubleSpace);
        return vr;
    }
}
