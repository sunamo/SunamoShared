namespace SunamoShared.Helpers;

public class CastHelper
{
    public static List<string> ToListString(object l)
    {
        var t = l.GetType();
        if (t == typeof(List<string>))
        {
            return (List<string>)l;
        }
        else if (t == typeof(string))
        {
            return SHGetLines.GetLines(l.ToString());
        }
        else
        {
            ThrowEx.DoesntHaveRequiredType(nameof(l));
        }

        return null;
    }

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
