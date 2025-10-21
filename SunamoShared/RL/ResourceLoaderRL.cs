// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.RL;
public class ResourceLoaderRL
{
    public string GetString(string k)
    {
        return Translate.FromKey(k);
    }
}
