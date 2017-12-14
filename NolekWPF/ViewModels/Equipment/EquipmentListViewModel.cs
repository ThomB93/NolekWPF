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
using NolekWPF.ViewModels;
using NolekWPF.Data.DataServices;
using NolekWPF.Helpers;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace NolekWPF.Equipment.ViewModels
{
    public class EquipmentListViewModel : ViewModelBase, IEquipmentListViewModel
    {
        private IEquipmentLookupDataService _equipmentLookupDataService;
        public ObservableCollection<EquipmentLookup> Equipments { get; }
        private IEventAggregator _eventAggregator;
        private IErrorDataService _errorDataService;
        public IEquipmentDetailViewModel EquipmentDetailViewModel { get; }
        ConvertTextToImage cv = new ConvertTextToImage();

        public EquipmentListViewModel(IEquipmentLookupDataService equipmentLookupDataService,
            IEventAggregator eventAggregator, IErrorDataService errorDataService, IEquipmentDetailViewModel equipmentDetailViewModel)
        {
            _equipmentLookupDataService = equipmentLookupDataService;
            Equipments = new ObservableCollection<EquipmentLookup>();
            //initialize event aggregator
            _eventAggregator = eventAggregator;
            _errorDataService = errorDataService;
            _eventAggregator.GetEvent<AfterEquipmentCreated>().Subscribe(RefreshList);
            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);
            EquipmentDetailViewModel = equipmentDetailViewModel;
            LoadDetailData();
            
            //var convert = cv.Convert(Equipments[0].ImagePath);
        }

        private async void LoadDetailData()
        {
            await EquipmentDetailViewModel.LoadCategoriesAsync();
            await EquipmentDetailViewModel.LoadConfigurationsAsync();
            await EquipmentDetailViewModel.LoadTypesAsync();
        }
        

        private async void RefreshList()
        {
            await LoadAsync();
        }

        private void OnLogin(Login user)
        {
            CurrentUser = user;
        }

        public Login CurrentUser { get; set; }      

        public async Task LoadAsync()
        {
            try
            {
                var lookup = await _equipmentLookupDataService.GetEquipmentLookupAsync();
                int i = 0;
                Equipments.Clear();
                foreach (var item in lookup)
                {
                    Equipments.Add(item);
                    //var convert = cv.Convert(Equipments[i].ImagePath);
                    i++;
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

        private EquipmentLookup _selectedEquipment;

        public EquipmentLookup SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                _selectedEquipment = value;
                if (_selectedEquipment != null && CurrentUser.Role == "Secretary")
                {
                    _eventAggregator.GetEvent<OpenEquipmentDetailViewEvent>()
                        .Publish(_selectedEquipment.EquipmentId);
                }
            }
        }

        
    }
}
