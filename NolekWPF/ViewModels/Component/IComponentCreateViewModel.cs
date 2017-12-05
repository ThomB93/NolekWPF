using System.Windows.Input;
using NolekWPF.Wrappers;

namespace NolekWPF.ViewModels.Component
{
    public interface IComponentCreateViewModel
    {
        ComponentWrapper Component { get; }
        ICommand CreateComponentCommand { get; }
        bool HasChanges { get; set; }
    }
}