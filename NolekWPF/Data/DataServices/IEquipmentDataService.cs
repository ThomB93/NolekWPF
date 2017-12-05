using System.Collections.Generic;
using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.Data.DataServices
{
    public interface IEquipmentDataService
    {
        Task<Model.Equipment> GetByIdAsync(int equipmentId);
        Task<IEnumerable<EquipmentType>> GetTypesAsync();
        Task<EquipmentView> GetViewByIdAsync(int equipmentId);
    }
}