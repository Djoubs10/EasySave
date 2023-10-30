using EasySave.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EasySave.Converters
{
    public class StateToCanModifyOrDeleteTransferConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (States)value is States.Ready ? Visibility.Visible : Visibility.Hidden;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
