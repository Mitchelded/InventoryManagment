using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.Interface;
using System.Runtime.CompilerServices;
using InventoryManagerMAUI.ViewModels.ViewModel;

namespace InventoryManagerMAUI.ViewModels.Popups;

public partial class AddEquipmentPopup : Popup
{
	private readonly EquipmentsViewModel _viewModel;
	public AddEquipmentPopup(EquipmentsViewModel viewModel)
	{
		_viewModel = viewModel;
		InitializeComponent();
		BindingContext = _viewModel;
	}

	private async void OnSelectImageClicked(object? sender, EventArgs e)
	{
		try
		{
			// Ожидаем выбора файла с помощью FilePicker
			var result = await FilePicker.PickAsync(new PickOptions
			{
				PickerTitle = "Select a file",
				FileTypes = FilePickerFileType.Images // Можно настроить типы файлов, например, для изображений
			});

			if (result != null)
			{
				// Файл выбран, можно обработать его
				// Пример: получить путь к файлу
				var filePath = result.FullPath;
				Console.WriteLine($"File selected: {filePath}");
            
				// Вы можете использовать этот путь для загрузки файла или выполнения других операций
			}
			else
			{
				// Если файл не был выбран
				Console.WriteLine("No file selected");
			}
		}
		catch (Exception ex)
		{
			// Обработка ошибок
			Console.WriteLine($"An error occurred: {ex.Message}");
		}
	}
}