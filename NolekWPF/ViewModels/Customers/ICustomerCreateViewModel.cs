using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NolekWPF.Model;
using NolekWPF.Wrappers;
using System.Threading.Tasks;

namespace NolekWPF.ViewModels.Customers
{
    public interface ICustomerCreateViewModel
    {
        ICommand CreateCustomerCommand { get; }
        Login CurrentUser { get; set; }
        CustomerWrapper Customer { get; }
        ObservableCollection<CustomerDepartment> Departments { get; set; }
        ObservableCollection<EquipmentLookup> Equipments { get; set; }
        bool HasChanges { get; set; }
        List<Model.Equipment> SelectedEquipments { get; set; }

        Task LoadEquipment();
    }
}