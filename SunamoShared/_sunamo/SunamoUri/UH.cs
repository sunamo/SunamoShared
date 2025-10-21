// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared._sunamo.SunamoUri;
internal class UH
{



    internal static string UrlEncode(string co)
    {
        return WebUtility.UrlEncode(co.Trim());
    }

}
