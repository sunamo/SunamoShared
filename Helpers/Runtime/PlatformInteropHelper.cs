namespace SunamoShared;

public class PlatformInteropHelper
{
    #region For easy copy
    public static bool IsSellingApp()
    {
        return false; //RH.ExistsClass("SellingHelper");
    }
    #endregion

    static bool? isUwp = null;

    /// <summary>
    /// Working excellent 11-3-19
    /// </summary>
    public static bool IsUwpWindowsStoreApp()
    {
        if (isUwp.HasValue)
        {
            return isUwp.Value;
        }

        var ass = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var item in ass)
        {
            Type[] types = null;
            try
            {
                types = item.GetTypes();
            }
            catch (Exception ex)
            {
                ThrowEx.DummyNotThrow(ex);
            }

            if (types != null)
            {
                foreach (var type in types)
                {
                    if (type.Namespace != null)
                    {
                        if (type.Namespace.StartsWith("Windows.UI"))
                        {
                            isUwp = true;
                            break;
                        }
                    }
                }

                if (isUwp.HasValue)
                {
                    break;
                }
            }
        }

        if (!isUwp.HasValue)
        {
            isUwp = false;
        }

        return isUwp.Value;
    }

    public static Type GetTypeOfResources()
    {
        throw new Exception();
        return null;
        //return typeof(Resources.ResourcesDuo);
    }

    /// <summary>
    /// Wpf.Tests = .NET Framework 4.8.4018.0
    /// ConsoleStandardApp2 = .NET Core 3.0.0
    /// GeoCachingTool = .NET Core 4.6.00001.0
    /// </summary>
    public static bool IsUseStandardProject()
    {
        // Return one of three values:
        var result = RuntimeInformation.FrameworkDescription;
        if (result.StartsWith(RuntimeFrameworks.netCore))
        {
            return true;
        }

        return false;
    }

    //public static AppDataBase<StorageFolder, StorageFile> AppData()
    //{
    //    if (IsUwpWindowsStoreApp())
    //    {
    //        return AppData.
    //    }
    //}
}
