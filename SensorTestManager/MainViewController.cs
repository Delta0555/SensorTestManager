using SensorData.ServiceClasses;
using SensorData.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SensorData.TestManager {
    
    class MainViewController {
        private SensorDataServiceProxy __pServiceProxy;
        private readonly IMainWindow __pView;
        ObservableCollection<TestParameterViewModel> __pParamModels = new ObservableCollection<TestParameterViewModel>();
        ObservableCollection<FullSensorViewModel> __pSensorModels = new ObservableCollection<FullSensorViewModel>();

        public MainViewController(IMainWindow pView, string sServiceAddress, string sServiceUser, string sServicePW) {
            __pView = pView;
            __pServiceProxy = new SensorDataServiceProxy( sServiceAddress, sServiceUser, sServicePW);

            __pView.AddNewSensor += __pView_AddNewSensor;
            __pView.EditSensor += __pView_EditSensor;
            
            __pView.AddNewTestParameters += __pView_AddNewTestParameters;
            __pView.EditTestParameters += __pView_EditTestParameters;

            __pView.OpModeChanged += __pView_OpModeChanged;            
        }

        private void __pView_EditSensor(object sender, SensorArgs e) {

            AddEditSensorDialog pDialog = new AddEditSensorDialog();
            pDialog.SetData(e.Sensor, __pParamModels.ToList());
           
            bool? bResult = pDialog.ShowDialog();

            if (bResult.HasValue && bResult.Value) {
                FullSensorViewModel pData = pDialog.Data;
                Sensor pSensor = new Sensor(pData.MaterialNumber, pData.Name, pData.TestParam == null ? null : (long?)pData.TestParam.ID);

                bool bSuccess = __pServiceProxy.UpdateSensor(pSensor);

                if (bSuccess) {
                    FullSensorViewModel pListSensor = __pSensorModels.FirstOrDefault(p => p.MaterialNumber == e.Sensor.MaterialNumber);
                    if(pListSensor != null) {                      

                        //This sucks, must be a better way to do this
                        List<FullSensorViewModel> pNewSensorList = __pSensorModels.Except(new List<FullSensorViewModel> { pListSensor }).ToList();
                        pNewSensorList.Add(pData);
                        __pSensorModels = new ObservableCollection<FullSensorViewModel>(pNewSensorList.OrderBy(p => p.MaterialNumber));
                        __pView.LoadSensorsPageData(__pSensorModels);
                    }
                }
            }
        }       

        private void __pView_AddNewSensor(object sender, EventArgs e) {
            AddEditSensorDialog pDialog = new AddEditSensorDialog();
            pDialog.SetData(null, __pParamModels.ToList());

            bool? bResult = pDialog.ShowDialog();

            if (bResult.HasValue && bResult.Value) {
                try {
                    FullSensorViewModel pData = pDialog.Data;
                    Sensor pSensor = new Sensor(pData.MaterialNumber, pData.Name, pData.TestParam == null ? null : (long?)pData.TestParam.ID);
                    //bool bSuccess =__pServiceProxy.AddSensor(pSensor);
                    __pServiceProxy.AddSensor(pSensor);

                    if (true) {                        
                        __pSensorModels.Add(pData);
                    }
                }
                catch (Exception ex) {
                    //todo: Add logging
                }                
            }         
        }

      

        private void __pView_OpModeChanged(object sender, OpModeChangedArgs e) {
            switch (e.Mode) {
                case OpMode.Sensors: {
                        UpdateSensors();
                        UpdateTestParams();

                        __pView.LoadSensorsPageData(__pSensorModels);
                        break;
                    }
                case OpMode.TestParams: {
                        UpdateTestParams();

                        __pView.LoadTestParamsPageData(__pParamModels);
                        break;
                    }
                case OpMode.TestRuns: {
                        __pView.LoadTestRunsPageData(UpdateTestRuns());
                        break;
                    }
            }
        }

        private List<FullTestRun> UpdateTestRuns() {
            List<FullTestRun> pResult = null;

            try {
                pResult = __pServiceProxy.GetAllFullTestRuns();               
            }
            catch(Exception e) {

            }

            return pResult ?? new List<FullTestRun>(0);
        }

        private void UpdateSensors() {
            List<FullSensor> pSensors = __pServiceProxy.GetAllFullSensors();
          
            if(pSensors != null && pSensors.Count > 0) {
                __pSensorModels = new ObservableCollection<FullSensorViewModel>(pSensors.OrderBy(p => p.MaterialNumber).Select(p => new FullSensorViewModel(p)));
            }
            else {
                __pSensorModels = new ObservableCollection<FullSensorViewModel>();
            }                      
        }

        private void UpdateTestParams() {
            List<TestParameter> pTestParameters = __pServiceProxy.GetAllTestParameters();

            if (pTestParameters != null && pTestParameters.Count > 0) {
                __pParamModels = new ObservableCollection<TestParameterViewModel>(pTestParameters.Select(p => new TestParameterViewModel(p)));
            }
            else {
                __pParamModels = new ObservableCollection<TestParameterViewModel>();
            }            
        }

        private void __pView_EditTestParameters(object sender, TestParameterArgs e) {
            try {
                __pServiceProxy.UpdateTestParameter(e.TestParams);
            }
            catch(Exception ex) {
                //todo: add logging
            }
        }

        private void __pView_AddNewTestParameters(object sender, EventArgs e) {

            AddEditTestParamDialog pDialog = new AddEditTestParamDialog();

            bool? bResult = pDialog.ShowDialog();

            if (bResult.HasValue && bResult.Value) {
                try {
                    TestParameterViewModel pData = pDialog.Data;

                    long nNewID = __pServiceProxy.AddTestParameter(pData);

                    if (nNewID > 0) {
                        pData.ID = nNewID;
                        __pParamModels.Add(pData);
                    }
                }
                catch (Exception ex) {
                    //todo: Add logging
                }                
            }                     
        }
    }
}
