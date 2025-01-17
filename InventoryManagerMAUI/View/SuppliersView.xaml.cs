namespace InventoryManagerMAUI.View;
using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.Popups.EditViews;
using InventoryManagerMAUI.ViewModels.Popups.AddViews;
using InventoryManagerMAUI.ViewModels.ViewModel;

public partial class SuppliersView : ContentPage
{
	public SuppliersView()
	{
		InitializeComponent();
    }
    private void BtnAdd_Clicked(object sender, EventArgs e)
    {
        var popup = new AddSuppliersPopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }
    private void SupplierEditButton_OnClicked(object? sender, EventArgs e)
    {
        var popup = new EditSupplierPopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }

}