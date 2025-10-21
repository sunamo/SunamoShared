// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoShared;

public class ProgressBar
{
    private static readonly int overallSongs = 10;
    public static event Action AnotherSong;
    public static event Action<int> OverallSongs;
    public static event Action WriteProgressBarEnd;


    public static List<int> GetAllSongFromInternet()
    {
        if (OverallSongs != null) OverallSongs(overallSongs);

        return GetAllSongFromInternet(overallSongs);
    }

    private static List<int> GetAllSongFromInternet(int overallSongs)
    {
        for (var i = 0; i < overallSongs; i++)
        {
            AnotherSong();
            Thread.Sleep(100);
        }

        WriteProgressBarEnd();

        return null;
        //return TestData.list04;
    }
}