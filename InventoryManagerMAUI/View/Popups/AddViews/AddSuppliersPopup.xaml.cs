using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.ViewModel;
using System.Runtime.CompilerServices;

namespace InventoryManagerMAUI.ViewModels.Popups.AddViews;

public partial class AddSuppliersPopup : Popup
{
	public AddSuppliersPopup(AdminViewModel? bindingContext)
	{
		InitializeComponent();
        BindingContext = bindingContext;
    }

	private void CloseButton_Click(object? sender, EventArgs e)
	{
		Close();
	}
}