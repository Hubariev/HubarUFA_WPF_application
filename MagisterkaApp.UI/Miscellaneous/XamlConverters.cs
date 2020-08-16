using MagisterkaApp.Domain.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace MagisterkaApp.UI.Miscellaneous
{
    public class TypeOfGTEMConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (TypeOfGTEM)value;
        }
    }
}
