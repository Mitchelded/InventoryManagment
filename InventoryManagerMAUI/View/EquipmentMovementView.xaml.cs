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
        try
        {
            var button = sender as Button; // Получаем кнопку
            var item = button?.CommandParameter as EquipmentMovement; // Получаем элемент, переданный через CommandParameter

            if (item != null)
            {
                var popup = new MovementDetailsPopup(item);
                this.ShowPopupAsync(popup); // Асинхронный вызов всплывающего окна
            }
            else
            {
                // Если элемент null, показываем сообщение
                if (Application.Current?.MainPage != null)
                {
                    Application.Current.MainPage.DisplayAlert(
                        "Ошибка",
                        "Не удалось получить информацию о перемещении.",
                        "OK"
                    );
                }
            }
        }
        catch (Exception ex)
        {
            // Логирование ошибки или уведомление пользователя
            if (Application.Current?.MainPage != null)
            {
                Application.Current.MainPage.DisplayAlert(
                    "Ошибка",
                    $"Произошла ошибка: {ex.Message}",
                    "OK"
                );
            }
        }
    }

    private void EditBtn_OnClicked(object? sender, EventArgs e)
    {
        var popup = new EditEquipmentMovementPopup(BindingContext as EquipmentMovementViewModel);
        this.ShowPopupAsync(popup);
    }
}