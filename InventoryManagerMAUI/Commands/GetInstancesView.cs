using MaterialDesignThemes.Wpf;

namespace InventoryManagment.Commands
{
	internal class GetInstancesView
	{
		public static T GetInstanceView<T>(object sender) where T : class
		{
			if (sender is PackIcon packIcon)
			{
				var employeeEdit = packIcon.DataContext as T;
				return employeeEdit;
			}
			return default;
		}
	}
}
