using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NolekWPF.Helpers
{
    public class AccessLevelToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int userRole = (int)values[0];
            int controlAccessLevel = (int)(values[1] as FrameworkElement).GetValue(VisibilitySecurityLevel.SecurityLevelProperty);

            return (userRole <= controlAccessLevel) ? Visibility.Visible : Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class VisibilitySecurityLevel
    {
        public static readonly DependencyProperty SecurityLevelProperty =
            DependencyProperty.RegisterAttached("SecurityLevel", typeof(int), typeof(VisibilitySecurityLevel), new PropertyMetadata(5));

        public static void SetSecurityLevel(UIElement element, int value)
        {
            element.SetValue(SecurityLevelProperty, value);
        }
        public static int GetSecurityLevel(UIElement element)
        {
            return (int)element.GetValue(SecurityLevelProperty);
        }
    }
}
