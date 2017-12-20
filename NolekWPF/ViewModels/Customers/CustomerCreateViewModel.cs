﻿using NolekWPF.Wrappers;
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
using NolekWPF.Model.Dto;
using System.Collections.ObjectModel;

namespace NolekWPF.ViewModels.Customers
{
    class CustomerCreateViewModel : ViewModelBase, ICustomerCreateViewModel
    {
        private CustomerWrapper _customer;

        private ICustomerRepository _customerRepository;
        private IErrorDataService _errorDataService;
        private IEventAggregator _eventAggregator;
        private bool _hasChanges;
        public Login CurrentUser { get; set; }

        public CustomerCreateViewModel(ICustomerRepository customerRepository, IErrorDataService errorDataService, IEventAggregator eventAggregator)
        {
            CreateCustomerCommand = new DelegateCommand(OnCreateCustomerExecute, OnCustomerCreateCanExecute);
            _customerRepository = customerRepository;
            _errorDataService = errorDataService;
            Customer = CreateNewCustomer();
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<AfterUserLogin>().Subscribe(OnLogin);
        }

        private void OnLogin(Login user)
        {
            CurrentUser = user;
        }

        public CustomerWrapper Customer
        {
            get { return _customer; }
            private set
            {
                _customer = value;
                OnPropertyChanged();
            }
        }

        private bool OnCustomerCreateCanExecute()
        {
            //validate fields to disable/enable buton
            return Customer != null && !Customer.HasErrors;
        }

        private async void OnCreateCustomerExecute()
        {
            try
            {
                await _customerRepository.SaveAsync();
                Customer = CreateNewCustomer();
                MessageBox.Show("Customer was successfully created.");
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
                    ErrorStackTrace = e.StackTrace,
                    LoginId = CurrentUser.LoginId
                };
                await _errorDataService.AddError(error);
            }
        }

        public ICommand CreateCustomerCommand { get; }

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

        private CustomerWrapper CreateNewCustomer() //calls the add method in the repository to insert new equipment and return it
        {
            var customer = new CustomerWrapper(new CustomerDto());

            //when property in equipment changes, and it has errors, disable the create button
            customer.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Customer.HasErrors))
                {
                    ((DelegateCommand)CreateCustomerCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)CreateCustomerCommand).RaiseCanExecuteChanged();

            //default values
            customer.CustomerName = "";
            customer.Equipments = new ObservableCollection<EquipmentDto>();
            customer.Departments = new ObservableCollection<CustomerDepartmentDto>();
           

            //_customerRepository.Add(customer.Model); //context is aware of the equipment to add
            return customer;
        }
    }
}
