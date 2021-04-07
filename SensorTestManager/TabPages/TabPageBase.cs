using System.ComponentModel;
using System.Windows.Controls;

namespace SensorData.TestManager {
    public class TabPageBase : UserControl, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string sProperty = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(sProperty));
            }
        }
    }
}
