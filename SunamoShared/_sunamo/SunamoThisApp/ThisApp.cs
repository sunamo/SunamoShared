namespace SunamoShared._sunamo.SunamoThisApp;
internal class ThisApp
{
    internal static string EventLogName;
    internal static string Name;
    internal static LangsShared l;







    internal static void SetStatus(TypeOfMessageShared st, string status, params string[] args)
    {
        var format = /*string.Format*/ string.Format(status, args);
        if (format.Trim() != string.Empty)
        {
            if (StatusSetted == null)
            {
                // For unit tests
                //////////DebugLogger.Instance.WriteLine(st + ": " + format);
            }
            else
            {
                StatusSetted(st, format);
            }
        }
    }

    internal static event Action<TypeOfMessageShared, string> StatusSetted;
}
