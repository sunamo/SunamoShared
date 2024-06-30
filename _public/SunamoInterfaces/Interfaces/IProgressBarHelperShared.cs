namespace SunamoShared;







public interface IProgressBarHelperShared
{
    void Done();
    void DonePartially();
    IProgressBarHelperShared CreateInstance( object pb, double overall,  object ui);
}