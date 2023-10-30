using EasySave.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace EasySave.Converters
{
    public class StateToForegroundProgressColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is States state)
            {
                if(state == States.Canceled)
                    return new BrushConverter().ConvertFrom("#E14747") ?? Brushes.Red;
                if (state == States.Paused)
                    return new BrushConverter().ConvertFrom("#E5D755") ?? Brushes.Orange;
            }

            return new BrushConverter().ConvertFrom("#7CED55") ?? Brushes.LightGreen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
