using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.ViewModel;
using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels.Popups.EditViews;

public partial class EditCategoryPopup : Popup
{
	private Category? _category;
	public EditCategoryPopup(AdminViewModel? adminViewModel)
	{
		InitializeComponent();
		BindingContext= adminViewModel;

    }

	private void CloseButton_Click(object? sender, EventArgs e)
	{
		Close();
	}
}