namespace SunamoShared.Helpers.Thread;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
/// <summary>
/// To not inicialize 
/// </summary>
public class FSThread
{
    public static string GetFileName(string n)
    {
        return Path.GetFileName(n);
    }
}