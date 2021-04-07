using System;
using System.Windows.Controls;

namespace SensorData.TestManager {
    /// <summary>
    /// Interaction logic for TestParamsControl.xaml
    /// </summary>
    public partial class TestParamsControl : BaseControl {        
        public TestParamsControl() {
            InitializeComponent();
            this.DataContext = this;
        }

        public void SetData(TestParameterViewModel pTestModel) {
            if (pTestModel != null) {
                tbk_ParamID.Text = pTestModel.ID.ToString();
                txb_ParamName.Text = pTestModel.Name;
                iud_PreTest.Value = Math.Max(0, pTestModel.PreTestMS);
                iud_Test.Value = Math.Max(0, pTestModel.TestTimeMS);
                dud_Highlimit.Value = (decimal)pTestModel.HighLimitmV;
                dud_Lowlimit.Value = (decimal)pTestModel.LowLimitmV;
            }
            else {
                tbk_ParamID.Text = "---";
                txb_ParamName.Text = string.Empty;
                iud_PreTest.Value = 0;
                iud_Test.Value = 0;
                dud_Highlimit.Value = 0;
                dud_Lowlimit.Value = 0;
            }
        }

        public TestParameterViewModel GetData() {
            long nID = 0;

            Int64.TryParse(tbk_ParamID.Text, out nID);

            TestParameterViewModel pResult = new TestParameterViewModel(nID, txb_ParamName.Text, iud_PreTest.Value.GetValueOrDefault(), iud_Test.Value.GetValueOrDefault(), (double)dud_Highlimit.Value.GetValueOrDefault(), (double)dud_Lowlimit.Value.GetValueOrDefault());

            return pResult;
        }

        public bool ValidateInput(out string sMessage) {
            sMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(txb_ParamName.Text)) {
                sMessage += "Name is empty";
            }

            if (dud_Highlimit.Value < dud_Lowlimit.Value) {
                sMessage += string.Format("{0}High limit cannot be lower than low limit", string.IsNullOrWhiteSpace(sMessage) ? string.Empty : " | ");
            }
            
            return string.IsNullOrWhiteSpace(sMessage);
        }     
    }
}
