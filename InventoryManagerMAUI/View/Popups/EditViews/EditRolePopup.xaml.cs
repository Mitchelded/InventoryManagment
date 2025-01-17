using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.ViewModel;
using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels.Popups.EditViews;

public partial class EditRolePopup : Popup
{
	public EditRolePopup(AdminViewModel? bindingContext)
	{
		InitializeComponent();
		BindingContext = bindingContext;
	}

	private void CloseButton_Click(object? sender, EventArgs e)
	{
		Close();
	}
}