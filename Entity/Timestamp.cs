namespace SunamoShared.Entity;

public class Timestamp
{
    public static List<string> GetAllTimeStamps(string p)
    {
        List<string> vr = new List<string>();
        var s = SHSplit.SplitCharMore(p, AllChars.space, AllChars.dot);
        foreach (var item in s)
        {
            if (item.Length == 9)
            {
                var ch0 = item[0];
                var ch1 = item[1];
                var ch2 = item[2];
                var ch4 = item[4];
                var ch5 = item[5];
                var ch7 = item[7];
                var ch8 = item[8];
                if (ch0 == 'T' && char.IsDigit(ch1) && char.IsDigit(ch2) && char.IsDigit(ch4) && char.IsDigit(ch5) && char.IsDigit(ch7) && char.IsDigit(ch8))
                {
                    vr.Add(item);
                }
            }
        }

        return vr;
    }


    public static string Get(DateTime dtTo4)
    {
        // tady to mus�m dle m�sta u�it�
        //return " T" + /*string.Join(*/MakeUpTo2NumbersToZero(AllStrings.lowbar, dtTo4.Hour, dtTo4.Minute, dtTo4.Second);
        return null;
    }

    public static object MakeUpTo2NumbersToZero(int p)
    {
        if (p.ToString().Length == 1)
        {
            return "0" + p;
        }
        return p;
    }
}
