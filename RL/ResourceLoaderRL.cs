namespace SunamoShared.RL;
public class ResourceLoaderRL
{
    public string GetString(string k)
    {
        return Translate.FromKey(k);
    }
}
