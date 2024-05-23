namespace SunamoShared;


public interface IFSWin
{
    void DeleteFileMaybeLocked(string s);
    void DeleteFileOrFolderMaybeLocked(string p);
}