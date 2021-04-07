using System.ComponentModel;
using System.Windows.Controls;

namespace SensorData.TestManager {
    public abstract class BaseControl : UserControl, INotifyPropertyChanged {
        protected bool __bIsReadOnly;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ReadOnly {
            get {
                return __bIsReadOnly;
            }

            set {
                __bIsReadOnly = value;
                NotifyPropertyChanged("ReadOnly");
            }
        }

        
        protected void NotifyPropertyChanged(string sProperty = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(sProperty));
            }
        }
    }
}
