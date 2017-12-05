using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.ViewModels.Component
{
    public interface IComponentListViewModel
    {
        ObservableCollection<Model.Component> Components { get; }
        Model.Component SelectedComponent { get; set; }

        Task LoadAsync();
    }
}