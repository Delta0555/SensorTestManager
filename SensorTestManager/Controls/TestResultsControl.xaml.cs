using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SensorData.TestManager {
    /// <summary>
    /// Interaction logic for TestResultsControl.xaml
    /// </summary>
    public partial class TestResultsControl : BaseControl {
        public TestResultsControl() {
            InitializeComponent();

            this.DataContext = this;
        }

        public bool QueryFieldsVisible {
            set {
                rdf_QueryFields.Height = value ? new GridLength(25) : new GridLength(0);
            }
        }

        public void SetData(List<FullTestRunModel> pRuns) {
            ddg_Results.ItemsSource = null;
            ddg_Results.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = new ListCollectionView(pRuns ?? new List<FullTestRunModel>(0)) });
        }
    }
}
