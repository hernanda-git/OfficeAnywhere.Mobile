using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace OfficeAnywhere.Mobile.Converters;

public class ImageUrlConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string imagePath && !string.IsNullOrEmpty(imagePath))
        {
            if (!imagePath.StartsWith("https://www.o-anywhere.com/", StringComparison.OrdinalIgnoreCase))
            {
                return $"https://www.o-anywhere.com/{imagePath}";
            }
            return imagePath;
        }
        return "default_profile.jpg";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}