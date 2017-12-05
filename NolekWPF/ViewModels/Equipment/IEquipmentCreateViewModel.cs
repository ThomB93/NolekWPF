using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using NolekWPF.Model;
using NolekWPF.Wrappers;

namespace NolekWPF.Equipment.ViewModels
{
    public interface IEquipmentCreateViewModel
    {
        ICommand CreateEquipmentCommand { get; }
        EquipmentWrapper Equipment { get; }
        Task LoadTypesAsync();
        Task LoadConfigurationsAsync();
        Task LoadCategoriesAsync();
    }
}