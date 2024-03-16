//namespace SunamoShared.TemplatesNotCompiled;

//public class MainWindowSunamo_Ctor
//{
//    public static async Task FirstSection<Dispatcher>(string appName, Action<Dispatcher> WpfAppInit, Func<IClipboardHelper> ClipboardHelperWinStdInstance, Action checkForAlreadyRunning, Action applyCryptData, Dispatcher d)
//    {
//        ThisApp.Name = appName;
//        ThisApp.EventLogName = appName;

//        WpfAppInit(d);
//        if (checkForAlreadyRunning != null)
//        {
//            checkForAlreadyRunning();
//        }

//        //ClipboardHelper.Instance = ClipboardHelperWinStdInstance();
//        AppData.ci.CreateAppFoldersIfDontExists(new CreateAppFoldersIfDontExistsArgs());
//        applyCryptData();

//        XlfResourcesHSunamo.SaveResouresToRLSunamo(await LocalizationLanguagesLoader.Load());
//    }
//}
