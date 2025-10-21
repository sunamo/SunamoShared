// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared._sunamo;

internal class ConvertDateTimeToFileNamePostfix
{
    private static char s_delimiter = '_';

    internal static string ToConvention(string postfix, DateTime dt, bool time)
    {
        //postfix = SHReplace.ReplaceAll(postfix, "", "_");
        return DTHelper.DateTimeToFileName(dt, time) + s_delimiter + postfix;
    }
}