using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolekWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public IEquipmentListViewModel EquipmentListViewModel { get; }
        public IEquipmentCreateViewModel EquipmentCreateViewModel { get; }

        public MainViewModel(IEquipmentListViewModel equipmentListViewModel, IEquipmentCreateViewModel equipmentCreateViewModel)
        {
            EquipmentListViewModel = equipmentListViewModel;
            EquipmentCreateViewModel = equipmentCreateViewModel;
        }

        public async Task LoadAsync() //method must be async when loading in async data and return a task
        {
            /*var friends = await _friendDataService.GetAllAsync();
            Friends.Clear(); //avoid duplicated if load method is called twice
            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }*/
            await EquipmentListViewModel.LoadAsync(); //load up the list to the left
        }
    }
}
