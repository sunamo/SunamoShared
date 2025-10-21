// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoShared.Helpers;
public class GridHelperSunamo
{
    /// <summary>
    /// After can be use with XamlGeneratorDesktop.Write*Definitions 
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    public static List<string> ForAllTheSame(int columns)
    {
        List<string> result = new List<string>(columns);
        var data = 100d / columns;
        for (int i = 0; i < columns; i++)
        {
            result.Add(data + "*");
        }

        return result;
    }
}
