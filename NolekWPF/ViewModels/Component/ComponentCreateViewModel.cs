
using NolekWPF.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.Model;
using System.Windows.Input;
using System.Windows;
using NolekWPF.Events;
using NolekWPF.Data.Repositories;
using NolekWPF.Data.DataServices;

namespace NolekWPF.ViewModels.Component
{
    public class ComponentCreateViewModel : ViewModelBase, IComponentCreateViewModel
    {
        private ComponentWrapper _component;

        private IComponentRepository _componentRepository;
        private IErrorDataService _errorDataService;
        private IEventAggregator _eventAggregator;
        private bool _hasChanges;

        public ComponentCreateViewModel(IComponentRepository equipmentRepository, IErrorDataService errorDataService, IEventAggregator eventAggregator)
        {
            CreateComponentCommand = new DelegateCommand(OnCreateComponentExecute, OnComponentCreateCanExecute);
            _componentRepository = equipmentRepository;
            _errorDataService = errorDataService;
            Component = CreateNewComponent();  
            _eventAggregator = eventAggregator;
        }
       
        public ComponentWrapper Component
        {
            get { return _component; }
            private set
            {
                _component = value;
                OnPropertyChanged();
            }
        }

        private bool OnComponentCreateCanExecute()
        {
            //validate fields to disable/enable buton
            return Component != null && !Component.HasErrors;
        }

        private async void OnCreateComponentExecute()
        {
            try
            {
                await _componentRepository.SaveAsync();
                Component = CreateNewComponent();
                MessageBox.Show("Equipment was successfully created.");
                _eventAggregator.GetEvent<AfterComponentCreated>().Publish();
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

        public ICommand CreateComponentCommand { get; }

        public bool HasChanges //is true if changes has been made to equipment
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                }
            }
        }

        private ComponentWrapper CreateNewComponent() //calls the add method in the repository to insert new equipment and return it
        {
            var component = new ComponentWrapper(new Model.Component());

            //when property in equipment changes, and it has errors, disable the create button
            component.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Component.HasErrors))
                {
                    ((DelegateCommand)CreateComponentCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)CreateComponentCommand).RaiseCanExecuteChanged();

            //default values
            

            _componentRepository.Add(component.Model); //context is aware of the equipment to add
            return component;
        }
    }
}
