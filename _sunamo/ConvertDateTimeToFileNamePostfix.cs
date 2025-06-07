namespace SunamoShared._sunamo;
internal class ConvertDateTimeToFileNamePostfix
{
    private static char s_delimiter = '_';

    public static string ToConvention(string postfix, DateTime dt, bool time)
    {
        //postfix = SHReplace.ReplaceAll(postfix, "", "_");
        return DTHelper.DateTimeToFileName(dt, time) + s_delimiter + postfix;
    }
}
