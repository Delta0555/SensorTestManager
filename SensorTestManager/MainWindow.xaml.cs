using SensorData.ServiceClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SensorData.TestManager {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow {
        public event EventHandler<EventArgs> AddNewSensor;
        public event EventHandler<SensorArgs> EditSensor;
        public event EventHandler<EventArgs> AddNewTestParameters;
        public event EventHandler<TestParameterArgs> EditTestParameters;
        public event EventHandler<OpModeChangedArgs> OpModeChanged;        

        public MainWindow() {
            InitializeComponent();

            tpp_Sensors.AddNewSensor += Tpp_Sensors_AddNewSensor;
            tpp_Sensors.EditSensor += Tpp_Sensors_EditSensor;

            tpp_TestParams.AddNewTestParameter += Tpp_TestParams_AddNewTestParameters;
            tpp_TestParams.EditTestParameter += Tpp_TestParams_EditTestParameters;
        }

        private void Tpp_Sensors_EditSensor(object sender, SensorArgs e) {
            if(EditSensor != null) {
                EditSensor(sender, e);
            }
        }

        private void Tpp_Sensors_AddNewSensor(object sender, EventArgs e) {
            if(AddNewSensor != null) {
                AddNewSensor(sender, e);
            }
        }

        private void Tpp_TestParams_EditTestParameters(object sender, TestParameterArgs e) {
            if(EditTestParameters != null) {
                EditTestParameters(sender, e);
            }
        }

        private void Tpp_TestParams_AddNewTestParameters(object sender, EventArgs e) {
            if(AddNewTestParameters != null) {
                AddNewTestParameters(sender, e);
            }
        }

        public void LoadTestParamsPageData(ObservableCollection<TestParameterViewModel> pParamModels) {
            tpp_TestParams.TestParameters = pParamModels;
        }

        public void LoadSensorsPageData(ObservableCollection<FullSensorViewModel> pSensorModels) {
            tpp_Sensors.Sensors = pSensorModels;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (OpModeChanged != null && e.OriginalSource == tbc_MasterTabs) {            
                if (tim_Sensors != null && tim_Sensors.IsSelected) {
                    OpModeChanged(this, new OpModeChangedArgs { Mode = OpMode.Sensors });
                }
                else if (tim_TestParameters != null && tim_TestParameters.IsSelected) {
                    OpModeChanged(this, new OpModeChangedArgs { Mode = OpMode.TestParams });
                }
                else if (tim_TestRuns != null && tim_TestRuns.IsSelected) {
                    OpModeChanged(this, new OpModeChangedArgs { Mode = OpMode.TestRuns });
                }
            }
        }

        public void LoadTestRunsPageData(List<FullTestRun> pRuns) {
            trp_TestRuns.TestRunData = pRuns;
        }
    }
}
