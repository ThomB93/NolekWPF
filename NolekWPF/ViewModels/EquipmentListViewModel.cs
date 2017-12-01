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
using System.Windows;

namespace NolekWPF.ViewModels
{
    public class EquipmentListViewModel : ViewModelBase, IEquipmentListViewModel
    {
        private IEquipmentLookupDataService _equipmentLookupDataService;
        public ObservableCollection<EquipmentLookup> Equipments { get; }
        private IEventAggregator _eventAggregator;
        private IErrorDataService _errorDataService;

        public EquipmentListViewModel(IEquipmentLookupDataService equipmentLookupDataService,
            IEventAggregator eventAggregator, IErrorDataService errorDataService)
        {
            _equipmentLookupDataService = equipmentLookupDataService;
            Equipments = new ObservableCollection<EquipmentLookup>();
            //initialize event aggregator
            _eventAggregator = eventAggregator;
            _errorDataService = errorDataService;
            _eventAggregator.GetEvent<AfterEquipmentCreated>().Subscribe(RefreshList);
        }

        private async void RefreshList()
        {
            await LoadAsync();
        }

        public async Task LoadAsync()
        {
            try
            {
                var lookup = await _equipmentLookupDataService.GetEquipmentLookupAsync();
                foreach (var item in lookup)
                {
                    Equipments.Add(item);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "An error occurred", MessageBoxButton.OK, MessageBoxImage.Warning);
                //create new error object from the exception and add to DB
                Error error = new Error
                {
                    ErrorMessage = e.Message,
                    ErrorTimeStamp = DateTime.Now,
                    ErrorStackTrace = e.StackTrace
                };
                await _errorDataService.AddError(error);
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
