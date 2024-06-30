namespace SunamoShared;

public class SmtpHelper
{
    public static int ParsePort(string s)
    {
        return BTS.ParseInt(s, NumConsts.defaultPortIfCannotBeParsed);
    }
}
