namespace InventoryManagerMAUI.View;

using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.Popups.EditViews;
using InventoryManagerMAUI.ViewModels.Popups.AddViews;
using InventoryManagerMAUI.ViewModels.ViewModel;

public partial class WarehouseView : ContentPage
{
	public WarehouseView()
	{
		InitializeComponent();
	}

    private void WarehouseEditButton_OnClicked(object? sender, EventArgs e)
    {
        var popup = new EditWarehousePopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }

    private void BtnAdd_Clicked(object sender, EventArgs e)
    {
        var popup = new AddWarehousePopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }
}