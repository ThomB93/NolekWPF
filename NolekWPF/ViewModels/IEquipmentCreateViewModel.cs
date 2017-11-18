using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using NolekWPF.Model;

namespace NolekWPF.ViewModels
{
    public interface IEquipmentCreateViewModel
    {
        ICommand CreateEquipmentCommand { get; }
        Equipment Equipment { get; }
        Task LoadTypesAsync();
    }
}