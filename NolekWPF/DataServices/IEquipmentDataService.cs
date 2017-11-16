using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.DataServices
{
    public interface IEquipmentDataService
    {
        Task<Equipment> GetByIdAsync(int equipmentId);
    }
}