using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.Equipment.ViewModels
{
    public interface IEquipmentDetailViewModel
    {
        Model.Equipment Equipment { get; }
        IEnumerable<EquipmentCategoryDto> EquipmentCategories { get; }
        IEnumerable<EquipmentConfigurationDto> EquipmentConfigurations { get; }
        IEnumerable<EquipmentTypeDto> EquipmentTypes { get; }
        ICommand UpdateCommand { get; }

        Task LoadAsync(int equipmentId);
        Task LoadCategoriesAsync();
        Task LoadConfigurationsAsync();
        Task LoadTypesAsync();
    }
}