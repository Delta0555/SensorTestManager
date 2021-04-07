using System.Windows;

namespace SensorData.TestManager {
    /// <summary>
    /// Interaction logic for AddEditTestParamDialog.xaml
    /// </summary>
    public partial class AddEditTestParamDialog : Window {
        public TestParameterViewModel Data { get; private set; }        
        public AddEditTestParamDialog() {
            InitializeComponent();
            SetData(null);        
        }

        public void SetData(TestParameterViewModel pParamModel) {
            if(pParamModel == null) {
                btn_Accept.Content = "Add";
                Title = "Add new Test Parameter";
            }
            else {
                btn_Accept.Content = "Save";
                Title = "Edit Test Parameter";
            }

            tpc_AddOrEditTestParam.SetData(pParamModel);
        }

       

        public TestParameterViewModel GetData() {
            return tpc_AddOrEditTestParam.GetData();
        }

        private void btn_Accept_Click(object sender, RoutedEventArgs e) {
            string sMessage = string.Empty;
                 
            if (tpc_AddOrEditTestParam.ValidateInput(out sMessage)) {
                TestParameterViewModel pData = tpc_AddOrEditTestParam.GetData();
                Data = pData;
                DialogResult = true;
            }
            else {
                sti_Status.Content = sMessage;
                Data = null;
            }
        }
    }
}
