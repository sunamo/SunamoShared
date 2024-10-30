namespace SunamoShared._public.SunamoData.Data;


public class WriteToJsonFileData
{
    public Newtonsoft.Json.Formatting formatting = Newtonsoft.Json.Formatting.None;
    public bool append = false;
    public bool TwoBackslashToSingle { get; set; } = false;

    //public Action<string> phWinCode = null;
}