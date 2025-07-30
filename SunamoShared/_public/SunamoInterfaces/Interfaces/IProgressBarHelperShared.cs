namespace SunamoShared._public.SunamoInterfaces.Interfaces;







public interface IProgressBarHelperShared
{
    void Done();
    void DonePartially();
    IProgressBarHelperShared CreateInstance( object pb, double overall,  object ui);
}