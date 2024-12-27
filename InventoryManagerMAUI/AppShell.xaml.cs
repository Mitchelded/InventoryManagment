using InventoryManagerMAUI.Commands;

namespace InventoryManagerMAUI
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			string role = Preferences.Get("CurrentRole", string.Empty);
			if (!string.IsNullOrEmpty(role))
			{
				var menuItems = RoleBasedMenuService.GetMenuForRole(role);
				foreach (var menuItem in menuItems)
				{
					Items.Add(menuItem);
				}
			}
		}
	}
}
