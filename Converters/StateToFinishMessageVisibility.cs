using EasySave.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EasySave.Converters
{
    public class StateToFinishMessageVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is States state)
            {
                if (state == States.Finished)
                    return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
