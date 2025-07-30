namespace SunamoShared.Generators;
public static class SpecialFolders
{
    public static string MyDocuments(string path)
    {
        return @"D:\Documents\" + path.TrimStart('\\');
    }

    public static string eMyDocuments(string path)
    {
        return @"E:\Documents\" + path.TrimStart('\\');
    }
}
