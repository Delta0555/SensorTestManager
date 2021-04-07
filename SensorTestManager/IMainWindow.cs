using SensorData.ServiceClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SensorData.TestManager {
    interface IMainWindow {
        event EventHandler<EventArgs> AddNewSensor;
        event EventHandler<SensorArgs> EditSensor;

        event EventHandler<EventArgs> AddNewTestParameters;
        event EventHandler<TestParameterArgs> EditTestParameters;

        event EventHandler<OpModeChangedArgs> OpModeChanged;

        void LoadTestParamsPageData(ObservableCollection<TestParameterViewModel> pParamModels);
        void LoadSensorsPageData(ObservableCollection<FullSensorViewModel> pSensorModels);
        void LoadTestRunsPageData(List<FullTestRun> pRuns);
    }

    public enum OpMode {
        Sensors,
        TestParams,
        TestRuns
    }
}
