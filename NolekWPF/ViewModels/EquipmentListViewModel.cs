using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.DataServices;
using System.Collections.ObjectModel;

namespace NolekWPF.ViewModels
{
    public class EquipmentListViewModel : ViewModelBase, IEquipmentListViewModel
    {
        private IEquipmentLookupDataService _equipmentLookupDataService;
        public ObservableCollection<EquipmentListItemViewModel> Equipments { get; }

        public EquipmentListViewModel(IEquipmentLookupDataService equipmentLookupDataService)
        {
            _equipmentLookupDataService = equipmentLookupDataService;
            Equipments = new ObservableCollection<EquipmentListItemViewModel>();
            //initialize event aggregator
        }

        public async Task LoadAsync()
        {

            var lookup = await _equipmentLookupDataService.GetEquipmentLookupAsync();
            Equipments.Clear();
            foreach (var item in lookup)
            {
                Equipments.Add(new EquipmentListItemViewModel(item.EquipmentId, item.DisplayMember));
            }
        }

        private EquipmentListItemViewModel _selectedEquipment;

        public EquipmentListItemViewModel SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                _selectedEquipment = value;
                OnPropertyChanged();
                if (_selectedEquipment != null)
                {
                    //event publish
                }
            }
        }
    }
}
