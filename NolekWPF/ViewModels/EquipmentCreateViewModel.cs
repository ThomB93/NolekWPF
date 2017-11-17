using NolekWPF.DataServices;
using NolekWPF.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolekWPF.ViewModels
{
    public class EquipmentCreateViewModel : ViewModelBase, IEquipmentCreateViewModel
    {
        private IEquipmentDataService _equipmentDataService;

        public EquipmentCreateViewModel(IEquipmentDataService equipmentDataService)
        {
            _equipmentDataService = equipmentDataService;
            InsertEquipmentCommand = new DelegateCommand(OnEquipmentInsertExecute, OnEquipmentInsertCanExecute);
        }

        private bool OnEquipmentInsertCanExecute() //check if the equipment can be inserted before doing so
        {
            return Equipment != null;
        }

        private void OnEquipmentInsertExecute()
        {
            Equipment.EquipmentStatus = false; //default status
            _equipmentDataService.InsertNewEquipment(Equipment);
        }

        private Equipment _equipment;

        public Equipment Equipment
        {
            get { return _equipment; }
            private set
            {
                _equipment = value;
                OnPropertyChanged();
            }
        }

        public ICommand InsertEquipmentCommand { get; }
    }
}
