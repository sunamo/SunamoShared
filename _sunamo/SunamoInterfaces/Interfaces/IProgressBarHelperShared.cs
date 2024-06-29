namespace SunamoShared;


/// <summary>
/// musí být v sunamo protože ho tu potřebují
/// ale v sunamo zase není DispatcherObject
/// 
/// </summary>
public interface IProgressBarHelperShared
{
    void Done();
    void DonePartially();
    IProgressBarHelperShared CreateInstance(/*System.Windows.Controls.ProgressBar*/ object pb, double overall, /*DispatcherObject*/ object ui);
}