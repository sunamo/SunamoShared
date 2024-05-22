namespace SunamoShared;


/// <summary>
/// musí být v sunamo protože ho tu potřebují
/// ale v sunamo zase není DispatcherObject
/// 
/// </summary>
internal interface IProgressBarHelper
{
    void Done();
    void DonePartially();
    IProgressBarHelper CreateInstance(/*System.Windows.Controls.ProgressBar*/ object pb, double overall, /*DispatcherObject*/ object ui);
}