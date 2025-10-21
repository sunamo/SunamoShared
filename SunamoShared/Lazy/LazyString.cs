// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.Lazy;
public class LazyString : LazyT<string>
{
    public LazyString(Func<string, bool, string> getCommonSettings, string key) : base(getCommonSettings, key)
    {

    }
}
