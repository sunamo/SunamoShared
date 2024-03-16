namespace SunamoShared._public;

public interface IResourceHelper
{
    string GetString(string name);
    Stream GetStream(string name);
}
