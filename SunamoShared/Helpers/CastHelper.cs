// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoShared.Helpers;

public class CastHelper
{


    public static string ToString(object l)
    {
        var temp = l.GetType();
        if (temp == typeof(string))
        {
            return l.ToString();
        }
        else if (temp == typeof(List<string>))
        {
            return string.Join(Environment.NewLine, (List<string>)l);
        }
        else
        {
            ThrowEx.DoesntHaveRequiredType(nameof(l));
        }

        return null;
    }
}
