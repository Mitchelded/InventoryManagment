using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels;
using InventoryManagerMAUI.ViewModels.ViewModel;

namespace InventoryManagerMAUI.View;

public partial class InventoryView : ContentPage
{
    public InventoryView()
    {
        InitializeComponent();
    }

    private void BtnAdd_OnClicked(object? sender, EventArgs e)
    {
      
        Dictionary<string, string> labelsDict = new Dictionary<string, string>
        {
            {"Enter equipment Name", "Name"},
            {"Enter equipment Serial Number", "Serial_Number"},
            
            {"Enter equipment Category", "CategoryID"},
            {"Enter equipment Status", "StatusID"},
            
            {"Enter equipment Department", "DepartmentID"},
            {"Enter equipment Location", "LocationID"},
            {"Enter equipment Supplier", "SupplierID"},
            
            {"Enter equipment Purchase Date", "PurchaseDate"},
            {"Enter equipment Warranty Expiration", "WarrantyExpiration"},
            {"Enter equipment Cost", "Cost"},
        };
        var popup = new InputPopup<EquipmentsViewModel>(labelsDict, BindingContext as EquipmentsViewModel);
        this.ShowPopupAsync(popup);
    }
}