using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.ViewModel;
using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels.Popups.EditViews;

public partial class EditWarehousePopup : Popup
{
	private Warehouse? _warehouse;
	public EditWarehousePopup(AdminViewModel? warehouse)
	{
		InitializeComponent();
		BindingContext = warehouse;
	}

	private void CloseButton_Click(object? sender, EventArgs e)
	{
		Close();
	}
}