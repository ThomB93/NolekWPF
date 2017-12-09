using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NolekWPF.Helpers
{
    public class BoolToVisibleOrCollapsed
    {
        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public BoolToVisibleOrCollapsed() { }
        #endregion

        #region IValueConverter Members
        public string Convert(bool value)
        {
            if (!value)
                return "Visible";
            else
                return "Collapsed";
        }

        #endregion
    }
}
