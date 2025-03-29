namespace SunamoShared.Interfaces;

public interface IResourceHelper
{
    string GetString(string name);
    Stream GetStream(string name);
}