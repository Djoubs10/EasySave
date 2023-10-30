using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using EasySave.Models;
namespace EasySave.Converters
{
    public class StateToCanPlayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((States)value == States.Ready ? Visibility.Visible : Visibility.Hidden);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
