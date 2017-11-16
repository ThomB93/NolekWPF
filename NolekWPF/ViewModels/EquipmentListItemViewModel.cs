using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.ViewModels
{
    public class EquipmentListItemViewModel : ViewModelBase
    {
        public EquipmentListItemViewModel(int id, string displayMember)
        {
            EquipmentId = id;
            DisplayMember = displayMember;
        }
        public int EquipmentId { get; }

        private string _displayMember;

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }
    }
}
