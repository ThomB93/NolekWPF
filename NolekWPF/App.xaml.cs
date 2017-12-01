using Autofac;
using NolekWPF.DataServices;
using NolekWPF.Model;
using NolekWPF.Startup;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NolekWPF.DataAccess;

namespace NolekWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private IErrorDataService _errorDataService;
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();
            var mainWindow = container.Resolve<MainWindow>(); //the main window must have the view model passed to its constructor, and the viewmodel needs the data access passed to its constructor
            //resolve will automatically inject all required object to constructors 
            mainWindow.Show();
        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Exception Caught", MessageBoxButton.OK, MessageBoxImage.Error); e.Handled = true;        

            Error error = new Error
            {
                ErrorMessage = e.Exception.Message,
                ErrorTimeStamp = DateTime.Now,
                ErrorStackTrace = e.Exception.StackTrace
            };
            //await _errorDataService.AddError(error);
        }
    }
}
