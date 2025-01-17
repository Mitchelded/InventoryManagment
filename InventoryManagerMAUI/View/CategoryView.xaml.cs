namespace InventoryManagerMAUI.View;

using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.Popups.EditViews;
using InventoryManagerMAUI.ViewModels.Popups.AddViews;
using InventoryManagerMAUI.ViewModels.ViewModel;

public partial class CategoryView : ContentPage
{
	public CategoryView()
	{
		InitializeComponent();
    }
    private void BtnAdd_Clicked(object sender, EventArgs e)
    {
        var popup = new AddCategoryPopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }
    private void CategoryEditButton_OnClicked(object? sender, EventArgs e)
    {
        var popup = new EditCategoryPopup(BindingContext as AdminViewModel);
        this.ShowPopupAsync(popup);
    }

}