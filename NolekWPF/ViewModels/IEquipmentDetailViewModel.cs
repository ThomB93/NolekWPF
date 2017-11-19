using System.Threading.Tasks;

namespace NolekWPF.ViewModels
{
    public interface IEquipmentDetailViewModel
    {
        Task LoadAsync(int equipmentId);
    }
}