
using NolekWPF.Data.DataServices;
using NolekWPF.Data.Repositories;
using NolekWPF.Events;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using NolekWPF.ViewModels;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NolekWPF.Equipment.ViewModels
{
    public class EquipmentDetailViewModel : ViewModelBase, IEquipmentDetailViewModel
    {
        private IEquipmentRepository _equipmentRepository;
        private IEventAggregator _eventAggregator;

        public EquipmentDetailViewModel(IEventAggregator eventAggregator, IErrorDataService errorDataService, IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenEquipmentDetailViewEvent>()
                .Subscribe(OnOpenEquipmentDetailView);
            _errorDataService = errorDataService;

            UpdateCommand = new DelegateCommand(OnUpdateExecute);
            Equipment = UpdateEquipment();
        }

        private async void OnUpdateExecute()
        {
            try
            {
                await _equipmentRepository.SaveAsync();
                Equipment = UpdateEquipment();
            }
            catch (Exception e)
            {
                Error error = new Error
                {
                    ErrorMessage = e.Message,
                    ErrorTimeStamp = DateTime.Now,
                    ErrorStackTrace = e.StackTrace
                };
                await _errorDataService.AddError(error);
            }
        }

        private async void OnOpenEquipmentDetailView(int equipmentId)
        {
            await LoadAsync(equipmentId);
        }

        public async Task LoadAsync(int equipmentId)
        {
            try
            {
                Equipment = await _equipmentRepository.GetByIdAsync(equipmentId);
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

        //load up data for the combo boxes
        public async Task LoadTypesAsync()
        {
            var types = await _equipmentRepository.GetEquipmentTypesAsync();
            EquipmentTypes = types;
        }
        public async Task LoadConfigurationsAsync()
        {
            var configurations = await _equipmentRepository.GetEquipmentConfigurationsAsync();
            EquipmentConfigurations = configurations;
        }
        public async Task LoadCategoriesAsync()
        {
            var categories = await _equipmentRepository.GetEquipmentCategoriesAsync();
            EquipmentCategories = categories;
        }

        public IEnumerable<EquipmentTypeDto> EquipmentTypes
        {
            get { return _equipmentType; }
            private set
            {
                _equipmentType = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<EquipmentCategoryDto> EquipmentCategories
        {
            get { return _equipmentCategory; }
            private set
            {
                _equipmentCategory = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<EquipmentConfigurationDto> EquipmentConfigurations
        {
            get { return _equipmentConfiguration; }
            private set
            {
                _equipmentConfiguration = value;
                OnPropertyChanged();
            }
        }

        private IErrorDataService _errorDataService;
        private IEnumerable<EquipmentTypeDto> _equipmentType;
        private IEnumerable<EquipmentCategoryDto> _equipmentCategory;
        private IEnumerable<EquipmentConfigurationDto> _equipmentConfiguration;
        private Model.Equipment _equipment;

        public ICommand UpdateCommand { get; }

        public Model.Equipment Equipment
        {
            get { return _equipment; }
            private set
            {
                _equipment = value;
                OnPropertyChanged();
            }
        }

        private Model.Equipment UpdateEquipment()
        {
            var equipment = new Model.Equipment();

            ((DelegateCommand)UpdateCommand).RaiseCanExecuteChanged();

            _equipmentRepository.Update(equipment);
            return equipment;
        }

    }
}
