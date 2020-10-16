using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MagisterkaApp.UI.Miscellaneous
{
    public class StringToSolidColorBrushConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string backgroundColor = value.ToString();
                if (!backgroundColor.Contains("#"))
                {
                    backgroundColor = value.ToString().Insert(0,"#");
                }
                return  new BrushConverter().ConvertFromString(backgroundColor) as Brush;
            }
            catch
            {
                throw new FormatException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
