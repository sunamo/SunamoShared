namespace SunamoShared;


public class PowershellHelperWrapper : IPowershellHelper
{
    public static IPowershellHelper p = null;
    public static PowershellHelperWrapper ci = new PowershellHelperWrapper();

    private PowershellHelperWrapper()
    {

    }

    /// <summary>
    /// Musí být string protože je ve třídě PowershellHelper. 
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public
#if ASYNC
    async Task
#else
    void  
#endif
 CmdC(string v, Func<bool, ITextBuilder> ciTextBuilder)
    {

#if ASYNC
        await
#endif
     p.CmdC(v, ciTextBuilder);
    }

    public
#if ASYNC
    async Task<string>
#else
    string  
#endif
 DetectLanguageForFileGithubLinguist(string windowsPath)
    {
        return
#if ASYNC
    await
#endif
 p.DetectLanguageForFileGithubLinguist(windowsPath);
    }

    public List<string> ProcessNames()
    {
        return p.ProcessNames();
    }
}
