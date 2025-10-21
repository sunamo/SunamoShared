// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared._sunamo.SunamoArgs;


internal class GetFilesMoreMascArgs : GetFilesBaseArgsShared
{
    internal bool LoadFromFileWhenDebug = false;
    internal string path;
    internal string masc = "*";
    internal SearchOption searchOption = SearchOption.TopDirectoryOnly;
    internal bool deleteFromDriveWhenCannotBeResolved = false;
}