using System.Collections.ObjectModel;
using NolekWPF.Data.DataServices;
using NolekWPF.Model.Dto;
using System.Threading.Tasks;

namespace NolekWPF.ViewModels.Customers
{
    public interface ICustomerListViewModel
    {
        ICustomerDataService _customerDataService { get; }
        IErrorDataService _errorDataService { get; }
        ObservableCollection<CustomerDto> Customers { get; set; }
        CustomerDto SelectedCustomer { get; set; }

        Task LoadAsync();
    }
}