using NolekWPF.Model;
using NolekWPF.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NolekWPF.Wrappers
{
   public class CustomerWrapper : ViewModelBase, INotifyDataErrorInfo
    {
        public CustomerWrapper(Model.Customer model)
        {
            Model = model;
        }

        public Model.Customer Model { get; }

        public int CustomerId
        {
            get { return Model.CustomerId; }
            set
            {
                Model.CustomerId = value; OnPropertyChanged();
            }
        }
        public string CustomerName
        {
            get { return Model.CustomerName; }
            set
            {
                Model.CustomerName = value; OnPropertyChanged();
                ValidateProperty(nameof(CustomerName));
            }
        }
        public int ContactPersonId
        {
            get { return Model.ContactPersonId; }
            set
            {
                Model.ContactPersonId = value; OnPropertyChanged();
                ValidateProperty(nameof(ContactPersonId));
            }
        }
        

        //--------------------------------------------------------------VALIDATION LOGIC-----------------------------------------------------------------------------
        private void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);
            switch (propertyName)
            {
                
                 
                case nameof(CustomerName):
                    if (CustomerName.Length > 50)
                    {
                        AddError(propertyName, "Name cannot exceeed 50 characters.");
                    }
                    if (CustomerName.ToString().Length < 1)
                    {
                        AddError(propertyName, "Name is required.");
                    }
                    break;
                
                default:
                    break;
            }
        }

        //Dictionary of error lists for each property
        private Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any(); //check of there are any entries in the dictionary

        //event to raise when there is a change in the amount of errors, receives the property changed as parameter
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        //check of the dictionary has a key of property name, if so returns a list of errors for the property, else return null
        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;
        }

        //event handler
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }

        //will add a new error to a property
        private void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName)) //check if there is an entry in the dictionary for the property
            {
                _errorsByPropertyName[propertyName] = new List<string>(); //clear the list of errors for the property
            }
            if (!_errorsByPropertyName[propertyName].Contains(error)) //check if the error already exists in the list
            {
                _errorsByPropertyName[propertyName].Add(error); //if it doesnt, add the error
                OnErrorsChanged(error);
            }
        }

        //will clear all errors of a chosen property
        private void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
