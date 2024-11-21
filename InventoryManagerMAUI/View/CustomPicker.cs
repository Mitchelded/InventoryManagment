namespace InventoryManagerMAUI.View;
public class CustomPicker : ContentView
{
    // Свойство для заголовка
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomPicker), string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<string>), typeof(CustomPicker), null);

    public IEnumerable<string> ItemsSource
    {
        get => (IEnumerable<string>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    
    public CustomPicker()
    {
        
        
        var picker = new Picker
        {
            BackgroundColor = Colors.Transparent,
            TextColor = Colors.Black,
            FontSize = 16,
            Margin = new Thickness(10),
        };
        picker.SetBinding(Picker.ItemsSourceProperty, new Binding(nameof(ItemsSource), source: this));

        var frame = new Frame
        {
            CornerRadius = 10,
            Padding = 0,
            BorderColor = Colors.LightGray,
            BackgroundColor = Colors.White,
            HasShadow = false
        };
        var titleLabel = new Label
        {
            Text = Title,
            FontSize = 14,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            Padding = new Thickness(10, 0)
        };
        
        var stackLayout = new StackLayout
        {
            Orientation = StackOrientation.Vertical,
            Children = { titleLabel, picker }
        };

        frame.Content = picker;

        // Изменяем цвет рамки при фокусе
        frame.Focused += (sender, e) =>
        {
            frame.BorderColor = Color.FromArgb("#3498db");
        };

        frame.Unfocused += (sender, e) =>
        {
            frame.BorderColor = Colors.LightGray;
        };

        Content = frame;
    }
}