using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.View;
using InventoryManagerMAUI.ViewModels.ViewModel;
using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels.Popups.AddViews;

public partial class AddCategoryPopup : Popup
{

	public AddCategoryPopup(AdminViewModel? category)
	{
		InitializeComponent();
		BindingContext = category;
	}

	private void CloseButton_Click(object? sender, EventArgs e)
	{
		Close();
	}
}