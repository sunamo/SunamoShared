namespace SunamoShared._sunamo.SunamoInterfaces.Interfaces;


internal interface IFSWin
{
    void DeleteFileMaybeLocked(string s);
    void DeleteFileOrFolderMaybeLocked(string p);
}