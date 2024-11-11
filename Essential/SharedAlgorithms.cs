namespace SunamoShared.Essential;
public class SharedAlgorithms
{
    public static int lastError = -1;
    public static Out RepeatAfterTimeXTimes<Out>(int times, int timeoutMs, Func<Out> a)
    {
        lastError = -1;
        Out result = default;
        bool ok = false;
        for (int i = 0; i < times; i++)
        {
            try
            {
                result = a();
                ok = true;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                if (m.StartsWith("The remote server returned an error: "))
                {
                    var p = SHSplit.SplitMore(SHReplace.ReplaceOnce(m, "The remote server returned an error: ", string.Empty), " ");
                    var s = p[0].TrimEnd(')').TrimStart('(');
                    lastError = int.Parse(s);
                }
                if (lastError == 404)
                {
                    return result;
                }
                //The remote server returned an error: (404) Not Found.
                Thread.Sleep(timeoutMs);
            }
            if (ok)
            {
                break;
            }
        }
        return result;
    }
}