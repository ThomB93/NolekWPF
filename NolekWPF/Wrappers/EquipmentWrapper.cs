using NolekWPF.Model;
using NolekWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;

namespace NolekWPF.Wrappers
{
    public class EquipmentWrapper : ViewModelBase, INotifyDataErrorInfo
    {
        public EquipmentWrapper(Model.Equipment model)
        {
            Model = model;
        }

        public Model.Equipment Model { get; set; }

        [Required]
        public int EquipmentId { get { return Model.EquipmentId; } }

        [Required]
        public DateTime EquipmentDateCreated
        {
            get { return Model.EquipmentDateCreated; }
            set
            {
                Model.EquipmentDateCreated = value; OnPropertyChanged();
                ValidateProperty(nameof(EquipmentDateCreated));
            }
        }

        public string EquipmentImagePath
        {
            get { return Model.EquipmentImagePath; }
            set
            {
                Model.EquipmentImagePath = value; OnPropertyChanged();
            }
        }

        [Required]
        public int EquipmentTypeID
        {
            get { return Model.EquipmentTypeID; }
            set
            {
                Model.EquipmentTypeID = value; OnPropertyChanged();
            }
        }
        [Required]
        public string EquipmentSerialnumber
        {
            get { return Model.EquipmentSerialnumber; }
            set
            {
                Model.EquipmentSerialnumber = value; OnPropertyChanged();
                ValidateProperty(nameof(EquipmentSerialnumber));
            }
        }
        [Required]
        public string EquipmentMainEquipmentNumber
        {
            get { return Model.EquipmentMainEquipmentNumber; }
            set
            {
                Model.EquipmentMainEquipmentNumber = value; OnPropertyChanged();
                ValidateProperty(nameof(EquipmentMainEquipmentNumber));
            }
        }
        [Required]
        public int EquipmentConfigurationID
        {
            get { return Model.EquipmentConfigurationID; }
            set
            {
                Model.EquipmentConfigurationID = value; OnPropertyChanged();
            }
        }
        [Required]
        public bool EquipmentStatus
        {
            get { return Model.EquipmentStatus; }
            set
            {
                Model.EquipmentStatus = value; OnPropertyChanged();
            }
        }
        [Required]
        public int EquipmentCategoryId
        {
            get { return Model.EquipmentCategoryId; }
            set
            {
                Model.EquipmentCategoryId = value; OnPropertyChanged();
            }
        }

        //--------------------------------------------------------------VALIDATION LOGIC-------------------------------------------------------------------------------

        //checks the property name and validates each one. If the validation fails, an error is added to the error list in the dictionary
        private void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);
            switch (propertyName)
            {
                case nameof(EquipmentDateCreated):

                    break;
                case nameof(EquipmentSerialnumber):
                    if (Regex.Matches(EquipmentSerialnumber, @"[a-zA-Z]").Count != 0)
                    {
                        AddError(propertyName, "Serial Number must consist of numbers only.");
                    }
                    if (EquipmentSerialnumber.Contains(" "))
                    {
                        AddError(propertyName, "Serial Number may not include any spaces.");
                    }
                    if (EquipmentSerialnumber.ToString().Length < 1)
                    {
                        AddError(propertyName, "Serial Number is required.");
                    }
                    break;
                case nameof(EquipmentMainEquipmentNumber):
                    if (Regex.Matches(EquipmentMainEquipmentNumber, @"[a-zA-Z]").Count != 0)
                    {
                        AddError(propertyName, "Main Equipment Number must consist of numbers only.");
                    }
                    if (EquipmentMainEquipmentNumber.ToString().Length < 1)
                    {
                        AddError(propertyName, "Main Equip. No. is required.");
                    }
                    break;
                //...
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
