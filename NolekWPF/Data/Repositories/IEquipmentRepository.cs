using System.Collections.Generic;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.Data.Repositories
{
    public interface IEquipmentRepository
    {
        void Add(Model.Equipment equipment);
        Task<Model.Equipment> GetByIdAsync(int equipId);
        Task<IEnumerable<EquipmentCategoryDto>> GetEquipmentCategoriesAsync();
        Task<IEnumerable<EquipmentConfigurationDto>> GetEquipmentConfigurationsAsync();
        Task<IEnumerable<EquipmentTypeDto>> GetEquipmentTypesAsync();
        bool HasChanges();
        void Remove(Model.Equipment model);
        Task SaveAsync();
        void Update(Model.Equipment model);
        void UpdateComponents(Model.Component model, int equipmentId, int quantity);
    }
}