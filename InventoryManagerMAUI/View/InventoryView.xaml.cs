using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels;
using InventoryManagerMAUI.ViewModels.Popups;
using InventoryManagerMAUI.ViewModels.Popups.AddViews;
using InventoryManagerMAUI.ViewModels.Popups.EditViews;
using InventoryManagerMAUI.ViewModels.ViewModel;

namespace InventoryManagerMAUI.View;

public partial class InventoryView : ContentPage
{
    public InventoryView()
    {
        InitializeComponent();
        UpdateEquipmentPhotosAsync();
    }

    public async Task UpdateEquipmentPhotosAsync()
    {
        using InventoryManagmentEntities db = new();
    

    }
    
    private void OnBindingContextChanged(object sender, EventArgs e)
    {
        if (BindingContext is Equipment item)
        {
            Console.WriteLine(item.Photo != null ? "Photo data loaded" : "Photo is null");
        }
    }

    private void BtnAdd_OnClicked(object? sender, EventArgs e)
    {
        var popup = new AddEquipmentPopup(BindingContext as EquipmentsViewModel);
        this.ShowPopupAsync(popup);
    }

    private void EditBtn_OnClicked(object? sender, EventArgs e)
    {
        var popup = new EditEquipmentPopup(BindingContext as EquipmentsViewModel);
        this.ShowPopupAsync(popup);
    }
}