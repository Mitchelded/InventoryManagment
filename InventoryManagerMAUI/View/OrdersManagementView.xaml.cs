using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using InventoryManagerMAUI.ViewModels.Popups;

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
        var popup = new AddOrderPopup(BindingContext as OrderDetail);
        this.ShowPopupAsync(popup);
    }
}