using NolekWPF.DataServices;
using NolekWPF.DataServices.Repositories;
using NolekWPF.Events;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using NolekWPF.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NolekWPF.ViewModels
{
    public class EquipmentDetailViewModel : ViewModelBase, IEquipmentDetailViewModel
    {
        private IEventAggregator _eventAggregator;
        private EquipmentView _equipmentView;
        private Equipment _equipment;
        //private EquipmentWrapper _equipment;
        private IEquipmentRepository _equipmentRepository;
        private IErrorDataService _errorDataService;
        private IEnumerable<EquipmentCategoryDto> _equipmentCategory;
        private IEnumerable<EquipmentConfigurationDto> _equipmentConfiguration;
        private IEnumerable<EquipmentTypeDto> _equipmentType;

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
            await _equipmentRepository.SaveAsync();
            Equipment = UpdateEquipment();
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

        public ICommand UpdateCommand { get; }

        //public EquipmentView EquipmentView
        //{
        //    get { return _equipmentView; }
        //    private set
        //    {
        //        _equipmentView = value;
        //        OnPropertyChanged();
        //    }
        //}

        public Equipment Equipment
        {
            get { return _equipment; }
            private set
            {
                _equipment = value;
                OnPropertyChanged();
            }
        }

        private Equipment UpdateEquipment() 
        {
            var equipment = new Equipment();

            ((DelegateCommand)UpdateCommand).RaiseCanExecuteChanged();

            _equipmentRepository.Update(equipment); 
            return equipment;
        }

    }
}
