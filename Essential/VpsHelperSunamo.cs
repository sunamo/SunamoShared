namespace SunamoShared.Essential;
public class VpsHelperSunamo
{
    #region For easy copy
    public static bool IsQ
    => Environment.MachineName == "NRJANCIK";
    #endregion

    public const string ip = "46.36.38.72";
    public const string ipMyPoda = "85.135.38.18";

    #region For easy copy
    /*
Zkopíroval jsem z C:\repos\_\Projects\PlatformIndependentNuGetPackages.webWithoutDep\SunamoExceptions.web\_\Essential\VpsHelperSunamo.cs
    nevím zda je to správný postup - NENÍ, pak je tu duplikátní s C:\repos\_\Projects\PlatformIndependentNuGetPackages\sunamo\Essential\VpsHelperSunExcSunamo.cs
     */
    //public static bool IsQ
    //=> Environment.MachineName == "NRJANCIK";
    #endregion

    public static string LocationOfSqlBackup(string s)
    {
        var p = string.Empty;
        //p = @"C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\";
        p = @"C:\mssqllserver\";

        var r = p += @"Backup\" + s + ".bak";
        return r;
    }

    public static string SunamoSln()
    {
        return BasePathsHelperShared.vs + @"sunamo\";
    }

    public static string SunamoCzSln()
    {
        return BasePathsHelperShared.vs + @"sunamo.cz\";
    }

    public static string SunamoProject()
    {
        return Path.Combine(SunamoSln(), "sunamo");
    }


}
