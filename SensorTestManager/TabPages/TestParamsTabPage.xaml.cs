using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System;

namespace SensorData.TestManager {
    /// <summary>
    /// Interaction logic for TestParamsTabPage.xaml
    /// </summary>
    public partial class TestParamsTabPage : TabPageBase {
        public event EventHandler<EventArgs> AddNewTestParameter;
        public event EventHandler<TestParameterArgs> EditTestParameter;       

        private ObservableCollection<TestParameterViewModel> __pTestParameters;
        public ObservableCollection<TestParameterViewModel> TestParameters {
            get { return __pTestParameters; }
            set {
                __pTestParameters = value;
                NotifyPropertyChanged("TestParameters");

                if (value != null && value.Count > 0) {
                    lst_TestParameters.SelectedIndex = 0;
                }
            }
        }

        public TestParamsTabPage() {
            InitializeComponent();
            this.DataContext = this;
        }

        //public event PropertyChangedEventHandler PropertyChanged;        

        private void btn_RemoveTestParameter_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Not implemented yet");
        }

        private void btn_EditTestParameter_Click(object sender, RoutedEventArgs e) {
            TestParameterViewModel pCurrentSelection = lst_TestParameters.SelectedItem as TestParameterViewModel; 
            if (pCurrentSelection == null) {
                MessageBox.Show("Please select a TestParameter from the list for editing", "No selection", MessageBoxButton.OK, MessageBoxImage.Information);                
            }
            else {
                AddEditTestParamDialog pDialog = new AddEditTestParamDialog();
                pDialog.SetData(pCurrentSelection);

                bool? bResult = pDialog.ShowDialog();

                if (bResult.HasValue && bResult.Value) {
                    if (EditTestParameter != null) {
                        EditTestParameter(this, new TestParameterArgs { TestParams = pDialog.Data });
                    }
                }
            }
        }

        private void btn_AddTestParameter_Click(object sender, RoutedEventArgs e) {
            if(AddNewTestParameter != null) {
                AddNewTestParameter(this, new EventArgs());
            }           
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Not implemented yet");
        }      

        private void lst_TestParameters_SelectionChanged(object sender, SelectionChangedEventArgs e) {
           if(e.AddedItems.Count > 0) {
                TestParameterViewModel pParam = e.AddedItems[0] as TestParameterViewModel;
                tpc_TestParam.SetData(pParam);
            }
        }
    }
}
