/* Ceated by Ya Lin. 2019/7/11 14:52:29 */

using System.ComponentModel;

namespace Thunder.Blazor.Components
{
    public class NotifyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void PropertyUpdate(object sender, string PropertyName)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(PropertyName));
        }

    }

}
