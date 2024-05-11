namespace SunamoShared;
public class LazyGetCommonSettings : LazyString
{
    public LazyGetCommonSettings(string key) : base(AppData.ci.GetCommonSettings, key)
    {

    }
}
