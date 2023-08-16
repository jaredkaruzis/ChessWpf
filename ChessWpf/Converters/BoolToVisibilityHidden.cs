using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace ChessWpf;

public class BoolToVisibilityHidden : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value.GetType() == typeof(bool)) {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }
        return Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value.GetType() == typeof(Visibility)) {
            return (Visibility)value == Visibility.Visible;
        }
        return false;
    }
}