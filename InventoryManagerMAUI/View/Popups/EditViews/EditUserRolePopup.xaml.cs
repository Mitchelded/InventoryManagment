using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.ViewModel;
using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels.Popups.EditViews;

public partial class EditUserRolePopup : Popup
{
	private UserRole? _userRole;
	public EditUserRolePopup(AdminViewModel? userRole)
	{
		InitializeComponent();
		BindingContext = userRole;
	}

	private void CloseButton_Click(object? sender, EventArgs e)
	{
		Close();
	}
}