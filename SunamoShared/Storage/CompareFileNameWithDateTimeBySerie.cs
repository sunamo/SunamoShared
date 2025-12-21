namespace SunamoShared.Storage;

public class CompareFileNameWithDateTimeBySerie<StorageFolder, StorageFile> : ISunamoComparer<FileNameWithDateTimeTU<StorageFolder, StorageFile>>
{
    public int Desc(FileNameWithDateTimeTU<StorageFolder, StorageFile> x, FileNameWithDateTimeTU<StorageFolder, StorageFile> y)
    {
        return x.SerieValue.CompareTo(y.SerieValue) * -1;
    }
    public int Asc(FileNameWithDateTimeTU<StorageFolder, StorageFile> x, FileNameWithDateTimeTU<StorageFolder, StorageFile> y)
    {
        return x.SerieValue.CompareTo(y.SerieValue);
    }
}