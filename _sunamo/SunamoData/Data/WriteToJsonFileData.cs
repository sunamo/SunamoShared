namespace SunamoShared;


internal class WriteToJsonFileData
{
    internal Newtonsoft.Json.Formatting formatting = Newtonsoft.Json.Formatting.None;
    internal bool append = false;
    internal Action<string> phWinCode = null;
}