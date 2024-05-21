namespace SunamoShared;
public class LazyGetCommonSettings : LazyString
{
    public LazyGetCommonSettings(string key, Func<string, bool, string> AppDataGetCommonSettings) : base(AppDataGetCommonSettings, key)
    {

    }
}
