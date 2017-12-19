using System.Windows.Input;
using NolekWPF.Wrappers;

namespace NolekWPF.ViewModels.Customers
{
    public interface ICustomerCreateViewModel
    {
        ICommand CreateCustomerCommand { get; }
        CustomerWrapper Customer { get; }
        bool HasChanges { get; set; }
    }
}