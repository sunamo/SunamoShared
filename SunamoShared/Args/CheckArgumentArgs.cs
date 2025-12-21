namespace SunamoShared.Args;

public class CheckArgumentArgs
{
    public StringBuilder stringBuilder = null;
    public string argName = null;
    public bool _trim = false;



    public CheckArgumentArgs(string argName, StringBuilder stringBuilder)
    {
        this.argName = argName;
        this.stringBuilder = stringBuilder;
    }
}