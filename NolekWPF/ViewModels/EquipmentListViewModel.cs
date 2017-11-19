using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.DataServices;
using System.Collections.ObjectModel;
using Prism.Events;
using NolekWPF.Events;

namespace NolekWPF.ViewModels
{
    public class EquipmentListViewModel : ViewModelBase, IEquipmentListViewModel
    {
        private IEquipmentLookupDataService _equipmentLookupDataService;
        public ObservableCollection<EquipmentLookup> Equipments { get; }
        private IEventAggregator _eventAggregator;

        public EquipmentListViewModel(IEquipmentLookupDataService equipmentLookupDataService,
            IEventAggregator eventAggregator)
        {
            _equipmentLookupDataService = equipmentLookupDataService;
            Equipments = new ObservableCollection<EquipmentLookup>();
            //initialize event aggregator
            _eventAggregator = eventAggregator;
        }

        public async Task LoadAsync()
        {
            var lookup = await _equipmentLookupDataService.GetEquipmentLookupAsync();
            foreach (var item in lookup)
            {
                Equipments.Add(item);
            }
        }

        private EquipmentLookup _selectedEquipment;

        public EquipmentLookup SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                _selectedEquipment = value;
                if (_selectedEquipment != null)
                {
                    _eventAggregator.GetEvent<OpenEquipmentDetailViewEvent>()
                        .Publish(_selectedEquipment.EquipmentId);
                }
            }
        }
    }
}
