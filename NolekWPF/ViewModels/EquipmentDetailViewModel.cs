using NolekWPF.DataServices;
using NolekWPF.Events;
using NolekWPF.Model;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NolekWPF.ViewModels
{
    public class EquipmentDetailViewModel : ViewModelBase, IEquipmentDetailViewModel
    {
        private IEquipmentDataService _dataService;
        private IEventAggregator _eventAggregator;
        private EquipmentView _equipment;
        private IErrorDataService _errorDataService;

        public EquipmentDetailViewModel(IEquipmentDataService dataService,
            IEventAggregator eventAggregator, IErrorDataService errorDataService)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenEquipmentDetailViewEvent>()
                .Subscribe(OnOpenEquipmentDetailView);
            _errorDataService = errorDataService;
        }

        private async void OnOpenEquipmentDetailView(int equipmentId)
        {
            await LoadAsync(equipmentId);
        }

        public async Task LoadAsync(int equipmentId)
        {
            try
            {
                Equipment = await _dataService.GetViewByIdAsync(equipmentId);
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
