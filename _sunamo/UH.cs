using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoShared;
public class UH
{
    public static string GetQueryAsHttpRequest(Uri uri)
    {
        return uri.Query;
    }

    public static string GetFileNameWithoutExtension(string p)
    {
        return Path.GetFileNameWithoutExtension(GetFileName(p));
    }

    public static string GetFileName(string fn)
    {
        return Path.GetFileName(fn);
    }

    public static string UrlEncode(string co)
    {
        return WebUtility.UrlEncode(co.Trim());
    }

    public static string GetPageNameFromUri(Uri uri)
    {
        int nt = uri.PathAndQuery.IndexOf(AllStrings.q);
        if (nt != -1)
        {
            return uri.PathAndQuery.Substring(0, nt);
        }
        return uri.PathAndQuery;
    }
}
