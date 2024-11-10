﻿using System.Globalization;

namespace InventoryManagerMAUI.Commands;

public class ByteArrayToImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is byte[] imageBytes)
        {
            return ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}