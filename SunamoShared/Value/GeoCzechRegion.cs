// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.Value;

public class GeoCzechRegion
{
    public GeoCzechRegion(string shortcutRZ, string name, string shortcutCSU, string mainCity)
    {
        ShortcutRZ = shortcutRZ;
        ShortcutCSU = shortcutCSU;
        Name = name;
        MainCity = mainCity;
    }

    public string ShortcutRZ { get; set; }
    public string ShortcutCSU { get; set; }
    public string Name { get; set; }
    public string MainCity { get; set; }
}
