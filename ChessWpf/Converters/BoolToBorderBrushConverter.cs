using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;

namespace ChessWpf;

public class BoolToBorderBrushConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value.GetType() == typeof(bool)) {
            return (bool)value ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.Transparent);
        }
        return new SolidColorBrush(Colors.Transparent);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value.GetType() == typeof(Color)) {
            return (SolidColorBrush)value == new SolidColorBrush(Colors.Yellow);
        }
        return false;
    }
}