namespace SunamoShared._public.SunamoInterfaces.Interfaces;


public interface IProgressBarShared
{
    bool isRegistered { get; set; }
    int writeOnlyDividableBy { get; set; }
    void Init(IPercentCalculator pc);
    void Init(IPercentCalculator pc, bool isNotUt);
    
    
    
    
    void LyricsHelper_AnotherSong(object asyncResult);
    void LyricsHelper_AnotherSong();
    void LyricsHelper_AnotherSong(int i);
    void LyricsHelper_OverallSongs(int obj);
    void LyricsHelper_WriteProgressBarEnd();
}