namespace InventoryManagerMAUI.View;
using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.Popups.EditViews;
using InventoryManagerMAUI.ViewModels.Popups.AddViews;
using InventoryManagerMAUI.ViewModels.ViewModel;

public partial class UsersView : ContentPage
{
	public UsersView()
	{
		InitializeComponent();
	}
    private void BtnAdd_Clicked(object sender, EventArgs e)
    {
        var popup = new AddUserPopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }
    private void UserEditButton_OnClicked(object? sender, EventArgs e)
    {
        var popup = new EditUserPopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }

}