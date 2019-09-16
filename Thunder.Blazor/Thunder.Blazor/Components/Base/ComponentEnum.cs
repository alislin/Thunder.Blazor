/* Ceated by Ya Lin. 2019/7/11 14:52:29 */

namespace Thunder.Blazor.Components
{
    public enum ButtonTypeValue
    {
        Close = 1,
        OK = 2,
        Cancel = 4,
        Yes = 8,
        No = 16
    }

    public enum ButtonType
    {
        OK = 2,
        Close = 1,
        OKCancel = 6,
        YesNo = 24,
        YesNoCancel = 28,
        Custom = 0
    }

    public enum ModalResultValue
    {
        None,
        OK,
        Cancel,
        Close,
        Yes,
        No
    }
}
