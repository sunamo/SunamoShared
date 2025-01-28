namespace SunamoShared._sunamo.SunamoUri;
internal class UH
{


    internal static string GetFileName(string fn)
    {
        return Path.GetFileName(fn);
    }

    internal static string UrlEncode(string co)
    {
        return WebUtility.UrlEncode(co.Trim());
    }

}
