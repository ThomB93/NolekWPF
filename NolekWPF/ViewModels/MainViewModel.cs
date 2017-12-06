using NolekWPF.Equipment.ViewModels;
using NolekWPF.ViewModels.Component;
using System.Threading.Tasks;

namespace NolekWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public IEquipmentListViewModel EquipmentListViewModel { get; }
        public IEquipmentCreateViewModel EquipmentCreateViewModel { get; }
        public IEquipmentDetailViewModel EquipmentDetailViewModel { get; }
        public IComponentListViewModel ComponentListViewModel { get; }
        public IComponentDetailViewModel ComponentDetailViewModel { get; }
        public IComponentCreateViewModel ComponentCreateViewModel { get; }

        public MainViewModel(IEquipmentListViewModel equipmentListViewModel,
            IEquipmentCreateViewModel equipmentCreateViewModel,
            IEquipmentDetailViewModel equipmentDetailViewModel, IComponentDetailViewModel componentDetailViewModel,
            IComponentCreateViewModel componentCreateViewModel, IComponentListViewModel componentListViewModel)
        {
            EquipmentListViewModel = equipmentListViewModel;
            EquipmentCreateViewModel = equipmentCreateViewModel;
            EquipmentDetailViewModel = equipmentDetailViewModel;
            ComponentListViewModel = componentListViewModel;
            ComponentDetailViewModel = componentDetailViewModel;
            ComponentCreateViewModel = componentCreateViewModel;
        }

        public async Task LoadAsync() //method must be async when loading in async data and return a task
        {
            //load list data
            await EquipmentListViewModel.LoadAsync();
            await ComponentListViewModel.LoadAsync();

            await EquipmentCreateViewModel.LoadTypesAsync();
            await EquipmentCreateViewModel.LoadConfigurationsAsync();
            await EquipmentCreateViewModel.LoadCategoriesAsync();

            await EquipmentDetailViewModel.LoadTypesAsync();
            await EquipmentDetailViewModel.LoadConfigurationsAsync();
            await EquipmentDetailViewModel.LoadCategoriesAsync();
        }
    }
}
