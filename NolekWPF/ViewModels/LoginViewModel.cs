using NolekWPF.Data.DataServices;
using NolekWPF.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NolekWPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private bool _isAuthenticated;
        private string _username;
        private string _password;
        private User _currentuser;

        private IUserDataService _userDataService;

        public LoginViewModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;

            LoginCommand = new DelegateCommand(Login);
        }

        #region Properties
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
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }       
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
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
        #endregion

        #region Commands

        public ICommand LoginCommand { get; }

        #endregion //Commands

        #region Command Methods

        public void Login()
        {
            List<User> Users = _userDataService.GetUser();
            foreach (var user in Users)
            {
                if (user.Username == Username && user.Password == Password)
                {
                    isAuthenticated = true;
                    break;
                }
            }
            if (isAuthenticated == false)
            {
                MessageBox.Show("Wrong username/password");
                Username = string.Empty;
                Password = string.Empty;
            }
            else if (isAuthenticated == true)
            {
                _currentuser = _userDataService.GetUserInstance(Username);
                Username = string.Empty;
                Password = string.Empty;
            }
        }
        #endregion
    }
}
