using System.Configuration;
using System.Windows;

namespace SensorData.TestManager {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void Application_Startup(object sender, StartupEventArgs e) {
            MainWindow pView = new MainWindow();

            string sServiceAddress = ConfigurationManager.AppSettings["ServiceAddress"];
            string sServiceUser = ConfigurationManager.AppSettings["ServiceUser"];
            string sServicePW = ConfigurationManager.AppSettings["ServicePW"];
            MainViewController pController = new MainViewController(pView, sServiceAddress, sServiceUser, sServicePW);

            pView.Show();
        }
    }
}
