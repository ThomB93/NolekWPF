using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.ViewModels.Component
{
    public interface IComponentDetailViewModel
    {
        Model.Component Component { get; }

        Task LoadAsync(int componentId);
    }
}