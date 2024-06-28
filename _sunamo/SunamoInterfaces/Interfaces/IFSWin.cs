namespace SunamoShared;


internal interface IFSWin
{
    void DeleteFileMaybeLocked(string s);
    void DeleteFileOrFolderMaybeLocked(string p);
}