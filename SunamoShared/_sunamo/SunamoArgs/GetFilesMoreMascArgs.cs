namespace SunamoShared._sunamo.SunamoArgs;

internal class GetFilesMoreMascArgs : GetFilesBaseArgsShared
{
    internal bool LoadFromFileWhenDebug = false;
    internal string path;
    internal string masc = "*";
    internal SearchOption searchOption = SearchOption.TopDirectoryOnly;
    internal bool deleteFromDriveWhenCannotBeResolved = false;
}