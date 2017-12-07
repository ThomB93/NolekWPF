using Autofac.Core;
using NolekWPF.Commands;
using NolekWPF.Data.DataServices;
using NolekWPF.Model;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace NolekWPF.ViewModels.Login
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private bool _isAuthenticated;
        private IEventAggregator _eventAggregator;
        private IUserDataService _userDataService;

        public LoginViewModel(IEventAggregator eventAggregator, IUserDataService userDataService)
        {
            LoginCommand = new DelegateCommand<bool>(Login);
            _eventAggregator = eventAggregator;
            _userDataService = userDataService;
        }

        public bool isAuthenticated
        {
            get { return _isAuthenticated; }
            set
            {
                if (value != _isAuthenticated)
                {
                    _isAuthenticated = value;
                    OnPropertyChanged("isAuthenticated");
                }
            }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public ICommand LoginCommand { get; }

        public bool Login()
        {
            //TODO check username and password vs database here.
            //If using membershipprovider then just call Membership.ValidateUser(UserName, Password)
            List<User> Users = _userDataService.GetUser();
            foreach (var user in Users)
            {
                if(user.Username == Username && user.Password == Password)
                {
                    return isAuthenticated = true;
                }
                else
                {
                    MessageBox.Show("Wrong username/password");
                    return isAuthenticated = false;

                }
            }
            return isAuthenticated = false;
        }
    }
}
