using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.ViewModel;
using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels.Popups.EditViews;

public partial class EditStatusPopup : Popup
{
	public EditStatusPopup(AdminViewModel? bindingContext)
	{
		InitializeComponent();
		BindingContext = bindingContext;

	}

	private void CloseButton_Click(object? sender, EventArgs e)
	{
		Close();
	}
}