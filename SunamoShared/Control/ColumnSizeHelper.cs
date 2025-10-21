// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.Control;

public class ColumnSizeHelper
{
    static Type type = typeof(ColumnSizeHelper);

    /// <summary>
    /// Sum all non-zero elements of A1 with A2
    /// </summary>
    /// <param name="d"></param>
    /// <param name="zmenaO"></param>
    public static List<double> CalculateWidthOfColumnsAgain(List<double> d, double zmenaO)
    {
        if (zmenaO == 0)
        {
            throw new Exception(Translate.FromKey(XlfKeys.ParameterZmenaOOfMethodColumnSizeHelperCalculateWidthOfColumnsAgainHasValue) + " ");
        }

        zmenaO /= d.Count;
        for (int i = 0; i < d.Count; i++)
        {
            if (d[i] != 0)
            {
                d[i] += zmenaO;
            }
        }

        return d;
    }
}
