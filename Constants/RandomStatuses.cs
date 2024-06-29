namespace SunamoShared;

public class RandomStatuses
{
    public void SetStatusOfType(TypeOfMessageShared type)
    {
        ThisApp.SetStatus(type, type.ToString());
    }

    public void SetAllTypes()
    {
        foreach (var item in EnumHelper.GetValues<TypeOfMessageShared>())
        {
            SetStatusOfType(item);
        }
    }
}
