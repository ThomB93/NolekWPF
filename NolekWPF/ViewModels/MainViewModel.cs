using NolekWPF.Data.DataServices;
using NolekWPF.Equipment.ViewModels;
using NolekWPF.Model;
using NolekWPF.ViewModels.Component;
using Prism.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NolekWPF.Helpers;

namespace NolekWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool _isAuthenticated;
        private string _visibility;
        private string _menuvisibility;
        private User _currentuser;

        public IEquipmentListViewModel EquipmentListViewModel { get; }
        public IEquipmentCreateViewModel EquipmentCreateViewModel { get; }
        public IEquipmentDetailViewModel EquipmentDetailViewModel { get; }
        public IComponentListViewModel ComponentListViewModel { get; }
        public IComponentDetailViewModel ComponentDetailViewModel { get; }
        public IComponentCreateViewModel ComponentCreateViewModel { get; }
        private IUserDataService _userDataService;

        public MainViewModel(IEquipmentListViewModel equipmentListViewModel,
            IEquipmentCreateViewModel equipmentCreateViewModel,
            IEquipmentDetailViewModel equipmentDetailViewModel, IComponentDetailViewModel componentDetailViewModel,
            IComponentCreateViewModel componentCreateViewModel, IComponentListViewModel componentListViewModel,
            IUserDataService userDataService)
        {
            EquipmentListViewModel = equipmentListViewModel;
            EquipmentCreateViewModel = equipmentCreateViewModel;
            EquipmentDetailViewModel = equipmentDetailViewModel;
            ComponentListViewModel = componentListViewModel;
            ComponentDetailViewModel = componentDetailViewModel;
            ComponentCreateViewModel = componentCreateViewModel;

            _userDataService = userDataService;
            
            MenuVisibility = "Collapsed";
            Btvoc = new BoolToVisibleOrCollapsed();

            LoginCommand = new DelegateCommand(Login);
        }
        public BoolToVisibleOrCollapsed Btvoc;

        public async Task LoadAsync() //method must be async when loading in async data and return a task
        {
            //load list data
            await EquipmentListViewModel.LoadAsync();
            await ComponentListViewModel.LoadAsync();

            await EquipmentCreateViewModel.LoadTypesAsync();
            await EquipmentCreateViewModel.LoadConfigurationsAsync();
            await EquipmentCreateViewModel.LoadCategoriesAsync();

            await EquipmentDetailViewModel.LoadTypesAsync();
            await EquipmentDetailViewModel.LoadConfigurationsAsync();
            await EquipmentDetailViewModel.LoadCategoriesAsync();
        }

        public bool isAuthenticated
        {
            get { return _isAuthenticated; }
            set
            {
                if (value != _isAuthenticated)
                {
                    _isAuthenticated = value;
                    OnPropertyChanged();
                }
            }
        }

        public User CurrentUser
        {
            get { return _currentuser; }
            set
            {
                _currentuser = value;
                OnPropertyChanged();
            }
        }

        public string Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }
        public string MenuVisibility
        {
            get { return _menuvisibility; }
            set
            {
                _menuvisibility = value;
                OnPropertyChanged();
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public void Login()
        {
            //TODO check username and password vs database here.
            //If using membershipprovider then just call Membership.ValidateUser(UserName, Password)
            List<User> Users = _userDataService.GetUser();
            foreach (var user in Users)
            {
                if (user.Username == Username && user.Password == Password)
                {
                    isAuthenticated = true;
                    Visibility = "Collapsed";
                    MenuVisibility = "Visible";
                    //MessageBox.Show("Good job");
                    break;
                }
            }
            if (isAuthenticated == false)
            {
                MessageBox.Show("Wrong username/password");
            }
        }

    }
}
