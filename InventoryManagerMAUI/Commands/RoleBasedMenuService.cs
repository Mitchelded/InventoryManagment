using InventoryManagerMAUI.View;

namespace InventoryManagerMAUI.Commands;

public class RoleBasedMenuService
{
    public static List<ShellItem> GetMenuForRole(string role)
    {
        var menu = new List<ShellItem>();

        if (role == "Администратор")
        {
            menu.Add(new ShellItem
            {
                Title = "Dashboard",
                Route = "dashboard",
                Icon = "icon_dashboard.png",
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(DashboardView))
                    }
                }
            });

            menu.Add(new ShellItem
            {
                Title = "Equipment Movement",
                Route = "Equipment_Movement",
                Icon = "icon_users.png",
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(EquipmentMovementView))
                    }
                }
            });
            
            menu.Add(new ShellItem
            {
                Title = "Orders Management",
                Route = "Orders_Management",
                Icon = "icon_users.png",
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(OrdersManagementView))
                    }
                }
            });
        }
        else if (role == "Кладовщик")
        {
            menu.Add(new ShellItem
            {
                Title = "Inventory",
                Route = "Inventory",
                Icon = "icon_profile.png",
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(InventoryView))
                    }
                }
            });
        }

        return menu;
    }
}
