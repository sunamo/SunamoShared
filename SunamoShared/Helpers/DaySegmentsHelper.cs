// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoShared.Helpers;

public class DaySegmentsHelper
{
    const int SecondsInOneSegment = 300;
    private const int CountOfSegments = 288;
    static readonly DateTime DtMinVal = new DateTime(1, 1, 1, 0, 0, 0, 0);

    public static int GetSegment()
    {
        var dt = DateTime.Now;
        return GetSegment(dt);
    }

    public static int GetSegment(DateTime dt)
    {
        var dt2 = new DateTime(1, 1, 1, dt.Hour, dt.Minute, dt.Second);

        var oc = (dt2 - DtMinVal).TotalSeconds;
        var data = oc / SecondsInOneSegment;
        return (int)data;
    }

}
