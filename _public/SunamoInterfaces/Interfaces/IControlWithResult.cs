namespace SunamoShared._public.SunamoInterfaces.Interfaces;


[ComVisible(true)]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]








public interface IControlWithResult
{
    
    
    
    
    event Action<bool?> ChangeDialogResult;
    
    
    
    
    
    bool? DialogResult { set; }
    
    void Accept(object input);
    void FocusOnMainElement();
    
}