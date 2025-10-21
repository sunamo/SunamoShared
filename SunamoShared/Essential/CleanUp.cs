// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.Essential;

public static class CleanUp
{
    public static void Streams(Stream stream, FileStream fileStream)
    {
        if (stream != null)
        {
            stream.Dispose();
        }
        if (fileStream != null)
        {
            fileStream.Dispose();
        }
    }
}
