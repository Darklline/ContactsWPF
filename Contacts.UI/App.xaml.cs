using Contacts.UI.Data;
using Contacts.UI.ViewModel;
using System.Windows;

namespace Contacts.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow(new MainViewModel(new ContactDataService()));
            mainWindow.Show();
        }
    }
}
