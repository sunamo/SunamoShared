namespace SunamoShared;


public class CheckBoxDataShared<T>
{
    /// <summary>
    /// Set to IsChecked when TwoWayTable.DataCellWrapper == AddBeforeControl.CheckBox
    /// </summary>
    internal bool? tick = false;
    /// <summary>
    /// Na to co se má zobrazit
    /// </summary>
    internal T t = default;
}