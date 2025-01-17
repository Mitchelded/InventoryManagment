using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.ViewModel;
using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels.Popups.EditViews;

public partial class EditSupplierPopup : Popup
{
	private Supplier? _supplier;
	public EditSupplierPopup(AdminViewModel? supplier)
	{
		InitializeComponent();
		BindingContext=supplier;
	}

	private void CloseButton_Click(object? sender, EventArgs e)
	{
		Close();
	}
}