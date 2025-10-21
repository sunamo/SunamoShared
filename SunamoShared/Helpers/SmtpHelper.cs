// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.Helpers;

public class SmtpHelper
{
    public static int ParsePort(string s)
    {
        return BTS.ParseInt(s, NumConsts.defaultPortIfCannotBeParsed);
    }
}
