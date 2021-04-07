using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;


namespace SensorData.TestManager {
    /// <summary>
    /// Interaction logic for AddEditSensor.xaml
    /// </summary>
    public partial class AddEditSensorDialog : Window, INotifyPropertyChanged {
        public event EventHandler<EventArgs> UpdateTestParams;
        public event PropertyChangedEventHandler PropertyChanged;

        private List<TestParameterViewModel> __pTestParameters;

        public AddEditSensorDialog() {
            InitializeComponent();
            this.DataContext = this;

            //Just initializing...
            SetData(null, null);
        }

        public FullSensorViewModel Data { get; private set; }

        public List<TestParameterViewModel> TestParams {
            get { return __pTestParameters; }
            private set {
                __pTestParameters = value;
                NotifyPropertyChanged("TestParams");                
            }
        }

        public void SetData(FullSensorViewModel pSensorModel, List<TestParameterViewModel> pTestParams) {
            if (pSensorModel == null) {
                btn_Accept.Content = "Add";
                Title = "Add new Sensor";
                ssc_NewSensor.ReadOnly = false;                
            }
            else {
                btn_Accept.Content = "Save";
                Title = "Edit Sensor";
                ssc_NewSensor.ReadOnly = true;
            }

            ssc_NewSensor.SetData(pSensorModel);

            TestParams = pTestParams;

            if(pSensorModel != null && pSensorModel.TestParam != null) {
                TestParameterViewModel pFittingParam = TestParams.FirstOrDefault(p => p.ID == pSensorModel.TestParam.ID);
                
                if(pFittingParam != null) {
                    ckb_AddTestParameter.IsChecked = true;
                    lst_TestParams.SelectedItem = pFittingParam;
                }
                else {
                    tpc_TestParam.SetData(new TestParameterViewModel(pSensorModel.TestParam));
                    ckb_AddTestParameter.IsChecked = false;
                }
            }
            else {
                tpc_TestParam.SetData(null);
            }            
        }

        private void NotifyPropertyChanged(string sProperty = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(sProperty));
            }
        }


        private void btn_Accept_Click(object sender, RoutedEventArgs e) {
            string sMessage = string.Empty;
            Data = null;

            if (ssc_NewSensor.ValidateInput(out sMessage)) {

                if(ckb_AddTestParameter.IsChecked.HasValue && ckb_AddTestParameter.IsChecked.Value && lst_TestParams.SelectedItem == null) {
                    sMessage += string.Format("{0} No Test selected", string.IsNullOrWhiteSpace(sMessage) ? string.Empty : sMessage + " | ");
                }
                else {
                    FullSensorViewModel pSensorData = ssc_NewSensor.GetData();

                    if (ckb_AddTestParameter.IsChecked.HasValue && ckb_AddTestParameter.IsChecked.Value) {
                        pSensorData.TestParam = tpc_TestParam.GetData();
                    }

                    Data = pSensorData;

                    DialogResult = true;
                }                
            }            
        }
       

        private void ckb_AddTestParameter_Checked(object sender, RoutedEventArgs e) {
            if (ckb_AddTestParameter.IsChecked.HasValue && ckb_AddTestParameter.IsChecked.Value) {
                if(UpdateTestParams != null) {
                    UpdateTestParams(this, new EventArgs());
                }
            }
            else {
                lst_TestParams.ItemsSource = null;
            }
        }

        private void lst_TestParams_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            TestParameterViewModel pParam = lst_TestParams.SelectedItem as TestParameterViewModel;
            tpc_TestParam.SetData(pParam);
        }
    }
}
