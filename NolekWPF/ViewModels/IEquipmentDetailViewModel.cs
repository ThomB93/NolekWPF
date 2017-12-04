using NolekWPF.Model;
using NolekWPF.Wrappers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolekWPF.ViewModels
{
    public interface IEquipmentDetailViewModel
    {
        Task LoadAsync(int equipmentId);
        ICommand UpdateCommand { get; }
        Equipment Equipment { get; }
        Task LoadTypesAsync();
        Task LoadConfigurationsAsync();
        Task LoadCategoriesAsync();
    }
}