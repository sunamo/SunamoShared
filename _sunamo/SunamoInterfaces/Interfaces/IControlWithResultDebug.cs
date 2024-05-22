namespace SunamoShared;


[ComVisible(true)]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]
internal interface IControlWithResultDebug : IControlWithResult
{
    int CountOfHandlersChangeDialogResult();
    void AttachChangeDialogResult(VoidBoolNullable a, bool throwException = true);
}