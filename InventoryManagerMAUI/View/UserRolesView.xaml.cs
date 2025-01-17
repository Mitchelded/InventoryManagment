namespace InventoryManagerMAUI.View;
using CommunityToolkit.Maui.Views;

using InventoryManagerMAUI.ViewModels.Popups.EditViews;
using InventoryManagerMAUI.ViewModels.Popups.AddViews;
using InventoryManagerMAUI.ViewModels;
using InventoryManagerMAUI.ViewModels.ViewModel;

public partial class UserRolesView : ContentPage
{
	public UserRolesView()
	{
		InitializeComponent();
    }
    private void BtnAdd_Clicked(object sender, EventArgs e)
    {
        var popup = new AddUserRolePopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }
    private void UserRoleEditButton_OnClicked(object? sender, EventArgs e)
    {
        var popup = new EditUserRolePopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }
}