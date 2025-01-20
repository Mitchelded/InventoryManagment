using InventoryManagerMAUI.View;
using InventoryManagerMAUI.ViewModels.ViewModel;

namespace InventoryManagerMAUI.Commands;

public class RoleBasedMenuService
{
    public static List<ShellItem> GetMenuForRole(string role)
    {
        try
        {
            return role switch
            {
                "Администратор" => GetAdminMenu(),
                "Кладовщик" => GetWarehouseMenu(),
                "Менеджер" => GetManagerMenu(),
                "Техническая поддержка" => GetSupportMenu(),
                _ => new List<ShellItem>() // Пустое меню для неизвестных ролей
            };
        }
        catch (Exception ex)
        {
            // Показать всплывающее окно с ошибкой
            if (Application.Current?.MainPage != null)
            {
                Application.Current.MainPage.DisplayAlert(
                    "Ошибка",
                    $"Не удалось загрузить меню для роли '{role}': {ex.Message}",
                    "OK"
                );
            }
            return new List<ShellItem>(); // Возвращаем пустое меню в случае ошибки
        }
    }

    private static List<ShellItem> GetSupportMenu()
    {
        return new List<ShellItem>
        {
              // Регистрация и ведение данных о ремонте и техническом обслуживании оборудования.
            new ShellItem
            {
                Title = "Претензии по гарантии",
                Route = "Warranty_Claims",
                Icon = "warranty_claims.png",
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(WarrantyClaimsView))
                    }
                }
            },
        };
    }

    private static List<ShellItem> GetManagerMenu()
    {
        return new List<ShellItem>
       {
            new ShellItem
            {
                Title = "Управление заказами",
                Route = "Orders_Management",
                Icon = "orders_management.png",
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(OrdersManagementView))
                    }
                }
            },  
            // Анализ данных (отчеты по оборудованию, заказам, складам и т. д.).
            new ShellItem
            {
                Title = "Приборная панель",
                Route = "dashboard",
                Icon = "icon_dashboard.png",
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(DashboardView))
                    }
                }
            },
       };
    }

    private static List<ShellItem> GetAdminMenu()
    {
        return new List<ShellItem>
    {
        new ShellItem
        {
            Title = "Администратор",
            Route = "Admin",
            Icon = "icon_admin.png",
            Items =
            {
                new ShellContent
                {
                    ContentTemplate = new DataTemplate(typeof(AdminView))
                }
            }
        },
    };
    }


    private static List<ShellItem> GetWarehouseMenu()
    {
        return new List<ShellItem>
    {
        new ShellItem
        {
            Title = "Инвентарь",
            Route = "Inventory",
            Icon = "icon_inventory.png",
            Items =
            {
                new ShellContent
                {
                    ContentTemplate = new DataTemplate(typeof(InventoryView))
                }
            }
        },
        new ShellItem
        {
            Title = "Перемещение оборудования",
            Route = "Equipment_Movement",
            Icon = "icon_movement.png",
            Items =
            {
                new ShellContent
                {
                    ContentTemplate = new DataTemplate(typeof(EquipmentMovementView))
                }
            }
        }
    };
    }
}
