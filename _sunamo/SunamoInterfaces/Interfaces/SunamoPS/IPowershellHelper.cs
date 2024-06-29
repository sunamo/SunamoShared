namespace SunamoShared;


internal interface IPowershellHelper
{
#if ASYNC
    Task
#else
void
#endif
    CmdC(string v);
#if ASYNC
    Task<string>
#else
string
#endif
    DetectLanguageForFileGithubLinguist(string windowsPath);
    List<string> ProcessNames();
}