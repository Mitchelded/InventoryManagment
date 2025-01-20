using System.Globalization;

namespace InventoryManagerMAUI.Commands;

public class ByteArrayToImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            if (value is byte[] imageBytes)
            {
                return ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
            return null;
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            Application.Current.MainPage.DisplayAlert(
                "Ошибка",
                $"Ошибка преобразования массива байтов в ImageSource: {ex.Message}",
                "OK"
            );
            Console.WriteLine($"Error converting byte array to ImageSource: {ex.Message}");
            return null;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}