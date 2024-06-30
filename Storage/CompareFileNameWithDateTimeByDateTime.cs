namespace SunamoShared;

public class CompareFileNameWithDateTimeByDateTime<StorageFolder, StorageFile> : ISunamoComparer<FileNameWithDateTimeTU<StorageFolder, StorageFile>>
{
    public int Desc(FileNameWithDateTimeTU<StorageFolder, StorageFile> x, FileNameWithDateTimeTU<StorageFolder, StorageFile> y)
    {
        return x.dt.CompareTo(y.dt) * -1;
    }
    public int Asc(FileNameWithDateTimeTU<StorageFolder, StorageFile> x, FileNameWithDateTimeTU<StorageFolder, StorageFile> y)
    {
        return x.dt.CompareTo(y.dt);
    }
}
