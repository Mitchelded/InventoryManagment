using CommunityToolkit.Maui.Views;

using System.Runtime.CompilerServices;
using InventoryManagerMAUI.ViewModels;
using InventoryManagerMAUI.ViewModels.ViewModel;
namespace InventoryManagerMAUI.ViewModels.Popups.AddViews;

public partial class AddUserRolePopup : Popup
{
	public AddUserRolePopup(AdminViewModel bindingContext)
	{
		InitializeComponent();
		BindingContext = bindingContext;
	}

	private void CloseButton_Click(object? sender, EventArgs e)
	{
		Close();
	}
}