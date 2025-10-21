// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.Lazy;
public class LazyGetCommonSettings : LazyString
{
    public LazyGetCommonSettings(string key, Func<string, bool, string> AppDataGetCommonSettings) : base(AppDataGetCommonSettings, key)
    {

    }
}
