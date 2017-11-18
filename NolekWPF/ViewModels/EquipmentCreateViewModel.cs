using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NolekWPF.DataServices;
using NolekWPF.DataServices.Repositories;
using NolekWPF.Model;
using Prism.Commands;

namespace NolekWPF.ViewModels
{
    public class EquipmentCreateViewModel : ViewModelBase, IEquipmentCreateViewModel
    {
        private Equipment _equipment;
        private IEnumerable<EquipmentCategory> _equipmentCategory;
        private IEnumerable<EquipmentConfiguration> _equipmentConfiguration;
        private IEnumerable<EquipmentTypeDto> _equipmentType;
        private IEquipmentRepository _equipmentRepository;
        private bool _hasChanges;

        public EquipmentCreateViewModel(IEquipmentRepository equipmentRepository)
        {
            CreateEquipmentCommand = new DelegateCommand(OnCreateEquipmentExecute, OnEquipmentCreateCanExecute);
            _equipmentRepository = equipmentRepository;
            Equipment = CreateNewEquipment(); //assign the equipment to add to the Equipment property
            
        }

        public async Task LoadTypesAsync()
        {
            var types = await _equipmentRepository.GetEquipmentTypes();
            EquipmentTypes = types;
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
        public IEnumerable<EquipmentCategory> EquipmentCategories
        {
            get { return _equipmentCategory; }
            private set
            {
                _equipmentCategory = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<EquipmentConfiguration> EquipmentConfigurations
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
           
            await _equipmentRepository.SaveAsync(); 
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
            equipment.EquipmentStatus = false;
            equipment.EquipmentCategoryId = 2;
            equipment.EquipmentConfigurationID = 2;
            //equipment.EquipmentTypeID = 2;
            //equipment.EquipmentCategoryId = 2;
            //equipment.EquipmentConfigurationID = 3;
            //equipment.EquipmentImagePath = "somepath.url";
            //equipment.EquipmentMainEquipmentNumber = "54874562";
            //equipment.EquipmentStatus = false;
            //equipment.EquipmentTypeID = 2;
            _equipmentRepository.Add(equipment); //context is aware of the equipment to add
            return equipment;
        }
    }
}
