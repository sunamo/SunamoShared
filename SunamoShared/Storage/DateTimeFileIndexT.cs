// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.Storage;

public class DateTimeFileIndexT<StorageFolder, StorageFile>
{
    static Type type = typeof(DateTimeFileIndexT<StorageFolder, StorageFile>);
    public AbstractCatalogShared<StorageFolder, StorageFile> ac;
    public event Action<List<FileNameWithDateTimeTU<StorageFolder, StorageFile>>> InitComplete;
    private StorageFolder _folder = default;
    private string _ext = null;
    //SunamoDictionary<string, DateTime> dict = new SunamoDictionary<string, DateTime>();
    public List<FileNameWithDateTimeTU<StorageFolder, StorageFile>> files = new List<FileNameWithDateTimeTU<StorageFolder, StorageFile>>();
    private FileEntriesDuplicitiesStrategy _ds = FileEntriesDuplicitiesStrategy.Time;
    private LangsShared _l = LangsShared.cs;

#pragma warning disable
    public string GetFullPath(FileNameWithDateTimeTU<StorageFolder, StorageFile> o)
    {
        return null;
        //ThrowEx.Custom
        //return FS.StorageFilePath<StorageFolder, StorageFile>(FS.GetStorageFile<StorageFolder, StorageFile>(_folder, o.fnwoe + _ext, ac), ac);
    }
#pragma warning restore

    public DateTimeFileIndexT()
    {
        //Initialize(af, _ext, _ds);
    }
    /// <summary>
    /// A4 was nowhere used, deleted
    /// </summary>
    /// <param name="af"></param>
    /// <param name="ext"></param>
    /// <param name="ds"></param>
    public void Initialize(/*AppFoldersShared af, */string ext, FileEntriesDuplicitiesStrategy ds, AbstractCatalogShared<StorageFolder, StorageFile> ac)
    {
        this.ac = ac;
        _ds = ds;
        //_folder = FS.CiStorageFolder<StorageFolder, StorageFile>(AppData.ci.GetFolder(af), null);
        _ext = ext;
        string mask = "????_??_??_";
        if (ds == FileEntriesDuplicitiesStrategy.Serie)
        {
            mask += "S_?*_";
        }
        else if (ds == FileEntriesDuplicitiesStrategy.Time)
        {
            mask += "??_??_";
        }
        else
        {
            throw new Exception(Translate.FromKey(XlfKeys.NotSupportedStrategyOfSavingFiles) + ".");
        }
        mask += "*" + ext;
        List<StorageFile> files2 = null; // Directory.GetFilesInterop(_folder, mask, false, ac);
        foreach (var item in files2)
        {
            //var itemS = FS.StorageFilePath<StorageFolder, StorageFile>(item, ac);
            //files.Add(CreateObjectFileNameWithDateTime(string.Empty, string.Empty, itemS, ac));
        }
        if (ds == FileEntriesDuplicitiesStrategy.Serie)
        {
            files.Sort(new CompareFileNameWithDateTimeBySerie<StorageFolder, StorageFile>().Desc);
        }
        files.Sort(new CompareFileNameWithDateTimeByDateTime<StorageFolder, StorageFile>().Desc);
        if (InitComplete != null)
        {
            InitComplete(files);
        }
    }
    private FileEntriesDuplicitiesStrategy GetFileEntriesDuplicitiesStrategy(string fnwoe, out int? serie, out int hour, out int minute, out string postfix)
    {
        serie = null;
        minute = hour = 0;
        if (fnwoe[11] == 'S')
        {
            var parts = SHSplit.Split(fnwoe, "_");
            serie = int.Parse(parts[4]);
            postfix = SHJoin.JoinFromIndex(5, "_", parts);
            return FileEntriesDuplicitiesStrategy.Serie;
        }
        else
        {
            string t = fnwoe.Substring(11, 5);
            var parts = SHSplit.Split(t, "_");
            hour = int.Parse(parts[0]);
            minute = int.Parse(parts[1]);
            postfix = SHJoin.JoinFromIndex(5, "_", parts);
            return FileEntriesDuplicitiesStrategy.Time;
        }
    }
    public FileNameWithDateTimeTU<StorageFolder, StorageFile> CreateObjectFileNameWithDateTime(string row1, string row2, AbstractCatalogShared<StorageFolder, StorageFile> ac)
    {
        FileNameWithDateTimeTU<StorageFolder, StorageFile> add = new FileNameWithDateTimeTU<StorageFolder, StorageFile>(row1, row2, ac);
        // Here must return c#
        string fnwoe = null; // Path.GetFileNameWithoutExtension<string, string>(item, null);
        #region Copy for inspire
        //2019_03_23_S_0_Pustkovec
        string dateS = fnwoe.Substring(0, 10);
        add.dt = DateTime.ParseExact(dateS, "yyyy_MM_dd", null);
        int? serie;
        int hour;
        int minute;
        string postfix;
        var strategy = GetFileEntriesDuplicitiesStrategy(fnwoe, out serie, out hour, out minute, out postfix);
        add.serie = serie;
        add.dt.AddMinutes(minute);
        add.dt.AddHours(hour);
        add.name = postfix;
        add.fnwoe = DeleteWrongCharsInFileName(fnwoe);
        add.Row1 = postfix;
        add.Row2 = add.dt.ToShortDateString();
        #endregion
        return add;
    }
    private static string GetDisplayText(DateTime date, int? serie, LangsShared l)
    {
        string displayText;
        if (serie == null)
        {
            displayText = DTHelper.DateTimeToString(date, l, new(1900, 1, 1));
        }
        else
        {
            int ser = serie.Value;
            string addSer = "";
            if (ser != 0)
            {
                addSer = " (" + ser + ")";
            }
            displayText = DTHelper.DateToString(date, l) + addSer;
        }
        return displayText;
    }
    private FileNameWithDateTimeTU<StorageFolder, StorageFile> CreateObjectFileNameWithDateTime(string row1, string row2, DateTime date, int? serie, string postfix, string fnwoe)
    {
        FileNameWithDateTimeTU<StorageFolder, StorageFile> add = new FileNameWithDateTimeTU<StorageFolder, StorageFile>(row1, row2, ac);
        add.dt = date;
        add.serie = serie;
        add.name = postfix;
        add.fnwoe = DeleteWrongCharsInFileName(fnwoe);
        return add;
    }
    private string DeleteWrongCharsInFileName(string fnwoe)
    {
        return SHReplace.ReplaceAll(FS.DeleteWrongCharsInFileName(fnwoe, false), "_", "");
    }
    public void DeleteFile(FileNameWithDateTimeTU<StorageFolder, StorageFile> o)
    {
        try
        {
            string t = GetStorageFile(o);
            //File.Delete(t);
            FS.TryDeleteFile(t);
            files.Remove(o);
        }
        catch (Exception ex)
        {
            //ThisApp.Error(Translate.FromKey(XlfKeys.FileCannotBeDeleted) + "" + Exceptions.TextOfExceptions(ex));
        }
    }

#pragma warning disable
    public string GetStorageFile(FileNameWithDateTimeTU<StorageFolder, StorageFile> o)
    {
        return null;
        //return FS.StorageFilePath<StorageFolder, StorageFile>(FS.GetStorageFile(_folder, o.fnwoe + _ext, ac), ac);
        //return Path.Combine(folder, o.fnwoe + ext);
    }
#pragma warning restore

    /// <summary>
    /// Zapíše soubor FileEntriesDuplicitiesStrategy se strategií specifikovanou v konstruktoru
    /// Nepřidává do kolekce files, vrací objekt
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="name"></param>
    public async Task<FileNameWithDateTimeTU<StorageFolder, StorageFile>> SaveFileWithDate(string name, string content)
    {
        //ThrowEx.NotImplementedMethod();
        //return null;
        DateTime dt = DateTime.Now;
        DateTime today = DateTime.Today;
        string fnwoe = "";
        int? max = null;
        if (_ds == FileEntriesDuplicitiesStrategy.Time)
        {
            fnwoe = ConvertDateTimeToFileNamePostfix.ToConvention(name, dt, true);
        }
        else if (_ds == FileEntriesDuplicitiesStrategy.Serie)
        {
            IList<int?> ml = files.Where(u => u.dt == today).Select(s => s.serie).ToList();
            max = ml.Count();
            if (max != 0)
            {
                max = ml.Max() + 1;
            }
            if (!max.HasValue)
            {
                max = 1;
            }
            fnwoe = DTHelper.DateTimeToFileName(dt, false) + "_S_" + max.Value + "_" + name;
        }
        else
        {
            // Zbytečné, kontroluje se již v konstruktoru
        }
        var storageFile = default(StorageFile); //FS.GetStorageFile<StorageFolder, StorageFile>(_folder, DeleteWrongCharsInFileName(fnwoe) + _ext, ac);
                                                //await File.WriteAllText<StorageFolder, StorageFile>(storageFile, content, ac);
#if DEBUG
        //DebugLogger.DebugWriteLine(storageFile.FullPath());
#endif
        var o = CreateObjectFileNameWithDateTime(GetDisplayText(dt, max, _l), name, dt, max, name, fnwoe);
        files.Add(o);
        return o;
    }
}