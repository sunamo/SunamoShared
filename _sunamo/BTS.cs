using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoShared;
public class BTS
{
    public static bool IntToBool(int v)
    {
        return Convert.ToBoolean(v);
    }

    public static int ParseInt(string entry, int _default)
    {
        //entry = SHSH.FromSpace160To32(entry);
        entry = entry.Replace(" ", string.Empty);
        //var ch = entry[3];

        int lastInt2 = 0;
        if (int.TryParse(entry, out lastInt2))
        {
            return lastInt2;
        }
        return _default;
    }
}
