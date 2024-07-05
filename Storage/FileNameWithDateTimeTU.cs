namespace SunamoShared.Storage;

public class FileNameWithDateTimeTU<StorageFolder, StorageFile>
{
    /// <summary>
    /// I'm in standard, not uwp, therefore I cant point to MainPage
    /// </summary>
    AbstractCatalogShared<StorageFolder, StorageFile> ac;
    public DateTime dt = DateTime.MinValue;
    public string name = "";
    /// <summary>
    /// Pokud bude vyplněná, nebude se používat čas, který i tak bude uložen v proměnné dt
    /// </summary>
    public int? serie = null;
    public string fnwoe = "";
    public int SerieValue
    {
        get
        {
            return serie.Value;
        }
    }
    private string _displayText = null;
    private string _row1 = string.Empty;
    private string _row2 = string.Empty;

    /// <summary>
    /// Create instance with CreateObjectFileNameWithDateTime<StorageFolder, StorageFile>
    /// Both can be SE, is used in dispalyText
    /// </summary>
    /// <param name="row1"></param>
    /// <param name="row2"></param>
    public FileNameWithDateTimeTU(string row1, string row2, AbstractCatalogShared<StorageFolder, StorageFile> ac)
    {
        _displayText = row1 + AllStrings.space + row2;
        _row1 = row1;
        _row2 = row2;
        this.ac = ac;
    }

    /// <summary>
    /// First row in SelectorHelperListViewUC
    /// </summary>
    public string Row1 { get { return _row1; } set { _row1 = value; } }
    /// <summary>
    /// Second row in SelectorHelperListViewUC
    /// </summary>
    public string Row2 { get { return _row2; } set { _row2 = value; } }
    public override string ToString()
    {
        return _displayText;
    }
}
