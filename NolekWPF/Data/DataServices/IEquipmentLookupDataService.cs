using System.Collections.Generic;
using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.Data.DataServices
{
    public interface IEquipmentLookupDataService
    {
        Task<IEnumerable<EquipmentLookup>> GetEquipmentLookupAsync();
    }
}