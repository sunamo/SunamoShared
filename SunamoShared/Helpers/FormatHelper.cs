// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoShared.Helpers;

public class FormatHelper
{
    static Type type = typeof(FormatHelper);

    public static string parsedName = null;
    public static string parsedSurname = null;

    public static string FormatEmail(string nameSurname, string postfix)
    {
        var parameter = SHSplit.Split(nameSurname, " ");
        if (parameter.Count != 2)
        {
            ThrowEx.WrongNumberOfElements(2, "p", parameter);
        }
        parsedName = parameter[0];
        parsedSurname = parameter[1];
        return FormatEmail(parameter[0], parameter[1], postfix);
    }

    public static string FormatEmail(string name, string surname, string postfix)
    {
        return SH.TextWithoutDiacritic(name.ToLower()) + "." + SH.TextWithoutDiacritic(surname.ToLower()) + "@" + postfix.TrimStart('@');
    }
}