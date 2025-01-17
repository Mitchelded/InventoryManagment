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

public partial class EquipmentMovementView : ContentPage
{
    public EquipmentMovementView()
    {
        InitializeComponent();
    }

    private void AddMovementButton_OnClicked(object? sender, EventArgs e)
    {
        var popup = new AddEquipmentMovementPopup(BindingContext as EquipmentMovementViewModel);
        this.ShowPopupAsync(popup);
    }

    private void ViewDetailBtn_OnClicked(object? sender, EventArgs e)
    {
        var button = sender as Button; // Получаем кнопку
        var item = button?.CommandParameter as EquipmentMovement; // Получаем элемент, переданный через CommandParameter

        if (item != null)
        {
            var popup = new MovementDetailsPopup(item);
            this.ShowPopupAsync(popup);
        }

    }

    private void EditBtn_OnClicked(object? sender, EventArgs e)
    {
        var popup = new EditEquipmentMovementPopup(BindingContext as EquipmentMovementViewModel);
        this.ShowPopupAsync(popup);
    }
}