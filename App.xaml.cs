using EasySave.ViewModels;
using System.Windows;

namespace EasySave
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            var vm = new MainViewModel();
            Window w = new MainWindow()
            {
                DataContext = vm,
            };
            w.Show();

            
            
            base.OnStartup(e);
        }
    }
}
