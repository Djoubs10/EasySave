using EasySave.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EasySave.Converters
{
    public class StateToCanResumeTransferConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (States)value == States.Paused ? Visibility.Visible : Visibility.Hidden;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
