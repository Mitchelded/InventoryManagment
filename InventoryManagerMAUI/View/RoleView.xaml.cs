using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.Popups.EditViews;
namespace InventoryManagerMAUI.View;
using InventoryManagerMAUI.ViewModels.Popups.AddViews;
using InventoryManagerMAUI.ViewModels.ViewModel;

public partial class RoleView : ContentPage
{
	public RoleView()
	{
		InitializeComponent();
	}
    private void BtnAdd_Clicked(object sender, EventArgs e)
    {
        var popup = new AddRolePopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }
    private void RoleEditButton_OnClicked(object? sender, EventArgs e)
    {
        var popup = new EditRolePopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }

}