// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared.Entity;
public partial class SongFromInternet : IEquatable<SongFromInternet>
{
    public List<string> AlternateArtists()
    {
        var remix = Remix();
        remix = SHReplace.ReplaceAll(remix, "Ft", "ft", Translate.FromKey(XlfKeys.Feat), "feat");
        remix = remix.Trim('.');
        remix = remix.Trim();
        var art = SHSplit.Split(remix, "&", " and ");
        return art;
    }

    public int Compare(object x, object y)
    {
        var xx = (SongFromInternet)x;
        var xy = (SongFromInternet)y;
        const float min = 0.5f;
        var f = xy.CalculateSimilarityAll(xx, false, min);
        if (min <= f)
        {
            return 1;
        }

        return 0;
    }

    public override bool Equals(object obj)
    {
        return Equals((SongFromInternet)obj);
    }

    public bool Equals(SongFromInternet other)
    {
        return BTS.IntToBool(Compare(this, other));
    }

    private readonly StringComparer comparer = StringComparer.OrdinalIgnoreCase;
    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }
}