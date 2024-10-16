namespace SunamoShared.Helpers;

public class CastHelper
{


    public static string ToString(object l)
    {
        var t = l.GetType();
        if (t == typeof(string))
        {
            return l.ToString();
        }
        else if (t == typeof(List<string>))
        {
            return string.Join(Environment.NewLine, (List<string>)l);
        }
        else
        {
            ThrowEx.DoesntHaveRequiredType(nameof(l));
        }

        return null;
    }
}
