using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp_feleves.Converters;

public class GameTypeToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string tipus)
        {
            switch (tipus)
            {
                case "Stratégiai":
                    return new SolidColorBrush(Color.FromRgb(255, 249, 196)); 
                case "Partijáték":
                    return new SolidColorBrush(Color.FromRgb(200, 230, 201)); 
                case "Kooperatív":
                    return new SolidColorBrush(Color.FromRgb(225, 245, 254)); 
                case "Logikai":
                    return new SolidColorBrush(Color.FromRgb(255, 204, 188));
                case "Családi":
                    return new SolidColorBrush(Color.FromRgb(243, 229, 245));
                default:
                    return Brushes.Transparent;
            }
        }
        return Brushes.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
