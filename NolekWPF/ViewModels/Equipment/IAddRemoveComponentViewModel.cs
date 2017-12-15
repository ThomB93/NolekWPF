using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.ViewModels.Equipment
{
    public interface IAddRemoveComponentViewModel
    {
        ICommand AddComponent { get; }
        ObservableCollection<ComponentDto> Components { get; }
        ObservableCollection<EquipmentLookup> Equipments { get; }
        ICommand RemoveComponent { get; }
        ICommand SaveChanges { get; }
        ComponentDto SelectedComponent { get; set; }
        EquipmentLookup SelectedEquipment { get; set; }

        Task LoadAsync();
    }
}