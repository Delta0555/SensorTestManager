using SensorData.ServiceClasses;
using System.Windows.Controls;


namespace SensorData.TestManager {
    /// <summary>
    /// Interaction logic for SensorControl.xaml
    /// </summary>
    public partial class SensorControl : BaseControl {       
        public SensorControl() {
            InitializeComponent();
        }

        public void SetData(SensorBase pSensor) {
            if(pSensor != null) {
                lud_MaterialNumber.Text = pSensor.MaterialNumber.ToString();
                txb_SensorName.Text = pSensor.Name;
            }
            else {
                lud_MaterialNumber.Text = string.Empty;
                txb_SensorName.Text = string.Empty;
            }
        }
        
        public FullSensorViewModel GetData() {
            return new FullSensorViewModel(lud_MaterialNumber.Value.Value, txb_SensorName.Text, null);
        }

        public bool ValidateInput(out string sMessage) {
            sMessage = string.Empty;

            if (lud_MaterialNumber.Value == null) {
                sMessage += "No Material number entered!";
            }            

            return string.IsNullOrWhiteSpace(sMessage);
        }
    }
}
