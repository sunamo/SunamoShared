namespace SunamoShared._public.SunamoInterfaces.Interfaces;


[ComVisible(true)]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]
public interface IControlWithResultDebugShared : IControlWithResult
{
    int CountOfHandlersChangeDialogResult();
    void AttachChangeDialogResult(Action<bool> a, bool throwException = true);
}