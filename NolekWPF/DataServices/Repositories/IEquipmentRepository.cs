using System.Collections.Generic;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.DataServices.Repositories
{
    public interface IEquipmentRepository
    {
        void Add(Equipment equipment);
        Task<Equipment> GetByIdAsync(int equipId);
        bool HasChanges();
        void Remove(Equipment model);
        Task SaveAsync();
        Task<IEnumerable<EquipmentTypeDto>> GetEquipmentTypesAsync();
        Task<IEnumerable<EquipmentConfigurationDto>> GetEquipmentConfigurationsAsync();
        Task<IEnumerable<EquipmentCategoryDto>> GetEquipmentCategoriesAsync();
    }
}