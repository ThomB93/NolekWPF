using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NolekWPF.Data.DataServices;
using NolekWPF.Events;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using Prism.Commands;

namespace NolekWPF.ViewModels.Equipment
{
    public class AddRemoveComponentViewModel : ViewModelBase, IAddRemoveComponentViewModel
    {
        private IEquipmentLookupDataService _equipmentLookupDataService;
        private IErrorDataService _errorDataService;
        private IComponentDataService _componentDataService;
        public ObservableCollection<EquipmentLookup> Equipments { get; }
        public ObservableCollection<ComponentDto> Components { get; }

        public AddRemoveComponentViewModel(IEquipmentLookupDataService equipmentLookupDataService, IErrorDataService errorDataService,
            IComponentDataService componentDataService)
        {
            _equipmentLookupDataService = equipmentLookupDataService;
            _componentDataService = componentDataService;
            _errorDataService = errorDataService;
            Equipments = new ObservableCollection<EquipmentLookup>();
            Components = new ObservableCollection<ComponentDto>();

            AddComponent = new DelegateCommand(OnComponentAdded);
            RemoveComponent = new DelegateCommand(OnComponentRemoved);
            SaveChanges = new DelegateCommand(OnChangesSaved);
        }

        private void OnChangesSaved()
        {
            throw new NotImplementedException();
        }

        private void OnComponentRemoved()
        {
            throw new NotImplementedException();
        }

        private void OnComponentAdded()
        {
            throw new NotImplementedException();
        }

        public async Task LoadAsync()
        {
            try
            {
                //LOAD EQUIPMENTS
                var lookup = await _equipmentLookupDataService.GetEquipmentLookupAsync();

                Equipments.Clear();
                foreach (var item in lookup)
                {
                    Equipments.Add(item);
                }
                //LOAD COMPONENTS
                var components = await _componentDataService.GetComponentLookupAsync();

                Components.Clear();
                foreach (var item in components)
                {
                    Components.Add(item);
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

        public ICommand AddComponent { get; }
        public ICommand RemoveComponent { get; }
        public ICommand SaveChanges { get; }
        //SELECTED EQUIPMENT AND COMPONENT
        private EquipmentLookup _selectedEquipment;

        public EquipmentLookup SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                _selectedEquipment = value;
            }
        }
        private ComponentDto _selectComponent;
        public ComponentDto SelectedComponent
        {
            get { return _selectComponent; }
            set
            {
                _selectComponent = value;
            }
        }
    }
}
