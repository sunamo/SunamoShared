// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.Storage;

public class FileNameWithDateTime : FileNameWithDateTimeTU<string, string>
{

    public FileNameWithDateTime(string row1, string row2) : base(row1, row2, null)
    {
    }
}
