using SensorData.ServiceClasses;
using System.Windows.Media;

namespace SensorData.TestManager {
    public class FullSensorViewModel : FullSensor {
        public string FriendlyName {
            get {
                return string.Format("{0} -{1}",MaterialNumber, Name);
            }
        }

        public SolidColorBrush ListItemColor {
            get {
                return TestParam == null ? Brushes.Black : Brushes.Green;
            }
        }

        public FullSensorViewModel(FullSensor pSensor) : base(pSensor.MaterialNumber, pSensor.Name, pSensor.TestParam) { }

        public FullSensorViewModel(long nMaterialNumber, string sName, TestParameter pParam): base(nMaterialNumber, sName, pParam) { }

        public void Update(FullSensorViewModel pFrom) {
            MaterialNumber = pFrom.MaterialNumber;
            Name = pFrom.Name;
            TestParam = pFrom.TestParam;
        }
    }

    public class TestParameterViewModel : TestParameter {
        public string FriendlyName {
            get {
                return string.Format("{0} - {1}", ID, Name);
            }
        }        

        public TestParameterViewModel(TestParameter pParam):base(pParam.ID,pParam.Name,pParam.PreTestMS,pParam.TestTimeMS,pParam.HighLimitmV,pParam.LowLimitmV) { }

        public TestParameterViewModel(long nID, string sName, int nPreTestMS, int nTestTimeMS, double fHighLimitmV, double fLowLimitmV) :base(nID,sName,nPreTestMS,nTestTimeMS, fHighLimitmV, fLowLimitmV) { }
    }


    public class FullTestRunModel : FullTestRun {
        public SolidColorBrush ItemColor {
            get {
                if (Succeeded.HasValue) {
                    return Succeeded.Value ? Brushes.Green : Brushes.Red;
                }
                else {
                    return Brushes.DarkGoldenrod;
                }
            }
        }

        public FullTestRunModel(FullTestRun pFrom) :
           base(pFrom.MaterialNumber, pFrom.SensorName, pFrom.TestParamID, pFrom.TestName, pFrom.PreTestMS, pFrom.TestTimeMS, pFrom.HighLimitmV, pFrom.LowLimitmV, pFrom.MeasuredValue, pFrom.Succeeded) { }

        public FullTestRunModel(long nMaterialNumber, string sSensorName, long nTestParamID, string sTestParamName, int nPreTestMS, int nTestTimeMS, double fHighLimitmV, double fLowLimitmV, double? fMeasurement, bool? bSucceeded):
            base(nMaterialNumber, sSensorName, nTestParamID, sTestParamName, nPreTestMS, nTestTimeMS, fHighLimitmV, fLowLimitmV, fMeasurement, bSucceeded) { }
    }
}
