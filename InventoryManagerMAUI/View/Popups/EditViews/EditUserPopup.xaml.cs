using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.ViewModel;
using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels.Popups.EditViews;

public partial class EditUserPopup : Popup
{
	private User? _user;
	public EditUserPopup(AdminViewModel? user)
	{
		InitializeComponent();
		BindingContext = user;
	}

	private void CloseButton_Click(object? sender, EventArgs e)
	{
		Close();
	}
}