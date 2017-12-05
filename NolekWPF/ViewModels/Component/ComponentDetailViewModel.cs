using NolekWPF.Data.DataServices;
using NolekWPF.Events;
using NolekWPF.Model;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NolekWPF.ViewModels.Component
{
    public class ComponentDetailViewModel : ViewModelBase, IComponentDetailViewModel
    {
        private IComponentDataService _dataService;
        private IEventAggregator _eventAggregator;
        private IErrorDataService _errorDataService;

        public ComponentDetailViewModel(IComponentDataService dataService,
            IEventAggregator eventAggregator, IErrorDataService errorDataService)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenComponentDetailViewEvent>()
                .Subscribe(OnOpenComponentDetailView);
            _errorDataService = errorDataService;
        }

        private async void OnOpenComponentDetailView(int componentId)
        {
            await LoadAsync(componentId);
        }

        public async Task LoadAsync(int componentId)
        {
            try
            {
                Component = await _dataService.GetByIdAsync(componentId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "An error occurred", MessageBoxButton.OK, MessageBoxImage.Warning);
                Error error = new Error
                {
                    ErrorMessage = e.Message,
                    ErrorTimeStamp = DateTime.Now,
                    ErrorStackTrace = e.StackTrace
                };
                await _errorDataService.AddError(error);
            }
        }

        private Model.Component _component;
        public Model.Component Component
        {
            get { return _component; }
            private set
            {
                _component = value;
                OnPropertyChanged();
            }
        }
    }
}
