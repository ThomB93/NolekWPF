using Prism.Commands;
using System.Threading.Tasks;
using System.Windows.Input;
using System;

namespace NolekWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //contains all view models
        public IEquipmentListViewModel EquipmentListViewModel { get; }
        public IEquipmentCreateViewModel EquipmentCreateViewModel { get; }
        public IEquipmentDetailViewModel EquipmentDetailViewModel { get; }

        //command to switch views
        public ICommand NavCommand { get; set; }

        public MainViewModel(IEquipmentListViewModel equipmentListViewModel,
            IEquipmentCreateViewModel equipmentCreateViewModel,
            IEquipmentDetailViewModel equipmentDetailViewModel)
        {
            EquipmentListViewModel = equipmentListViewModel;
            EquipmentCreateViewModel = equipmentCreateViewModel;
            EquipmentDetailViewModel = equipmentDetailViewModel;

            NavCommand = new DelegateCommand<string>(OnNavExecute);
        }

        //each case corresponds to a menu item
        private void OnNavExecute(string destination)
        {
            switch (destination)
            {
                case "equipList":
                    CurrentViewModel = (EquipmentListViewModel)EquipmentListViewModel;
                    break;
                case "equipCreate":
                    
                    CurrentViewModel = (EquipmentCreateViewModel)EquipmentCreateViewModel;
                    break;
                    //...
                default:
                    break;
            }
        }

        public async Task LoadAsync() //method must be async when loading in async data and return a task
        {
            //load data for dropdowns
            await EquipmentListViewModel.LoadAsync();
            await EquipmentCreateViewModel.LoadTypesAsync();
            await EquipmentCreateViewModel.LoadConfigurationsAsync();
            await EquipmentCreateViewModel.LoadCategoriesAsync();
        }

        private ViewModelBase currentViewModel { get; set; }

        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                OnPropertyChanged();
            }
        }

        
    }
}
