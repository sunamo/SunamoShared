namespace SunamoShared.Storage;

public class FileNameWithDateTime : FileNameWithDateTimeTU<string, string>
{

    public FileNameWithDateTime(string row1, string row2) : base(row1, row2, null)
    {
    }
}
