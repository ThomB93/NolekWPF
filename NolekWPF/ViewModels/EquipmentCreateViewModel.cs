using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NolekWPF.DataServices;
using NolekWPF.DataServices.Repositories;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using Prism.Commands;

namespace NolekWPF.ViewModels
{
    public class EquipmentCreateViewModel : ViewModelBase, IEquipmentCreateViewModel
    {
        private Equipment _equipment;
        private IEnumerable<EquipmentCategoryDto> _equipmentCategory;
        private IEnumerable<EquipmentConfigurationDto> _equipmentConfiguration;
        private IEnumerable<EquipmentTypeDto> _equipmentType;
        private IEquipmentRepository _equipmentRepository;
        private bool _hasChanges;

        public EquipmentCreateViewModel(IEquipmentRepository equipmentRepository)
        {
            CreateEquipmentCommand = new DelegateCommand(OnCreateEquipmentExecute, OnEquipmentCreateCanExecute);
            _equipmentRepository = equipmentRepository;
            Equipment = CreateNewEquipment(); //assign the equipment to add to the Equipment property           
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

        public Equipment Equipment
        {
            get { return _equipment; }
            private set
            {
                _equipment = value;
                OnPropertyChanged();
            }
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

        private bool OnEquipmentCreateCanExecute()
        {
            return true; //need validation
        }

        private async void OnCreateEquipmentExecute()
        {
            MessageBox.Show("Equipment was successfully created.");
            await _equipmentRepository.SaveAsync();
            Equipment = CreateNewEquipment();
        }

        public ICommand CreateEquipmentCommand { get; }

        public bool HasChanges //is true if changes has been made to equipment
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)CreateEquipmentCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private Equipment CreateNewEquipment() //calls the add method in the repository to insert new equipment and return it
        {
            var equipment = new Equipment();

            //default values
            equipment.EquipmentStatus = false;
            equipment.EquipmentCategoryId = 1;
            equipment.EquipmentConfigurationID = 1;
            equipment.EquipmentTypeID = 1;
            
            _equipmentRepository.Add(equipment); //context is aware of the equipment to add
            return equipment;
        }
    }
}
