// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.Constants;

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
