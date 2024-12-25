using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.Popups.AddViews;
using InventoryManagerMAUI.ViewModels.Popups.DetailViews;

namespace InventoryManagerMAUI.View;

public partial class WarrantyClaimsView : ContentPage
{
    public WarrantyClaimsView()
    {
        InitializeComponent();
    }

    private void ViewDetailBtn_OnClicked(object? sender, EventArgs e)
    {
        var popup = new WarrantyClaimDetailsPopup(BindingContext as WarrantyClaim);
        this.ShowPopupAsync(popup);
    }

    private void AddWarrantyButton_OnClicked(object? sender, EventArgs e)
    {
        var popup = new AddWarrantyClaimPopup(BindingContext as WarrantyClaim);
        this.ShowPopupAsync(popup);
    }
}