using SensorData.ServiceClasses;
using System.Collections.Generic;
using System.Linq;

namespace SensorData.TestManager {
    /// <summary>
    /// Interaction logic for TestRunTabPage.xaml
    /// </summary>
    public partial class TestRunTabPage : TabPageBase {
        public TestRunTabPage() {
            InitializeComponent();

            this.DataContext = this;
        }

        public List<FullTestRun> TestRunData {
            set {
                if(value != null && value.Count > 0) {
                    trc_Results.SetData(value.Select(p => new FullTestRunModel(p)).ToList());
                }
                else {
                    trc_Results.SetData(null);
                }                
            }
        }
    }
}
