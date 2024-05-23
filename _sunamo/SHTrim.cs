using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoShared;
public class SHTrim
{
    public static bool TrimIfStartsWith(ref string s, string p)
    {
        if (s.StartsWith(p))
        {
            s = s.Substring(p.Length);
            return true;
        }
        return false;
    }
}
