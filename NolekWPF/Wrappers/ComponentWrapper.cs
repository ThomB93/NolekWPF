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
    public class ComponentWrapper : ViewModelBase, INotifyDataErrorInfo
    {
        public ComponentWrapper(Model.Component model)
        {
            Model = model;
        }

        public Model.Component Model { get; }

        public int ComponentId
        {
            get { return Model.ComponentId; }
            set
            {
                Model.ComponentId = value; OnPropertyChanged();
            }
        }
        public string ComponentType
        {
            get { return Model.ComponentType; }
            set
            {
                Model.ComponentType = value; OnPropertyChanged();
                ValidateProperty(nameof(ComponentType));
            }
        }
        public string ComponentDescription
        {
            get { return Model.ComponentDescription; }
            set
            {
                Model.ComponentDescription = value; OnPropertyChanged();
                ValidateProperty(nameof(ComponentDescription));
            }
        }
        public string ComponentOrderNumber
        {
            get { return Model.ComponentOrderNumber; }
            set
            {
                Model.ComponentOrderNumber = value; OnPropertyChanged();
                ValidateProperty(nameof(ComponentOrderNumber));
            }
        }
        public string ComponentSerialNumber
        {
            get { return Model.ComponentSerialNumber; }
            set
            {
                Model.ComponentSerialNumber = value; OnPropertyChanged();
                ValidateProperty(nameof(ComponentSerialNumber));
            }
        }
        
        public string ComponentSupplyNumber
        {
            get { return Model.ComponentSupplyNumber; }
            set
            {
                Model.ComponentSupplyNumber = value; OnPropertyChanged();
                ValidateProperty(nameof(ComponentSupplyNumber));
            }
        }

        //--------------------------------------------------------------VALIDATION LOGIC-----------------------------------------------------------------------------
        private void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);
            switch (propertyName)
            {
                
                case nameof(ComponentType):
                    if (ComponentType.Length > 50)
                    {
                        AddError(propertyName, "Type Name cannot exceeed 50 characters.");
                    }
                    if (ComponentType.ToString().Length < 1)
                    {
                        AddError(propertyName, "Type Name is required.");
                    }
                    break;
                case nameof(ComponentOrderNumber):
                    if (ComponentOrderNumber.Length > 50)
                    {
                        AddError(propertyName, "Order No. cannot exceeed 50 characters.");
                    }
                    break;
                case nameof(ComponentSerialNumber):
                    if (ComponentSerialNumber.Length > 50)
                    {
                        AddError(propertyName, "Serial No. cannot exceeed 50 characters.");
                    }
                    if (ComponentSerialNumber.ToString().Length < 1)
                    {
                        AddError(propertyName, "Serial No. is required.");
                    }
                    break;
                case nameof(ComponentSupplyNumber):
                    if (ComponentSupplyNumber.Length > 50)
                    {
                        AddError(propertyName, "Supply No. cannot exceeed 50 characters.");
                    }
                    if (Regex.Matches(ComponentSupplyNumber, @"[a-zA-Z]").Count != 0)
                    {
                        AddError(propertyName, "Supply Number must consist of numbers only.");
                    }
                    if (ComponentSupplyNumber.ToString().Length < 1)
                    {
                        AddError(propertyName, "Supply No. is required.");
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
