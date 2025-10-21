// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// Instance variables refactored according to C# conventions
namespace SunamoShared.Lazy;

public class LazyT<T>
{
    private Func<string, bool, T> getCommonSettings;
    private string argument;
    T value = default;

    public T Value
    {
        get
        {
            if (EqualityComparer<T>.Default.Equals(value, default))
            {
                value = getCommonSettings(argument, true);
            }
            return value;
        }
    }

    public LazyT(Func<string, bool, T> getCommonSettings, string pwUsersScz)
    {
        this.getCommonSettings = getCommonSettings;
        argument = pwUsersScz;
    }
}
