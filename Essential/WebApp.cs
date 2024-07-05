

namespace SunamoShared.Essential;



public class WebApp
{
    public static LangsShared l = LangsShared.en;
    public static ResourcesShared Resources;
    public static string Name;
    public static readonly bool initialized = false;
    public static string Namespace = "";
    public static event Action<TypeOfMessageShared, string> StatusSetted;



    public static void SetStatus(TypeOfMessageShared st, string status, params string[] args)
    {
        StatusSetted(st, string.Format(status, args));
    }
}
