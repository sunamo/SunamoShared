namespace SunamoShared.Generators;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
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