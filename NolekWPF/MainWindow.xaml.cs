using NolekWPF.Pages;
using NolekWPF.ViewModels;
using System;
using System.Collections;
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

namespace NolekWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel; //assign injected view model to local property
            DataContext = viewModel; //set the data context for the view to the viewmodel
            Loaded += MainWindow_Loaded; //event when the window first loads
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        private void mnuEquipList_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new ListEquipmentPage(_viewModel.EquipmentListViewModel));
        }
        private void mnuEquipCreate_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new CreateEquipmentPage(_viewModel.EquipmentCreateViewModel));
        }
        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void compList_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new ListComponentPage(_viewModel.ComponentListViewModel));
        }

        private void mnuCreateComp_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Navigate(new CreateComponentPage(_viewModel.ComponentCreateViewModel));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = String.Empty;
            txtPassword.Text = String.Empty;
        }
    }
}
