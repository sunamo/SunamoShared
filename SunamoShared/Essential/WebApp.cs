// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
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
