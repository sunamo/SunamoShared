namespace SunamoShared.Helpers;

public class SmtpHelper
{
    public static int ParsePort(string s)
    {
        return BTS.ParseInt(s, NumConsts.defaultPortIfCannotBeParsed);
    }
}
