namespace SunamoShared.Helpers.Runtime;

public partial class PlatformInteropHelper
{
    #region For easy copy
    public static bool IsSellingApp()
    {
        return false; //RH.ExistsClass("SellingHelper");
    }
    #endregion
}
