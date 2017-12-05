using NolekWPF.Data.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.Model;
using System.Collections.ObjectModel;
using Prism.Events;
using NolekWPF.Events;
using System.Windows;

namespace NolekWPF.ViewModels.Component
{
    public class ComponentListViewModel : ViewModelBase, IComponentListViewModel
    {
        private IComponentDataService _componentDataService;
        public ObservableCollection<Model.Component> Components { get; }
        private IEventAggregator _eventAggregator;
        private IErrorDataService _errorDataService;

        public ComponentListViewModel(IComponentDataService componentDataService,
            IEventAggregator eventAggregator, IErrorDataService errorDataService)
        {
            _componentDataService = componentDataService;
            Components = new ObservableCollection<Model.Component>();
            //initialize event aggregator
            _eventAggregator = eventAggregator;
            _errorDataService = errorDataService;
            _eventAggregator.GetEvent<AfterComponentCreated>().Subscribe(RefreshList);
        }

        private async void RefreshList()
        {
            await LoadAsync();
        }

        public async Task LoadAsync()
        {
            try
            {
                var lookup = await _componentDataService.GetComponentLookupAsync();
                foreach (var item in lookup)
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

        private Model.Component _selectedComponent;

        public Model.Component SelectedComponent
        {
            get { return _selectedComponent; }
            set
            {
                _selectedComponent = value;
                if (_selectedComponent != null)
                {
                    _eventAggregator.GetEvent<OpenComponentDetailViewEvent>()
                        .Publish(_selectedComponent.ComponentId);
                }
            }
        }
    }
}
