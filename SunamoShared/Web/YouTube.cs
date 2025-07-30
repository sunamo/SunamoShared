namespace SunamoShared.Web;

public static class YouTube
{
    /// <summary>
    /// G null pokud se YT kód nepodaří získat
    /// </summary>
    /// <param name = "uri"></param>
    public static string ParseYtCode(string uri)
    {

        Regex regex = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
        var match = regex.Match(uri);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        return null;
    }

    public static string GetLinkToVideo(string kod)
    {
        return "http://www.youtube.com/watch?v=" + kod;
    }

    public static string GetHtmlAnchor(string kod)
    {
        return "<a href='" + GetLinkToVideo(kod) + "'>" + kod + "</a>";
    }

    public static string GetLinkToSearch(string co)
    {
        return "http://www.youtube.com/results?search_query=" + UH.UrlEncode(co);
    }

    public static string ReplaceAll(string r, List<string> what, string forWhat)
    {
        foreach (var item in what)
        {
            r = r.Replace(item, forWhat);
        }

        return r;
    }
}
