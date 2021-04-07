using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SensorData.TestManager {
    /// <summary>
    /// Interaction logic for SensorsTabPage.xaml
    /// </summary>
    public partial class SensorsTabPage : TabPageBase {
        public event EventHandler<SensorArgs> EditSensor;
        public event EventHandler<EventArgs> AddNewSensor;
        

        //public event EventHandler<Event>

        private ObservableCollection<FullSensorViewModel> __pFullSensors;

        public ObservableCollection<FullSensorViewModel> Sensors {
            get { return __pFullSensors; }
            set {
                __pFullSensors = value;              
                NotifyPropertyChanged("Sensors");

                if(value != null && value.Count > 0) {
                    lst_Sensors.SelectedIndex = 0;
                }
            }
        }

        private void __pFullSensors_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            throw new NotImplementedException();
        }

        public SensorsTabPage() {
            InitializeComponent();
            this.DataContext = this;
        }        

        private void lst_Sensors_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count > 0) {
                FullSensorViewModel pSensor = e.AddedItems[0] as FullSensorViewModel;

                ssc_Sensor.SetData(pSensor);
                if(pSensor != null && pSensor.TestParam != null) {
                    tpc_TestParams.SetData(new TestParameterViewModel(pSensor.TestParam ));
                }
                else {
                    tpc_TestParams.SetData(null);
                }
                
            }
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Not implemented yet");
        }

        private void btn_AddSensor_Click(object sender, RoutedEventArgs e) {
            if(AddNewSensor != null) {
                AddNewSensor(this, new EventArgs());
            }          
        }

        private void PDialog_UpdateTestParams(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void btn_EditSensor_Click(object sender, RoutedEventArgs e) {
            FullSensorViewModel pCurrentSelection = lst_Sensors.SelectedItem as FullSensorViewModel;

            if (pCurrentSelection == null) {
                MessageBox.Show("Please select a Sensor from the list for editing", "No selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else {
                if (EditSensor != null) {
                    EditSensor(this, new SensorArgs { Sensor = pCurrentSelection });                    
                }
            }
        }

        private void Update() {
            NotifyPropertyChanged("Sensors");
        }

        private void btn_RemoveSensor_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Not implemented yet");
        }
    }
}
