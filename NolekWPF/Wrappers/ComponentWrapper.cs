using NolekWPF.Model;
using NolekWPF.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        public string ComponentName
        {
            get { return Model.ComponentName; }
            set
            {
                Model.ComponentName = value; OnPropertyChanged();
            }
        }
        public string ComponentDescription
        {
            get { return Model.ComponentDescription; }
            set
            {
                Model.ComponentDescription = value; OnPropertyChanged();
            }
        }
        public string ComponentOrderNumber
        {
            get { return Model.ComponentOrderNumber; }
            set
            {
                Model.ComponentOrderNumber = value; OnPropertyChanged();
            }
        }
        public string ComponentSerialNumber
        {
            get { return Model.ComponentSerialNumber; }
            set
            {
                Model.ComponentSerialNumber = value; OnPropertyChanged();
            }
        }
        public int ComponentQuantity
        {
            get { return Model.ComponentQuantity; }
            set
            {
                Model.ComponentQuantity = value; OnPropertyChanged();
            }
        }
        public string ComponentSupplyNumber
        {
            get { return Model.ComponentSupplyNumber; }
            set
            {
                Model.ComponentSupplyNumber = value; OnPropertyChanged();
            }
        }

        //--------------------------------------------------------------VALIDATION LOGIC-----------------------------------------------------------------------------
        private void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);
            switch (propertyName)
            {
                //...validation
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
