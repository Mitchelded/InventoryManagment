using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels;
using InventoryManagerMAUI.ViewModels.Popups;
using InventoryManagerMAUI.ViewModels.ViewModel;

namespace InventoryManagerMAUI.View;

public partial class InventoryView : ContentPage
{
    public InventoryView()
    {
        InitializeComponent();
        
    }

    public async Task UpdateEquipmentPhotosAsync()
    {
        using InventoryManagmentEntities db = new();
    
        foreach (var equipment in db.Equipments.ToList())
        {
            equipment.Photo = await LoadImageAsync(); // Await the Task to get byte[]
        }

        await db.SaveChangesAsync(); // Use the async version for saving
    }

    
    async Task<byte[]> LoadImageAsync()
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Select a file",
            FileTypes = FilePickerFileType.Images // Restricts to image files
        });

        if (result != null)
        {
            using (var stream = await result.OpenReadAsync())
            {
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        return null; // No file selected
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
}