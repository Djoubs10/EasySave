using EasySave.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EasySave.Converters
{
    public class StateToDisplayProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((States)value == States.Trasfering || (States)value == States.Paused || (States)value == States.Canceled) ? Visibility.Visible : Visibility.Collapsed;
        

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
