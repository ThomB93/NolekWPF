using System.Windows.Input;

namespace NolekWPF.ViewModels.Login
{
    public interface ILoginViewModel
    {
        bool isAuthenticated { get; set; }
        ICommand LoginCommand { get; }
        string Password { get; set; }
        string Username { get; set; }

        bool Login();
    }
}