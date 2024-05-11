

namespace SunamoShared;



public class WebApp
{
    public static Langs l = Langs.en;
    public static ResourcesShared Resources;
    public static string Name;
    public static readonly bool initialized = false;
    public static string Namespace = "";
    public static event Action<TypeOfMessage, string> StatusSetted;



    public static void SetStatus(TypeOfMessage st, string status, params string[] args)
    {
        StatusSetted(st, string.Format(status, args));
    }
}
