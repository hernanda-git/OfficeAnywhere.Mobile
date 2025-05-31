using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace OfficeAnywhere.Mobile.Converters;

public class StringBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!string.IsNullOrEmpty(value.ToString()))
        {
            return true;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!string.IsNullOrEmpty(value.ToString()))
        {
            return true;
        }

        return false;
    }
}