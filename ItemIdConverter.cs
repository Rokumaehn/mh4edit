using System.Globalization;
using System.Windows.Data;

namespace mh4edit;

public class StatusConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        int id = (int)values[0];
        return MonHunItem.names[id];
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}