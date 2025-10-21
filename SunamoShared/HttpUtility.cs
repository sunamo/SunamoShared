// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared;

/// <summary>
///     Není ve sunamo, tím pádem nebudu dávat do NS
///     Třída byla vytvořena abych nemusel importovat System.Web pro metody jež nejsou v WebUtility
/// </summary>
public class HttpUtility
{
    public static NameValueCollection ParseQueryString(string queryString)
    {
        NameValueCollection queryParameters = new();
        var querySegments = queryString.Split('&');
        foreach (var segment in querySegments)
        {
            var parts = segment.Split('=');
            if (parts.Length > 0)
            {
                var key = parts[0].Trim('?', ' ');
                var val = parts[1].Trim();
                queryParameters.Add(key, val);
            }
        }

        return queryParameters;
    }


    public static string HtmlDecode(string v)
    {
        return WebUtility.HtmlDecode(v);
    }

    public static string HtmlEncode(string html)
    {
        return HtmlEncodeWithCompatibility(html);
    }

    public static string HtmlEncodeWithCompatibility(string html, bool backwardCompatibility = true)
    {
        if (html == null) throw new Exception("html");
        // replace & by &amp; but only once!
        var rx = backwardCompatibility
            ? new Regex("&(?!(amp;)|(lt;)|(gt;)|(quot;))", RegexOptions.IgnoreCase)
            : new Regex("&(?!(amp;)|(lt;)|(gt;)|(quot;)|(nbsp;)|(reg;))", RegexOptions.IgnoreCase);
        return rx.Replace(html, "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
    }

    public static string UrlEncode(string slnName)
    {
        return WebUtility.UrlEncode(slnName);
    }

    public static string UrlDecode(string v)
    {
        return WebUtility.UrlDecode(v);
    }
}