namespace SunamoShared.Helpers;

public class LocaleHelper
{
    public static void Init()
    {
        foreach (var item in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {

        }
    }

    /// <summary>
    /// Its not good idea because for en return AG
    /// Must use GetCountryForLang2
    /// </summary>
    /// <param name="lang"></param>
    /// <returns></returns>
    public static string GetCountryForLang(string lang)
    {
        lang = lang.ToLower();
        foreach (var item in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {
            var parameter = SHSplit.Split(item.Name, "-");
            if (parameter.Count > 1)
            {
                if (parameter[0] == lang)
                {
                    if (parameter[1].Length == 2)
                    {
                        ComplexInfoString cis = new ComplexInfoString(parameter[1]);
                        if (cis.QuantityUpperChars == 2)
                        {
                            // Its not good idea because for en return AG
                            return parameter[1];
                        }
                    }

                }
            }
        }
        return null;
    }
}