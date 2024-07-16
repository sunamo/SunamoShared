namespace SunamoShared.RL;

/// <summary>
/// Whole class copied from apps - want to use RL in any type of my apps
/// </summary>
public class RL
{
    private static Type type = typeof(RL);

    /// <summary>
    /// Pokud chceš používat tuto třídu, musíš zároveň prvně zavolat RL.Initialize()
    /// </summary>
    private static class Xaml
    {
        public static byte lid = 1;

        public static void Initialize(LangsShared l)
        {
            lid = (byte)l;
        }
    }

    /// <summary>
    /// Globální proměnná pro nastavení jazyka celé app
    /// Musí to být výčet protože aplikace může mít více jazyků
    /// </summary>
    public static LangsShared l
    {
        set
        {
            ThisApp.l = value;
        }
        get
        {
            return ThisApp.l;
        }
    }

    public static IResourceHelper loader = null;
    private static int s_langsLength = 0;

    static RL()
    {
        s_langsLength = Enum.GetValues(typeof(LangsShared)).Length;
    }

    public static void Initialize(LangsShared l)
    {
        RL.l = l;
        AppLangHelperShared.currentCulture = new CultureInfo(l.ToString());
        AppLangHelperShared.currentUICulture = new CultureInfo(l.ToString());
    }

    /// <summary>
    /// Dont use, only throw exception
    /// In desktop app will be use in smaller uses
    /// Primary is for web apps where is language chaning with every user
    /// </summary>
    /// <param name="v"></param>
    /// <param name="cs"></param>
    public static string GetStringByLang(string v, LangsShared cs)
    {
        throw new Exception(sess.i18n(XlfKeys.InDesktopAppDontPassLangs));
        //if (l == Langs.en)
        //{
        //    return sess.i18n(k];
        //}
        //return RLData.cs[k];
        return null;
    }
}
