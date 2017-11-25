using NolekWPF.DataServices;
using NolekWPF.Events;
using NolekWPF.Model;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.ViewModels
{
    public class EquipmentDetailViewModel : ViewModelBase, IEquipmentDetailViewModel
    {
        private IEquipmentDataService _dataService;
        private IEventAggregator _eventAggregator;

        public EquipmentDetailViewModel(IEquipmentDataService dataService,
            IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenEquipmentDetailViewEvent>()
                .Subscribe(OnOpenEquipmentDetailView);
        }

        private async void OnOpenEquipmentDetailView(int equipmentId)
        {
            await LoadAsync(equipmentId);
        }

        public async Task LoadAsync(int equipmentId)
        {
            Equipment = await _dataService.GetViewByIdAsync(equipmentId);
        }

        private EquipmentView _equipment;    

        public EquipmentView Equipment
        {
            get { return _equipment; }
            private set
            {
                _equipment = value;
                OnPropertyChanged();
            }
        }

    }
}
