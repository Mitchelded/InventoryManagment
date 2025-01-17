namespace InventoryManagerMAUI.View;
using InventoryManagerMAUI.ViewModels.Popups.AddViews;
using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.Popups.EditViews;
using InventoryManagerMAUI.ViewModels.ViewModel;

public partial class StatusView : ContentPage
{
	public StatusView()
	{
		InitializeComponent();
	}
    private void BtnAdd_Clicked(object sender, EventArgs e)
    {
        var popup = new AddStatusPopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }

    private void StatusEditButton_OnClicked(object? sender, EventArgs e)
    {
        var popup = new EditStatusPopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }
}