using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.Popups;
using InventoryManagerMAUI.ViewModels.Popups.AddViews;
using InventoryManagerMAUI.ViewModels.Popups.DetailViews;
using InventoryManagerMAUI.ViewModels.Popups.EditViews;
using InventoryManagerMAUI.ViewModels.ViewModel;

namespace InventoryManagerMAUI.View;

public partial class OrdersManagementView : ContentPage
{
    public OrdersManagementView()
    {
        InitializeComponent();
    }

    private void ViewDetailBtn_OnClicked(object? sender, EventArgs e)
    {
        var popup = new OrderDetailsPopup(BindingContext as OrderDetail);
        this.ShowPopupAsync(popup);
    }

    private void AddOrderButton_OnClicked(object? sender, EventArgs e)
    {
        var popup = new AddOrderPopup(BindingContext as OrdersManagementViewModel);
        this.ShowPopupAsync(popup);
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        var popup = new EditOrderPopup(BindingContext as OrdersManagementViewModel, BindingContext as OrdersManagementViewModel);
        this.ShowPopupAsync(popup);
    }
}