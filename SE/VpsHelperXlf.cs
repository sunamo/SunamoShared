namespace SunamoShared;
//namespace Xlf
//{ 

public class VpsHelperXlf
{
    #region For easy copy

    //public static bool IsQ
    //=> Environment.MachineName == Consts.qMachineName;

    public const string path = @"C:\_";

    public static bool IsVps
    {
        get
        {
            bool v = Directory.Exists(path);
            return v;
        }
    }

    #endregion
}
//}
